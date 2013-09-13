using ConsoleXmasImplementation.Model.Modules;
using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib.Module;

namespace ConsoleXmasImplementation.Model.Entities
{
	public class GrabberAgent : ConsoleAgent
	{
		public GrabberAgent (string name) : base(name)
		{
			this.RegisterModule(new VisionModule());
			this.RegisterModule(new VisionRangeModule(5));
			this.RegisterModule(new PositionModule());
			this.RegisterModule(new PackageGrabbingModule());
		}

		protected override SpeedModule ConstructSpeedModule ()
		{
			return new SpeedModule (300);
		}
	}
}

