using System.Collections.Generic;
using System.Linq;

namespace XmasEngineModel.Rule
{
    
    /// <summary>
    /// The RuleHierarchy is a list of sets of rules, each set of rules is at a certain priority
    /// </summary>
    /// <typeparam name="TPriority">The type the priority is stored as</typeparam>
    /// <typeparam name="TObject">The type of object the rules are deciding over</typeparam>
	public class RuleHierarchy<TPriority, TObject>
	{
		private LinkedList<KeyValuePair<TPriority, TransformationRule<TObject>>> hiarch =
			new LinkedList<KeyValuePair<TPriority, TransformationRule<TObject>>>();

		private Dictionary<TPriority, TransformationRule<TObject>> lookup = new Dictionary<TPriority, TransformationRule<TObject>>();

        /// <summary>
        /// Adds a new priority layer this priority take precedence over all prior layers of priority
        /// </summary>
        /// <param name="priority">The priority object linking this layer to that priority object</param>
        /// <param name="ruleSet">The rule set for the new layer</param>
		public void AddLayer(TPriority priority, TransformationRule<TObject> ruleSet)
		{
			hiarch.AddFirst(new KeyValuePair<TPriority, TransformationRule<TObject>>(priority, ruleSet));
			lookup.Add(priority, ruleSet);
		}

        /// <summary>
        /// Gets the rule set linked to a certain priorirt object
        /// </summary>
        /// <param name="p">The priority object</param>
        /// <returns>The rule set</returns>
		public TransformationRule<TObject> GetRule(TPriority p)
		{
			return lookup[p];
		}

        /// <summary>
        /// Attempts to find a rule set through a priority object
        /// </summary>
        /// <param name="p">The priority object</param>
        /// <param name="rule">The ruleset that is found</param>
        /// <returns>Whether or not the attempt was succesful</returns>
		public bool TryGetRule(TPriority p, out TransformationRule<TObject> rule)
		{
			return lookup.TryGetValue(p, out rule);
		}

        /// <summary>
        /// This causes the RuleHierarchy to go through all its layer from highest priority to lowest, and see if a layer can come to a conclusion.
        /// Once a layer has reached a conclusion then no other priority layers are asked. 
        /// If no layers can come to a conclusion then a DontCareConclusion is given.
        /// </summary>
        /// <param name="t">The object to conclude over</param>
        /// <returns>The conclusion</returns>
		public Conclusion Conclude(TObject t)
		{
			Conclusion c = null;

			object o = hiarch.FirstOrDefault(kp =>
				{
					c = kp.Value.Conclude(t);
					return !(c is DontCareConclusion);
				});
			if (o == null)
				return new DontCareConclusion();
			return c;
		}
	}
}