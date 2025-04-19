using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace New_Library
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string path = "book list.txt";
        FileStream fl = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

        private void Form1_Load(object sender, EventArgs e)
        {
               
        }
        private void filter_Click1(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            fl.Seek(0, SeekOrigin.Begin);
            StreamReader stt = new StreamReader(fl);
            string line = stt.ReadLine();
            int x = 70, y = 50;
            Label lab;
            while (line != null)
            {
                string yr = "", rt = "", ln = "", img = "", name = "", ath = "", tpc = "";
                if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox3.SelectedItem != null && comboBox4.SelectedItem != null)
                {
                    int flag = 1;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '|') flag++;
                        else if (flag == 3) yr += line[i];
                        else if (flag == 4) rt += line[i];
                        else if (flag == 5) ln += line[i];
                        else if (flag == 7) tpc += line[i];
                        else if (flag == 1) name += line[i];
                        else if (flag == 2) ath += line[i];
                        else if (flag == 6) img += line[i];
                    }
                    if (comboBox1.SelectedItem.ToString() == ln && comboBox2.SelectedItem.ToString() == tpc &&
                        yr == comboBox3.SelectedItem.ToString() && rt == comboBox4.SelectedItem.ToString())
                    {
                        lab = new Label();
                        lab.Text = ath;
                        lab.Location = new Point(x, y + 200);
                        lab.Size = new Size(200, 50);
                        panel2.Controls.Add(lab);
                        PictureBox pic = new PictureBox();
                        pic.Size = new Size(150, 200);
                        //pic.BackColor = Color.Red;
                        pic.Image = Image.FromFile(img);
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Location = new Point(x, y);
                        panel2.Controls.Add(pic);
                        x = x + 200;
                    }
                }
                line = stt.ReadLine();
            }
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            PictureBox pic;
            panel2.Controls.Clear();
            fl.Seek(0, SeekOrigin.Begin);
            StreamReader stt = new StreamReader(fl);
            string line = stt.ReadLine();
            Label lb;
            int a = 70, b = 50;
            while (line != null)
            {
                string name = "", ath = "", img = "";
                if (!string.IsNullOrEmpty(search_bx.Text))
                {
                    int flag = 1;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '|') flag++;
                        else if (flag == 1) name += line[i];
                        else if (flag == 2) ath += line[i];
                        else if (flag == 6) img += line[i];
                    }
                    if (search_bx.Text == name || search_bx.Text == ath)
                    {
                        lb = new Label();
                        lb.Text = ath;
                        lb.Location = new Point(a, b + 200);
                        lb.Size = new Size(200, 50);
                        panel2.Controls.Add(lb);
                        pic = new PictureBox();
                        pic.Size = new Size(150, 200);
                        pic.Image = Image.FromFile(img);
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Location = new Point(a, b);
                        panel2.Controls.Add(pic);
                        a = a + 200;
                        break;
                    }
                    line = stt.ReadLine();
                }
            }
        }
        public void AddNode(string title, string author, string year, string rating, string language, string imgPath, string topic, string goodreadsLink)
        {
            string book = $"{title}|{author}|{year}|{rating}|{language}|{imgPath}|{topic}|{goodreadsLink}";
            fl.Seek(0, SeekOrigin.End);
            StreamWriter streamWriter = new StreamWriter(fl);
            streamWriter.WriteLine(book);
            streamWriter.Flush();
        }
    }
}
