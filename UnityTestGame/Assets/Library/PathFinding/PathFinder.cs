using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections.Specialized;

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
        protected abstract float getEdgeCost(TPos from, TPos to);
        protected virtual float getHeuristic(TPos from, TPos to)
        {
            return 0;
        }
      
        protected abstract int getPosHash(TPos pos);
        protected abstract bool isPosEqual(TPos pos1, TPos pos2);


        public bool FindFirst(TPos start, TPos goalPos,out  Path<TMap, TPos> foundPath)
        {
            return FindFirst(start, pos => isPosEqual(pos,goalPos),pos => getHeuristic(pos,goalPos), out foundPath);
        }

        public bool FindFirst(TPos start, Predicate<TPos> goalCondition, Func<TPos, float> getHeuristic, out  Path<TMap, TPos> foundPath)
        {
            Path<TMap, TPos>[] path = astarSearch(start, goalCondition, getHeuristic, true);
            if (path.Length > 0)
            {
                foundPath = path[0];
                return true;
            }
            else
            {
                foundPath = default(Path<TMap,TPos>);
                return false;
            }           
        }

        public Path<TMap, TPos>[] FindAll(TPos start, TPos goalPos)
        {
            return FindAll(start, pos => isPosEqual(pos, goalPos), pos => getHeuristic(pos, goalPos));
        }

        public Path<TMap, TPos>[] FindAll(TPos start, Predicate<TPos> goalCondition, Func<TPos, float> getHeuristic)
        {
            return astarSearch(start, goalCondition, getHeuristic, false);
        }


        private Path<TMap, TPos>[] astarSearch(TPos start, Predicate<TPos> goalCondition, Func<TPos, float> getHeuristic, bool stopOnFirst)
        {
            Dictionary<PathNode<TPos>, float> guessmap = new Dictionary<PathNode<TPos>, float>();
            SortedDictionary<float, OrderedDictionary> open = new SortedDictionary<float, OrderedDictionary>();

            Dictionary<PathNode<TPos>, PathNode<TPos>> closed = new Dictionary<PathNode<TPos>, PathNode<TPos>>();
            
            List<PathNode<TPos>> possibleGoals = new List<PathNode<TPos>>();

            AddToSorted(open, guessmap,makeNode(null, 0, start),0);

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
                    
                    float guesscost = neighbour.Cost + getHeuristic(neighbour.Location);

                    AddToSorted(open, guessmap, neighbour,guesscost);
                    
                }
                closed.Add(current,current);
                CleanList(open);
            }
            
            return possibleGoals.Select(node => convertNodeToPath(node)).ToArray();
        }

        

        private PathNode<TPos> makeNode(PathNode<TPos> parent, float cost, TPos pos)
        {
            PathNode<TPos> node = new PathNode<TPos>(parent, cost, pos, getPosHash, isPosEqual);
            return node;
        }

        private PathNode<TPos> generateNeighbour(PathNode<TPos> parent, TPos pos)
        {
            float cost = parent.Cost + getEdgeCost(parent.Location, pos);
            
            return makeNode(parent, cost, pos);
        }

        private bool tryFind(SortedDictionary<float, OrderedDictionary> sort, Dictionary<PathNode<TPos>, float> guessmap, PathNode<TPos> node, out PathNode<TPos> found)
        {
            float guess;
            found = null;

            if (!guessmap.TryGetValue(node, out guess))
                return false;

            var nodes = sort[guess];

            found = (PathNode<TPos>)nodes[node];
            if (found != null)
                return true;

            return false;
        }

        private void AddToSorted(SortedDictionary<float, OrderedDictionary> sort, Dictionary<PathNode<TPos>, float> guessmap, PathNode<TPos> node, float guess)
        {
            OrderedDictionary set;
            if (sort.TryGetValue(guess, out set))
            {
                set = sort[guess];
            }
            else
            {
                set = new OrderedDictionary();
                sort.Add(guess, set);
            }
            set.Add(node, node);
            guessmap.Add(node, guess);
        }

        private void RemoveFromSorted(SortedDictionary<float, OrderedDictionary> sort, Dictionary<PathNode<TPos>, float> guessmap, PathNode<TPos> node)
        {
            float guess;

            if (!guessmap.TryGetValue(node, out guess))
                return;
            OrderedDictionary set;
            if (sort.TryGetValue(guess, out set))
            {
                set = sort[guess];
            }

            set.Remove(node);
            guessmap.Remove(node);
        }

        private void CleanList(SortedDictionary<float, OrderedDictionary> sort)
        {
            if(sort.Count == 0)
                return;
            var first = sort.First();
            while (first.Value.Count == 0)
            {
                sort.Remove(first.Key);
                if (sort.Count == 0)
                    return;
                first = sort.First();
            }
        }

        private PathNode<TPos> PopFromSorted(SortedDictionary<float, OrderedDictionary> sort, Dictionary<PathNode<TPos>, float> guessmap)
        {
            var first = sort.First();

            var firstNode = (PathNode<TPos>)first.Value[0];
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
