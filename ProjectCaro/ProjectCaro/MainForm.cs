/*
 * Author: Hoang Ngoc Thinh
 * Created: 03/03/2020
 * Description: screen play game
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectCaro
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private byte[,] arrCaro;
		private bool 	player1; // check turn of player1 or player 2
		private coors 	oldPoint;
		private coors 	nowPoint;
		
		private int 	numOfLine;
		private int 	sizeCaro;
		private int 	marginCaro;
		private int 	paddingLeftRight;
		private int 	paddingTopBottom;
		
		public MainForm()
		{
			InitializeComponent();
			
			this.setDefaultValue();
			this.initializeCustomComponent();
		}
		private void setDefaultValue()
		{
			this.Width = param.window.width;
			this.Height = param.window.height;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			
			this.oldPoint = new coors();
			
			bool heightIsBigger = this.ClientSize.Height > this.ClientSize.Width;
			this.sizeCaro = heightIsBigger ? this.ClientSize.Width : this.ClientSize.Height; // without margin
			this.numOfLine = sizeCaro / param.size;
			this.marginCaro = (sizeCaro - (param.size * numOfLine)) / 2;
			this.sizeCaro = param.size * numOfLine++;
			this.paddingLeftRight = heightIsBigger ? 0 : (this.ClientSize.Width - this.ClientSize.Height) / 2;
			this.paddingTopBottom = !heightIsBigger ? 0 : (this.ClientSize.Height - this.ClientSize.Width) / 2;
		
			this.arrCaro = new byte[this.numOfLine - 2, this.numOfLine - 2];
			this.player1 = true;
		}
		private void initializeCustomComponent() 
		{
			int locationX = paddingLeftRight + marginCaro;
			int locationY = paddingTopBottom + marginCaro;
			
			this.drawPlayer();
			this.drawCaro();
			this.initializeImplicitNetCaro(numOfLine - 2, locationX + param.size / 2, locationY + param.size / 2);
		}
		private void drawPlayer()
		{
			int pictureBoxSize = Convert.ToInt16(this.paddingLeftRight != 0 ? (this.paddingLeftRight * 0.8) : (this.paddingTopBottom * 0.8));
			int x;
			int y;
			
			PictureBox pbPlayer1 = new PictureBox();
			pbPlayer1.Name = "player1";
			pbPlayer1.Size = new Size(pictureBoxSize, pictureBoxSize);
			pbPlayer1.BackgroundImage = param.image.player1;
			pbPlayer1.BackgroundImageLayout = ImageLayout.Stretch;
			x = (this.paddingLeftRight != 0 ? (this.paddingLeftRight - pictureBoxSize) : (this.ClientSize.Width - pictureBoxSize)) / 2;
			y = (this.paddingTopBottom != 0 ? (this.paddingTopBottom - pictureBoxSize) : (this.ClientSize.Height - pictureBoxSize)) / 2;
			pbPlayer1.Location = new Point(x, y);
			pbPlayer1.Visible = this.player1;
			this.Controls.Add(pbPlayer1);
			
			PictureBox pbPlayer2 = new PictureBox();
			pbPlayer2.Name = "player2";
			pbPlayer2.Size = new Size(pictureBoxSize, pictureBoxSize);
			pbPlayer2.BackgroundImage = param.image.player2;
			pbPlayer2.BackgroundImageLayout = ImageLayout.Stretch;
			x = this.paddingLeftRight != 0 ? (this.paddingLeftRight - pictureBoxSize) / 2 + this.paddingLeftRight + this.sizeCaro : (this.ClientSize.Width - pictureBoxSize) / 2;
			y = this.paddingTopBottom != 0 ? (this.paddingTopBottom - pictureBoxSize) / 2 + this.paddingTopBottom + this.sizeCaro : (this.ClientSize.Height - pictureBoxSize) / 2;
			pbPlayer2.Location = new Point(x, y);
			pbPlayer2.Visible = !this.player1;
			this.Controls.Add(pbPlayer2);
		}
		private void drawCaro()
		{
			int locationX = paddingLeftRight + marginCaro;
			int locationY = paddingTopBottom + marginCaro;

			this.drawLine(1, sizeCaro, locationX, locationY);
			this.drawLine(sizeCaro, 1, locationX, locationY);
		}
		private void drawLine(int horizontal, int vertical, int x, int y) 
		{
			for (int i = 0; i < numOfLine; i++) {
				Label lb = new Label();
				lb.BackColor = Color.Gray;
				lb.Location = new Point(x, y);
				lb.Size = new Size(horizontal, vertical);
				
				this.Controls.Add(lb);
				
				x += horizontal == 1 ? param.size : 0;
				y += vertical == 1 ? param.size : 0;
			}
		}
		private void initializeImplicitNetCaro(int numOfNet, int x, int y)
		{
			int xRoot = x;
			for (byte row = 0; row < numOfNet; row++) {
				for (byte col = 0; col < numOfNet; col++) {
					PictureBox pb = new PictureBox();
					pb.Name = "pb" + string.Format("{0:00}", row) + string.Format("{0:00}", col);
					pb.Location = new Point(x, y);
					pb.Size = new Size(param.size, param.size);
					pb.BackgroundImageLayout = ImageLayout.Stretch;
					
					pb.Click += pb_Click;
					pb.DoubleClick += pb_DoubleClick;
					
					this.Controls.Add(pb);
					
					x += param.size;
				}
				x = xRoot;
				y += param.size;
			}
		}
		void pb_Click(object sender, EventArgs e)
		{
			PictureBox pb = sender as PictureBox;
			int row = Convert.ToInt16(pb.Name.Substring(2, 2));
			int col = Convert.ToInt16(pb.Name.Substring(4, 2));
			if (this.arrCaro[row, col] != 0) {
				return;
			}
			if (this.arrCaro[this.oldPoint.y, this.oldPoint.x] == 0) {
				this.Controls["pb" + string.Format("{0:00}", this.oldPoint.y) + string.Format("{0:00}", this.oldPoint.x)].BackgroundImage = null;
			}
			pb.BackgroundImage = param.image.selected;
			this.oldPoint.y = row;
			this.oldPoint.x = col;
		}
		void pb_DoubleClick(object sender, EventArgs e)
		{
			PictureBox pb = sender as PictureBox;
			int row = Convert.ToInt16(pb.Name.Substring(2, 2));
			int col = Convert.ToInt16(pb.Name.Substring(4, 2));
			if (this.arrCaro[row, col] != 0) {
				return;
			}
			pb.BackgroundImage = this.player1 ? param.image.player1 : param.image.player2;
			this.arrCaro[row, col] = Convert.ToByte(this.player1 ? 1 : 2);
			
			this.nowPoint = new coors(col, row);			
			bool checkWin = new algorithm(this.arrCaro, this.nowPoint).checkWin();
			if (!checkWin) {
				this.player1 = !this.player1;
				this.Controls["player1"].Visible = this.player1;
				this.Controls["player2"].Visible = !this.player1;
				return;
			}
			MessageBox.Show("Player " + (this.player1 ? "Red" : "Blue") + " is winner");
			this.Close();
		}
	}
}
