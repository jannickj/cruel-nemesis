namespace XmasEngineModel.World
{
    /// <summary>
    /// This class is meant to be extended by implementations of other positions of other worlds
    /// </summary>
	public abstract class XmasPosition
	{
        /// <summary>
        /// Generates a spawn from a position
        /// </summary>
        /// <returns>the spawn information</returns>
        public abstract EntitySpawnInformation GenerateSpawn();
    }
}