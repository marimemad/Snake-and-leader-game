using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_ladder
{
    public partial class Form1 : Form
    {
        PictureBox player = new PictureBox();
        PictureBox opponent = new PictureBox();
        Button btn = new Button();
        Label lab = new Label();
        


        bool playerTurn =false;
        bool opponentTurn = false;

        int playerPos = 1;
        int opponentPos =1 ;



        List<PictureBox> Moves = new List<PictureBox>();


        public Form1()
        {
            InitializeComponent();
            this.Width=750;
            this.Height= 650;

            setup();
        }

         public void roll_dice(object sender, EventArgs e) {
            var rand = new Random();
            lab.Visible = true;
            if (playerTurn==true && opponentTurn==false)
            {
                playerTurn=false;
                opponentTurn=true;
                int ran = rand.Next(1, 7);

                lab.Location = new Point(630, 100);
                lab.Text = "you rolled " + ran.ToString();

                if (playerPos+ran < 100)
                {
                    move(player,playerPos,playerPos+ran);
                    playerPos=chack_snake_ladder(playerPos+ran);
                    move(player,"img"+(playerPos-1).ToString());
                    btn.Location = new Point(630,420);
                }
                else
                {
                    btn.Location = new Point(630, 420);
                }

                if (playerPos + ran == 100)
                {
                    move(player, "img" + (99).ToString());
                    MessageBox.Show("player1 win ");
                    playerTurn = false;
                    opponentTurn = false;
                }

            }

            else if (playerTurn==false && opponentTurn==true)
            {
                playerTurn=true;
                opponentTurn=false;
                int ran = rand.Next(1, 7);

                lab.Location = new Point(630, 420);
                lab.Text = "you rolled " + ran.ToString();

                if (opponentPos+ran < 100)
                {
                    move(opponent,opponentPos,opponentPos+ran);
                    opponentPos=chack_snake_ladder(opponentPos+ran);
                    move(opponent,"img"+(opponentPos-1).ToString());
                    
                    btn.Location = new Point(630,100);
                }
                else
                {
                    btn.Location = new Point(630, 100);
                }
                if (opponentPos + ran == 100)
                {
                    move(opponent, "img" + (99).ToString());
                    MessageBox.Show("player2 win ");
                    playerTurn = false;
                    opponentTurn = false;
                }


            }

        }

        public void setup()
        {
            int top =545;
            int left=5;
            int row = 0;

            PictureBox player1 = new PictureBox();
            PictureBox player2 = new PictureBox();

            player1.Load($"templet/player1.gif");
            player1.SizeMode = PictureBoxSizeMode.StretchImage;
            player1.Width = 70;
            player1.Height = 70;
            player1.Location = new Point(630,20);
            this.Controls.Add(player1);

            btn.Text="rol";
            btn.Location = new Point(630,100);
            this.Controls.Add(btn);
            btn.Click += new EventHandler(roll_dice);

            lab.Location = new Point(630, 100);
            lab.Width = 100;
            lab.Height = 30;
            lab.Visible = false;
            lab.ForeColor = Color.Red;
            this.Controls.Add(lab);

            player2.Load($"templet/player2.gif") ;
            player2.SizeMode = PictureBoxSizeMode.StretchImage;
            player2.Width = 70;
            player2.Height = 70;
            player2.Location = new Point(630,320);
            this.Controls.Add(player2);

            player.Load($"templet/player1.gif");
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            player.Width = 40;
            player.Height = 40;
            this.Controls.Add(player);

            opponent.Load($"templet/player2.gif");
            opponent.SizeMode = PictureBoxSizeMode.StretchImage;
            opponent.Width = 40;
            opponent.Height = 40;
            this.Controls.Add(opponent);
            
            for (int i=0; i<100; i++)
            {
                PictureBox imgPhoto = new PictureBox();
                imgPhoto.Load($"templet/{i}.jpg");
                imgPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                imgPhoto.Width = 60;
                imgPhoto.Height = 60;
                imgPhoto.Name="img"+i.ToString();

                if (row == 10)
                {
                    top -=60;
                    left = 545;
                    row = 30;
                }
                if (row == 20)
                {
                    top -=60;
                    left = 5;
                    row = 0;
                }
                if (row < 10)
                {
                    imgPhoto.Location = new Point(left,top);
                    this.Controls.Add(imgPhoto);
                    Moves.Add(imgPhoto);
                    left +=60;
                    row++;
                }
                if (row > 20)
                {
                    imgPhoto.Location = new Point(left, top);
                    this.Controls.Add(imgPhoto);
                    Moves.Add(imgPhoto);
                    left -=60;
                    row--;
                }
            }
            
            move(player,"img0");
            move(opponent,"img0"); 
            playerTurn=true;
        }

        public int chack_snake_ladder(int num)
        {
           Dictionary<int, int> sn_la = new Dictionary<int, int>() {
            {4, 25},
            {30, 7},
            {21, 39},
            {29, 74 },
            {47, 15},
            {43, 76},
            {56, 19},
            {71, 89},
            {73, 51},
            {63, 80},
            {82, 42},
            {92, 75},
            {98, 55}
            };
            
            foreach(int s in sn_la.Keys)
            {
                if (s == num)
                {
                    return (sn_la[num]);
                }
            }

            return (num);

        }

        public void move(PictureBox player, string pice)
        {
            foreach (PictureBox card in Moves)
            {
                if (card.Name == pice)
                {
                    player.Location = new Point(card.Location.X+10,card.Location.Y+20);
                }
            }
        }

        public void move(PictureBox player, int crrunt_pos,int pos)
        {
            for (int i= crrunt_pos; i< pos; i++)
            {
                move(player,"img"+(i).ToString());
                Thread.Sleep(70);
            }
            //pos=chack_snake_ladder(pos);

        }

       
    }
}
