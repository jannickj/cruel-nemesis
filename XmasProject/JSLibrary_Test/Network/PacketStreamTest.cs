using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JSLibrary.Network;
using NUnit.Framework;
using JSLibrary.IiLang.DataContainers;
using JSLibrary.IiLang.Parameters;
using System.Xml.Serialization;
using System.Xml;
using JSLibrary;

namespace JSLibrary_Test.Network
{
	[TestFixture]
	public class PacketStreamTest
	{

		[Test]
		public void ReadPacket_HelloWorldMsg_GetsPacketCorretly()
		{
			string expected = "Hello world";
			MemoryStream mems = new MemoryStream();
			PacketStream pstream = new PacketStream(mems);
			byte[] teststring = Encoding.UTF8.GetBytes(expected);
			byte[] len = BitConverter.GetBytes(teststring.Length);
            Array.Reverse(len);
			mems.Write(len, 0, len.Length);
			mems.Write(teststring,0, teststring.Length);
            mems.Position = 0;

			pstream.ReadNextPackage();

			string actual = new StreamReader(pstream,Encoding.UTF8).ReadToEnd();
			
			Assert.AreEqual(expected,actual);
		}

		[Test]
		public void ReadAndSendPacket_TwoPackages_GetsTheSentPacketsCorrectly()
		{
            string expected1 = "packet 1";
            string expected2 = "packet 2";
			MemoryStream memsout = new MemoryStream();

            PacketStream packet = new PacketStream(memsout);

			StreamWriter sw = new StreamWriter(new PacketStream(memsout));

			StreamReader sr = new StreamReader(packet);

			sw.WriteLine(expected1);
			sw.Flush();
			sw.WriteLine(expected2);
			sw.Flush();

            memsout.Position = 0;

            packet.ReadNextPackage();
			string actual1 = sr.ReadLine();
            packet.ReadNextPackage();
			string actual2 = sr.ReadLine();

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            
		}

        [Test]
        public void ReadStream_XmlStreamWithTwoPackets_DeserializeXmlCorrectly()
        {
            IilAction expected = new IilAction("moveTo", new IilNumeral(2), new IilNumeral(3));

            XmlSerializer serializer = new XmlSerializer(typeof(IilAction));


            string xmldata = "<action name=\"moveTo\">\n" +
                                "<actionParameter>\n" +
                                    "<number value=\"2\" />\n" +
                                "</actionParameter>\n" +
                                "<actionParameter>\n" +
                                    "<number value=\"3\" />\n" +
                                "</actionParameter>\n" +
                            "</action>\n";

            MemoryStream memsout = new MemoryStream();

 

            StreamWriter sw = new StreamWriter(new PacketStream(memsout));
            sw.Write(xmldata);
            sw.Flush();
            sw.Write(xmldata);
            sw.Flush();
            memsout.Position = 0;
           
            PacketStream packet = new PacketStream(memsout);
           

            StreamReader sreader = new StreamReader(packet, Encoding.UTF8);

            packet.ReadNextPackage();
            
            XmlReaderSettings rset = new XmlReaderSettings();
            rset.ConformanceLevel = ConformanceLevel.Fragment;
            //XmlReader xreader= XmlReader.Create(sreader, rset);
            
            IilAction actual;

            actual = (IilAction)serializer.Deserialize(sreader);
            Assert.AreEqual(expected, actual);

            packet.ReadNextPackage();

            actual = (IilAction)serializer.Deserialize(sreader);
            Assert.AreEqual(expected, actual);



        }


	}
}
