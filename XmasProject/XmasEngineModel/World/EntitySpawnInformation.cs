namespace XmasEngineModel.World
{

    /// <summary>
    /// This class is meant to be for implementations of other worlds.
    /// </summary>
	public abstract class EntitySpawnInformation
	{
		private XmasPosition pos;
		
        /// <summary>
        /// Constructor for making an EntitySpawnInformation
        /// </summary>
        /// <param name="pos">The position of the spawn</param>
		public EntitySpawnInformation(XmasPosition pos)
		{
			this.pos = pos;
		}
		
        /// <summary>
        /// Gets where the entity is meant to spawn on
        /// </summary>
		public XmasPosition Position
		{
			get { return pos; }
		}
	}
}
