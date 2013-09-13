using XmasEngineModel;

namespace XmasEngineView
{
    /// <summary>
    /// The basic world view class
    /// </summary>
	public class XmasWorldView
	{
		private XmasWorld model;

        /// <summary>
        /// Instatiates a XmasWorldView object
        /// </summary>
        /// <param name="world">The world the view is meant to mirror</param>
		public XmasWorldView(XmasWorld world)
		{
			model = world;
		}

        /// <summary>
        /// The world of the model
        /// </summary>
		public XmasWorld Model
		{
			get { return model; }
		}
	}
}