using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management.Actions;
using XmasEngineModel.EntityLib;
using XmasEngineExtensions.TileExtension;
using Cruel.Library.PathFinding;
using JSLibrary.Data.GenericEvents;
using XmasEngineModel.Management;

namespace Cruel.GameLogic.Actions
{
	public class MovePathAction : MultiAction
	{
        public Path<TileWorld, TilePosition> Path { get; private set; }

        public MovePathAction(XmasEntity ent, Path<TileWorld, TilePosition> path, int durPerMove)
        {
            this.Path = path;

            foreach (TilePosition pos in path.Road)
            {
                this.AddAction(ent, new MoveAction(pos.Point, durPerMove));
            }
            this.SingleActionCompleted += new UnaryValueHandler<XmasAction>(MovePathAction_SingleActionCompleted);
            this.AddRaiseActionEvent(ent);
        }

      

        void MovePathAction_SingleActionCompleted(object sender, UnaryValueEvent<XmasAction> evt)
        {
            MoveAction mv = (MoveAction)evt.Value;

            if (mv.ActionFailed || mv.MoveStopped)
                this.StopMultiAction();
        }




        public XmasEntity Entity { get; set; }
    }
}
