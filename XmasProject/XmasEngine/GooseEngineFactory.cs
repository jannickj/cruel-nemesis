using System;
using System.Threading;
using XmasEngineController;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;

namespace XmasEngine
{
	public abstract class GooseEngineFactory<TView,TController>
		where TView : GooseView
		where TController : GooseController
	{
		public virtual XmasModel ConstructModel(XmasMap map)
		{

			//TODO: FIX Factory code
			XmasWorld world = null; //new XmasWorld(map);
			ActionManager actman = ConstructActionManager();
			EventManager evtman = ConstructEventManager();
			XmasFactory fact = ConstructGameFactory(actman);
			XmasModel engine = new XmasModel(world, actman, evtman, fact);

			return engine;
		}

		protected virtual XmasFactory ConstructGameFactory(ActionManager actman)
		{
			return new XmasFactory(actman);
		}

		protected virtual EventManager ConstructEventManager()
		{
			return new EventManager();
		}

		protected virtual ActionManager ConstructActionManager()
		{
			return new ActionManager();
		}

		public abstract TView ConstructView(XmasModel model);


        public abstract TController ContructController(XmasModel model, TView view);


        
		public Tuple<XmasModel,TView,TController> FullConstruct(XmasMap map,params AgentFactory[] agentFactory)
		{
			XmasModel model = this.ConstructModel(map);
            TView view = this.ConstructView(model);
            TController controller = this.ContructController(model, view);

            foreach (AgentFactory afact in agentFactory)
                controller.AddAiServer(afact.ContructServer());
           

			return Tuple.Create(model, view, controller);
		}

		public void StartEngine(XmasModel model, GooseView view, GooseController controller)
		{
			XmasFactory fact = model.Factory;
			Thread modelt = fact.CreateThread(model.Start);
			Thread viewt = fact.CreateThread(view.Start);
			Thread cont = fact.CreateThread(controller.Start);

			modelt.Name = "Model Thread";
			viewt.Name = "View Thread";
			cont.Name = "Controller Thread";

			model.Initialize();
			view.Initialize();
			controller.Initialize();

			modelt.Start();
			viewt.Start();
			cont.Start();
		}
	}
}