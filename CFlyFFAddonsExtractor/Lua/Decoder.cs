using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * Decoder.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 28/07/2014 23:10:32
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace FFLua
{
    public static class Decoder
    {
        /// <summary>
        /// Add Lua header for Luadec
        /// </summary>
        /// <param name="data"></param>
        private static void AddHeader(ref Byte[] data)
        {
            List<Byte> list = new List<Byte>(data);
            list.Insert(0, 0);
            data = list.ToArray();
            data[0] = 0x1B;
            data[1] = 0x4C;
            data[2] = 0x75;
            data[3] = 0x61;
            data[4] = 0x51;
        }

        /// <summary>
        /// Decompile a lua file
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="outputFile"></param>
        public static void Decompile(String inputFile, String outputFile)
        {
            Byte[] _data = File.ReadAllBytes(inputFile);

            for (Int32 i = 0; i < _data.Length; i++)
            {
                _data[i] ^= 194;
                _data[i] = (Byte)((_data[i] << 4) | (_data[i] >> 4));
            }
            AddHeader(ref _data);
            File.WriteAllBytes(outputFile, _data);
            ExecuteLuadecProcess(outputFile);
        }

        /// <summary>
        /// Execute luadec process
        /// </summary>
        /// <param name="outputFile"></param>
        private static void ExecuteLuadecProcess(String outputFile)
        {
            ProcessStartInfo _info = new ProcessStartInfo("Binaries\\luadec.exe", "\"" + outputFile + "\"") { RedirectStandardOutput = true, RedirectStandardError = true, UseShellExecute = false, CreateNoWindow = true };
            Process _luaProcess = Process.Start(_info);
            String _std = _luaProcess.StandardOutput.ReadToEnd();
            String _error = _luaProcess.StandardError.ReadToEnd();
            _luaProcess.WaitForExit();
            _luaProcess.Close();
            _luaProcess.Dispose();

            if (String.IsNullOrEmpty(_error) == false)
            {
                System.Windows.Forms.MessageBox.Show(_error);
            }

            FileStream _stream = new FileStream(outputFile, FileMode.Create);
            StreamWriter _writer = new StreamWriter(_stream, Encoding.Default);
            _writer.Write(_std);
            _writer.Close();
            _writer.Dispose();
            _stream.Close();
            _stream.Dispose();
        }
    }
}
