namespace JSLibrary.Data
{
	public struct Size
	{
		private int height;
		private int width;

		public Size(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		public int Height
		{
			get { return height; }
		}

		public int Width
		{
			get { return width; }
		}
	}
}