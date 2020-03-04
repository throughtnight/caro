/*
 * Author: Hoang Ngoc Thinh
 * Created: 03/03/2020
 * Description: algotihm
 */
using System;

namespace ProjectCaro
{
	/// <summary>
	/// Description of algorithm.
	/// </summary>
	public class coors
	{
		public int x;
		public int y;
		public coors()
		{
			this.x = 0;
			this.y = 0;
		}
		public coors(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
	public class algorithm
	{
		private readonly byte[,] 	arr;
		private readonly int 		size;
		private readonly coors 		point;
		private readonly byte 		playerSymbol;
		
		private bool checkDiagonalDown(int minX, int maxX, int minY, int maxY)
		{
			byte count = 0;
			while (minX <= maxX && minY <= maxY) {
				if (arr[minY++, minX++] == this.playerSymbol) {
					count++;
					if (count == 5) {
						return true;
					}
				} else {
					count = 0;
				}
			}
			return false;
		}
		private bool checkDiagonalUp(int minX, int maxX, int minY, int maxY)
		{
			byte count = 0;
			while (minX <= maxX && minY <= maxY) {
				if (arr[minY++, maxX--] == this.playerSymbol) {
					count++;
					if (count == 5) {
						return true;
					}
				} else {
					count = 0;
				}
			}
			return false;
		}
		private bool checkHorizontal(int minX, int maxX, int y)
		{
			byte count = 0;
			while (minX <= maxX) {
				if (arr[y, minX++] == this.playerSymbol) {
					count++;
					if (count == 5) {
						return true;
					}
				} else {
					count = 0;
				}
			}
			return false;
		}
		private bool checkVertical(int x, int minY, int maxY)
		{
			byte count = 0;
			while (minY <= maxY) {
				if (arr[minY++, x] == this.playerSymbol) {
					count++;
					if (count == 5) {
						return true;
					}
				} else {
					count = 0;
				}
			}
			return false;
		}
		public bool checkWin()
		{
			int minX = this.point.x - 4 < 0 ? 0 : this.point.x - 4;
			int minY = this.point.y - 4 < 0 ? 0 : this.point.y - 4;
			int maxX = this.point.x + 4 > this.size ? this.size : this.point.x + 4;
			int maxY = this.point.y + 4 > this.size ? this.size : this.point.y + 4;
			
			return this.checkDiagonalDown(minX, maxX, minY, maxY)
				|| this.checkDiagonalUp(minX, maxX, minY, maxY)
				|| this.checkHorizontal(minX, maxX, this.point.y)
				|| this.checkVertical(this.point.x, minY, maxY);
		}
		public algorithm(byte[,] arr, coors point)
		{
			this.size = Convert.ToInt16(Math.Sqrt(arr.Length)) - 1;
			this.arr = arr;
			this.point = point;
			this.playerSymbol = this.arr[this.point.y, this.point.x];
		}
	}
}
