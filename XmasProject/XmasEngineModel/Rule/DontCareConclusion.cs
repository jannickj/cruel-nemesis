namespace XmasEngineModel.Rule
{
    /// <summary>
    /// A conclusion for something not mattering
    /// </summary>
	public class DontCareConclusion : Conclusion
	{
		public DontCareConclusion()
			: base("dont care")
		{
		}
	}
}