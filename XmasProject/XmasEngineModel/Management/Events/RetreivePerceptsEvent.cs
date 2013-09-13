namespace XmasEngineModel.Management.Events
{
	public class RetreivePerceptsEvent : XmasEvent
	{
		private PerceptCollection percepts;

		public RetreivePerceptsEvent(PerceptCollection percepts)
		{
			this.percepts = percepts;
		}

		public PerceptCollection Percepts
		{
			get { return percepts; }
		}
	}
}