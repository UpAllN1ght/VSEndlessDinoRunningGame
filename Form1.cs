using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_Endless_Runner_Game
{
    public partial class Form1 : Form
    {
        bool jumping = false; // boolean to check if player is jumping or not
        int jumpSpeed = 10; // integer to set jump speed
        int force = 12; // force of the jump in an integer
        int score = 0; // default score integer set to 0
        int obstacleSpeed = 10; // the default speed for the obstacles
        Random rnd = new Random(); // create a new random class
        public Form1()
        {
            InitializeComponent();
            resetGame(); // run the reset game function
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void gameEvent(object sender, EventArgs e)
        {
            // linking the jumpspeed integer with the player picture boxes to location
            dino.Top += jumpSpeed;
            // show the score on the score text label
            scoreText.Text = "Score: " + score;
            // if jumping is true and force is less than 0
            // then change jumping to false
            if (jumping && force < 0)
            {
                jumping = false;
            }
            // if jumping is true
            // then change jump speed to -12 
            // reduce force by 1
            if (jumping)
            {
                jumpSpeed = -15;
                force -= 1;
            }
            else
            {
                // else change the jump speed to 12
                jumpSpeed = 15;
            }
            foreach (Control x in this.Controls)
            {
                // is X is a picture box and it has a tag of obstacle
                if (x is PictureBox && x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed; // move the obstacles towards the left
                    if (x.Left + x.Width < -120)
                    {
                        // if the obstacles have gone off the screen
                        // then we respawn it off the far right
                        // in this case we are calculating the form width and a random number between 200 and 800
                        x.Left = this.ClientSize.Width + rnd.Next(200, 800);
                        // we will add one to the score
                        score++;
                    }
                    // if the t rex collides with the obstacles
                    if (dino.Bounds.IntersectsWith(x.Bounds))
                    {
                        // we stop the timer
                        gameTimer.Stop();
                        // change the t rex image to the dead one
                        dino.Image = Dino_Endless_Running_Game.Properties.Resources.dead1;
                        // show press r to restart on the score text label
                        scoreText.Text += "  Press R to restart";
                    }
                }
            }
            // if the t rex top is greater than or equals to 380 AND
            // jumping is not true
            if (dino.Top >= 380 && !jumping)
            {
                // then we do the following
                force = 15; // set the force to 8
                dino.Top = floor.Top - dino.Height; // also we place the player on top of the picture box
                jumpSpeed = 0; // set the jump speed to 0
            }
            // if score is equals or greater than 10
            if (score >= 10)
            {
                // the obstacle speed change to 15
                obstacleSpeed = 15;
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            //if the player pressed the space key and jumping boolean is false
            // then we set jumping to true
            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            // if the R key is pressed and released then we run the reset function
            if (e.KeyCode == Keys.R)
            {
                resetGame();
            }
            //when the keys are released we check if jumping is true
            // if so we need to set it back to false so the player can jump again
            if (jumping)
            {
                jumping = false;
            }
        }

        public void resetGame()
        {
            // This is the reset function
            force = 15; // set the force to 8
            dino.Top = floor.Top - dino.Height; // also we place the player on top of the picture box
            jumpSpeed = 0; // set the jump speed to 0
            jumping = false; // change jumping to false
            score = 0; // set score to 0
            obstacleSpeed = 10; // set obstacle speed back to 10
            scoreText.Text = "Score: " + score; // change the score text to just show the score
            dino.Image = Dino_Endless_Running_Game.Properties.Resources.dino; // change the t rex image to running
            foreach (Control x in this.Controls)
            {
                // is X is a picture box and it has a tag of obstacle
                if (x is PictureBox && x.Tag == "obstacle")
                {
                    // generate a random number in the position integer between 600 and 1000
                    int position = rnd.Next(600, 1000);
                    // change the obstacles left position to a random location begining of the game
                    x.Left = 640 + (x.Left + position + x.Width * 3);
                }
            }
            gameTimer.Start(); // start the timer
        }
    }
}
