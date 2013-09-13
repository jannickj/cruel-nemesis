namespace XmasEngineModel.Percepts
{
	public class EmptyNamedPercept : Percept
	{
		private string name;
		
		public EmptyNamedPercept(string name)
		{
			this.name = name;
		}
		
		public string Name
		{
			get { return name; }
		}
	}
}
