//tabs=4
// --------------------------------------------------------------------------------
// TODO fill in this information for your driver, then remove this line!
//
// ASCOM Focuser driver for EasyFocus
//
// Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
//				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
//				erat, sed diam voluptua. At vero eos et accusam et justo duo 
//				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
//				sanctus est Lorem ipsum dolor sit amet.
//
// Implements:	ASCOM Focuser interface version: <To be completed by driver developer>
// Author:		(XXX) Your N. Here <your@email.here>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 17-Aug-2014	MIT	0.1.1	Initial edit, created from ASCOM driver template
// 30-Oct-2017  MIT 0.2.0   Added temp monitoring
// --------------------------------------------------------------------------------
//


// This is used to define code in the template that is specific to one class implementation
// unused code canbe deleted and this definition removed.
#define Focuser

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;

using ASCOM;
using ASCOM.Astrometry;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;
using System.Globalization;
using System.Collections;

namespace ASCOM.EasyFocus
{
    //
    // Your driver's DeviceID is ASCOM.EasyFocus.Focuser
    //
    // The Guid attribute sets the CLSID for ASCOM.EasyFocus.Focuser
    // The ClassInterface/None addribute prevents an empty interface called
    // _EasyFocus from being created and used as the [default] interface
    //
    // TODO Replace the not implemented exceptions with code to implement the function or
    // throw the appropriate ASCOM exception.
    //


    /// <summary>
    /// ASCOM Focuser Driver for EasyFocus.
    /// </summary>
    [Guid("080f5ec5-3d5b-422d-abc0-557e390ca2d2")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Focuser : IFocuserV2
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.EasyFocus.Focuser";
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "ASCOM Focuser Driver for EasyFocus.";

        internal static string comPortProfileName = "COM Port"; // Constants used for Profile persistence
        internal static string comPortDefault = "COM1";
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";
        internal static string positionProfileName = "Focuser Position";
        internal static string positionDefault = "3000";
        internal static string minPositionProfileName = "Min Focuser Position";
        internal static string minPositionDefault = "0";
        internal static string maxPositionProfileName = "Max Focuser Position";
        internal static string maxPositionDefault = "10000";
        internal static string stepDelayProfileName = "Step Delay";
        internal static string stepDelayDefault = "20";


 

        internal static string comPort; // Variables to hold the currrent device configuration
        internal static bool traceState;

        //private ArduinoComms m_serial = new ArduinoComms();  //Creates the object that points to the serial port
        private ArduinoComms m_serial;

        // Define the Arduino comms parameters
        const byte HELLO = 199; //199 tells the Arduino that this is a comms test. respond with "!" (byte = 33)
        const byte MOVE_IN = 251; // tells the Arduino to move in by 1 step
        const byte MOVE_OUT = 252; // tells the Arduino to move out by 1 step
        const byte GET_CUR_POS = 198; // Requests that the Arduino send it's current position
        const byte WRITE_POSITION = 197; // Advises the Arduino that it's about to receive a position update
        const byte GET_CUR_TEMP = 196; // Requests that current temperature from the Arduino's LM335

        const bool MOVE_FOCUSER_IN = true;
        const bool MOVE_FOCUSER_OUT = false;

        const bool ABSOLUTE = true;

        

        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        private bool connectedState;

        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        private AstroUtils astroUtilities;

        /// <summary>
        /// Private variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        private TraceLogger tl;

        /// <summary>
        /// Initializes a new instance of the <see cref="EasyFocus"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public Focuser()
        {
            ReadProfile(); // Read device configuration from the ASCOM Profile store

            tl = new TraceLogger("", "EasyFocus");
            tl.Enabled = traceState;
            tl.LogMessage("Focuser", "Starting initialisation");

            connectedState = false; // Initialise connected to false
            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro utilities object
            //TODO: Implement your additional construction here

            tl.LogMessage("Focuser", "Completed initialisation");
        }


        //
        // PUBLIC COM INTERFACE IFocuserV2 IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            //if (IsConnected)
            //    System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm())
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    ReadProfile();
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                ArrayList Al = new ArrayList();
                Al.Add("StepDelay");
                Al.Add("MaxStep");
                Al.Add("MinStep");
                Al.Add("Position");
                return Al;
                //tl.LogMessage("SupportedActions Get", "Returning empty arraylist");
                //return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            if (actionName == "StepDelay")
            {
                if (actionParameters == "Get")
                {
                    return StepDelay.ToString();
                }
                else
                {
                    StepDelay = Convert.ToInt16(actionParameters);
                    return StepDelay.ToString();
                }
            }
                    
            else if(actionName == "MaxStep")
            {
                MaxStep = Convert.ToInt16(actionParameters);
                return MaxStep.ToString();
            }
            else if (actionName == "MinStep")
            {
                if (actionParameters == "Get")
                {
                    return MinStep.ToString();
                }
                else
                {
                    MinStep = Convert.ToInt16(actionParameters);
                    return MinStep.ToString();
                }
            }
            else if (actionName == "Position")
            {
                focuserPosition = Convert.ToInt16(actionParameters);
                if(m_serial != null)
                m_serial.SetCurPos(focuserPosition);
                return Position.ToString();
            }
            else
            {
                return "?";
            }
            //throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            // Call CommandString and return as soon as it finishes
            this.CommandString(command, raw);
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            string ret = CommandString(command, raw);
            // TODO decode the return string and return true or false
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            // it's a good idea to put all the low level communication with the device here,
            // then all communication calls this function
            // you need something to ensure that only one command is in progress at a time

            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public void Dispose()
        {
            // Clean up the tracelogger and util objects
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
            utilities.Dispose();
            utilities = null;
            astroUtilities.Dispose();
            astroUtilities = null;
        }

        public bool Connected
        {
            get
            {
                tl.LogMessage("Connected Get", IsConnected.ToString());
                return IsConnected;
            }
            set
            {
                tl.LogMessage("Connected Set", value.ToString());
                if (value == IsConnected)
                    return;

                if (value)
                {
                    connectedState = true;
                    tl.LogMessage("Connected Set", "Connecting to port " + comPort);
                    try
                    {
                        m_serial = new ArduinoComms();
                        m_serial.PortName = comPort.ToString();
                        //m_serial.Open();
                        //m_serial.Close();
                    }
                    catch (System.Exception ex)
                    {
                        //Error encountered when trying to establish a link to the arduino
                        connectedState = false;
                        throw new System.Exception(ex.Message);
                    }

                    //m_serial seems to have connected ok 
                    //so let's see if we can get a response from the Arduino.
                    for (int i = 0; i < 2; i++)
                    {
                        int serial_response = m_serial.TestConnection(HELLO);
                        if (serial_response == 33)
                        {
                            //success
                            focuserPosition = m_serial.GetData(GET_CUR_POS);
                            connectedState = true;
                            // Start the small counter window in a separate thread
                            //oThread.IsBackground = true;
                           // oThread.Start();
                            //m_serial.Open();
                            //m_serial.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
                            break;
                        }
                        else if (serial_response == 99)
                        {
                            if (i == 0)
                            {
                                System.Threading.Thread.Sleep(500);
                            }
                            else
                            {
                                throw new Exception("Connection Test to Arduino Focuser timed out.");
                            }
                        }
                        else
                        {
                            //failure
                            connectedState = false;
                            throw new Exception("Connection test to Arduino Focuser failed.");
                        }
                    }
                }
                else
                {
                    connectedState = false;
                    tl.LogMessage("Connected Set", "Disconnecting from port " + comPort);
                    connectedState = false;
                }
            }
        }

        public string Description
        {
            // TODO customise this device description
            get
            {
                tl.LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = "Driver for Auduino EasyFocus Focuser: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                tl.LogMessage("InterfaceVersion Get", "2");
                return Convert.ToInt16("2");
            }
        }

        public string Name
        {
            get
            {
                string name = "EasyFocus";
                tl.LogMessage("Name Get", name);
                return name;
            }
        }


        #endregion

        #region IFocuser Implementation

        private int focuserPosition = 0; // Class level variable to hold the current focuser position
        private const int focuserSteps = 10000;
        private bool isMoving = false;
        private int maxStep = 10000;
        private int minStep = -10000;
        private int stepDelay; //delay between steps (in ms)

        public bool Absolute
        {
            get
            {
                tl.LogMessage("Absolute Get", ABSOLUTE.ToString());
                return ABSOLUTE; // This is an absolute focuser
            }
        }

        public void Halt()
        {
            tl.LogMessage("Halt", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("Halt");
        }

        public bool IsMoving
        {
            get
            {
                tl.LogMessage("IsMoving Get", isMoving.ToString());
                return isMoving; 
            }
        }

        public bool Link
        {
            get
            {
                tl.LogMessage("Link Get", this.Connected.ToString());
                return this.Connected; // Direct function to the connected method, the Link method is just here for backwards compatibility
            }
            set
            {
                tl.LogMessage("Link Set", value.ToString());
                this.Connected = value; // Direct function to the connected method, the Link method is just here for backwards compatibility
            }
        }

        public int MaxIncrement
        {
            get
            {
                //Todo
                tl.LogMessage("MaxIncrement Get", focuserSteps.ToString());
                return focuserSteps; // Maximum change in one move
            }
        }

        public int MaxStep
        {
            get
            {
                tl.LogMessage("MaxStep Get", maxStep.ToString());
                return maxStep; // Maximum extent of the focuser, so position range is MinStep to MaxStep
            }
            set
            {
                tl.LogMessage("MaxStep Set", maxStep.ToString());
                maxStep = value;
            }
        }

        public int MinStep
        {
            get
            {
                tl.LogMessage("MinStep Get", minStep.ToString());
                return minStep; // Minimum extent of the focuser, so position range is MinStep to MaxStep
            }
            set
            {
                tl.LogMessage("MaxStep Set", minStep.ToString());
                minStep = value;
            }
        }

        public void Move(int newPosition)
        {
            tl.LogMessage("Move", Position.ToString());

            bool moveResult;
            switch (ABSOLUTE)
            {
                case true: //Absolute Focuser
                    if (this.isMoving)
                    {
                        moveResult = false;
                        throw new Exception("Unable to perform requested Move command. The focuser is already moving");
                    }
                    this.isMoving = true;
                    int intMoveAmount;
                    if (newPosition < minStep || newPosition > maxStep)
                    {
                        //Invalid "val" value. 
                        throw new Exception("Move(int val) - val contains an invalid value. " + newPosition.ToString() + " Allowable values = " + minStep.ToString() + " to " + maxStep.ToString());
                    }
                    intMoveAmount = newPosition - focuserPosition;
                    if (focuserPosition + intMoveAmount > maxStep)
                        intMoveAmount = maxStep - focuserPosition;
                    if (focuserPosition + intMoveAmount < minStep)
                        intMoveAmount = -focuserPosition;
                    if (intMoveAmount < 0)
                    {
                        intMoveAmount = Math.Abs(intMoveAmount);
                        if (m_serial.GotoPos(MOVE_OUT, intMoveAmount, stepDelay))
                        //{ focuserPosition = newPosition; }
                        { focuserPosition = m_serial.GetData(GET_CUR_POS); }
                        else
                        {
                            //TODO
                        }
                    }
                    else
                    {
                        if (m_serial.GotoPos(MOVE_IN, intMoveAmount, stepDelay))
                        //{ focuserPosition = newPosition; }
                        { focuserPosition = m_serial.GetData(GET_CUR_POS); }
                        else
                        {
                            //TODO
                        }
                    }
                    this.isMoving = false;
                    WritePosition();
                    break;

                case false: //Relative Focuser

                    break;
            }
        }

        public int Position
        {
            get
            {
                if(Connected) return m_serial.GetData(GET_CUR_POS);
                else
                return focuserPosition; // Return the focuser position
            }
        }

        public double StepSize
        {
            get
            {
                tl.LogMessage("StepSize Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("StepSize", false);
            }
        }

        public bool TempComp
        {
            get
            {
                tl.LogMessage("TempComp Get", false.ToString());
                return false;
            }
            set
            {
                tl.LogMessage("TempComp Set", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("TempComp", false);
            }
        }

        public bool TempCompAvailable
        {
            get
            {
                tl.LogMessage("TempCompAvailable Get", false.ToString());
                return false; // Temperature compensation is not available in this driver
            }
        }

        public double Temperature
        {
            get
            {
                int t;
                tl.LogMessage("Temperature Get", "Not implemented");
                t = m_serial.GetData(GET_CUR_TEMP);
                return t;
            }
        }

        private int StepDelay
        {
            get
            {
                tl.LogMessage("StepDelay Get", stepDelay.ToString());
                return stepDelay;
            }
            set
            {
                tl.LogMessage("StepDelay Set", stepDelay.ToString());
                stepDelay = value; 
            }
        }

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "Focuser";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected
        {
            get
            {
                // TODO check that the driver hardware connection exists and is connected to the hardware
                return connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Focuser";
                traceState = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
                comPort = driverProfile.GetValue(driverID, comPortProfileName, string.Empty, comPortDefault);
                focuserPosition = Convert.ToInt16( driverProfile.GetValue(driverID, positionProfileName, string.Empty, positionDefault));
                maxStep = Convert.ToInt16(driverProfile.GetValue(driverID, maxPositionProfileName, string.Empty, maxPositionDefault));
                minStep = Convert.ToInt16(driverProfile.GetValue(driverID, minPositionProfileName, string.Empty, minPositionDefault));
                stepDelay = Convert.ToInt16(driverProfile.GetValue(driverID, stepDelayProfileName, string.Empty, stepDelayDefault));

            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Focuser";
                driverProfile.WriteValue(driverID, traceStateProfileName, traceState.ToString());
                driverProfile.WriteValue(driverID, comPortProfileName, comPort.ToString());
                driverProfile.WriteValue(driverID, positionProfileName, focuserPosition.ToString());
                driverProfile.WriteValue(driverID, maxPositionProfileName, maxStep.ToString());
                driverProfile.WriteValue(driverID, minPositionProfileName, minStep.ToString());
                driverProfile.WriteValue(driverID, stepDelayProfileName, stepDelay.ToString());
            }
        }

        /// <summary>
        /// Write the focuser positionto the  ASCOM  Profile store
        /// </summary>
        internal void WritePosition()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Focuser";
                driverProfile.WriteValue(driverID, positionProfileName, focuserPosition.ToString());
            }
        }

        /// <summary>
        /// Send the comand to the Arduino
        /// </summary>
        //public bool SendToFocuser(byte cmd, int Steps, int stepdelay)
        //{
        //    bool result = true;
        //    int b1, b2, b3, b4;
        //    int response;
        //    int temperature;

        //    if (!m_serial.IsOpen)
        //    {
        //        m_serial.PortName = comPort.ToString();
        //        m_serial.ReadTimeout = 2000;
        //        m_serial.WriteTimeout = 2000;
        //        m_serial.Open();
        //    }
        //    byte[] direction = new byte[1];
        //    direction[0] = cmd;
        //    m_serial.DataReceived -= new SerialDataReceivedEventHandler(comPort_DataReceived);
        //    for (int i = 0; i < Steps; i++)
        //    {
        //        m_serial.Write(direction, 0, direction.Length);

        //        try
        //        {
        //            b1 = m_serial.ReadByte();
        //            b2 = m_serial.ReadByte();
        //            b3 = m_serial.ReadByte();
        //            b4 = m_serial.ReadByte();
        //            response = ((int)b1) * 256 + b2;
        //            temperature = (b3 * 256 + b4);
        //            temperature = temperature / 10;

        //        }
        //        catch (System.TimeoutException ex)
        //        {
        //            result = false;
        //            throw new TimeoutException(ex.Message);
        //        }
        //        focuserPosition = response;
        //        System.Threading.Thread.Sleep(stepdelay);
        //        if (Halt_State)
        //        {
        //            Halt_State = false;
        //            m_serial.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        //            break;
        //        }

        //        //The following is statement is required to ensure safe threading code
        //        if (C.txtPosition.InvokeRequired)
        //        {
        //            SetTextCallback d = new SetTextCallback(SetText);
        //            C.Invoke(d, new object[] { position.ToString() });
        //        }
        //        else
        //        {
        //            C.txtPosition.Text = focuserPosition.ToString();
        //        }
        //    }
        //    m_serial.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        //    return result;
        //}

        #endregion

    }
}
