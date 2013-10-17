using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;
using JSLibrary.Data;
using JSLibrary.Network;
using NUnit.Framework;
using XmasEngineExtensions.EisExtension.Controller.AI;
using XmasEngineExtensions.EisExtension.Model;
using XmasEngineExtensions.EisExtension.Model.Conversion.IiLang.Actions;
using XmasEngineExtensions.EisExtension.Model.Conversion.IiLang.Percepts;
using XmasEngineExtensions.TileEisExtension.Conversion;
using XmasEngineExtensions.TileExtension;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace XmasEngineExtensions_Test.EisExtension.AI
{
	//Scenario Test

	//[TestFixture]
	public class EISAgentControllerTest
	{
		private bool lock1 = true;
		private EISAgentController controller;

		private void test2()
		{
			TcpListener tcp = new TcpListener(IPAddress.Parse("127.0.0.1"), 6661);
			tcp.Start();
			lock1 = false;
			TcpClient client = tcp.AcceptTcpClient();
			string actiontext = @"<action name=""getAllPercepts"">
				</action>";

			StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8);
			writer.Write(actiontext);
			writer.Flush();
		}

		private void test1()
		{
			controller.Start();
		}

		//[Test]
		public void SingleUpdate_RecievedGetAllPercepts_PickUpPerceptsAndReturnThemThroughWriter()
		{
			ActionManager manager = new ActionManager(new EventManager());
			TileWorldBuilder worldBuilder = new TileWorldBuilder(new Size(4, 4));
			
			
			Agent agent = new Agent("testagent");
			worldBuilder.AddEntity(() => agent, new TileSpawnInformation(new TilePosition(new Point(0, 0))));

			XmasFactory fact = new XmasFactory(manager);
			TileWorld world = (TileWorld)worldBuilder.Build();
			agent.ActionManager = manager;
			agent.World = world;
			agent.Factory = fact;




			Thread thread1 = new Thread(test2);
			thread1.Name = "TCP Server";
			thread1.Start();

			while (lock1)
			{
			}
			TcpClient client = new TcpClient("localhost", 6661);
			Stream stream = client.GetStream();

			StringBuilder sb = new StringBuilder();


			EisConversionTool ctool = new EisConversionTool();
			ctool.AddConverter(new GetAllPerceptsActionConverter());
			ctool.AddConverter(new EISPerceptCollectionSerializer());
			ctool.AddConverter(new EisTileVisionSerializer());
			ctool.AddConverter(new EISSingleNumeralSerializer());

			IILActionParser parser = new IILActionParser();

			XmlReaderSettings set = new XmlReaderSettings();
			set.ConformanceLevel = ConformanceLevel.Fragment;
			XmlReader xreader = XmlReader.Create(new StreamReader(stream, Encoding.UTF8));
			XmlWriterSettings wset = new XmlWriterSettings();
			wset.OmitXmlDeclaration = true;
			XmlWriter xwriter = XmlWriter.Create(sb, wset);
			controller = new EISAgentController(agent, client, manager, new PacketStream(stream), new StreamReader(stream), new StreamWriter(stream), ctool, parser);


			Thread thread2 = new Thread(test1);
			thread2.Name = "Controller";
			thread2.Start();

			while (manager.ExecuteActions() == 0) ;
			string returned = null;
			do
			{
				try
				{
					returned = sb.ToString();
				}
				catch
				{
				}
			} while (String.IsNullOrEmpty(returned));
		}
	}
}