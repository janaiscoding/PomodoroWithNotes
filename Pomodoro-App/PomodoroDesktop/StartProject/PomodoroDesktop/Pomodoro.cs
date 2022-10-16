using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PomodoroDesktop
{
    public partial class Pomodoro : Form
    {
        DataTable table;
        private const int Seconds_Per_Minute = 60;
        private const int Pomodoro_Minutes = 25;
        private const int Short_Break_Minutes = 5;
        private const int Long_Break_Minutes = 15;
        private int timeLeft;

        public Pomodoro()
        {
            InitializeComponent();
        }
        private void Pomodoro_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("Title", typeof(String));
            table.Columns.Add("Messages", typeof(String));

            dataGridView1.DataSource = table;

            dataGridView1.Columns["Messages"].Visible = false;
            dataGridView1.Columns["Title"].Width = 155;

        }
        private void btnPomodoro_Click(object sender, EventArgs e)
        {
            StartPomodoroCountdown(Pomodoro_Minutes);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           if (IsTimeUp())
            {
                timer1.Stop();
                MessageBox.Show("It's stretching time!", "Gratz!");
                
            }
            else
            {
                this.timeLeft--;
                PomodoroCounter();
            }
        }
        private bool IsTimeUp()
        {
            return this.timeLeft == 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Start();    
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void btnShortBreak_Click(object sender, EventArgs e)
        {
            StartPomodoroCountdown(Short_Break_Minutes);
        }

        private void btnLongBreak_Click(object sender, EventArgs e)
        {
            StartPomodoroCountdown(Long_Break_Minutes);
        }
        private void PomodoroCounter()
        {
            lblMinutes.Text = (this.timeLeft / Seconds_Per_Minute).ToString("00");
            lblSeconds.Text = (this.timeLeft % Seconds_Per_Minute).ToString("00");
        }

        private void StartPomodoroCountdown(int minutesToCountdown)
        {
            this.timeLeft = minutesToCountdown * Seconds_Per_Minute;
            PomodoroCounter();
            timer1.Start();
        }

        private void btn_NewNote_Click(object sender, EventArgs e)
        {
            txt_Title.Clear();
            txt_Message.Clear();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            table.Rows.Add(txt_Title.Text, txt_Message.Text);

            txt_Title.Clear();
            txt_Message.Clear();
        }

        private void btn_Read_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            if (index > -1)
            {
                txt_Title.Text = table.Rows[index].ItemArray[0].ToString();
                txt_Message.Text = table.Rows[index].ItemArray[1].ToString();
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            table.Rows[index].Delete();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
