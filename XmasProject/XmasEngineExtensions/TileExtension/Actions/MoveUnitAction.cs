using JSLibrary.Data;
using XmasEngineExtensions.TileExtension.Events;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.Management.Actions;

namespace XmasEngineExtensions.TileExtension.Actions
{
	public class MoveUnitAction : EntityXmasAction<Agent>
	{
		private Vector direction;
		private double time;

		/// <summary>
		///     Initializes a move action, which is used to move entities in a gameworld
		/// </summary>
		/// <param name="world"> The world the unit is moved in</param>
		/// <param name="unit"> The unit that gets moved</param>
		/// <param name="direction"> the direction vector of the move</param>
		/// <param name="time"> the time in miliseconds that the move takes</param>
//		public MoveUnitAction(Vector direction, double time)
//        {
//            this.direction = direction.Direction;
//            this.time = time;
//        }

		public MoveUnitAction(Vector direction)
		{
			this.direction = direction.Direction;
		}

		protected override void Execute()
		{
			TilePosition tile = World.GetEntityPosition(Source) as TilePosition;
			Point newloc = tile.Point + direction;
			UnitMovePreEvent before = new UnitMovePreEvent(newloc);

			Source.Raise(before);
			if (before.IsStopped)
			{
				Complete();
				return;
			}
			time = Source.Module<SpeedModule>().Speed;
			if (time == 0)
			{
				if (World.SetEntityPosition(Source, new TilePosition(newloc)))
					Source.Raise(new UnitMovePostEvent(newloc));
				else
					Source.Raise(new UnitMovePostEvent(tile.Point));
				Complete();
			}
			else
			{
				TimedAction gt = Factory.CreateTimer(() =>
					{
						if (World.SetEntityPosition(Source, new TilePosition(newloc)))
							Source.Raise(new UnitMovePostEvent(newloc));
						else
							Source.Raise(new UnitMovePostEvent(tile.Point));

						Complete();
					});

				
					gt.SetSingle(time);
                    this.RunAction(gt);
			}
		}
	}
}