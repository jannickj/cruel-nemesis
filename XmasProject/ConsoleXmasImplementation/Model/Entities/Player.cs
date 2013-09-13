using XmasEngineExtensions.TileExtension.Modules;

namespace ConsoleXmasImplementation.Model.Entities
{
	public class Player : ConsoleAgent
	{
		public Player() : base("player")
		{
			this.RegisterModule(new VisionModule());
			this.RegisterModule(new VisionRangeModule(5));
			
		}

		protected override SpeedModule ConstructSpeedModule()
		{
			return new SpeedModule(100);
		}
	}
}