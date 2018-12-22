using Chronos.Core.Encryption;
using Chronos.Protocol.Enums;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FFProxy
{
	public class Program
	{
		private static Socket _client;
		private static Socket _server;

		private static KeyPair _keyPairClient;
		private static KeyPair _keyPairServer;

		private static void Main()
		{
			_server = new Socket(SocketType.Stream, ProtocolType.Tcp);
			_server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9111));
			_server.Listen(10);
			_server.BeginAccept(ar =>
								{
									_server = _server.EndAccept(ar);

									_client = new Socket(SocketType.Stream, ProtocolType.Tcp);
									_client.BeginConnect("123.58.172.213", 9111, a =>
																				 {
																					 _client.EndConnect(a);

																					 BeginReceiveServer((Socket) a.AsyncState);
																				 }, _client);

									BeginReceiveClient(_server);
								}, _server);

            Console.ReadLine();
		}

		private static void BeginReceiveClient(Socket server)
		{
			var buffer = new byte[0xFFFF];
			server.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, result =>
																			{
																				OnReceiveClient(buffer, server.EndReceive(result));

																				BeginReceiveClient(server);
																			}, buffer);
		}

		private static void BeginReceiveServer(Socket client)
		{
			var buffer = new byte[0xFFFF];
			client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, result =>
																			{
																				OnReceiveServer(buffer, client.EndReceive(result));

																				BeginReceiveServer(client);
																			}, buffer);
		}

		private static void Dump(byte[] buffer, int size, string path)
		{
			var lala = "";
			for (var i = 0; i < size; i++)
				lala += string.Format("{0:X2} ", buffer[i]);
			lala += "\r\n\r\n";

			File.AppendAllText(path, lala);
		}

		private static int _received;
		private static int _currentSize;

		private static void DumpInfo(byte[] data)
		{

			var reader = new BinaryReader(new MemoryStream(data));
			_currentSize = reader.ReadInt32();
			var opcode = reader.ReadUInt16();
            if (opcode == 0)
                return;
			if (opcode == 0x2002)
			{
				var count = reader.ReadUInt16();
				Debug.Assert(count > 0);

				ushort type;
				var typeFirstByte = reader.ReadByte();
				if (typeFirstByte > 128)
					type = (ushort) (typeFirstByte << 8 | reader.ReadByte());
				else
					type = typeFirstByte;

				//if (type == 23 && count == 1)
				if (type == 23)
				{
					//var bytes = reader.ReadBytes((int) (data.Length - reader.BaseStream.Position));

					//Dump(bytes, _currentSize - 9, string.Format("{0}.txt", DateTime.Now.Ticks));

					var id = reader.ReadUInt32();
					var unknown = reader.ReadBytes(9);
					var x = reader.ReadUInt32();
					var y = reader.ReadUInt32();
					var z = reader.ReadUInt32();

					Console.WriteLine("Id: {0}, X: {1}, Y: {2}, Z: {3}, Unknown: {4}", id, x, y, z, GetString(unknown));
				}

				Console.WriteLine("S> Opcode: {0:X4} (Count: {1}, First Type: {2}), Size: {3}", opcode, count, type, _currentSize);
			}
            Console.WriteLine("S> Opcode: {0}, Size: {1}", (HeaderEnum)opcode, _currentSize);

        }

        private static string GetString(byte[] bytes)
		{
			var builder = new StringBuilder();

			for (var i = 0; i < bytes.Length; i++)
			{
				builder.AppendFormat("{0:X2}", bytes[i]);

				if (i != bytes.Length - 1)
					builder.Append(" ");
			}

			return builder.ToString();
		}

		private static void OnReceiveClient(byte[] buffer, int size)
		{
			Debug.Assert(_keyPairClient != null);

			var newBuffer = new byte[buffer.Length];
			buffer.CopyTo(newBuffer, 0);

			_keyPairClient.Decrypt(ref newBuffer, 0, size);

			//Dump(newBuffer, size, "client.txt");

			_client.Send(buffer, size, SocketFlags.None);
		}

		private static void OnReceiveServer(byte[] buffer, int size)
		{
			if (_keyPairClient == null && _keyPairServer == null)
			{
				var reader = new BinaryReader(new MemoryStream(buffer, 0, size));
				Debug.Assert(reader.ReadUInt32() == 8);

				var seed = reader.ReadInt32();
				_keyPairClient = new KeyPair(seed);
				_keyPairServer = new KeyPair(seed);
			}
			else
			{
				var newBuffer = new byte[buffer.Length];
				buffer.CopyTo(newBuffer, 0);

				_keyPairServer.Decrypt(ref newBuffer, 0, size);

				//Dump(newBuffer, size, "server.txt");
				DumpInfo(newBuffer);
			}
            _server.Send(buffer, size, SocketFlags.None);
			//_server.BeginSend(buffer, 0, size, SocketFlags.None, ar => _server.EndSend(ar), _server);
		}
	}
}