using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleXmasImplementation.View
{
	public struct DrawSceen
	{
		private char[] screen;
		private int width;
		private int height;
		private int truewidth;

		public DrawSceen(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.truewidth = width + 1;

			this.screen = new char[truewidth*height];

			
		}

		public char this[int x, int y]
		{
			set { screen[x + (height - 1 - y)*truewidth] = value; }
		}

		public char[] GenerateScreen()
		{
			for (int i = 0; i < height; i++)
			{
				this[width, i] = '\n';
			}
			return screen;
		}
	}
}
