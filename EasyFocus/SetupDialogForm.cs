using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ASCOM.Utilities;
using ASCOM.EasyFocus;

namespace ASCOM.EasyFocus
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        Focuser _Focuser;
        //private ASCOM.DriverAccess.Focuser Focuser;
        public SetupDialogForm()
        {
            InitializeComponent();
            // Initialise current values of user settings from the ASCOM Profile 
            comboBoxComPort.Items.Clear();
            using (ASCOM.Utilities.Serial serial = new Utilities.Serial())
            {
                foreach (var item in serial.AvailableCOMPorts)
                {
                    comboBoxComPort.Items.Add(item);
                }
            }

            comboBoxComPort.Text = Focuser.comPort;
            chkTrace.Checked = Focuser.traceState;
            _Focuser = new Focuser();
            textStepDelay.Text = _Focuser.Action("StepDelay", "Get");
            textCurrentPos.Text = _Focuser.Position.ToString();
            textMaxPos.Text = _Focuser.MaxStep.ToString();
            textMinPos.Text = _Focuser.Action("MinStep", "Get");


        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            //_Focuser.ReadProfile();
            Focuser.comPort = comboBoxComPort.Text; 
            Focuser.traceState = chkTrace.Checked;

            _Focuser.Action("MaxStep", this.textMaxPos.Text);
            _Focuser.Action("MinStep", this.textMinPos.Text);
            _Focuser.Action("Position", this.textCurrentPos.Text);
            _Focuser.Action("StepDelay", this.textStepDelay.Text);
            _Focuser.WriteProfile();
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            //Focuser = new Focuser();
            //textCurrentPos.Text = Focuser.Position.ToString();
            //textMaxPos.Text = Focuser.MaxStep.ToString();
            //textMinPos.Text = Focuser.Action("MinStep", "Get");
            //textStepDelay.Text = Focuser.Action("StepDelay", "Get");
        }

        private bool ValidNumber(string s)
        {
            int i;
            return int.TryParse(s, out i);
        }

        private void textMinPos_Leave(object sender, EventArgs e)
        {
            if (!ValidNumber(textMinPos.Text))
            {
                MessageBox.Show("Invalid Input. Please ener a numeric value only");
                textMinPos.Focus();
            }
            else if (Convert.ToInt16(textMinPos.Text) >= Convert.ToInt16(textMaxPos.Text))
            {
                MessageBox.Show("Invalid Input. The minimum position must be less than the maximum position");
                textMinPos.Focus();
            }
            else if (Convert.ToInt16(textMinPos.Text) > Convert.ToInt16(textCurrentPos.Text))
            {
                MessageBox.Show("Invalid Input. The minimum position must be less than or equal to the current position");
                textMinPos.Focus();
            }

        }

        private void textMaxPos_Leave(object sender, EventArgs e)
        {
            if (!ValidNumber(textMaxPos.Text))
            {
                MessageBox.Show("Invalid Input. Please ener a numeric value only");
                textMaxPos.Focus();
            }
            else if (Convert.ToInt16(textMaxPos.Text) <= Convert.ToInt16(textMinPos.Text))
            {
                MessageBox.Show("Invalid Input. The maximum position must be greater than the minimum position");
                textMaxPos.Focus();
            }
            else if (Convert.ToInt16(textMaxPos.Text) < Convert.ToInt16(textCurrentPos.Text))
            {
                MessageBox.Show("Invalid Input. The maximum position must be greater than or equal to the current position");
                textMaxPos.Focus();
            }
        }

        private void textStepDelay_Leave(object sender, EventArgs e)
        {
            if (!ValidNumber(textStepDelay.Text))
            {
                MessageBox.Show("Invalid Input. Please ener a numeric value only");
                textStepDelay.Focus();
            }
        }

        private void textCurrentPos_Leave(object sender, EventArgs e)
        {
            if (!ValidNumber(textCurrentPos.Text))
            {
                MessageBox.Show("Invalid Input. Please ener a numeric value only");
                textCurrentPos.Focus();
            }
            else if (Convert.ToInt16(textCurrentPos.Text) > Convert.ToInt16(textMaxPos.Text))
            {

                MessageBox.Show("Invalid Input. Value exceeds allowable limits");
                textCurrentPos.Focus();
            }
            else if (Convert.ToInt32(textCurrentPos.Text) < Convert.ToInt16(textMinPos.Text))
            {

                MessageBox.Show("Invalid Input. Value exceeds allowable limits");
                textCurrentPos.Focus();
            }
        }
    }
}