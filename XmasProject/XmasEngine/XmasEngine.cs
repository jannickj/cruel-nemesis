using System.Collections.Generic;
using System.Threading;
using XmasEngine.Exceptions;
using XmasEngineController;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;

namespace XmasEngine
{
    /// <summary>
    /// The engine manager, this is responsible for starting up the model and all its views and controllers
    /// </summary>
	public class XmasEngineManager
	{
		private XmasModelFactory factory;

		private Thread modelThread;
		private List<Thread> viewThreads = new List<Thread>();
		private List<Thread> controllerThreads = new List<Thread>();

        /// <summary>
        /// Instantiates a Manager for the engine
        /// </summary>
        /// <param name="factory">The Model Factory that can create major components</param>
		public XmasEngineManager(XmasModelFactory factory)
		{
			this.factory = factory;
		}

        /// <summary>
        /// Starts the engine by providing the model and all its view and controllers with each their own thread.
        /// All actions queued to the engine is executed before the threads are started.
        /// </summary>
        /// <param name="model">The model of the engine</param>
        /// <param name="views">The views of the engine</param>
        /// <param name="controllers">The controllers of the engine</param>
		public void StartEngine(XmasModel model,ICollection<XmasView> views, ICollection<XmasController> controllers)
		{
			if (modelThread != null)
			{
				throw new EngineAlreadyStartedException();
			}
			XmasFactory fact = model.Factory;
			Thread modelt = fact.CreateThread(model.Start);

			model.Initialize();

			int i = 1;

			foreach (var xmasView in views)
			{
				model.AddActor(xmasView);
				xmasView.Initialize();
				Thread viewt = fact.CreateThread(xmasView.Start);
				viewt.Name = "View Thread "+i;
				i++;
				viewThreads.Add(viewt);
			}

			i = 1;
			foreach (var xmasController in controllers)
			{
				model.AddActor(xmasController);
				xmasController.Initialize();
				Thread cont = fact.CreateThread(xmasController.Start);
				cont.Name = xmasController.ThreadName()+" "+ i;
				i++;
				controllerThreads.Add(cont);
			}

			modelt.Name = "Model Thread";
			modelThread = modelt;

			model.ActionManager.ExecuteActions();

			modelt.Start();
			foreach (var viewThread in viewThreads)
			{
				viewThread.Start();
			}

			foreach (var controllerThread in controllerThreads)
			{
				controllerThread.Start();
			}

			
			
		}
	}
}