using System.Collections.Generic;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management.Events;

namespace XmasEngineModel.Management.Actions
{
	public class GetAllPerceptsAction : EntityXmasAction<Agent>
	{
		#region implemented abstract members of GameAction

		protected override void Execute()
		{
			Source.Raise(new RetreivePerceptsEvent(new PerceptCollection(new List<Percept>(Source.Percepts))));
			Complete();
		}

		#endregion
	}
}