using System.Collections.Generic;

namespace XmasEngineModel
{

    /// <summary>
    /// A collection of percepts
    /// </summary>
	public class PerceptCollection : XmasObject
	{
		private ICollection<Percept> percepts;

        /// <summary>
        /// Instantiates a collection of percepts
        /// </summary>
        /// <param name="percepts">A collection of percepts in form of an ICollection</param>
		public PerceptCollection(ICollection<Percept> percepts)
		{
			this.percepts = percepts;
		}

        /// <summary>
        /// returns the percept in an ICollection
        /// </summary>
		public ICollection<Percept> Percepts
		{
			get { return percepts; }
		}
	}
}