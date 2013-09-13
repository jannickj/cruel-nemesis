using XmasEngineModel;
using XmasEngineModel.Interfaces;
using XmasEngineModel.Management;

namespace XmasEngineView
{

    /// <summary>
    /// A basic view for the engine
    /// </summary>
	public abstract class XmasView : XmasActor, IStartable
	{
		private ThreadSafeEventManager evtmanager;

        /// <summary>
        /// Gets the ThreadSafe EventManager linked to this view
        /// </summary>
		public ThreadSafeEventManager ThreadSafeEventManager
		{
			get { return evtmanager; }
		}
		
        /// <summary>
        /// constructs a XmasView
        /// </summary>
        /// <param name="evtmanager">The ThreadSafe EventManager meant to be used by this view</param>
		public XmasView(ThreadSafeEventManager evtmanager)
		{
			this.evtmanager = evtmanager;
		}

        /// <summary>
        /// Called before the engine is started
        /// </summary>
		public virtual void Initialize()
		{
		}

        /// <summary>
        /// Called when the XmasView is given its very own thread.
        /// </summary>
		public abstract void Start();
	}
}