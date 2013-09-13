using System.Collections.Generic;
using System.Linq;

namespace XmasEngineModel.EntityLib
{
	///<summary>
	///     An agent meant to be added to a XmasWorld and controlled by an AI
	///</summary>
	public class Agent : XmasEntity
	{
        private string name;

		///<summary>
		///     Initializes a new instance of the Agent
		///</summary>
		/// <param name="name"> The name of the agent, notice: this name should be unique</param>
        public Agent(string name)
        {
            this.name = name;
			
        }

		///<summary>
		///     Gets the name unique to the agent, only the agent's inheritors is able to set its name.
		///</summary>
		public string Name
		{
			get { return name; }
			protected set { name = value; }
		}

		///<summary>
		///     Gets the percepts of the agent by getting the percepts of each of its modules.
		///</summary>
		public IEnumerable<Percept> Percepts
		{
			get 
			{ 
				return moduleMap.Values.SelectMany(m => m.Percepts).ToArray();
			}
		}

		public override string ToString ()
		{
			string basestr = string.Format("{0} '{1}' [{2}]", GetType().Name, name, Id);
			try
			{
				return string.Format("{0} at {1}", basestr, Position);
			}
			catch
			{
				return basestr;
			}
		}
	}
}