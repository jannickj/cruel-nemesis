using System;
using System.Threading;
using JSLibrary.Data.GenericEvents;
using XmasEngineModel.Exceptions;
using XmasEngineModel.Interfaces;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;

namespace XmasEngineModel
{
    /// <summary>
    /// The core of the engine, this is responsible for starting all other components used within the model
    /// </summary>
	public class XmasModel : IStartable
	{
		private AutoResetEvent actionRecieved = new AutoResetEvent(false);
		private ActionManager actman;
		private Exception engineCrash;
		private EventManager evtman;
		private XmasFactory factory;
		private bool stopEngine;
		private XmasWorld world;


		public XmasModel(XmasWorld world, ActionManager actman, EventManager evtman, XmasFactory factory)
		{
			World = world;
			ActionManager = actman;
			EventManager = evtman;
			Factory = factory;
			world.EventManager = evtman;

			EventManager.Register(new Trigger<EngineCloseEvent>(evtman_EngineClose));
			ActionManager.ActionQueuing += actman_ActionQueuing;
			ActionManager.ActionQueued += actman_ActionQueued;

			foreach (var action in ActionManager.QueuedActions)
			{
				this.AddActor(action);
			}
		}


        /// <summary>
        /// Initialization of the model of the engine
        /// </summary>
		public void Initialize()
		{
		}

        /// <summary>
        /// The main method of the model of the engine
        /// </summary>
		public void Start()
		{
			try
			{
				stopEngine = false;

				while (true)
				{
					ActionManager.ExecuteActions();
					lock (this)
					{
						if (stopEngine)
							break;
					}
					actionRecieved.WaitOne();
				}
			}
			catch (ForceStopEngineException)
			{
			}
			catch (Exception e)
			{
				engineCrash = e;
			}
		}

        /// <summary>
        /// Checks if the engine has momentarily creashed
        /// </summary>
        /// <param name="exception">The exception that caused the crash</param>
        /// <returns>Whether or not the engine crashed</returns>
		public bool EngineCrashed(out Exception exception)
		{
			if (engineCrash != null)
			{
				exception = engineCrash;
				return true;
			}

			exception = null;
			return false;
		}

        /// <summary>
        /// Makes an actor part of the engine, by providing it with all necessary tools
        /// </summary>
        /// <param name="actor">The actor to be made part of the engine</param>
		public void AddActor(XmasActor actor)
		{
			actor.ActionManager = ActionManager;
			actor.EventManager = EventManager;
			actor.World = World;
			actor.Factory = Factory;
			
		}

		#region EVENTS

		private void evtman_EngineClose(EngineCloseEvent e)
		{
			stopEngine = true;

			actionRecieved.Set();
		}

		private void actman_ActionQueuing(object sender, UnaryValueEvent<XmasAction> evt)
		{
			evt.Value.EventManager = EventManager;
			evt.Value.Factory = Factory;
			evt.Value.World = World;
			evt.Value.ActionManager = ActionManager;
		}

		private void actman_ActionQueued(object sender, UnaryValueEvent<XmasAction> evt)
		{
			this.actionRecieved.Set();
		}

		#endregion

		#region PROPERTIES

		public XmasWorld World
		{
			get { return world; }
			internal set { world = value; }
		}

		public XmasFactory Factory
		{
			get { return factory; }

			internal set { factory = value; }
		}


		public EventManager EventManager
		{
			get { return evtman; }
			internal set { evtman = value; }
		}

		public ActionManager ActionManager
		{
			get { return actman; }
			internal set { actman = value; }
		}

		#endregion
	}
}