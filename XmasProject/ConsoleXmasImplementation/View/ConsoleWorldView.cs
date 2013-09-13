using System;
using System.Collections.Generic;
using ConsoleXmasImplementation.Model;
using JSLibrary.Data;
using XmasEngineExtensions.TileExtension;
using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.View
{
	public class ConsoleWorldView
	{
		private TileWorld model;
		private Dictionary<XmasEntity, ConsoleEntityView> viewlookup = new Dictionary<XmasEntity, ConsoleEntityView>();
		private XmasEntity focus;
		private Func<XmasEntity, bool> focusCheck;

		public ConsoleWorldView(TileWorld model)
		{
			this.model = model;
            this.focusCheck = _ => false;
		}

		public ConsoleWorldView(TileWorld model, Func<XmasEntity, bool> focusCheck) : this(model)
		{
			this.focusCheck = focusCheck;
		}

		public int Width
		{
			get { return model.Size.Width; }
		}

		public int Height
		{
			get { return model.Size.Height; }
		}

		public void AddEntity(ConsoleEntityView entview)
		{
			viewlookup.Add(entview.Model, entview);
			if (focusCheck(entview.Model))
				this.focus = entview.Model;
		}

		public DictionaryList<Point, ConsoleEntityView> AllEntities()
		{
			var locs = new DictionaryList<Point, ConsoleEntityView>();
			Size wbSize = this.model.BurstSize;

			if (focus == null || focus.Module<VisionModule>().Vision == null)
			{
				

				foreach (KeyValuePair<XmasEntity, ConsoleEntityView> kv in viewlookup)
				{
					Point p = ((TilePosition) kv.Value.Position).Point;
					Point transp = new Point(p.X + wbSize.Width, p.Y + wbSize.Height);

					locs.Add(transp, kv.Value);
				}
			}
			else
			{
				var vision = focus.Module<VisionModule>();

				foreach (var kv in vision.Vision.VisibleTiles)
				{
					Point p = kv.Key;
					Point transp = new Point(p.X + wbSize.Width, p.Y + wbSize.Height);
					foreach (var ent in kv.Value.Entities)
					{
						ConsoleEntityView vent;
						if(viewlookup.TryGetValue(ent,out vent))
							locs.Add(transp, vent);
					}
				}
			}
			return locs;
		}

		internal void RemoveEntity(XmasEntity entity)
		{
			viewlookup[entity].Dispose();
			viewlookup.Remove(entity);
		}
	}
}