using System;
using XmasEngineModel.Rule;

namespace XmasEngineModel.EntityLib.Module
{

	/// <summary>
	/// An abstract module for adding heirachel rules to an entity
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public abstract class RuleBasedModule<TEntity> : UniversalModule where TEntity : XmasEntity
	{
		private RuleHierarchy<Type,TEntity> ruleHierarchy = new RuleHierarchy<Type, TEntity>();


		/// <summary>
		/// Adds a rule to a certain layer in the rule hierachy, with the rule is added a conclusion that is reached if the rule is ever true
		/// </summary>
		/// <param name="toLayer">The layer in the rule hierachy that the rule is added to</param>
		/// <param name="rule">The rule that is added to the certain layer</param>
		/// <param name="conclusion">The conclusion that is reached if its rule is ever true</param>
		protected void AddRule(Type toLayer, Predicate<TEntity> rule, Conclusion conclusion)
		{
			TransformationRule<TEntity> tr;

			if (ruleHierarchy.TryGetRule(toLayer, out tr))
			{
				tr.AddPremise(rule, conclusion);
			}
		}

		/// <summary>
		/// Adds a new layer to the Rule hierachy, the new layer supercedes all prioer layers
		/// </summary>
		/// <param name="layer">The layer inform of a type</param>
		protected void PushRuleLayer(Type layer)
		{
			ruleHierarchy.AddLayer(layer, new TransformationRule<TEntity>());
		}

		protected Conclusion Conclude(TEntity ent)
		{
			return this.ruleHierarchy.Conclude(ent);
		}
	}
}