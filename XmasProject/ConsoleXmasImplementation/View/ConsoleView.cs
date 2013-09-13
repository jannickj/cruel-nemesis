using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Timers;
using JSLibrary.Data;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineView;
using JSLibrary;

namespace ConsoleXmasImplementation.View
{
	public class ConsoleView : XmasView
	{
		private const int UPDATE_DELAY = 1000/25;
		private const int WORK_PCT = 90;

		private ConsoleViewFactory entityFactory;
		private ThreadSafeEventQueue eventqueue;
		private ConsoleWorldView viewWorld;
        private Point drawPos;

		public ConsoleView(XmasModel model, Point drawPos, ConsoleWorldView viewWorld, ConsoleViewFactory entityFactory, ThreadSafeEventManager evtmanager) : base(evtmanager)
		{
			this.viewWorld = viewWorld;
			this.entityFactory = entityFactory;
            this.drawPos = drawPos;
			eventqueue = model.EventManager.ConstructEventQueue();
			evtmanager.AddEventQueue(eventqueue);
			eventqueue.Register(new Trigger<EntityAddedEvent>(Model_EntityAdded));
			eventqueue.Register(new Trigger<EntityRemovedEvent>(model_EntityRemoved));
		}

		private void model_EntityRemoved(EntityRemovedEvent evt)
		{
			viewWorld.RemoveEntity(evt.RemovedXmasEntity);
		}

		public void Setup()
		{
		}

		private void Draw()
		{
            lock (ExtendedConsole.ConsoleWriterLock)
            {
                Console.SetCursorPosition(drawPos.X, drawPos.Y);
                Console.Write(Area());
            }
		}

		public Char[] Area()
		{
			int width = viewWorld.Width;
			int height = viewWorld.Height;
			DictionaryList<Point, ConsoleEntityView> entities = viewWorld.AllEntities();
			DrawSceen screen = new DrawSceen(width, height);

			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++ )
					screen[x,y] = ' ';

			foreach (Point p in entities.Keys)
			{
				int x = p.X;
				int y = p.Y;
				ICollection<ConsoleEntityView> ents;
				if (entities.TryGetValues(p, out ents))
				{
					int count = ents.Count;
					if (count > 1)
						screen[x, y] = count.ToString().ToArray()[0];
					else
						screen[x, y] = ents.First().Symbol;
				}
			}
			return screen.GenerateScreen();
		}

		private void Update()
		{
            long slept = 0;
			DateTime start = DateTime.Now;
			Draw();

			long updateDelayTicks = UPDATE_DELAY * 10000;
			long workTicks = (long)((WORK_PCT / 100.0) * updateDelayTicks);

			Func<long> remainPct = () => ((DateTime.Now.Ticks - start.Ticks) / 100) / UPDATE_DELAY;
			Func<long> remainingTicks = () => workTicks - (DateTime.Now.Ticks - start.Ticks);

//			while (this.evtmanager.ExecuteNext() && remainPct() <= WORK_PCT) { }
			while (true)
			{
				var ticksLeft = remainingTicks();
				if(ticksLeft < 0)
					break;
                long sleptNow;
				this.ThreadSafeEventManager.ExecuteNextWhenReady(new TimeSpan(ticksLeft),out sleptNow);
                slept += sleptNow;
			}
            DateTime after = DateTime.Now;
            long timespent =  ((after.Ticks - start.Ticks) / 10000L);
			long sleeptime = UPDATE_DELAY - timespent;
			
			

			
//			Console.Write("\rLOAD: " + pct + "%\t\t\t");
            if (sleeptime > 0)
            {
                Thread.Sleep((int)sleeptime);
                slept += sleeptime * 10000;
            }
            lock (ExtendedConsole.ConsoleWriterLock)
            {
//                Console.SetCursorPosition(drawPos.X, drawPos.Y);
                long uticks = UPDATE_DELAY * 10000;
                long pct = (uticks - slept) / (updateDelayTicks / 100); 
//                Console.Write("\rLOAD: " + pct + "%\t\t\t");
            }

		}

		public override void Start()
		{
			while (true)
			{
				Update();
			}	
		}

		

		private void Model_EntityAdded(EntityAddedEvent evt)
		{
            
			viewWorld.AddEntity((ConsoleEntityView)entityFactory.ConstructEntityView(evt.AddedXmasEntity,evt.AddedPosition));
		}
	}
}