using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using NLog;
using Chronos.Core.Attributes;
using Chronos.Core.Reflection;
using Chronos.Core.Xml.Docs;

namespace Chronos.Core.Xml.Config
{
    public class XmlConfig
    {
        public static CultureInfo Culture = CultureInfo.InvariantCulture;

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<string, Assembly> m_assemblies = new Dictionary<string, Assembly>();
        private readonly Dictionary<Assembly, string> m_assembliesDocFile = new Dictionary<Assembly, string>();
        private readonly Dictionary<string, XmlConfigNode> m_nodes = new Dictionary<string, XmlConfigNode>();
        private readonly Dictionary<Type, object> m_instances = new Dictionary<Type, object>();

        private readonly string m_configPath;
        private readonly string m_schemaPath;

        private XmlTextReader m_reader;

        private XmlDocument m_document;
        private XmlSchemaSet m_schema;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "XmlConfig" /> class.
        /// </summary>
        /// <param name = "uriConfig">The URI config.</param>
        public XmlConfig(string uriConfig)
        {
            m_configPath = Path.GetFullPath(uriConfig);
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "XmlConfig" /> class.
        /// </summary>
        /// <param name = "uriConfig">The URI config.</param>
        /// <param name = "uriSchema">The URI schema.</param>
        public XmlConfig(string uriConfig, string uriSchema)
        {
            m_configPath = Path.GetFullPath(uriConfig);
            m_schemaPath = Path.GetFullPath(uriSchema);
        }

        /// <summary>
        ///   Validation event handler.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "System.Xml.Schema.ValidationEventArgs" /> instance containing the event data.</param>
        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            var elem = sender as XmlElement;

            if (e.Severity == XmlSeverityType.Error)
            {
                throw new Exception("Schema error : " + e.Message);
            }
        }

        public string FilePath
        {
            get { return m_configPath; }
        }

        public bool IgnoreUnloadedAssemblies
        {
            get;
            set;
        }

        public bool Loaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Create a new config file based on the loaded assemblies
        /// </summary>
        public void Create(bool overwrite = false)
        {
            if (Loaded)
                Unload();

            if (m_assemblies.Count <= 0)
                throw new Exception("No assemblies defined");

            if (!overwrite && File.Exists(m_configPath))
                throw new Exception("Cannot overwrite an existing file");

            LoadNodesFromAssemblies();
            BuildConfig();

            Loaded = true;
        }

        private void Unload()
        {
            if (!Loaded)
                return;

            m_nodes.Clear();
        }

        public void Load()
        {
            if (Loaded)
                throw new Exception("Cannot call Load() twice");

            if (m_assemblies.Count <= 0)
                throw new Exception("No assemblies defined");

            if (!File.Exists(m_configPath))
                throw new FileNotFoundException("Config file is not found");

            m_reader = new XmlTextReader(new MemoryStream(File.ReadAllBytes(m_configPath)));

            if (m_reader.EOF)
                throw new Exception("Config file is empty, delete it");

            m_document = new XmlDocument();
            m_document.Load(m_reader);

            if (!string.IsNullOrEmpty(m_schemaPath))
                CheckSchema();

            LoadNodesFromConfig();
            AssignValuesFromNodes(false);

            Loaded = true;
        }

        /// <summary>
        /// Reloading only change variables that have DefineableRunning to true
        /// The others values are stored to prevent any change in the config file when saving
        /// </summary>
        public void Reload()
        {
            if (!Loaded)
                throw new Exception("Call Load() before reloading");

            if (m_assemblies.Count <= 0)
                throw new Exception("No assemblies defined");

            if (!File.Exists(m_configPath))
                throw new FileNotFoundException("Config file is not found");

            m_reader = new XmlTextReader(new MemoryStream(File.ReadAllBytes(m_configPath))); 
            
            if (m_reader.EOF)
                throw new Exception("Config file is empty, delete it");

            m_document = new XmlDocument();
            m_document.Load(m_reader);

            if (!string.IsNullOrEmpty(m_schemaPath))
                CheckSchema();

            LoadNodesFromConfig();
            AssignValuesFromNodes(true);
        }

        /// <summary>
        /// Reload the config completely
        /// </summary>
        public void Reset()
        {
            if (!Loaded)
                return;

            if (m_assemblies.Count <= 0)
                throw new Exception("No assemblies defined");

            if (!File.Exists(m_configPath))
                throw new FileNotFoundException("Config file is not found");

            m_reader = new XmlTextReader(new MemoryStream(File.ReadAllBytes(m_configPath)));

            if (m_reader.EOF)
                throw new Exception("Config file is empty, delete it");

            m_document = new XmlDocument();
            m_document.Load(m_reader);

            if (!string.IsNullOrEmpty(m_schemaPath))
                CheckSchema();

            LoadNodesFromConfig();
            AssignValuesFromNodes(false);
        }

        public void Save()
        {
            if (!Loaded)
                throw new Exception("Call Load() before saving");

            File.Copy(m_configPath, m_configPath + ".bak", true);

            BuildConfig();
        }

        /// <summary>
        /// Add an assembly where the XmlConfig will search variables to define
        /// </summary>
        /// <param name="assembly"></param>
        public void AddAssembly(Assembly assembly)
        {
            AddAssemblies(assembly);
        }

        /// <summary>
        /// Add an assembly where the XmlConfig will search variables to define
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="docFile">Xml documentation file used to create a new config file</param>
        public void AddAssembly(Assembly assembly, string docFile)
        {
            AddAssembly(assembly);

            m_assembliesDocFile.Add(assembly, docFile);
        }

        /// <summary>
        /// Add assemblies where the XmlConfig will search variables to define
        /// </summary>
        public void AddAssemblies(params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                m_assemblies.Add(assembly.GetName().Name, assembly);
            }
        }

        public void RemoveAssembly(Assembly assembly)
        {
            m_assemblies.Remove(assembly.GetName().Name);
        }

        public void AddInstance(Type type, object instance)
        {
            m_instances.Add(type, instance);
        }

        private void CheckSchema()
        {
            if (!File.Exists(m_schemaPath))
                throw new FileNotFoundException("Schema file is not found");

            m_schema = new XmlSchemaSet();
            using (var reader = new StreamReader(m_schemaPath))
            {
                m_schema.Add(XmlSchema.Read(reader, ValidationEventHandler));
            }

            m_document.Schemas = m_schema;
            m_document.Validate(ValidationEventHandler);
        }

        private void LoadNodesFromAssemblies()
        {
            foreach (var assembly in m_assemblies.Values)
            {
                DotNetDocumentation documentation = null;
                if (m_assembliesDocFile.ContainsKey(assembly))
                    if (File.Exists(m_assembliesDocFile[assembly]))
                        documentation = DotNetDocumentation.Load(m_assembliesDocFile[assembly]);

                foreach (var type in assembly.GetTypes())
                {
                    var fields = from field in type.GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                                 where field.GetCustomAttribute<VariableAttribute>() != null
                                 select field;

                    object instance = null;
                    if (m_instances.ContainsKey(type))
                        instance = m_instances[type];

                    foreach (var field in fields)
                    {
                        var node = new XmlConfigNode(field);
                        var isStatic = field.IsStatic;

                        if (!isStatic)
                        {
                            if (instance != null)
                                node.Instance = instance;
                            else
                                throw new Exception(
                                    string.Format(
                                        "{0} is not static. Declare it static or bind an instance to the type {1}",
                                        field.Name, type.Name));
                        }
                        DocEntry member = null;
                        if (documentation != null)
                            member = documentation.Members.FirstOrDefault(entry => entry.Name == type.FullName + "." + field.Name);

                        if (member != null)
                            node.Documentation = member.Summary;

                        m_nodes.Add(node.Path, node);
                    }

                    var properties = from property in type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                                     where property.GetCustomAttribute<VariableAttribute>() != null
                                     select property;

                    foreach (var property in properties)
                    {
                        var node = new XmlConfigNode(property);
                        var isStatic = property.GetGetMethod().IsStatic;

                        if (!isStatic)
                        {
                            if (instance != null)
                                node.Instance = instance;
                            else
                                throw new Exception(
                                    string.Format(
                                        "{0} is not static. Declare it static or bind an instance to the type {1}",
                                        property.Name, type.Name));
                        }

                        DocEntry member = null;
                        if (documentation != null)
                            member = documentation.Members.FirstOrDefault(entry => entry.Name == type.FullName + "." + property.Name);

                        if (member != null)
                            node.Documentation = member.Summary;

                        m_nodes.Add(node.Path, node);
                    }
                }
            }
        }

        private void LoadNodesFromConfig()
        {
            m_nodes.Clear();

            foreach (var variableNode in from XPathNavigator navigator in m_document.CreateNavigator().Select("//Variable[@name]") where navigator.IsNode select new XmlConfigNode(((IHasXmlNode) navigator).GetNode()))
            {
                if (string.IsNullOrEmpty(variableNode.Name))
                {
                    logger.Error(string.Format("Variable in {0} has not attribute 'name'", variableNode.Path));
                    continue;
                }

                m_nodes.Add(variableNode.Path, variableNode);
            }
        }

        private void AssignValuesFromNodes(bool reload)
        {
            // avoid multiple useless reallocations
            var assemblies = m_assemblies.Values.ToArray();
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (XmlConfigNode xmlConfigNode in m_nodes.Values)
            {
                try
                {
                    Type classType = SearchType(xmlConfigNode.Namespace + "." + xmlConfigNode.ClassName, assemblies);

                    if (classType == null)
                    {
                        if (!IgnoreUnloadedAssemblies)
                            logger.Error(string.Format("Cannot found the class '{0}', is the assembly loaded ?", xmlConfigNode.Namespace + "." + xmlConfigNode.ClassName));
                        
                        continue;
                    }

                    if (m_instances.ContainsKey(classType))
                        xmlConfigNode.Instance = m_instances[classType];

                    Type elementType;
                    FieldInfo field = classType.GetField(xmlConfigNode.Name);
                    if (field != null)
                    {
                        xmlConfigNode.BindToField(field);
                        elementType = field.FieldType;
                    }
                    else
                    {
                        PropertyInfo property = classType.GetProperty(xmlConfigNode.Name);
                        if (property != null)
                        {
                            xmlConfigNode.BindToProperty(property);
                            elementType = property.PropertyType;
                        }
                        else
                        {
                            logger.Error(string.Format("Field or property '{0}' doesn't exist", xmlConfigNode.Path));
                            continue;
                        }
                    }

                    try
                    {
                        object value = xmlConfigNode.Serialized ?
                            ReadElement(xmlConfigNode.Node, elementType) :
                            ReadElement(xmlConfigNode.Node.InnerXml, elementType);

                        xmlConfigNode.SetValue(value, reload);
                    }
                    catch (InvalidCastException)
                    {
                        logger.Warn(string.Format("Cannot cast {0} to the correct type : {1}", xmlConfigNode.Path, elementType));
                    }
                }

                catch (Exception e)
                {
                    logger.Warn(string.Format("Cannot define the variable {0} : {1}", xmlConfigNode.Path, e));
                }
            }
        }

        /// <summary>
        /// Search a type by his name in the given assemblies
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private static Type SearchType(string typeName, Assembly[] assemblies)
        {
            Type valueType = null;
            int i = 0;
            while (valueType == null && i < assemblies.Length)
            {
                valueType = assemblies[i].GetType(typeName);

                i++;
            }

            return valueType;
        }

        private void BuildConfig()
        {
            var writer = new XmlTextWriter(m_configPath, Encoding.UTF8)
                             {Formatting = Formatting.Indented, IndentChar = ' ', Indentation = 2};

            writer.WriteStartElement("Configuration");

            var groupsByNamespace = from entry in m_nodes
                                    where entry.Value.Attribute != null
                                    group entry by entry.Value.Namespace
                                    into grp
                                    let priority = grp.Max(subentry => subentry.Value.Attribute.Priority)
                                    orderby priority descending, grp.Key
                                    select grp;

            var lastNamespace = new List<string>();

            foreach (var namespaceNodeGroup in groupsByNamespace)
            {
                var currentNamespace = namespaceNodeGroup.Key.Split('.');

                // close/open namespaces
                int count = Math.Max(lastNamespace.Count, currentNamespace.Length);
                for (int i = 0; i < count; i++)
                {
                    if (currentNamespace.Length <= i)
                    {
                        int nsToClose = lastNamespace.Count - i;
                        for (int j = 0; j < nsToClose; j++)
                        {
                            writer.WriteEndElement();

                            lastNamespace.RemoveAt(lastNamespace.Count - 1);
                        }
                    }
                    else if (lastNamespace.Count <= i)
                    {
                        for (int j = i; j < currentNamespace.Length; j++)
                        {
                            writer.WriteStartElement(currentNamespace[j]);

                            lastNamespace.Add(currentNamespace[j]);
                        }
                    }
                    else if (lastNamespace[i] != currentNamespace[i])
                    {
                        int nsToClose = lastNamespace.Count - i;
                        for (int j = 0; j < nsToClose; j++)
                        {
                            writer.WriteEndElement();

                            lastNamespace.RemoveAt(lastNamespace.Count - 1);
                        }

                        for (int j = i; j < currentNamespace.Length; j++)
                        {
                            writer.WriteStartElement(currentNamespace[j]);

                            lastNamespace.Add(currentNamespace[j]);
                        }

                        break;
                    }
                }

                var groupsByClass = from entry in namespaceNodeGroup
                                    group entry by entry.Value.ClassName
                                    into grp
                                    let priority = grp.Max(subentry => subentry.Value.Attribute.Priority)
                                    orderby priority descending, grp.Key
                                    select grp;

                foreach (var classNodeGroup in groupsByClass)
                {
                    writer.WriteStartElement(classNodeGroup.Key);

                    foreach (var node in classNodeGroup)
                    {
                        if (node.Value.BindedField == null &&
                            node.Value.BindedProperty == null)
                        {
                            logger.Error("Cannot save variable '{0}' because it has no binded field or property", node.Value.Path);
                            continue;
                        }

                        if (!string.IsNullOrEmpty(node.Value.Documentation))
                            writer.WriteComment(node.Value.Documentation);

                        writer.WriteComment("Editable as Running : " + node.Value.Attribute.DefinableRunning);

                        writer.WriteStartElement("Variable");
                        writer.WriteAttributeString("name", node.Value.Name);

                        var elementType = node.Value.BindedField != null
                                              ? node.Value.BindedField.FieldType
                                              : node.Value.BindedProperty.PropertyType;

                        // is primitive type
                        if (!node.Value.Serialized)
                        {
                            writer.WriteAttributeString("serialized", "false");
                            var value = node.Value.GetValue(true);

                            if (value is IFormattable)
                                writer.WriteValue(( value as IFormattable ).ToString(null, Culture));
                            else
                                writer.WriteValue(value.ToString());
                        }
                        else
                        {
                            writer.WriteAttributeString("serialized", "true");

                            var stringWriter = new StringWriter();
                            var xmlWriter = new XmlTextWriter(stringWriter)
                                                {
                                                    Formatting = Formatting.Indented,
                                                    IndentChar = '\t',
                                                    Indentation = 1
                                                };

                            new XmlSerializer(elementType).Serialize(xmlWriter, node.Value.GetValue(true));

                            var xmlReader =
                                new XmlTextReader(new StringReader(stringWriter.GetStringBuilder().ToString()));
                            XPathNavigator navigator = new XPathDocument(xmlReader).CreateNavigator();

                            writer.WriteNode(navigator, false);
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }
            }

            foreach (var ns in lastNamespace)
            {
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();
        }

        private static object ReadElement(object element, Type type)
        {
            if (element is XmlNode)
            {
                return new XmlSerializer(type).Deserialize(new StringReader((element as XmlNode).InnerXml));
            }
            if (element is string)
            {
                element = UnEscapeXml(element as string);
            }
            if (type.IsEnum)
            {
                return new XmlSerializer(type).Deserialize(new StringReader(element.ToString()));
            }

            if (element.ToString() == string.Empty)
                if (element is string)
                    return string.Empty;
                else
                    return null;

            return Convert.ChangeType(element, type, Culture);
        }

        private static string UnEscapeXml(string xmlString)
        {
            if (string.IsNullOrEmpty(xmlString))
                return xmlString;

            xmlString = xmlString.Replace("&apos;", "'");
            xmlString = xmlString.Replace("&quot;", "\"");
            xmlString = xmlString.Replace("&gt;", ">");
            xmlString = xmlString.Replace("&lt;", "<");
            xmlString = xmlString.Replace("&amp;", "&");

            return xmlString;
        }
    }
}