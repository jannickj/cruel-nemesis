using XmasEngineModel.Management;
using JSLibrary.Data;

namespace XmasEngineExtensions.TileExtension.Events
{
	public class UnitMovePreEvent : XmasEvent
	{
		public bool IsStopped { get; set; }

		public Point NewPos { get; private set; }

		public UnitMovePreEvent(Point newpos)
		{
			NewPos = newpos;
		}
	}
}