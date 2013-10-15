using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Library.PathFinding
{
	public abstract class PathFinder<TMap,TPos>
	{
        private TMap map;

        public TMap Map
        {
            get { return map; }
        }

        public PathFinder(TMap map)
        {
            this.map = map;
        }

        protected abstract TPos[] getNeighbours(TPos node);
        protected abstract int getEdgeCost(TPos from, TPos to);
        protected abstract int getHeuristic(TPos from, TPos to);

        public Path<TMap, TPos> FindFirst(TPos start, TPos goalPos,out bool foundPath)
        {
            return FindFirst(start, pos => { Debug.Log(pos + " == " + goalPos+" is "+pos.Equals(goalPos)); return pos.Equals(goalPos); }, out foundPath);
        }

        public Path<TMap,TPos> FindFirst(TPos start, Predicate<TPos> goalCondition ,out bool foundPath)
        {
            Path<TMap, TPos>[] path = astarSearch(start, goalCondition, true);
            if (path.Length > 0)
            {
                foundPath = true;
                return path[0];
            }
            else
            {
                foundPath = false;
                return default(Path<TMap, TPos>);
            }           
        }

        public Path<TMap, TPos>[] FindAll(TPos start, TPos goalPos)
        {
            return FindAll(start, pos => pos.Equals(goalPos));
        }

        public Path<TMap, TPos>[] FindAll(TPos start, Predicate<TPos> goalCondition)
        {
            return astarSearch(start, goalCondition, false);
        }


        private Path<TMap, TPos>[] astarSearch(TPos start, Predicate<TPos> goalCondition ,bool stopOnFirst)
        {
            Dictionary<PathNode<TPos>, int> guessmap = new Dictionary<PathNode<TPos>, int>();
            SortedDictionary<int, Dictionary<PathNode<TPos>, PathNode<TPos>>> open = new SortedDictionary<int, Dictionary<PathNode<TPos>, PathNode<TPos>>>();

            Dictionary<PathNode<TPos>, PathNode<TPos>> closed = new Dictionary<PathNode<TPos>, PathNode<TPos>>();
            
            List<PathNode<TPos>> possibleGoals = new List<PathNode<TPos>>();

            AddToSorted(open, guessmap,new PathNode<TPos>(null, 0, start),0);

            while (open.Count > 0)
            {
                PathNode<TPos> current = PopFromSorted(open, guessmap);

                
                if (goalCondition(current.Location))
                {
                    possibleGoals.Add(current);
                    if (stopOnFirst)
                        break;
                }

                PathNode<TPos>[] neighbours = getNeighbours(current.Location).Select(pos => generateNeighbour(current, pos)).ToArray();

                foreach (PathNode<TPos> neighbour in neighbours)
                {
                    PathNode<TPos> oldNode;
                    if (tryFind(open,guessmap, neighbour,out oldNode))
                        if (oldNode.Cost < neighbour.Cost)
                            continue;

                    if (closed.TryGetValue(neighbour,out oldNode))
                        if (oldNode.Cost < neighbour.Cost)
                            continue;

                    RemoveFromSorted(open, guessmap, neighbour);
                    closed.Remove(neighbour);

                    int guesscost = neighbour.Cost + getHeuristic(current.Location, neighbour.Location);

                    AddToSorted(open, guessmap, neighbour,guesscost);
                    
                }

                closed.Add(current,current);
            }
            
            return possibleGoals.Select(node => convertNodeToPath(node)).ToArray();
        }

        private PathNode<TPos> generateNeighbour(PathNode<TPos> parent, TPos pos)
        {
            int cost = parent.Cost + getEdgeCost(parent.Location, pos);
            
            return new PathNode<TPos>(parent, cost, pos);
        }

        private bool tryFind(SortedDictionary<int, Dictionary<PathNode<TPos>, PathNode<TPos>>> sort, Dictionary<PathNode<TPos>, int> guessmap, PathNode<TPos> node, out PathNode<TPos> found)
        {
            int guess;
            found = null;

            if (!guessmap.TryGetValue(node, out guess))
                return false;

            var nodes = sort[guess];
            
            if(nodes.TryGetValue(node, out found))
                return true;

            return false;
        }

        private void AddToSorted(SortedDictionary<int, Dictionary<PathNode<TPos>, PathNode<TPos>>> sort, Dictionary<PathNode<TPos>, int> guessmap, PathNode<TPos> node, int guess)
        {
            Dictionary<PathNode<TPos>, PathNode<TPos>> set;
            if (sort.TryGetValue(guess, out set))
            {
                set = sort[guess];
            }
            else
            {
                set = new Dictionary<PathNode<TPos>, PathNode<TPos>>();
                sort.Add(guess, set);
            }
            set.Add(node, node);
            guessmap.Add(node, guess);
        }

        private void RemoveFromSorted(SortedDictionary<int, Dictionary<PathNode<TPos>, PathNode<TPos>>> sort, Dictionary<PathNode<TPos>, int> guessmap, PathNode<TPos> node)
        {
            int guess;

            if (!guessmap.TryGetValue(node, out guess))
                return;
            Dictionary<PathNode<TPos>, PathNode<TPos>> set;
            if (sort.TryGetValue(guess, out set))
            {
                set = sort[guess];
            }

            set.Remove(node);
            guessmap.Remove(node);
        }

        private PathNode<TPos> PopFromSorted(SortedDictionary<int, Dictionary<PathNode<TPos>, PathNode<TPos>>> sort, Dictionary<PathNode<TPos>, int> guessmap)
        {
            var first = sort.First();
            while (first.Value.Count == 0)
            {
                sort.Remove(first.Key);
                first = sort.First();
            }

            var firstNode = first.Value.First().Key;
            RemoveFromSorted(sort, guessmap, firstNode);
            return firstNode;
        }

        private Path<TMap, TPos> convertNodeToPath(PathNode<TPos> node)
        {
            List<TPos> pathlist = new List<TPos>();
            generatePathRecursive(node, pathlist);

            return new Path<TMap, TPos>(this.map, pathlist);
        }

        private void generatePathRecursive(PathNode<TPos> node, List<TPos> pathlist)
        {
            if (node == null)
                return;
    
            generatePathRecursive(node.Parent, pathlist);
            pathlist.Add(node.Location);
        }


        

	}
}
