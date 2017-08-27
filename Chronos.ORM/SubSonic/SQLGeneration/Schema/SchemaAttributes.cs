// 
//   SubSonic - http://subsonicproject.com
// 
//   The contents of this file are subject to the New BSD
//   License (the "License"); you may not use this file
//   except in compliance with the License. You may obtain a copy of
//   the License at http://www.opensource.org/licenses/bsd-license.php
//  
//   Software distributed under the License is distributed on an 
//   "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
//   implied. See the License for the specific language governing
//   rights and limitations under the License.
// 

using System;
using System.Data;
using System.Linq;
using Chronos.ORM.SubSonic.Schema;

namespace Chronos.ORM.SubSonic.SQLGeneration.Schema
{
    public interface IClassMappingAttribute
    {
        bool Accept(ITable table);
        void Apply(ITable table);
    }

    public interface IPropertyMappingAttribute
    {
        bool Accept(IColumn column);
        void Apply(IColumn column);
    }

	[AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute, IClassMappingAttribute
	{
        public string TableName { get; set; }

        public TableNameAttribute(string tableName)
        {
			TableName = tableName;
        }

        public bool Accept(ITable table)
        {
            return true;
        }

        public void Apply(ITable table)
        {
            table.Name = TableName;
        }
    }

	[AttributeUsage(AttributeTargets.Property)]
	public class ColumnAttribute : Attribute, IPropertyMappingAttribute
	{
		public string ColumnName { get; set; }

		public ColumnAttribute(string tableName)
		{
			ColumnName = tableName;
		}

		public bool Accept(IColumn col)
		{
			return true;
		}

		public void Apply(IColumn col)
		{
			col.Name = ColumnName;
		}
	}

    [AttributeUsage(AttributeTargets.Property)]
    public class NullStringAttribute : Attribute, IPropertyMappingAttribute 
    {
        public bool Accept(IColumn column)
        {
            return DbType.String == column.DataType;
        }

        public void Apply(IColumn column)
        {
            column.IsNullable = true;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LongStringAttribute : Attribute, IPropertyMappingAttribute
    {
        public bool Accept(IColumn column)
        {
            return DbType.String == column.DataType;
        }

        public void Apply(IColumn column)
        {
            column.MaxLength = 8001;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute, IPropertyMappingAttribute
    {
        public bool AutoIncrement { get; set; }

        public PrimaryKeyAttribute(string primaryKey, bool autoIncrement = true)
        {
            AutoIncrement = autoIncrement;
            Name = primaryKey;
        }

        public string Name
        {
            get;
            private set;
        }

        public string SequenceName
        {
            get;
            set;
        }

        public bool Accept(IColumn column)
        {
            return true;
        }

        public void Apply(IColumn column)
        {
            column.IsPrimaryKey = true;
            column.IsNullable = false;
            if (column.IsNumeric)
                column.AutoIncrement = AutoIncrement;
            else if (column.IsString && column.MaxLength == 0)
                column.MaxLength = 255;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class IndexAttribute : Attribute, IPropertyMappingAttribute
    {
        public IndexAttribute()
        {
            
        }

        public IndexAttribute(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        public bool Accept(IColumn column)
        {
            return true;
        }

        public void Apply(IColumn column)
        {
            var index = column.Table.Indexes.FirstOrDefault(x => x.Name.ToLower() == Name.ToLower());
            if (index == null || string.IsNullOrEmpty(Name))
            {
                index = new DatabaseColumnIndex(column)
                    {
                        Name = Name
                    };

                column.Table.Indexes.Add(index);
            }
            else
                index.Columns.Add(column);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class StringLengthAttribute : Attribute, IPropertyMappingAttribute
    {
        public int Length { get; set; }
        
        public StringLengthAttribute(int length)
        {
            Length = length;
        }

        public bool Accept(IColumn column)
        {
            return DbType.String == column.DataType;
        }

        public void Apply(IColumn column)
        {
            column.MaxLength = Length;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NumericPrecisionAttribute : Attribute, IPropertyMappingAttribute
    {
        public int Precision { get; set; }
        public int Scale { get; set; }

        public NumericPrecisionAttribute(int precision, int scale)
        {
            Scale = scale;
            Precision = precision;
        }

        public bool Accept(IColumn column)
        {
            return column.DataType == DbType.Decimal || column.DataType == DbType.Double;
        }

        public void Apply(IColumn column)
        {
            column.NumberScale = Scale;
            column.NumericPrecision = Precision;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultSettingAttribute : Attribute, IPropertyMappingAttribute
    {
        public object DefaultSetting { get; set; }

        public DefaultSettingAttribute(object defaultSetting)
        {
            DefaultSetting = defaultSetting;
        }

        public bool Accept(IColumn column)
        {
            return true;
        }

        public void Apply(IColumn column)
        {
            column.DefaultSetting = DefaultSetting;
        }
    }
}