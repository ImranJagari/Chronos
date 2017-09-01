using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * FileEntry.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 15/07/2014 20:19:42
 * 
 * Notes:
 * Wdf file entry
 * -------------------------------------------------------*/

namespace WindsoulDataFile
{
    public class FileEntry
    {
        public UInt32 UniqueId { get; set; }
        public UInt32 StartAdress { get; set; }
        public Int32 FileSize { get; set; }
        public UInt32 ReservedSpace { get; set; }
    }
}
