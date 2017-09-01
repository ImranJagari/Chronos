using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * WdfPackage.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 15/07/2014 20:18:17
 * 
 * Notes:
 * Wdf structure:
 * - Wdf Header : 0x57444650
 * - Number of files in the wdf archive
 * - Adresse of the first file
 * -------------------------------------------------------*/

namespace WindsoulDataFile
{
    public class WdfPackage
    {
        #region FIELDS

        public String FileName { get; private set; }

        private readonly UInt32 Header = 0x57444650;
        public Int32 FileCount { get; private set; }
        public UInt32 StartAdress { get; private set; }

        private FileStream Stream { get; set; }
        private BinaryReader Reader { get; set; }

        private Dictionary<UInt32, FileEntry> Files { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Creates a new WdfPackage instance
        /// </summary>
        /// <param name="fileName">File name</param>
        public WdfPackage(String fileName)
        {
            this.FileName = fileName;
            this.Files = new Dictionary<UInt32, FileEntry>();
            this.Stream = new FileStream(this.FileName, FileMode.Open);
            this.Reader = new BinaryReader(this.Stream, Encoding.UTF8);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Open the WDF packages and load all files in memory
        /// </summary>
        public void Open()
        {
            UInt32 _header = this.Reader.ReadUInt32();

            if (_header != this.Header)
            {
                throw new Exception("Bad header");
            }
            this.FileCount = this.Reader.ReadInt32();
            this.StartAdress = this.Reader.ReadUInt32();
            this.Reader.BaseStream.Seek(this.StartAdress, SeekOrigin.Begin);
            for (Int32 i = 0; i < this.FileCount; ++i)
            {
                FileEntry _entry = new FileEntry();
                _entry.UniqueId = this.Reader.ReadUInt32();
                _entry.StartAdress = this.Reader.ReadUInt32();
                _entry.FileSize = this.Reader.ReadInt32();
                _entry.ReservedSpace = this.Reader.ReadUInt32();
                this.Files.Add(_entry.UniqueId, _entry);
            }
        }

        /// <summary>
        /// Close the Wdf packages and release all resources
        /// </summary>
        public void Close()
        {
            this.Stream.Close();
            this.Stream.Dispose();
            this.Reader.Close();
            this.Reader.Dispose();
            this.Files.Clear();
        }

        /// <summary>
        /// Extract a file from the WDF package
        /// </summary>
        /// <param name="fileName"></param>
        public Boolean Extract(String fileName)
        {
            UInt32 _fileHash = fileName.Hash();

            foreach (KeyValuePair<UInt32, FileEntry> entry in this.Files)
            {
                if (entry.Value.UniqueId == _fileHash)
                {
                    this.Reader.BaseStream.Seek(entry.Value.StartAdress, SeekOrigin.Begin);
                    Byte[] _data = this.Reader.ReadBytes(entry.Value.FileSize);
                    String _path = fileName.Split('\\').Last();

                    FileStream _stream = new FileStream(_path, FileMode.Create);
                    BinaryWriter _writer = new BinaryWriter(_stream, Encoding.Default);
                    _writer.Write(_data);
                    _writer.Close();
                    _writer.Dispose();
                    _stream.Close();
                    _stream.Dispose();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Extracts the file name to a specified folder
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="destinationFolder"></param>
        public Boolean Extract(String fileName, String destinationFolder)
        {
            UInt32 _fileHash = fileName.Hash();

            foreach (KeyValuePair<UInt32, FileEntry> entry in this.Files)
            {
                if (entry.Value.UniqueId == _fileHash)
                {
                    this.Reader.BaseStream.Seek(entry.Value.StartAdress, SeekOrigin.Begin);
                    Byte[] _data = this.Reader.ReadBytes(entry.Value.FileSize);
                    String _path = fileName.Split('\\').Last();

                    FileStream _stream = new FileStream(destinationFolder + "\\" + _path, FileMode.Create);
                    BinaryWriter _writer = new BinaryWriter(_stream, Encoding.Default);
                    _writer.Write(_data);
                    _writer.Close();
                    _writer.Dispose();
                    _stream.Close();
                    _stream.Dispose();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the file data
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Byte[] GetFileData(String fileName)
        {
            Byte[] _data = null;
            UInt32 _fileHash = fileName.Hash();

            foreach (KeyValuePair<UInt32, FileEntry> entry in this.Files)
            {
                if (entry.Value.UniqueId == _fileHash)
                {
                    this.Reader.BaseStream.Seek(entry.Value.StartAdress, SeekOrigin.Begin);
                    _data = this.Reader.ReadBytes(entry.Value.FileSize);
                    break;
                }
            }
            return _data;
        }

        #endregion
    }
}
