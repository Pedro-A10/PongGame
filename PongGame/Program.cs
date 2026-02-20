using System;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace PongGame
{
    internal static class Program
    {
        class PongFrame : Form
        {
            //ball
            int ballXSpeed = 8;
            int ballYSpeed = 8;
            int ballX;
            int ballY;
            //Player
            int barPlayerSpeed = 6;
            int barPlayer;
            //Computer
            int barComputerSpeed = 6;
            int barComputer;
            //Moviment
            bool goUp, goDown;
            //Points
            int playerPoints = 0;
            int computerPoints = 0;
            //Font
            Font font = new Font("Arial", 40, FontStyle.Bold);

            public PongFrame()
            {
                //Frame
                this.Text = "Pong Game";
                this.Width = 1000;
                this.Height = 800;
                this.BackColor = Color.Black;
                this.DoubleBuffered = true;

                //Objects in game
                ballX = this.ClientSize.Width / 2;
                ballY = this.ClientSize.Height / 2;

                barPlayer = this.ClientSize.Height / 2;
                barComputer = this.ClientSize.Height / 2;

                //Loop
                Timer gameTimer = new Timer();
                gameTimer.Interval = 20;
                gameTimer.Tick += GameTimerEvent;
                gameTimer.Start();

                this.KeyDown += KeyIsDown;
                this.KeyUp += KeyIsUp;


            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                int centerX = this.ClientSize.Width / 2;
                int centerY = this.ClientSize.Height / 2;
                int ballSize = 35;

                using Brush whiteBrush = new SolidBrush(Color.White);
                using var whitePen = new Pen(Color.White, 2);

                //Line:
                e.Graphics.DrawLine(whitePen, centerX, 0, centerX, this.ClientSize.Height);

                //Ball:
                //Position:
                e.Graphics.DrawEllipse(whitePen, ballX, ballY, ballSize, ballSize);

                e.Graphics.FillEllipse(whiteBrush, ballX, ballY, ballSize, ballSize);

                //Left - Player:
                e.Graphics.DrawRectangle(whitePen, 20, barPlayer, 20, 100);
                e.Graphics.FillRectangle(whiteBrush, 20, barPlayer, 20, 100);

                //Right - IA :
                e.Graphics.DrawRectangle(whitePen, 945, barComputer, 20, 100);
                e.Graphics.FillRectangle(whiteBrush, 945, barComputer, 20, 100);

                //Points Viwer
                e.Graphics.DrawString(computerPoints.ToString(), font, whiteBrush, 935, 10);
                e.Graphics.DrawString(playerPoints.ToString(), font, whiteBrush, 0, 10);

                whitePen.Dispose();
                whiteBrush.Dispose();
            }
            //Reset ball
            private void resetBall()
            {
                ballX = this.ClientSize.Width / 2 - 17;
                ballY = this.ClientSize.Height / 2 - 17;

                ballXSpeed = ballXSpeed > 0 ? -8 : 8;
                ballYSpeed = 8;
            }
            //Moviment
            private void KeyIsDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                {
                    goDown = true;
                }
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                {
                    goUp = true;
                }
            }
            private void KeyIsUp(object sender, KeyEventArgs e)
            {
                if(e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                {
                    goDown = false;
                }
                if(e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                {
                    goUp = false;
                }
            }
            //GameLoop
            private void GameTimerEvent(object sender, EventArgs e)
            {
                //ball speed
                ballX += ballXSpeed;
                ballY += ballYSpeed;

                //Collision
                Rectangle ballRect = new Rectangle(ballX, ballY, 35, 35);
                Rectangle playerRect = new Rectangle(20, barPlayer, 20, 100);
                Rectangle computerRect = new Rectangle(945, barComputer, 20, 100);

                //IA bar
                int ballCenter = ballY + 17;
                int computerCenter = barComputer + 50;

                if (ballY <= 0 || ballY + 35 >= this.ClientSize.Height)
                {
                    ballYSpeed = -ballYSpeed;
                }
                if (ballX <= 0 || ballX + 35 >= this.ClientSize.Width)
                {
                    ballXSpeed = -ballXSpeed;
                }

                //bar moviment Player
                if (goUp && barPlayer > 0)
                {
                    barPlayer -= barPlayerSpeed;
                }
                if (goDown && barPlayer + 100 < this.ClientSize.Height)
                {
                    barPlayer += barPlayerSpeed;
                }

                //Computer moviment 
                if (ballXSpeed > 0)
                {
                    if(ballCenter < computerCenter && barComputer > 0)
                    {
                        barComputer -= barComputerSpeed;
                    }
                    else if(ballCenter > computerCenter && barComputer + 100 < this.ClientSize.Height)
                    {
                        barComputer += barComputerSpeed;
                    }
                }

                //Collision
                if (ballRect.IntersectsWith(playerRect))
                {
                    ballXSpeed = -ballXSpeed;
                }
                if (ballRect.IntersectsWith(computerRect))
                {
                    ballXSpeed = -ballXSpeed;
                }

                //Points
                if(ballX < 0)
                {
                    computerPoints++;
                    resetBall();
                }
                if(ballX + 35 > this.ClientSize.Width)
                {
                    playerPoints++;
                    resetBall();
                }
                
                Invalidate();
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