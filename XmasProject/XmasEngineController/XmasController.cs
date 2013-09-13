using System.Collections.Generic;
using System.Threading;
using XmasEngineController.AI;
using XmasEngineModel;
using XmasEngineView;

namespace XmasEngineController
{
    /// <summary>
    /// A very basic form of a controller
    /// </summary>
	public abstract class XmasController : XmasActor
	{

		/// <summary>
		/// Called before the engine is fully started
		/// </summary>
		public virtual void Initialize()
		{

		}

        /// <summary>
        /// Called when the controller is given its own thread
        /// </summary>
		public virtual void Start()
		{
			
		}

        /// <summary>
        /// The name the controller wishes to name its thread
        /// </summary>
        /// <returns>name of the thread</returns>
		public virtual string ThreadName()
		{
			return "Controller Thread";
		}
	}
}