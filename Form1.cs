using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace BadApple
{
    public partial class Form1 : Form
    {
        private Bitmap Holland = new Bitmap("Holland.bmp");
        private Bitmap Spain = new Bitmap("Spain.bmp");
        /**
         * 目标图片从BadApple.flv0000.bmp 到 BadApple.flv6569.bmp
         **/
        private int imageNumber = 0000;
        private StringBuilder imagePath = null;
        /**
         * 用于载入原来的位图
         * */
        private Bitmap theImage;
        /**
         * 处理之后的位图
         * */
        private Bitmap dealedImage;
        /**
         * 用于定时调用
         * */
        private System.Timers.Timer freshTimer = null;
        public Form1()
        {
            InitializeComponent();
            //开启双缓冲
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.UserPaint |
                    ControlStyles.AllPaintingInWmPaint, true);
            imagePath = new StringBuilder("");
            theImage = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            dealedImage = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            freshTimer = new System.Timers.Timer() { Interval=40 };//1000 / 24 约等于40
            freshTimer.Elapsed +=new ElapsedEventHandler(freshTimer_Elapsed);
            freshTimer.Start();
        }
        private void nextImage() 
        {
            imageNumber++;
            imagePath.Clear();
            imagePath.Append("BadApple.flv");
            imagePath.Append(imageNumber.ToString("D4") + ".bmp");
            theImage = new Bitmap(imagePath.ToString());
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics displayGraphics = e.Graphics;
            Graphics g = Graphics.FromImage(dealedImage);
            dealImage(g);
            displayGraphics.DrawImage(dealedImage, ClientRectangle);
        }
        private void freshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            nextImage();
            //dealImage();
            if (imageNumber > 6560) freshTimer.Stop();
            try { 
                this.Invalidate();
                this.Update(); 
            }catch (Exception ex)
            {
                ex.GetType();
            }
        }
        private void dealImage(Graphics gtmp)
        {
            //SolidBrush china = new SolidBrush
            try
            {
                int x = 0, y = 0;
                Color col = Color.White;
                //Graphics gtmp = null;
                //gtmp = Graphics.FromImage(this.dealedImage);
                for (x = 1; x <= 512; x += 8)
                {
                    for (y = 1; y <= 384; y += 6)
                    {
                        col = theImage.GetPixel(x, y);
                        if (col == Color.FromArgb(255, 253, 253, 253))
                        {
                            gtmp.DrawImage(Spain, new Rectangle(x, y, 7, 5));
                        }
                        else
                        {
                            gtmp.DrawImage(Holland, new Rectangle(x, y, 7, 5));
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                exc.GetType();
            }
        }
    }
}
