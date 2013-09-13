namespace XmasEngineModel.Rule
{

    //The most basic form of a conclusion with only a single object tagged to it
	public class Conclusion
	{
		private object tag;

        /// <summary>
        /// Instanstiates a conclusion object with no tag
        /// </summary>
		public Conclusion()
		{
		}

        /// <summary>
        /// Instanstiates a conclusion object with a tag
        /// </summary>
        /// <param name="tag">The object tagged to the conclusion</param>
		public Conclusion(object tag)
		{
			this.tag = tag;
		}

        /// <summary>
        /// Gets or Sets the object tagged to the conclusion
        /// </summary>
		public object Tag
		{
			get { return tag; }
			set { tag = value; }
		}

		public override string ToString()
		{
			return tag.ToString();
		}
	}
}