using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Net.Sockets;
using JSLibrary.Network;
using System.Net;
using CruelServer;
using CruelNetwork;
using JSLibrary.Network.Data;
using System.Runtime.Serialization.Formatters.Binary;
using Cruel.GameLogic;
using CruelGameData.GameLogic.Game.Maps;
using CruelNetwork.Messages;

namespace CruelTest.ScenarioTests
{
    [TestClass]
    public class ServerClientTest
    {
        private AutoResetEvent serverReady = new AutoResetEvent(false);

        //This is not a unit test this require a person to manually check if things are working as expected.
        [TestMethod]
        public void ServerTest()
        {
            var serverThread = new Thread(new ParameterizedThreadStart(_ => ServerCode()));
            serverThread.Name = "SERVER THREAD";
            var clientThread = new Thread(new ParameterizedThreadStart(_ => ClientCode()));
            clientThread.Name = "CLIENT THREAD";
            serverThread.Start();
            clientThread.Start();
            while (true) { }
        }

        private void ClientCode()
        {
            serverReady.WaitOne();

            var EngineFactory = new CruelEngineFactory();
            TcpClient client = new TcpClient();
            var messageTool = new CruelMessageTool();
            var Engine = EngineFactory.CreateEngine(new StandardGameMapBuilder());
            messageTool.SetEngine(Engine);
            var formatter = new BinaryFormatter();
            client.Connect(IPAddress.Parse("127.0.0.1"), 4000);
            CruelConnection connect = new CruelConnection(client.GetStream(), messageTool, formatter);
            var sendUpdater = new Thread(new ThreadStart(() => 
                {
                    try
                    {
                        while (true) connect.UpdateSending();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("Send Fail: " + e.Message);
                    }
                }));
            sendUpdater.Name = "CLIENT SENDER";
            var receiveUpdater = new Thread(new ThreadStart(() => { while(true) connect.UpdateRecieving(); }));
            receiveUpdater.Name = "CLIENT RECIEVER";
            sendUpdater.Start();
            receiveUpdater.Start();
            connect.QueueMessage(new CreateGameMessage());
        }


        private void ServerCode()
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 4000);
                
                var ServerFactor = new ServerFactory();
                var ClientManager = new CruelClientManager();

                CruelServerApp server = new CruelServerApp(ServerFactor, listener, ClientManager);
                listener.Start();
                serverReady.Set();
               
                server.Start();
            }
            catch(Exception e)
            {
                Assert.Fail("SERVER FAILED: " + e.Message);
            }
        }
    }
}
