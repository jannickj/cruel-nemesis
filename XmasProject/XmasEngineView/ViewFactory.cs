using System;
using System.Collections.Generic;
using XmasEngineModel.EntityLib;
using XmasEngineModel.World;

namespace XmasEngineView
{
	public abstract class ViewFactory
	{
		protected Dictionary<Type, Type> typeDict = new Dictionary<Type, Type>();

        /// <summary>
        /// Links a specific EntityView Type to a specific XmasEntity Type.
        /// </summary>
        /// <typeparam name="TModel">The entity type</typeparam>
        /// <typeparam name="TView">The view type</typeparam>
		public void AddTypeLink<TModel, TView>()
			where TModel : XmasEntity
			where TView : EntityView
		{
			typeDict.Add(typeof (TModel), typeof (TView));
		}

        /// <summary>
        /// Constructs a new entity view meant to visualize an actual entity
        /// </summary>
        /// <param name="model">the entity the view is visualizing</param>
        /// <param name="position">The position of the entity</param>
        /// <returns>The view of the entity</returns>
        public abstract EntityView ConstructEntityView(XmasEntity model, XmasPosition position);
	}
}