using System;
using System.Windows.Forms;

namespace Numbers
{
    public partial class Numbers : Form
    {

        Player playerWindow;
        Computer computerWindow;
        Rules aboutWindow;

        public Numbers()
        {
            InitializeComponent();
        }

        private void playerButton_Click(object sender, EventArgs e)
        {
            this.playerWindow = new Player();
            this.playerWindow.Show();
        }

        private void computerButton_Click(object sender, EventArgs e)
        {
            this.computerWindow = new Computer();
            this.computerWindow.Show();
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            this.aboutWindow = new Rules();
            this.aboutWindow.Show();
        }
    }
}
