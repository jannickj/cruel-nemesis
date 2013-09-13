using System;
using System.Collections.Generic;
using System.Linq;
using XmasEngineModel.Rule.Exceptions;

namespace XmasEngineModel.Rule
{

    /// <summary>
    /// A set of rules that can come to one or many conclusions
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class TransformationRule<T>
	{
		private Dictionary<Conclusion, LinkedList<Predicate<T>>> premises =
			new Dictionary<Conclusion, LinkedList<Predicate<T>>>();

        /// <summary>
        /// Links a rule to a conclusion
        /// </summary>
        /// <param name="p">The rule in form of a predicate</param>
        /// <param name="c">The conclusion that is reached if the predicate is true</param>
		public void AddPremise(Predicate<T> p, Conclusion c)
		{
			LinkedList<Predicate<T>> list;
			if (!premises.TryGetValue(c, out list))
			{
				list = new LinkedList<Predicate<T>>();
				premises.Add(c, list);
			}

			list.AddFirst(p);
		}

        /// <summary>
        /// Forces the ruleset to come up with a single conclusion. If multiple conclusions are reached a MultiConclusionException is thrown.
        /// </summary>
        /// <exception cref="MultiConclusionException"></exception>
        /// <param name="t">The object that it concludes over</param>
        /// <returns>the reached conclusion</returns>
		public Conclusion Conclude(T t)
		{
			ICollection<Conclusion> cons = MultiConcluding(t);
			if (cons.Count > 1)
				throw new MultiConclusionException(cons.ToArray());
			if (cons.Count == 0)
				return new DontCareConclusion();
			return cons.First();
		}

        /// <summary>
        /// Makes the ruleset come up with all conclusions it can on a certain object.
        /// </summary>
        /// <param name="t">The object it is meant to conclude on</param>
        /// <returns>The conclusions reached</returns>
		public ICollection<Conclusion> MultiConcluding(T t)
		{
			return premises.Where(kp => kp.Value.Any(p => p(t))).Select(kp => kp.Key).ToArray();
		}
	}
}