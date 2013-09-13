using XmasEngineModel.EntityLib;
using XmasEngineModel.World;

namespace XmasEngineModel.Management.Events
{
	public class EntityAddedEvent : XmasEvent
	{
		private XmasEntity addedXmasEntity;
        private XmasPosition addedPosition;

        

		public EntityAddedEvent(XmasEntity addedXmasEntity, XmasPosition position)
		{
			this.addedXmasEntity = addedXmasEntity;
            this.addedPosition = position;
		}


        public XmasPosition AddedPosition
        {
            get { return addedPosition; }
        }

		public XmasEntity AddedXmasEntity
		{
			get { return addedXmasEntity; }
		}
	}
}