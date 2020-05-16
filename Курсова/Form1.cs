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
        public int direction = 1;
        public int changedirection = -1;
        public List<TextBox> scale = new List<TextBox>();

        public Panel startpanel = new Panel();
        public Panel diffchoosepanel = new Panel();
        public Panel rulespanel = new Panel();
        public Panel stats = new Panel();

        public Button diff1 = new Button();
        public Button diff2 = new Button();
        public Button diff3 = new Button();

        public Label diflabel = new Label();
        public Label gametimelabel = new Label();
        public Label checkslabel = new Label();
        public Label scorelabel = new Label();
        public Label scorelabel2 = new Label();
        public Label secs = new Label();

        public int dif = 0;
        public int score = 0;
        public int gametime = 0;
        public int checks = 0;

        public List<int> unrepeat = new List<int>();
        public Button check = new Button();
        public int multtime = 1;
        public double percents = 0.1;
        public Panel loadpanel = new Panel();
        public Label lo = new Label();
        public Label percentslabel = new Label();

        void Buttonsdestroy()/*Метод видалення кнопок рівнів складності*/
        {
            diff1.Dispose();
            diff2.Dispose();
            diff3.Dispose();
        }

        public new void Menu()/*стартове меню*/
        {
            startpanel.Location = new Point(0, 0);
            startpanel.Size = new Size(1200, 900);
            startpanel.BackgroundImage = Image.FromFile("Crossword.png");
            startpanel.Click += new EventHandler(this.startpanel_Click);
            Controls.Add(startpanel);
        }
        public static int CheckLocation(ref List<List<TextBox>> list, Point pos, char verb, ref List<TextBox> verbs, ref List<string> true_ans)
        {
            /*Метод перевіре наявність текстбокса з певними кординатами та його літеру*/
            for (int k = 0; k < list.Count; k++)
            {
                for (int l = 0; l < list[k].Count; l++)
                {
                    if (pos.ToString() == list[k][l].Location.ToString())
                    {
                        if (true_ans[k][l] == verb)
                        {
                            verbs.Add(list[k][l]);
                            return 1;/*Текстбокс знайдений та його літера співпадае з літерою новостворенного текстбокса*/
                        }
                        else
                            return -1;/*Текстбокс знайдений та його літера не співпадае з літерою новостворенного текстбокса*/
                    }
                }
            }
            return 0;/*Текстбокс не знайдений*/
        }
        private void diff3_click(object sender, EventArgs e)
        {
            dif = 3;
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
            dif = 2;
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
            dif = 1;
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
        void Rulespanel()
        {
            rulespanel.BackgroundImage = Image.FromFile("Rules.png");
            rulespanel.Location = new Point(0, 0);
            rulespanel.Size = new Size(1200, 900);
            Controls.Add(rulespanel);
            rulespanel.Click += new EventHandler(rulespanel_click);
        }

        private void rulespanel_click(object sender, EventArgs e)
        {
            rulespanel.Dispose();
            Controls.Add(diff1);
            Controls.Add(diff2);
            Controls.Add(diff3);
            Controls.Add(diffchoosepanel);
        }

        private void startpanel_Click(object sender, EventArgs e)
        {
            BackColor = ColorTranslator.FromHtml("#202020");
            diffchoosepanel.BackgroundImage = Image.FromFile("Diffchoose.png");
            diffchoosepanel.Location = new Point(0, 0);
            diffchoosepanel.Size = new Size(1200, 900);
            startpanel.Dispose();
            Rulespanel();
        }

        public Form1()
        {
            InitializeComponent();
            lo.Location = new Point(300, 25);
            lo.Size = new Size(600, 50);
            lo.ForeColor = Color.Orange;

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

            diflabel.Location = new Point(590, 310);
            diflabel.Size = new Size(130, 20);
            diflabel.TextAlign = ContentAlignment.MiddleRight;
            diflabel.Font = new Font("Times New Roman", this.Height / 35);
            diflabel.BackColor = ColorTranslator.FromHtml("#fb9b49");
            diflabel.Text = "none";

            gametimelabel.Location = new Point(590, 365);
            gametimelabel.Size = new Size(130, 20);
            gametimelabel.TextAlign = ContentAlignment.MiddleRight;
            gametimelabel.Font = new Font("Times New Roman", this.Height / 35);
            gametimelabel.BackColor = ColorTranslator.FromHtml("#fb9b49");
            gametimelabel.Text = gametime / 60 / 10 + "" + gametime / 60 % 10 + ":" + gametime % 60 / 10 + "" + gametime % 60 % 10;

            checkslabel.Location = new Point(590, 420);
            checkslabel.Size = new Size(130, 20);
            checkslabel.TextAlign = ContentAlignment.MiddleRight;
            checkslabel.Font = new Font("Times New Roman", this.Height / 35);
            checkslabel.BackColor = ColorTranslator.FromHtml("#fb9b49");
            checkslabel.Text = checks.ToString();

            scorelabel.Location = new Point(590, 480);
            scorelabel.Size = new Size(130, 20);
            scorelabel.TextAlign = ContentAlignment.MiddleRight;
            scorelabel.Font = new Font("Times New Roman", this.Height / 35);
            scorelabel.BackColor = ColorTranslator.FromHtml("#fb9b49");

            scorelabel2.Location = new Point(1100, 200);
            scorelabel2.Size = new Size(150, 50);
            scorelabel2.TextAlign = ContentAlignment.MiddleLeft;
            scorelabel2.Font = new Font("Times New Roman", this.Height / 35);
            scorelabel2.BackColor = ColorTranslator.FromHtml("#202020");
            scorelabel2.ForeColor = ColorTranslator.FromHtml("#ffb26b");

            diff1.FlatStyle = FlatStyle.Flat;
            diff2.FlatStyle = FlatStyle.Flat;
            diff3.FlatStyle = FlatStyle.Flat;
            diff1.BackColor = ColorTranslator.FromHtml("#ffb26b");
            diff2.BackColor = ColorTranslator.FromHtml("#ffb26b");
            diff3.BackColor = ColorTranslator.FromHtml("#ffb26b");

            diff1.Click += new EventHandler(diff1_click);
            diff2.Click += new EventHandler(diff2_click);
            diff3.Click += new EventHandler(diff3_click);
            ClientSize = new Size(1200, 900);
            Launch();
            Menu();

        }
        void Launch()/*Чорний єкран на початку*/
        {
            {
                {
                    Panel launch = new Panel();
                    launch.Location = new Point(0, 0);
                    launch.Size = new Size(1200, 900);


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

                    Controls.Add(launch);
                    Paint += (sender, args) =>
                    {
                        switch (time)
                        {
                            default:
                                launch.BackColor = ColorTranslator.FromHtml("#202020");
                                break;
                            case 6:
                                timer.Stop();
                                launch.Dispose();
                                break;
                        }
                    };
                }

            }

        }
        void LoadScreen()/*Приховує процес побудови кросворду*/
        {

            percentslabel.Location = new Point(550, 400);
            percentslabel.ForeColor = ColorTranslator.FromHtml("#ffb26b");
            percentslabel.BackColor = ColorTranslator.FromHtml("#202020");
            percentslabel.Text = Math.Truncate(percents).ToString();
            percentslabel.Size = new Size(300, 300);
            percentslabel.Font = new Font("Times New Roman", this.Height / 20);
            Controls.Add(percentslabel);

            loadpanel.Location = new Point(0, 0);
            loadpanel.Size = new Size(1200, 900);
            loadpanel.BackColor = ColorTranslator.FromHtml("#202020");
            Controls.Add(loadpanel);


        }


        void sec(bool start)/*Секундомер гри*/
        {
            Timer tm = new Timer();
            tm.Tick += new EventHandler(tm_Tick);
            tm.Interval = 1000;
            {
                if (start)
                {
                    gametime++;
                    Invalidate();
                }
            };
            tm.Stop();
            score = 0;
            checks = 0;
            gametime = 0;
            tm.Start();



            void tm_Tick(object sender, EventArgs e)
            {
                scorelabel2.Text = "Рахунок:\n" + score.ToString();
                secs.Text = "Час:\n" + gametime / 60 / 10 + "" + gametime / 60 % 10 + ":" + gametime % 60 / 10 + "" + gametime % 60 % 10;
                gametime++;
            }


        }
        void Main(int verb8, int verb7, int verb6, int verb5, int verb4)/*Метод розподілення слів до генерації відносно рівня складності*/
        {
            int range_top_left_angle = 3;//*50-in points
            int range_bottom_right_angle = 16;//*50-in points
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            var rand = new Random();
            percents = 0.1;

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

            percentslabel.Dispose();
            lo.Font = new Font("Times New Roman", this.Height / 60);
            lo.TextAlign = ContentAlignment.MiddleCenter;
            check.Location = new Point(1050, 100);
            check.Size = new Size(100, 25);
            check.Text = "Перевірка";
            check.BackColor = ColorTranslator.FromHtml("#ffb26b");
            check.Font = new Font("Times New Roman", this.Height / 80);
            check.FlatStyle = FlatStyle.Flat;
            Controls.Add(check);
            Controls.Add(lo);

            check.Click += new EventHandler(check_click);

            /*Початок методу центрування кросворду*/
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
            int deltaX = 600 - (minX + maxX) / 2;
            int deltaY = 450 - (minY + maxY) / 2;
            for (int i = 0; i < list_of_Textboxes.Count; i++)
            {
                for (int j = 0; j < list_of_Textboxes[i].Count; j++)
                {
                    foreach (TextBox p in scale)
                    {
                        if (p == list_of_Textboxes[i][j])
                            goto skip;
                    }
                    list_of_Textboxes[i][j].Location = new Point(list_of_Textboxes[i][j].Location.X + deltaX, list_of_Textboxes[i][j].Location.Y + deltaY);
                    scale.Add(list_of_Textboxes[i][j]);
                skip:;
                }
            }
            /*Кінець методу центування*/

            sec(true);/*запуск секундомера*/
            secs.Location = new Point(1100, 150);
            secs.Size = new Size(100, 25);
            secs.BackColor = ColorTranslator.FromHtml("#202020");
            secs.ForeColor = ColorTranslator.FromHtml("#ffb26b");
            Controls.Add(secs);
            Controls.Add(scorelabel2);
            loadpanel.Dispose();

            void Word(int num, int lg)/*Алгоритм генерації*/
            {
                /*Випадковим чином бере слово з БД та перевія його на можливість додання до кросворду*/
                for (int j = 0; j < num; j++)
                {
                restart:
                    changedirection++;
                    int randword = rand.Next(1, 99);

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
                    //lo.Text = "Слoво в обробці:   " + command.ExecuteScalar().ToString() + "\nСтворено слів:  " + list_of_Textboxes.Count + "\nНапрямок:     " + direction;

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
                            int px = 50 * row; int py = 50 * col;

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

                                switch (CheckLocation(ref list_of_Textboxes, new Point(px, py), command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers))
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
                                                if (CheckLocation(ref list_of_Textboxes, pos_right, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0)
                                                    goto Skip;
                                            }
                                            else
                                                if (i == 0)
                                                if (CheckLocation(ref list_of_Textboxes, pos_left, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0)
                                                    goto Skip;
                                        }
                                        else
                                        {
                                            if (i == command.ExecuteScalar().ToString().Length - 1)
                                            {

                                                if (CheckLocation(ref list_of_Textboxes, pos_buttom, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0)
                                                    goto Skip;
                                            }
                                            if (i == 0)
                                            {
                                                if (CheckLocation(ref list_of_Textboxes, pos_top, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0)
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
                                            if (CheckLocation(ref list_of_Textboxes, pos_top, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0
                                                || CheckLocation(ref list_of_Textboxes, pos_left, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0)
                                                goto Skip;
                                        }

                                        if (direction == 1)
                                        {
                                            if (CheckLocation(ref list_of_Textboxes, pos_top, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0 ||
                                                CheckLocation(ref list_of_Textboxes, pos_buttom, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0)
                                                goto Skip;

                                            if (i == command.ExecuteScalar().ToString().Length - 1)//last verb
                                            {
                                                if (CheckLocation(ref list_of_Textboxes, pos_right, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0)
                                                    goto Skip;
                                            }
                                        }
                                        else
                                        {
                                            if (CheckLocation(ref list_of_Textboxes, pos_left, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0 ||
                                                CheckLocation(ref list_of_Textboxes, pos_right, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0)
                                                goto Skip;

                                            if (i == command.ExecuteScalar().ToString().Length - 1)//last verb
                                            {
                                                if (CheckLocation(ref list_of_Textboxes, pos_buttom, command.ExecuteScalar().ToString()[i], ref verbs, ref true_answers) != 0)
                                                    goto Skip;
                                            }
                                        }
                                        if (px > 1200 || py > 900)
                                            goto Skip;

                                        verbs.Add(new TextBox());
                                        verbs[i].Name = "w" + j.ToString() + "k" + i.ToString();
                                        verbs[i].Location = new Point(px, py);
                                        verbs[i].Size = new Size(50, 50);
                                        verbs[i].Multiline = true;
                                        verbs[i].MaxLength = 1;
                                        verbs[i].TextAlign = HorizontalAlignment.Center;
                                        verbs[i].CharacterCasing = CharacterCasing.Upper;
                                        verbs[i].Font = new Font("Times New Roman", this.Height / 30);
                                        verbs[i].BackColor = ColorTranslator.FromHtml("#ffb26b");
                                        verbs[i].BorderStyle = BorderStyle.Fixed3D;
                                        if (direction == 1)
                                            px += 50;
                                        else
                                            py += 50;

                                        //verbs[i].Text = command.ExecuteScalar().ToString()[i].ToString();
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
                            percents += 9.09;
                            percentslabel.Text = Math.Truncate(percents).ToString() + "%";
                        Skip:;
                        }
                    }
                    goto restart;

                end:;
                }
            }


        }
        public void EndGame()/*Метод закінчення гри та виведення статистики*/
        {
            scorelabel2.Dispose();
            secs.Dispose();
            for (int i = 0; i < list_of_Textboxes.Count; i++)
            {
                for (int j = 0; j < list_of_Textboxes[i].Count; j++)
                {
                    list_of_Textboxes[i][j].Dispose();
                }
            }
            lo.Dispose();
            check.Dispose();
            BackgroundImage = Image.FromFile("Results3.png");
            checkslabel.Text = checks.ToString();

            gametimelabel.Text = "X" + multtime + ".    " + gametime / 60 / 10 + "" + gametime / 60 % 10 + ":" + gametime % 60 / 10 + "" + gametime % 60 % 10;
            switch (dif)
            {
                case 1:
                    diflabel.Text = "Легкий";
                    break;
                case 2:
                    diflabel.Text = "Середній";
                    break;
                case 3:
                    diflabel.Text = "Складний";
                    break;
                default:
                    break;

            }
            multtime = 1;
            if ((gametime / 60) < 5)
                multtime = 3;
            else
                 if ((gametime / 60) < 10)
                multtime = 2;

            checkslabel.Text = "X(1/" + checks + ")    " + checks.ToString();
            gametimelabel.Text = "X" + multtime + "    " + gametime / 60 / 10 + "" + gametime / 60 % 10 + ":" + gametime % 60 / 10 + "" + gametime % 60 % 10;
            scorelabel.Text = "X(" + multtime + "/" + checks + ")   " + score * multtime / checks;
            Controls.Add(scorelabel);
            Controls.Add(checkslabel);
            Controls.Add(gametimelabel);
            Controls.Add(diflabel);
        }

        private void check_click(object sender, EventArgs e)/*Кнопка перевірки*/
        {
            checks++;
            bool alldone = true;
            for (int i = 0; i < list_of_Textboxes.Count; i++)
            {
                string conc = string.Empty;
                for (int j = 0; j < list_of_Textboxes[i].Count; j++)
                {
                    conc = String.Concat(conc, list_of_Textboxes[i][j].Text);
                }
                if (true_answers[i] == conc)
                    ChangeColor(i);
                else
                    alldone = false;
            }
            Scorecalc();
            if (alldone)
                EndGame();
        }
        void Scorecalc()/*Метод вираховування очок*/
        {
            for (int i = 0; i < list_of_Textboxes.Count; i++)
            {
                bool norepeat = true;
                foreach (int j in unrepeat)
                {
                    if (i == j)
                    {
                        norepeat = false;
                        break;
                    }
                }
                if (list_of_Textboxes[i][0].Enabled == false && norepeat)
                {
                    unrepeat.Add(i);
                    score += 305 * list_of_Textboxes[i].Count;
                }

            }

        }
        void ChangeColor(int word)/*Зафарбовує в зелений та блокує правильно введені слова*/
        {

            for (int j = 0; j < list_of_Textboxes[word].Count; j++)
            {
                list_of_Textboxes[word][j].BackColor = Color.LightGreen;
                list_of_Textboxes[word][j].Enabled = false;
            }
        }

        void Selectword(int num)/*Виділянє усе слово при натисканні на 1 з його текстбоксів*/
        {
            for (int i = 0; i < list_of_Textboxes.Count; i++)
            {
                for (int j = 0; j < list_of_Textboxes[i].Count; j++)
                {
                    if (list_of_Textboxes[i][j].Enabled)
                        list_of_Textboxes[i][j].BackColor = ColorTranslator.FromHtml("#ffb26b");
                }
            }
            for (int i = 0; i < list_of_Textboxes[num].Count; i++)
            {
                if (list_of_Textboxes[num][i].Enabled)
                    list_of_Textboxes[num][i].BackColor = Color.Azure;
            }
        }

        /*Події для натискання на текстбокси, які є частинами слів. Хотілося би це реалізувати 1 циклом*/
        private void quest1_click(object sender, EventArgs e)
        {
            Selectword(0);
            lo.Text = questions[0].ToString();
        }
        private void quest2_click(object sender, EventArgs e)
        {
            Selectword(1);
            lo.Text = questions[1].ToString();
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
