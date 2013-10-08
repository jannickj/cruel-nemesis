using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using Assets.GameLogic.Unit;

namespace Assets.GameLogic.Actions
{
	public class MoveAction : EntityXmasAction
	{
        private int duration;
        private Vector v;

        public MoveAction(Vector v, int duration)
        {
            this.v = v;
            this.duration = duration;
        }

        protected override void Execute()
        {
            var unit = (UnitEntity)this.Source;
            TilePosition p = (TilePosition)this.World.GetEntityPosition(this.Source);
            Point currentPos = p.Point;
            Point newPos = currentPos + v;
            var preMove = new PreMoveEvent(unit, currentPos, newPos);
            this.Source.Raise(preMove);
            if (preMove.MoveStopped)
            {
                this.Complete();
                return;
            }
            this.Source.Raise(new BeginMoveEvent(unit, currentPos, newPos,this.duration));
            if (duration != 0)
            {
                XmasTimer t = this.Factory.CreateTimer(this, delegate
                    {
                        MoveUnit(currentPos, newPos);
                        this.Complete();
                    });
                t.StartSingle(duration);
            }
            else
            {
                MoveUnit(currentPos, newPos);
                this.Complete();
            }


        }

        private void MoveUnit(Point currentPos, Point newPos)
        {
            this.World.SetEntityPosition(this.Source, new TilePosition(newPos));
            this.Source.Raise(new EndMoveEvent((UnitEntity)this.Source, currentPos, newPos));
        }
    }
}
