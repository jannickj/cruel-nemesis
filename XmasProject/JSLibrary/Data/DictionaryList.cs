using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JSLibrary.Data
{
	public class DictionaryList<TKey, TValue>
	{
		private Dictionary<TKey, HashSet<TValue>> dic = new Dictionary<TKey, HashSet<TValue>>();

		public ICollection<TValue> this[TKey key]
		{
			get
			{
				lock (dic)
				{
					HashSet<TValue> vals;
					if (dic.TryGetValue(key, out vals))
						return vals.ToArray();

					return new TValue[0];
				}
			}
		}

		public ICollection<TValue> Get(TKey key)
		{
			lock (dic)
			{
				return this[key].ToArray();
			}
		}

		public bool Add(TKey key, TValue val)
		{
			lock (dic)
			{
				HashSet<TValue> vals;
				if (!dic.TryGetValue(key, out vals))
				{
					vals = new HashSet<TValue>();
					dic[key] = vals;
				}
				return vals.Add(val);
			}
		}

		public bool Remove(TKey key, TValue val)
		{
			lock (dic)
			{
				HashSet<TValue> vals;
				if (dic.TryGetValue(key, out vals))
				{
					if (vals.Remove(val))
					{
						if (vals.Count == 0)
							dic.Remove(key);
						return true;
					}
				}
				return false;
			}
		}

		public ICollection<TKey> Keys
		{
			get { return this.dic.Keys.ToArray(); }
		}






		public bool TryGetValues(TKey key, out ICollection<TValue> values)
		{
			HashSet<TValue> vals;
			if (this.dic.TryGetValue(key, out vals))
			{
				values = vals.ToArray();
				return true;
			}
			values = null;
			return false;
		}
	}
}