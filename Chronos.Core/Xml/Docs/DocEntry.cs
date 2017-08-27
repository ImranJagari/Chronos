using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Chronos.Core.Xml.Docs
{
	public class DocEntry
	{
		private string m_fullName;
		private string m_name;
		private MemberType m_type;

	    public string Name
	    {
	        get { return m_name; }
	    }

	    public MemberType MemberType
		{
			get
			{
				return m_type;
			}
		}

		[XmlAttribute("name")]
		public string FullName
		{
			get
			{
				return m_fullName;
			}
			set
			{
				m_fullName = value;
				m_type = DotNetDocumentation.GetMemberType(m_fullName[0]);
				var lastIndex = m_fullName.IndexOf('(');
				
				if (lastIndex < 0)
				{
					lastIndex = m_fullName.Length;
				}
				m_name = m_fullName.Substring(2, lastIndex-2);
			}
		}

        [XmlElement("summary")]
        public object[] SummaryObjects
        {
            get;
            set;
        }

        public string Summary
        {
            get
            {
                return string.Join(" ", SummaryObjects.Cast<XmlNode[]>().First().Select(entry => entry.Value)).Trim();
            }
        }
	}
}