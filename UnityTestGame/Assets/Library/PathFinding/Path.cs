using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Library.PathFinding
{
	public struct Path<TMap, TPos>
	{
        private TPos[] path;
        private TMap map;


        public Path(TMap map)
        {
            this.path = new TPos[0];
            this.map = map;
        }

        public Path(TMap map, IEnumerable<TPos> path)
        {
            this.path = path.ToArray();
            this.map = map;
        }

        public Path(Path<TMap, TPos> startpath, params Path<TMap, TPos>[] paths)
        {
            map = startpath.map;
            path = startpath.path.Concat(paths.SelectMany(pa => pa.path)).ToArray();
        }

        public Path(IEnumerable<Path<TMap, TPos>> paths) : this(paths.First(),paths.Skip(1).ToArray())
        {

        }

        public LinkedList<TPos> Road
        {
            get
            {
                return new LinkedList<TPos>(path);
            }
        }


        public TMap Map
        {
            get { return map; }
        }

	}
}
