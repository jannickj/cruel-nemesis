using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using Assets.GameLogic.Unit;
using XmasEngineModel.Management.Actions;

namespace Assets.GameLogic.Actions
{
	public class MoveAction : EntityXmasAction
	{
        private int duration;
        private Vector v;
        private Point toward;
        private bool usePoint = false;
        private bool moveStopped = false;

        public MoveAction(Vector v, int duration)
        {
            //ensure x and y is a under that is either 1, 0 or -1
            int x = v.X > 1 ? 1 : (v.X < -1 ? -1 : v.X);
            int y = v.Y > 1 ? 1 : (v.Y < -1 ? -1 : v.Y);

            this.v = new Vector(x,y);
            
            this.duration = duration;
        }

        public MoveAction(Point towardPoint, int duration)
        {
            this.toward = towardPoint;
            this.usePoint = true;
            this.duration = duration;
        }

        public bool MoveStopped 
        {
            get
            {
                return moveStopped;
            }
        }

        protected override void Execute()
        {


            var unit = (UnitEntity)this.Source;
            TilePosition p = (TilePosition)this.World.GetEntityPosition(this.Source);
            Point currentPos = p.Point;

            if (usePoint)
                v = new Vector(currentPos, toward);

            Point newPos = currentPos + v;
            var preMove = new PreMoveEvent(unit, currentPos, newPos);
            this.Source.Raise(preMove);
            if (preMove.MoveStopped)
            {
                this.moveStopped = true;
                return;
            }
            this.Source.Raise(new BeginMoveEvent(unit, currentPos, newPos,this.duration));
            if (duration != 0)
            {
                TimedAction t = this.Factory.CreateTimer(delegate
                    {
                        MoveUnit(currentPos, newPos);
                        
                    });
                t.SetSingle(duration);
                this.RunAction(t);
            }
            else
            {
                MoveUnit(currentPos, newPos);
                
            }


        }

        private void MoveUnit(Point currentPos, Point newPos)
        {
            this.World.SetEntityPosition(this.Source, new TilePosition(newPos));
            this.Source.Raise(new EndMoveEvent((UnitEntity)this.Source, currentPos, newPos));
        }

        
    }
}
