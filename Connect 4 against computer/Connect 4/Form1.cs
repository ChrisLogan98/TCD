using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Connect_4
{
    public partial class Form1 : Form
    {
        //possible square values
        const int EMPTY = 0;
        const int RED = 1;
        const int BLACK = 2;

        //board dimensions
        const int WIDTH = 7;
        const int HEIGHT = 6;
        const int TILE_SIZE = 50;
        //turn storage
        int whoseTurn = RED;

        int[,] board = new int[WIDTH, HEIGHT]; //board is 7x6

        Graphics graphics; //draw

        SoundPlayer checkerDropSound = new SoundPlayer(global::Connect_4.Properties.Resources.snd_checker_drop);

        Image redSquare = global::Connect_4.Properties.Resources.square_red;
        Image blackSquare = global::Connect_4.Properties.Resources.square_black;
        Image emptySquare = global::Connect_4.Properties.Resources.square_empty;
        Image turnBlack = global::Connect_4.Properties.Resources.turn_black;
        Image turnRed = global::Connect_4.Properties.Resources.turn_red;

        public Form1()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
            Reset();
        }

        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("BEHOLD! I BRING YOU CONNECT 4 IN C#.");
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void Reset()
        {
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    board[i, j] = EMPTY;
                }
            }
        }
        void DrawBoard()
        {
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    if(board[i, j] == EMPTY)
                    {
                        graphics.DrawImage(emptySquare, i * TILE_SIZE, j * TILE_SIZE);
                    }
                    else if (board[i, j] == BLACK)
                    {
                        graphics.DrawImage(blackSquare, i * TILE_SIZE, j * TILE_SIZE);
                    }
                    else if (board[i, j] == RED)
                    {
                        graphics.DrawImage(redSquare, i * TILE_SIZE, j * TILE_SIZE);
                    }
                }
            }
        }

        void DropChecker(int col)
        {
            for (int i = HEIGHT - 1; i >= 0; i--)
            {
                if (board[col, i] == EMPTY)
                {
                    board[col, i] = whoseTurn;
                    DrawBoard();
                    checkerDropSound.Play();
                    HandleWinningMove();
                    SwitchTurn();
                    break;
                }
            }
        }

        void SwitchTurn()
        {
            if(whoseTurn == RED)
            {
                whoseTurn = BLACK;
                turnBox.Image = global::Connect_4.Properties.Resources.turn_black;
            }
            else
            {
                turnBox.Image = global::Connect_4.Properties.Resources.turn_red;
                whoseTurn = RED;
            }
        }

        //returns horizontal connect 4 anywhere on
        bool HorizontalConnect4()
        {
            for (int col = 0; col <= 3; col++)
            {
                for (int row = 0; row < HEIGHT; row++)
                {
                    //cannot start on empty square
                    if (board[col, row] != EMPTY)
                    {
                        if (board[col, row] == board[col + 1, row] &&
                            board[col, row] == board[col + 2, row] &&
                            board[col, row] == board[col + 3, row])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        bool VerticalConnect4()
        {
            for (int col = 0; col < WIDTH; col++)
            {
                for (int row = 0; row <= 2; row++)
                {
                    //cannot start on empty square
                    if (board[col, row] != EMPTY)
                    {
                        if (board[col, row] == board[col, row+1] &&
                            board[col, row] == board[col, row+2] &&
                            board[col, row] == board[col, row+3])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        bool DiagonalDownConnect4()
        {
            for (int col = 0; col <+ 3; col++)
            {
                for (int row = 0; row <= 2; row++)
                {
                    //cannot start on empty square
                    if (board[col, row] != EMPTY)
                    {
                        if (board[col, row] == board[col - 1, row + 1] &&
                            board[col, row] == board[col - 2, row + 2] &&
                            board[col, row] == board[col - 3, row + 3])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        bool DiagonalUpConnect4()
        {
            for (int col = 0; col <= 3; col++)
            {
                for (int row = HEIGHT  - 3; row < HEIGHT; row++)
                {
                    //cannot start on empty square
                    if (board[col, row] != EMPTY)
                    {
                        if (board[col, row] == board[col + 1, row - 1] &&
                            board[col, row] == board[col + 2, row - 2] &&
                            board[col, row] == board[col + 3, row - 3])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        void HandleWinningMove()
        {
            if(HorizontalConnect4() || VerticalConnect4() || DiagonalUpConnect4() || DiagonalDownConnect4())
            {
                DrawBoard();
                MessageBox.Show("Connect 4!");
                Reset();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DropChecker(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DropChecker(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DropChecker(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DropChecker(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DropChecker(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DropChecker(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DropChecker(6);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DropChecker(7);
        }

        private void nEWGAMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private bool TryChecker(int row, int color)
        {
            for (int i = HEIGHT - 1; i >= 0; i--)
            { 
                
            }
            return false;
        }

        private void RemoveChecker(int col)
        {
            for (int i = 0; i >= HEIGHT; i++)
            {
                
            }
        }


    }
}
