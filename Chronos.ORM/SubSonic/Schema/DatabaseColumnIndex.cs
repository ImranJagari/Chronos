#region License GNU GPL
// DatabaseColumnIndex.cs
// 
// Copyright (C) 2013 - BehaviorIsManaged
// 
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation;
// either version 2 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details. 
// You should have received a copy of the GNU General Public License along with this program; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
#endregion

using System.Collections.Generic;
using System.Linq;
using Chronos.ORM.SubSonic.DataProviders;

namespace Chronos.ORM.SubSonic.Schema
{
    public class DatabaseColumnIndex : IColumnIndex
    {
        public DatabaseColumnIndex(ITable table)
        {
            Columns = new List<IColumn>();
        }

        public DatabaseColumnIndex(IColumn column)
        {
            Columns = new List<IColumn>() { column };
            Table = column.Table;
        }

        public string Name
        {
            get;
            set;
        }

        public IList<IColumn> Columns
        {
            get;
            set;
        }

        public ITable Table
        {
            get;
            set;
        }

        public IDataProvider Provider
        {
            get
            {
                return Table.Provider;
            }
            set
            {
                Table.Provider = value;
            }
        }

        public string FriendlyName
        {
            get;
            set;
        }

        public string QualifiedName
        {
            get
            {
                return Name;
            }
        }

        public string SchemaName
        {
            get
            {
                return Table.SchemaName;
            }
            set
            {
                Table.SchemaName = value;
            }
        }
    }
}