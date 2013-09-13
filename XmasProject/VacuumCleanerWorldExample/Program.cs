using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmasEngine;
using XmasEngineController;
using XmasEngineExtensions.LoggerExtension;
using XmasEngineModel;
using XmasEngineView;

namespace VacuumCleanerWorldExample
{
	class Program
	{
		static void Main(string[] args)
		{
			//The factory responsible for constructing components needed by the engine
			XmasModelFactory factory = new XmasModelFactory();

			VacuumMap1 map1 = new VacuumMap1();

			//Construct the model with all its required components
			XmasModel model = factory.ConstructModel(map1);

			//makes a file where all view info is logged
			StreamWriter sw = File.CreateText("viewlog.log");
			

			//Construct the view for the vacuum world
			VacuumWorldView view = new VacuumWorldView(model, new Logger(sw, DebugLevel.All));

			//Construct the manager for the agent controller with the name of the agent
			VacuumAgentManager controller = new VacuumAgentManager("vacuum_cleaner");

			//Instantiate and start the engine with the view and the controller
			XmasEngineManager engine = new XmasEngineManager(factory);
			engine.StartEngine(model, new XmasView[] { view }, new XmasController[] { controller });
			
		}
	}
}
