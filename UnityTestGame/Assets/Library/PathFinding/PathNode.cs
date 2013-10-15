using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Library.PathFinding
{
	public class PathNode<TPos>
	{
        private PathNode<TPos> parent;
        private int cost;

        private TPos loc;


        public PathNode(PathNode<TPos> parent, int cost, TPos pos)
        {
            this.parent = parent;
            this.cost = cost;
            this.loc = pos;
        }

        public TPos Location
        {
            get { return loc; }
        }

        public PathNode<TPos> Parent
        {
            get { return parent; }
        }

        public int Cost
        {
            get { return cost; }
        }

        public override int GetHashCode()
        {
            return loc.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            PathNode<TPos> node = obj as PathNode<TPos>;
            if (node != null)
            {
                
                return this.loc.Equals(node.loc);
            }
            else
                return false;
        }

        public static bool operator ==(PathNode<TPos> p1, PathNode<TPos> p2)
        {
            if (((object)p1) == null && ((object)p2) == null)
                return true;

            if (((object)p1) == null || ((object)p2) == null)
                return false;

            return p1.loc.Equals(p2.loc);
        }

        public static bool operator !=(PathNode<TPos> p1, PathNode<TPos> p2)
        {
            return !(p1 == p2);
        }
    }
}
