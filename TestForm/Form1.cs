using System;
using System.Windows.Forms;

namespace ASCOM.EasyFocus
{
    public partial class Form1 : Form
    {

        private ASCOM.DriverAccess.Focuser driver;

        public Form1()
        {
            InitializeComponent();
            SetUIState();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsConnected)
                driver.Connected = false;

            Properties.Settings.Default.Save();
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DriverId = ASCOM.DriverAccess.Focuser.Choose(Properties.Settings.Default.DriverId);
            SetUIState();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                driver.Connected = false;
                btnMoveIn.Enabled = false;
                btnMoveOut.Enabled = false;
                btnSetPos.Enabled = false;
                textMaxPos.Enabled = false;
                textCurrentPos.Enabled = false;
                textMinPos.Enabled = false;
                textMoveIn.Enabled = false;
                textMoveOut.Enabled = false;
                textStepDelay.Enabled = false;
                txtTemp.Enabled = false;
            }
            else
            {
                driver = new ASCOM.DriverAccess.Focuser(Properties.Settings.Default.DriverId);
                driver.Connected = true;
                btnMoveIn.Enabled = true;
                btnMoveOut.Enabled = true;
                btnSetPos.Enabled = true;
                textMaxPos.Enabled = true;
                textCurrentPos.Enabled = true;
                textMinPos.Enabled = true;
                textMoveIn.Enabled = true;
                textMoveOut.Enabled = true;
                textStepDelay.Enabled = true;
                txtTemp.Enabled = true;
            }
            SetUIState();
        }

        private void SetUIState()
        {
            buttonConnect.Enabled = !string.IsNullOrEmpty(Properties.Settings.Default.DriverId);
            buttonChoose.Enabled = !IsConnected;
            buttonConnect.Text = IsConnected ? "Disconnect" : "Connect";
            if (IsConnected)
            {
                textCurrentPos.Text = driver.Position.ToString();
                textMaxPos.Text = driver.MaxStep.ToString();
                textMinPos.Text = driver.Action("MinStep", "Get");
            }
        }

        private bool IsConnected
        {
            get
            {
                return ((this.driver != null) && (driver.Connected == true));
            }
        }

        private bool ValidNumber(string s)
        {
            int i;
            return int.TryParse(s, out i);
        }

        private void btnMoveIn_Click(object sender, EventArgs e)
        {
            if (!ValidNumber(textMoveIn.Text))
            {
                MessageBox.Show("Invalid Input. Please ener a numeric value only");
                textMoveIn.Focus();
            }
            else if ((driver.Position - Convert.ToInt32(textMoveIn.Text)) < Convert.ToInt32(textMinPos.Text))
            {

                MessageBox.Show("Invalid Input. Value exceeds allowable limits");
                textMoveIn.Focus();
            }
            else
            {
                int newposition = driver.Position - Convert.ToInt32(textMoveIn.Text);
                driver.Move(newposition);
                textCurrentPos.Text = driver.Position.ToString();
            }
            SetUIState();
        }

        private void btnMoveOut_Click(object sender, EventArgs e)
        {
            if (!ValidNumber(textMoveOut.Text))
            {
                MessageBox.Show("Invalid Input. Please ener a numeric value only");
                textMoveOut.Focus();
            }
            else if ((Convert.ToInt32(textMoveOut.Text) + driver.Position) > Convert.ToInt32(textMaxPos.Text))
            {

                MessageBox.Show("Invalid Input. Value exceeds allowable limits");
                textMoveOut.Focus();
            }
            else
            {
                int newposition = driver.Position + Convert.ToInt32(textMoveOut.Text);
                driver.Move(newposition);
                textCurrentPos.Text = driver.Position.ToString();
            }
            SetUIState();
        }

        private void textStepDelay_Leave(object sender, EventArgs e)
        {
            driver.Action("StepDelay", this.textStepDelay.Text);
        }

        private void textMinPos_Leave(object sender, EventArgs e)
        {
            driver.Action("MinStep", this.textMinPos.Text);
        }

        private void textMaxPos_Leave(object sender, EventArgs e)
        {
            driver.Action("MaxStep", this.textMaxPos.Text);
        }

        private void btnSetPos_Click(object sender, EventArgs e)
        {
            if (!ValidNumber(textCurrentPos.Text))
            {
                MessageBox.Show("Invalid Input. Please ener a numeric value only");
                textCurrentPos.Focus();
            }
            else if (Convert.ToInt32(textCurrentPos.Text) > driver.MaxStep)
            {

                MessageBox.Show("Invalid Input. Value exceeds allowable limits");
                textCurrentPos.Focus();
            }
            else if (Convert.ToInt32(textCurrentPos.Text) < Convert.ToInt32(driver.Action("MinStep", "Get")))
            {

                MessageBox.Show("Invalid Input. Value exceeds allowable limits");
                textCurrentPos.Focus();
            }
            else
            {
                textCurrentPos.Text = driver.Action("Position", textCurrentPos.Text);
            }
            SetUIState();
        }

        private void btnGetTemp_Click(object sender, EventArgs e)
        {
            txtTemp.Text = driver.Temperature.ToString();
        }
    }
}
