using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ConsoleXmasImplementation.ConsoleLogger;
using ConsoleXmasImplementation.Controller;
using ConsoleXmasImplementation.Model;
using ConsoleXmasImplementation.Model.Entities;
using ConsoleXmasImplementation.View;
using XmasEngine;
using XmasEngineController;
using XmasEngineExtensions.EisExtension;
using XmasEngineExtensions.EisExtension.Controller.AI;
using XmasEngineExtensions.LoggerExtension;
using XmasEngineExtensions.TileEisExtension;
using XmasEngineExtensions.TileExtension;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;
using JSLibrary.Data;
using ConsoleXmasImplementation.Model.Conversion;

namespace ConsoleXmasImplementation
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			ConsoleFactory factory = new ConsoleFactory();

			//Model contruction
			XmasModel model = factory.ConstructModel(new TestWorld1());

			//View construction
			ThreadSafeEventManager evtman1 = new ThreadSafeEventManager();
			ConsoleView view1 = new ConsoleView(model,new Point(0,0), new ConsoleWorldView((TileWorld)model.World,xe => xe is GrabberAgent), new ConsoleViewFactory(evtman1), evtman1);

            ThreadSafeEventManager evtman2 = new ThreadSafeEventManager();
            ConsoleView view2 = new ConsoleView(model, new Point(0,24), new ConsoleWorldView((TileWorld)model.World), new ConsoleViewFactory(evtman2), evtman2);

			StreamWriter sw = File.CreateText("error.log");

			List<XmasView> views = new List<XmasView>();
			var loggerevtman = new ThreadSafeEventManager();
			var logger = new Logger(sw, DebugLevel.Timing);
			views.Add(new ConsoleLoggerView(model,new LoggerViewFactory(loggerevtman,logger),loggerevtman,logger));
			views.Add(view1);
            views.Add(view2);

			//Controller construction
			var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 44444);
			var eisConverter = new ConsoleEisConversionTool();
			var iilParser = new ConsoleIilActionParser();
			var eisserver = new EISAgentServer(listener, eisConverter, iilParser);


			var humancontroller = new HumanInterfaceManager(new KeyboardSettings());

			List<XmasController> controllers = new List<XmasController>();

			controllers.Add(eisserver);
			controllers.Add(humancontroller);


			var engine = new XmasEngineManager(factory);

			engine.StartEngine(model,views,controllers);
		}
	}
}