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
        private float cost;

        private TPos loc;
        private Func<TPos,int> posHashingFunc;
        private Func<TPos, TPos, bool> posEqualFunc;

        public PathNode(PathNode<TPos> parent, float cost, TPos pos, Func<TPos,int> posHashingFunc, Func<TPos,TPos,bool> posEqualFunc)
        {
            this.parent = parent;
            this.cost = cost;
            this.loc = pos;
            this.posEqualFunc = posEqualFunc;
            this.posHashingFunc = posHashingFunc;
        }

        public TPos Location
        {
            get { return loc; }
        }

        public PathNode<TPos> Parent
        {
            get { return parent; }
        }

        public float Cost
        {
            get { return cost; }
        }

        public override int GetHashCode()
        {
            return posHashingFunc(loc);
        }

        public override bool Equals(object obj)
        {
            PathNode<TPos> node = obj as PathNode<TPos>;
            if (node != null)
            {
                
                return this.posEqualFunc(this.Location,node.Location);
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

            return p1.Equals(p2);
        }

        public static bool operator !=(PathNode<TPos> p1, PathNode<TPos> p2)
        {
            return !(p1 == p2);
        }
    }
}
