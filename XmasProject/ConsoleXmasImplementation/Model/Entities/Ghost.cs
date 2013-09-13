using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib.Module;

namespace ConsoleXmasImplementation.Model.Entities
{
	public class Ghost : ConsoleAgent
	{
		public Ghost(string name) : base(name)
		{
			this.RegisterModule(new VisionModule());
			this.RegisterModule(new VisionRangeModule(5));
			this.RegisterModule(new PositionModule());
		}

		protected override SpeedModule ConstructSpeedModule()
		{
			return new SpeedModule(200);
		}
	}
}
