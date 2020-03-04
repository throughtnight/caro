/*
 * Author: Hoang Ngoc Thinh
 * Created: 03/03/2020
 * Description: parameter
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectCaro
{
	/// <summary>
	/// Description of param.
	/// </summary>
	public static class param
	{
		private const string link = "\\..\\..\\src\\img\\";
		private const string imgSelected = "style0.png";
		private const string imgPlayer1 = "style1.png";
		private const string imgPlayer2 = "style2.png";
		public static class window
		{
			public static int width = 1280;
			public static int height = 720;
		}
		public static int size = 20; // margin horizontial and vertical line
		public static class image
		{
			public static Image selected = new Bitmap(Application.StartupPath + link + imgSelected);
			public static Image player1 = new Bitmap(Application.StartupPath + link + imgPlayer1);
			public static Image player2 = new Bitmap(Application.StartupPath + link + imgPlayer2);
		}
	}
}
