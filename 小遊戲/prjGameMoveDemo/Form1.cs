using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGameMoveDemo
{
    public partial class Form1 : Form
    {

        private int interval = 0;
        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private bool isManTouched(int x,int y)
        {
            return (x <= (picMain.Left + picMain.Width) && x >= picMain.Left &&
                y >= picMain.Top && y <= (picMain.Top + picMain.Height));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int speed = 10;
            if (e.Control)
                speed *= 10;

            
            if (e.KeyValue == 39)
            {
                picMain.Left += speed;
                picMain.Image = picRight.Image;
            }
            else if (e.KeyValue == 37)
            {
                picMain.Left -= speed;
                picMain.Image = picLeft.Image;
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            interval++;
            if (interval % 5 == 0)
                newBomb();
            else if (interval % 2 == 0)
            {
                foreach (Control t in this.Controls)
                {
                    if (t is CBomb)
                    {
                        CBomb b = t as CBomb;
                        b.Top += b.speed;
                        if (isManTouched(b.Left, b.Top) ||
                            isManTouched(b.Left, b.Top + b.Height) ||
                            isManTouched(b.Left + b.Width, b.Top) ||
                            isManTouched(b.Left + b.Width, b.Top + b.Height))
                        {
                            timer1.Enabled = false;
                            MessageBox.Show("Game Over");
                            return;
                        }
                    }
                }
            }

        }

        private void newBomb()
        {
            
            CBomb pic = new CBomb();
            pic.Width = 32;
            pic.Height = 64;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Image = Bitmap.FromFile(@"C:\Cs sample\pics\bomb.png");
            pic.Top = 0;
            pic.speed = rand.Next(10, 100);
            pic.Left = rand.Next(0, this.Width);
            pic.BringToFront();
            this.Controls.Add(pic);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}
