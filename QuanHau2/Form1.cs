using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuanHau2
{
    public partial class Form1 : Form
    {
        private static readonly int n = 8;
        private static char HAU = 'H';
        private static char TRONG = '.';
        private char[,] BanCo = new char[n, n];
        private List<char[,]> dapAn = new List<char[,]>();
        private Button[,] cell = new Button[n, n];
        private int thutu = 0;

        public char[,] KhoiTaoBanCo()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    BanCo[i, j] = TRONG;
                }
            }
            return BanCo;
        }
        public void LuuDapAn()
        {
            char[,] tmp = new char[n,n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    tmp[i, j] = BanCo[i, j];
            }
            dapAn.Add(tmp);
        }
        public void DatHau(int i)
        {
            if (i == n)
            {
                LuuDapAn();
                return;
            }

            for (int j = 0; j < n; j++)
            {
                if (BanCo[i, j] == HAU)
                {
                    DatHau(i + 1);
                    return;
                }
            }

            for (int j = 0; j < n; j++)
            {
                bool trung = false;

                for (int hang = n - 1; hang >= 0; hang--) //kiem tra cot
                {
                    if (BanCo[hang, j] == HAU)
                    {
                        trung = true;
                    }
                }
                for (int hang = i - 1, cot = j - 1; hang >= 0 && cot >= 0; hang--, cot--) //kiem tra cheo trai tren
                {
                    if (BanCo[hang, cot] == HAU)
                    {
                        trung = true;
                    }
                }
                for (int hang = i + 1, cot = j - 1; hang < n && cot >= 0; hang++, cot--) //kiem tra cheo trai duoi
                {
                    if (BanCo[hang, cot] == HAU)
                    {
                        trung = true;
                    }
                }
                for (int hang = i - 1, cot = j + 1; hang >= 0 && cot < n; hang--, cot++) //kiem tra cheo phai tren
                {
                    if (BanCo[hang, cot] == HAU)
                    {
                        trung = true;
                    }
                }
                for (int hang = i + 1, cot = j + 1; hang < n && cot < n; hang++, cot++) //kiem tra cheo phai duoi
                {
                    if (BanCo[hang, cot] == HAU)
                    {
                        trung = true;
                    }
                }
                if (!trung)
                {
                    BanCo[i, j] = HAU;
                    DatHau(i + 1);
                    BanCo[i, j] = TRONG;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            KhoiTaoBanCo();
            int sideLen = 50;
            tableLayoutPanel1.ColumnCount = 8;
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.Margin = new Padding(0);
            for(int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    cell[i, j] = new Button();
                    cell[i, j].Margin = new Padding(0);
                    cell[i, j].Size = new Size(sideLen, sideLen);
                    cell[i, j].Font = new Font(cell[i, j].Font.FontFamily, 20);
                    cell[i, j].Tag = new int[] { i, j };
                    cell[i,j].MouseUp += OnCellClick;
                    if ((i + j) % 2 == 0)
                    {
                        cell[i,j].BackColor = Color.White;
                    }
                    else
                    {
                        cell[i, j].BackColor = Color.Black;  
                        cell[i, j].ForeColor = Color.White;
                    }
                   // cell[i, j].Text = "♕";
                  
                    tableLayoutPanel1.Controls.Add(cell[i,j], j, i);
                }
            }
        }
        public void OnCellClick(object sender, MouseEventArgs e)
        {
            int[] index = (int[])(sender as Button).Tag;
            int hang = index[0];
            int cot = index[1];

            if (e.Button == MouseButtons.Left)
            {
                BanCo[hang, cot] = HAU;
                cell[hang, cot].Text = "♕";
            }
            else if (e.Button == MouseButtons.Right)
            {
                BanCo[hang, cot] = TRONG;
                cell[hang, cot].Text = "";
            }
           // BanCo[hang, cot] = HAU;
            //cell[hang, cot].Text = "♕";

        }

        private void hienThi()
        {
            char[,] board = dapAn[thutu];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i, j] == HAU)
                    {
                        cell[i, j].Text = "♕";
                    }
                    else
                    {
                        cell[i, j].Text = "";
                    }

                }
            }
        }
        private void btnXepHau_Click(object sender, EventArgs e)
        {
            btnXepHau.Visible = false;
            DatHau(0);
            if (dapAn.Count > 0) { 
                btnPrev.Visible = true;
                btnNext.Visible = true;
                hienThi();
                lbThu.Text = "Lời Giải thứ " + (thutu + 1) + "/" + dapAn.Count;
            } else
            {
                MessageBox.Show("Không có đáp án!");
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (thutu > 0)
                thutu--;
            lbThu.Text = "Lời Giải thứ " + (thutu + 1) + "/" + dapAn.Count;
            hienThi();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (thutu < dapAn.Count - 1)
                thutu++;
            lbThu.Text = "Lời Giải thứ " + (thutu + 1) + "/" + dapAn.Count;
            hienThi();
        }
    }
}
