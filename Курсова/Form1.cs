using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Курсова
{
    public partial class Form1 : Form
    {
        public static string connectString = "Provider=Microsoft.JET.OLEDB.4.0;Data Source=Database1.mdb;";
        private OleDbConnection myConnection;
        public List<string> true_answers = new List<string>();
        public List<int> usedwords = new List<int>();
        public List<List<TextBox>> list_of_Textboxes = new List<List<TextBox>>();
        public List<String> questions = new List<string>();
        public int changedirection = -1;
        public List<TextBox> scale = new List<TextBox>();

        public Panel startpanel = new Panel();
        public Panel diffchoosepanel = new Panel();
        public Button diff1 = new Button();
        public Button diff2 = new Button();
        public Button diff3 = new Button();

        public int direction = 1;
        public List<TextBox> list_of_used_points = new List<TextBox>();

        public Label lo = new Label();
        void Buttonsdestroy()
        {
            diff1.Dispose();
            diff2.Dispose();
            diff3.Dispose();
        }

        public static int CheckLocation(ref List<List<TextBox>> list, Point pos, char verb, ref List<TextBox> verbs)
        {
            for (int k = 0; k < list.Count; k++)
            {
                for (int l = 0; l < list[k].Count; l++)
                {
                    if (pos.ToString() == list[k][l].Location.ToString())
                    {
                        if (list[k][l].Text == verb.ToString())
                        {
                            verbs.Add(list[k][l]);
                            return 1;
                        }
                        else
                            return -1;
                    }
                }
            }
            return 0;
        }
        private void diff3_click(object sender, EventArgs e)
        {
            int verb8 = 3;
            int verb7 = 3;
            int verb6 = 2;
            int verb5 = 1;
            int verb4 = 2;
            Buttonsdestroy();
            diffchoosepanel.Dispose();
            LoadScreen();
            Main(verb8, verb7, verb6, verb5, verb4);

        }

        private void diff2_click(object sender, EventArgs e)
        {
            int verb8 = 2;
            int verb7 = 2;
            int verb6 = 2;
            int verb5 = 2;
            int verb4 = 3;
            Buttonsdestroy();
            diffchoosepanel.Dispose();
            LoadScreen();
            Main(verb8, verb7, verb6, verb5, verb4);

        }

        private void diff1_click(object sender, EventArgs e)
        {
            int verb8 = 1;
            int verb7 = 1;
            int verb6 = 2;
            int verb5 = 3;
            int verb4 = 4;
            Buttonsdestroy();
            diffchoosepanel.Dispose();
            LoadScreen();
            Main(verb8, verb7, verb6, verb5, verb4);

        }
        private void startpanel_Click(object sender, EventArgs e)
        {
            BackColor = ColorTranslator.FromHtml("#202020");
            diffchoosepanel.BackgroundImage = Image.FromFile("Diffchoose.png");
            diffchoosepanel.Location = new Point(0, 0);
            diffchoosepanel.Size = new Size(1200, 900);
            startpanel.Dispose();

            Controls.Add(diff1);
            Controls.Add(diff2);
            Controls.Add(diff3);

            Controls.Add(diffchoosepanel);
        }

        public Form1()
        {
            InitializeComponent();
            //DoubleBuffered = true;

            BeforeMenu();
            lo.Location = new Point(400, 50);
            lo.Size = new Size(200, 50);
            lo.ForeColor = Color.Orange;
            Controls.Add(lo);

            startpanel.Location = new Point(0, 0);
            startpanel.Size = new Size(1200, 900);
            diff1.Text = "Легкий";
            diff2.Text = "Середній";
            diff3.Text = "Складний";
            diff1.Location = new Point(200, 600);
            diff2.Location = new Point(500, 600);
            diff3.Location = new Point(800, 600);
            diff1.Size = new Size(200, 50);
            diff2.Size = new Size(200, 50);
            diff3.Size = new Size(200, 50);
            diff1.Font = new Font("Times New Roman", this.Height / 30);
            diff2.Font = new Font("Times New Roman", this.Height / 30);
            diff3.Font = new Font("Times New Roman", this.Height / 30);







            diff1.FlatStyle = FlatStyle.Flat;
            diff2.FlatStyle = FlatStyle.Flat;
            diff3.FlatStyle = FlatStyle.Flat;
            diff1.BackColor = ColorTranslator.FromHtml("#ffb26b");
            diff2.BackColor = ColorTranslator.FromHtml("#ffb26b");
            diff3.BackColor = ColorTranslator.FromHtml("#ffb26b");

            diff1.Click += new EventHandler(diff1_click);
            diff2.Click += new EventHandler(diff2_click);
            diff3.Click += new EventHandler(diff3_click);

            Image screen = Image.FromFile("Crossword.png");
            startpanel.BackgroundImage = screen;
            Controls.Add(startpanel);
            startpanel.Click += new EventHandler(this.startpanel_Click);


            ClientSize = new Size(1200, 900);
            //#383838



        }
        void BeforeMenu()
        {
            {
                {
                    Panel Load = new Panel();
                    Load.Location = new Point(0, 0);
                    Load.Size = new Size(1200, 900);


                    int time = -10;
                    var timer = new Timer
                    {
                        Interval = 100
                    };
                    timer.Tick += (sender, args) =>
                    {
                        time++;
                        Invalidate();
                    };
                    timer.Start();

                    Controls.Add(Load);
                    Paint += (sender, args) =>
                    {
                        switch (time)
                        {
                            default:
                                Load.BackColor = ColorTranslator.FromHtml("#202020");
                                break;
                            case 6:
                                timer.Stop();
                                Load.Dispose();
                                break;
                        }
                    };
                }

            }

        }

        void LoadScreen()
        {

        }

        void Main(int verb8, int verb7, int verb6, int verb5, int verb4)
        {
            Panel crosswordpanel = new Panel();
            crosswordpanel.Location = new Point(0, 0);
            crosswordpanel.Size = new Size(1200, 900);
            int range_top_left_angle = 3;//*50-in points
            int range_bottom_right_angle = 15;//*50-in points
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            var rand = new Random();


            Word(verb8, 8);
            usedwords.Clear();
            Word(verb7, 7);
            usedwords.Clear();
            Word(verb6, 6);
            usedwords.Clear();
            Word(verb5, 5);
            usedwords.Clear();
            Word(verb4, 4);
            usedwords.Clear();
            lo.Text = "ГОТОВО!";


            int maxX = int.MinValue;
            int maxY = int.MinValue;
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            for (int i = 0; i < list_of_Textboxes.Count; i++)
            {
                for (int j = 0; j < list_of_Textboxes[i].Count; j++)
                {
                    maxX = Math.Max(maxX, list_of_Textboxes[i][j].Location.X);
                    maxY = Math.Max(maxY, list_of_Textboxes[i][j].Location.Y);
                    minX = Math.Min(minX, list_of_Textboxes[i][j].Location.X);
                    minY = Math.Min(minY, list_of_Textboxes[i][j].Location.Y);
                    switch (i + 1)
                    {
                        case 1:
                            list_of_Textboxes[i][j].Click += new EventHandler(quest1_click);
                            break;
                        case 2:
                            list_of_Textboxes[i][j].Click += new EventHandler(quest2_click);
                            break;
                        case 3:
                            list_of_Textboxes[i][j].Click += new EventHandler(quest3_click);
                            break;
                        case 4:
                            list_of_Textboxes[i][j].Click += new EventHandler(quest4_click);
                            break;
                        case 5:
                            list_of_Textboxes[i][j].Click += new EventHandler(quest5_click);
                            break;
                        case 6:
                            list_of_Textboxes[i][j].Click += new EventHandler(quest6_click);
                            break;
                        case 7:
                            list_of_Textboxes[i][j].Click += new EventHandler(quest7_click);
                            break;
                        case 8:
                            list_of_Textboxes[i][j].Click += new EventHandler(quest8_click);
                            break;
                        case 9:
                            list_of_Textboxes[i][j].Click += new EventHandler(quest9_click);
                            break;
                        case 10:
                            list_of_Textboxes[i][j].Click += new EventHandler(quest10_click);
                            break;
                        case 11:
                            list_of_Textboxes[i][j].Click += new EventHandler(quest11_click);
                            break;
                    }


                }
            }
            int deltaX = Math.Abs(minX - Math.Abs(1200 - maxX)) / 2;
            int deltaY = Math.Abs(minY - Math.Abs(900 - maxY)) / 2;
            if (minX > deltaX)
            {
                deltaX *= -1;

            }
            if (minY > deltaY)
            {
                deltaY *= -1;
            }
            lo.Text = "deltaX: " + deltaX + "\ndeltaY: " + deltaY;
            for (int i = 0; i < list_of_Textboxes.Count; i++)
            {
                for (int j = 0; j < list_of_Textboxes[i].Count; j++)
                {
                    foreach (TextBox p in scale)
                    {
                        if (p == list_of_Textboxes[i][j])
                            goto skip;
                    }
                    list_of_Textboxes[i][j].Location = new Point(list_of_Textboxes[i][j].Location.X - deltaX, list_of_Textboxes[i][j].Location.Y + deltaY);
                    scale.Add(list_of_Textboxes[i][j]);
                skip:;
                }
            }

            void Word(int num, int lg)
            {

                for (int j = 0; j < num; j++)
                {
                restart:
                    changedirection++;
                    list_of_used_points.Clear();


                    var randword = rand.Next(1, 99);

                    foreach (int i in usedwords)
                    {
                        if (randword == i)
                            goto restart;
                    }

                    if (changedirection == 10)
                    {
                        changedirection = 0;
                        direction *= -1;
                    }

                    string query = "SELECT verb" + lg + " FROM Words WHERE num=" + randword;
                    OleDbCommand command = new OleDbCommand(query, myConnection);
                    lo.Text = "Слoво в обробці:   " + command.ExecuteScalar().ToString() + "\nСтворено слів:  " + list_of_Textboxes.Count + "\nНапрямок:     " + direction;

                    bool done = false;
                    for (int row = range_top_left_angle; row < range_bottom_right_angle; row++)
                    {
                        for (int col = range_top_left_angle; col < range_bottom_right_angle; col++)
                        {

                            if (done)
                            {
                                goto end;
                            }
                            done = false;
                            bool cross = false;
                            List<TextBox> verbs = new List<TextBox>();
                            int px = 50 * col; int py = 50 * row;

                            if (list_of_Textboxes.Count == 0)
                            {
                                px = 250; py = 300;
                            }

                            bool doublecross = false;
                            for (int i = 0; i < command.ExecuteScalar().ToString().Length; i++)
                            {

                                Point pos_top = new Point(px, py - 50);
                                Point pos_left = new Point(px - 50, py);
                                Point pos_right = new Point(px + 50, py);
                                Point pos_buttom = new Point(px, py + 50);

                                switch (CheckLocation(ref list_of_Textboxes, new Point(px, py), command.ExecuteScalar().ToString()[i], ref verbs))
                                {
                                    case 1:
                                        cross = true;
                                        if (doublecross)
                                            goto Skip;

                                        if (direction == 1)
                                        {
                                            px += 50;
                                            if (i == command.ExecuteScalar().ToString().Length - 1)
                                            {

                                                if (CheckLocation(ref list_of_Textboxes, pos_right, command.ExecuteScalar().ToString()[i], ref verbs) != 0)
                                                    goto Skip;
                                            }
                                            else
                                                if (i == 0)
                                            {
                                                if (CheckLocation(ref list_of_Textboxes, pos_left, command.ExecuteScalar().ToString()[i], ref verbs) != 0)
                                                    goto Skip;
                                            }
                                        }
                                        else
                                        {
                                            if (i == command.ExecuteScalar().ToString().Length - 1)
                                            {

                                                if (CheckLocation(ref list_of_Textboxes, pos_buttom, command.ExecuteScalar().ToString()[i], ref verbs) != 0)
                                                    goto Skip;
                                            }
                                            if (i == 0)
                                            {
                                                if (CheckLocation(ref list_of_Textboxes, pos_top, command.ExecuteScalar().ToString()[i], ref verbs) != 0)
                                                    goto Skip;
                                            }
                                            py += 50;
                                        }

                                        doublecross = true;
                                        break;

                                    case 0:
                                        doublecross = false;
                                        pos_top = new Point(px, py - 50);
                                        pos_left = new Point(px - 50, py);
                                        pos_right = new Point(px + 50, py);
                                        pos_buttom = new Point(px, py + 50);

                                        if (i == 0)//first verb
                                        {
                                            if (CheckLocation(ref list_of_Textboxes, pos_top, command.ExecuteScalar().ToString()[i], ref verbs) != 0
                                                || CheckLocation(ref list_of_Textboxes, pos_left, command.ExecuteScalar().ToString()[i], ref verbs) != 0)
                                                goto Skip;
                                        }

                                        if (direction == 1)
                                        {
                                            if (CheckLocation(ref list_of_Textboxes, pos_top, command.ExecuteScalar().ToString()[i], ref verbs) != 0 ||
                                                CheckLocation(ref list_of_Textboxes, pos_buttom, command.ExecuteScalar().ToString()[i], ref verbs) != 0)
                                                goto Skip;

                                            if (i == command.ExecuteScalar().ToString().Length - 1)//last verb
                                            {
                                                if (CheckLocation(ref list_of_Textboxes, pos_right, command.ExecuteScalar().ToString()[i], ref verbs) != 0)
                                                    goto Skip;
                                            }
                                        }
                                        else
                                        {
                                            if (CheckLocation(ref list_of_Textboxes, pos_left, command.ExecuteScalar().ToString()[i], ref verbs) != 0 ||
                                                CheckLocation(ref list_of_Textboxes, pos_right, command.ExecuteScalar().ToString()[i], ref verbs) != 0)
                                                goto Skip;

                                            if (i == command.ExecuteScalar().ToString().Length - 1)//last verb
                                            {
                                                if (CheckLocation(ref list_of_Textboxes, pos_buttom, command.ExecuteScalar().ToString()[i], ref verbs) != 0)
                                                    goto Skip;
                                            }
                                        }
                                        verbs.Add(new TextBox());
                                        verbs[i].Name = "w" + j.ToString() + "k" + i.ToString();
                                        verbs[i].Location = new Point(px, py);
                                        verbs[i].Size = new Size(50, 50);
                                        verbs[i].Multiline = true;
                                        verbs[i].MaxLength = 1;
                                        verbs[i].TextAlign = HorizontalAlignment.Center;
                                        verbs[i].BorderStyle = BorderStyle.Fixed3D;
                                        verbs[i].CharacterCasing = CharacterCasing.Upper;
                                        verbs[i].Font = new Font("Times New Roman", this.Height / 30);
                                        verbs[i].BackColor = ColorTranslator.FromHtml("#ffb26b");
                                        if (direction == 1)
                                            px += 50;
                                        else
                                            py += 50;

                                        verbs[i].Text = command.ExecuteScalar().ToString()[i].ToString();
                                        break;

                                    default:
                                        goto Skip;
                                }
                            }

                            if (!cross && list_of_Textboxes.Count != 0)
                                goto Skip;

                            //Слово затверджено
                            done = true;
                            usedwords.Add(randword);
                            list_of_Textboxes.Add(verbs);
                            true_answers.Add(command.ExecuteScalar().ToString());
                            direction *= -1;
                            string quest = "SELECT question" + lg + " FROM Words WHERE num=" + randword;
                            OleDbCommand command2 = new OleDbCommand(quest, myConnection);
                            questions.Add(command2.ExecuteScalar().ToString());
                            foreach (TextBox o in verbs)
                                Controls.Add(o);

                            Skip:;
                        }
                    }
                    goto restart;

                end:;
                }
            }



        }
        void Selectword(int num)
        {
            for (int i = 0; i < list_of_Textboxes.Count; i++)
            {
                for (int j = 0; j < list_of_Textboxes[i].Count; j++)
                {
                    list_of_Textboxes[i][j].BackColor = ColorTranslator.FromHtml("#ffb26b");
                }
            }
            for (int i = 0; i < list_of_Textboxes[num].Count; i++)
            {
                list_of_Textboxes[num][i].BackColor = Color.Azure;
            }
        }

        private void quest1_click(object sender, EventArgs e)
        {
            Selectword(0);
            lo.Text = questions[0].ToString();
        }
        private void quest2_click(object sender, EventArgs e)
        {
            Selectword(1);
        }
        private void quest3_click(object sender, EventArgs e)
        {
            Selectword(2);
            lo.Text = questions[2].ToString();
        }
        private void quest4_click(object sender, EventArgs e)
        {
            Selectword(3);
            lo.Text = questions[3].ToString();
        }
        private void quest5_click(object sender, EventArgs e)
        {
            Selectword(4);
            lo.Text = questions[4].ToString();
        }
        private void quest6_click(object sender, EventArgs e)
        {
            Selectword(5);
            lo.Text = questions[5].ToString();
        }
        private void quest7_click(object sender, EventArgs e)
        {
            Selectword(6);
            lo.Text = questions[6].ToString();
        }
        private void quest8_click(object sender, EventArgs e)
        {
            Selectword(7);
            lo.Text = questions[7].ToString();
        }
        private void quest9_click(object sender, EventArgs e)
        {
            Selectword(8);
            lo.Text = questions[8].ToString();
        }
        private void quest10_click(object sender, EventArgs e)
        {
            Selectword(9);
            lo.Text = questions[9].ToString();
        }
        private void quest11_click(object sender, EventArgs e)
        {
            Selectword(10);
            lo.Text = questions[10].ToString();
        }


    }
}
