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


        public Path(TMap map, IEnumerable<TPos> path)
        {
            this.path = path.ToArray();
            this.map = map;
        }

        public Queue<TPos> Road
        {
            get
            {
                return new Queue<TPos>(path);
            }
        }


        public TMap Map
        {
            get { return map; }
        }

	}
}
