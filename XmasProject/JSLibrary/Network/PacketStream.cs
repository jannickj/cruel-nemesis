using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace JSLibrary.Network
{
    /// <summary>
    /// A stream designed specially for sending and recieving data in forms of packages
    /// </summary>
	public class PacketStream : Stream
	{
		private Stream input;
		private MemoryStream memsIn = new MemoryStream();
		private MemoryStream memsOut = new MemoryStream();
		private byte[] buf = new byte[4096];

		public PacketStream(Stream input)
		{
			this.input = input;
		}

        /// <summary>
        /// Causes the stream to read the next package on the stream
        /// </summary>
		public void ReadNextPackage()
		{
			int mustRead = 4;
			byte[] packetLengthArray = new byte[mustRead];
            while (mustRead > 0)
            {
                mustRead -= input.Read(packetLengthArray, 0, mustRead);
                if (mustRead == 0)
                    break;
                Thread.Sleep(200);
            }
            Array.Reverse(packetLengthArray);

			int packetLength = BitConverter.ToInt32(packetLengthArray, 0);

			memsIn.Position = 0;
            memsIn.SetLength(0);
			mustRead = packetLength;
			while (mustRead > 0)
			{
				int maxread = mustRead > buf.Length ? buf.Length : mustRead;

				int haveread = input.Read(buf, 0, maxread);

				memsIn.Write(buf, 0, haveread);

				mustRead -= haveread;
			}

			memsIn.Position = 0;


		}

        /// <summary>
        /// causes the stream to send out the data stored as a package
        /// </summary>
		public override void Flush()
		{
            if (memsOut.Length == 0)
                return;

			byte[] len = BitConverter.GetBytes((int)memsOut.Length);
            Array.Reverse(len);
			this.input.Write(len,0,len.Length);
			this.memsOut.WriteTo(input);
			this.input.Flush();
			memsOut.Position = 0;
			memsOut.SetLength(0);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.memsIn.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			this.memsIn.SetLength(value);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.memsIn.Read(buffer, offset, count);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			this.memsOut.Write(buffer,offset,count);
		}

		public override bool CanRead
		{
            get { return this.memsIn.CanRead; }
		}

		public override bool CanSeek
		{
			get { return memsIn.CanSeek; }
		}

		public override bool CanWrite
		{
			get { return input.CanWrite; }
		}

		public override long Length
		{
			get { return memsIn.Length; }
		}

		public override long Position { get { return memsIn.Position; } set { memsIn.Position = value; } }
	}
}
