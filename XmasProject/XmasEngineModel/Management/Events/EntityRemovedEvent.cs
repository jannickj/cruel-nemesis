using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management.Events
{
	public class EntityRemovedEvent : XmasEvent
	{
		public XmasEntity RemovedXmasEntity { get; private set; }

		public EntityRemovedEvent (XmasEntity removedXmasEntity)
		{ 
			RemovedXmasEntity = removedXmasEntity;
		}
	}
}

