using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 迷路
{
    public partial class Form1 : Form
    {
        Bitmap bitmap, bitmap2;
        Rectangle[] srcRect, srcRect2;
        int[,] mapData;
        int dir;
        Point point;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap("image4.png");
            srcRect = new Rectangle[16];
            dir = 1;
            Random random = new Random();
            mapData = new int[10, 10];
            int[] pattern = { 0, 0, 8 };

            #region 画像データ
            for (int i = 0; i < srcRect.Length; i++)
            {
                srcRect[i] = new Rectangle((i % 4) * 32, (i / 4) * 32, 32, 32);
            }

            bitmap2 = new Bitmap("image5.png");
            srcRect2 = new Rectangle[12];

            for (int i = 0; i < srcRect2.Length; i++)
            {
                srcRect2[i] = new Rectangle((i % 3) * 32, (i / 3) * 32, 32, 32);
            }
            #endregion

            #region マップデータ生成
            for (int y = 0; y < mapData.GetLength(1); y++)
            {
                for (int x = 0; x < mapData.GetLength(0); x++)
                {
                    int r = random.Next(pattern.GetLength(0));
                    mapData[x, y] = pattern[r];
                }
            }
            int goalX = random.Next(mapData.GetLength(0));
            int goalY = random.Next(mapData.GetLength(1));
            mapData[goalX,goalY] = 12;

            point = new Point(random.Next(mapData.GetLength(0)), random.Next(mapData.GetLength(1)));

            #endregion

            ClientSize = new Size(mapData.GetLength(0) * 32, mapData.GetLength(1) * 32);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (int y = 0; y < mapData.GetLength(1); y++)
            {
                for (int x = 0; x < mapData.GetLength(0); x++)
                {
                    e.Graphics.DrawImage(bitmap, new Rectangle(x * 32, y * 32, 32, 32), srcRect[mapData[x,y]], GraphicsUnit.Pixel);
                }
            }

            e.Graphics.DrawImage(bitmap2,new Rectangle( point.X * 32, point.Y * 32, 32, 32),srcRect2[dir],GraphicsUnit.Pixel);

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    if(point.Y>0&&mapData[point.X,point.Y-1]!=8)
                    {
                        point.Y--;
                    }
                    dir = 10;
                    break;

                case Keys.Right:
                    if(point.X<mapData.GetLength(0)-1&& mapData[point.X+1, point.Y] != 8)
                    {
                        point.X++;
                    }
                    dir = 7;
                    break;

                case Keys.Down:
                    if(point.Y< mapData.GetLength(1) - 1&& mapData[point.X, point.Y + 1] != 8)
                    {
                        point.Y++;
                    }
                    dir = 1;
                    break;

                case Keys.Left:
                    if(point.X>0&& mapData[point.X-1, point.Y] != 8)
                    {
                        point.X--;
                    }
                    dir = 4;
                    break;
            }
            Invalidate();

            if(mapData[point.X, point.Y] ==12)
            {
                MessageBox.Show("ゴール");
                Application.Exit();
            }

        }

    }
        
}
