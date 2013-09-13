namespace XmasEngineModel.Percepts
{
	public class SingleNumeralPercept : Percept
	{
		private string name;
		private double value;

		public SingleNumeralPercept(string name, double value)
		{
			this.name = name;
			this.value = value;
		}

		public string Name
		{
			get { return name; }
		}

		public double Value
		{
			get { return value; }
		}
	}
}