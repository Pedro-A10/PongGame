using System;
using System.Windows.Forms;

namespace PongGame
{
    internal static class Program
    {
        class PongFrame : Form
        {
            public PongFrame()
            {
                //Janela
                this.Text = "Pong Game";
                this.Width = 1000;
                this.Height = 800;
                this.BackColor = Color.Black;
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                int centerX = this.ClientSize.Width / 2;
                int centerY = this.ClientSize.Height / 2;
                int ballSize = 35;

                using (Brush whiteBrush = new SolidBrush(Color.White))
                {
                    using (var whitePen = new Pen(Color.White, 2))
                    {
                        //Linha:
                        e.Graphics.DrawLine(whitePen, centerX, 0, centerX, this.ClientSize.Height);

                        //Bola:
                        //posição:
                        int ballX = centerX - ballSize / 2;
                        int ballY = centerY - ballSize / 2;

                        e.Graphics.DrawEllipse(whitePen, ballX, ballY, ballSize, ballSize);

                        e.Graphics.FillEllipse(whiteBrush ,ballX, ballY , ballSize, ballSize);

                        whitePen.Dispose();
                        whiteBrush.Dispose();
                    }
                }
            }
        }
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.Run(new PongFrame());
        }
    }
}