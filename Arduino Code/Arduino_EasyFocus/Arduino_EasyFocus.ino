#include <Wire.h>

#include <LiquidCrystal_I2C.h>

#include <EEPROM.h>

/*
  Arduino Stepper Focuser (Easy Focus (L298 version))

  Matt Todman
  2017

  ========================================================================================
  Version 0.1
  Basic working version. No bells & Whistles.

  Version 0.2
  Added EPROM storage and reverse direction flag

  Version 0.3
  Added Temp Monitoring

  Version 0.4
  Adding LCD support
  ===========================================================================================

  Works with Arduino Focus 0.1.0

*/

//const boolean TEMP_COMP = true;
const char ver[] = "0.4";
//int incoming_Byte;
int currentpos = 5000;  // The current focuser position
byte currentpos_hi; // Hi order byte used to transfer currentpos to & from the PC application
byte currentpos_lo; // Low order byte used to transfer currentpos to & from the PC application
byte i;
bool revDir = true; //Set to "true" if the default stepper direction should be reversed
byte serial_pause = 200;
//int button_pause = 500;


// Temperature variables
int reading = 0;
float voltage = 0;
float temp;
float averageTemp = 0;
float averageTempSum = 0;
int averageTempCount = 0;
int averageTempReadTimer = millis();
int averageTempReadFrequency = 500;  // Frequency (ms) of the temperature read timer. Used to calculate the average temperature.
unsigned long tempTimer = 0;
float tempCount = 0;
unsigned long tempTotal = 0;


unsigned long idleTimer = 0; // Variable that counts milliseconds since last action (button press, move, etc)
unsigned long lastMoveTimer = 0; // contains last time a move command was performed in order to assist with writing to EEPROM
int lastMoveThreshold = 30000; // number of milliseconds after which a move command is issued that currentpos will be written to the EEPROM
boolean moveWait = false; // used to check if a move has happened in the last "lastMoveThreshold" seconds
unsigned long numEEPROMWrites; //contains the number of times the EEPROM has been writtem to
byte firstWrite = 33; //A random number used to determine if the EEPROM has been initialised yet (ie, is this the first time the arduino is bein used?)
int idlePeriod = 5000; // Period of time (ms) before arduino should start sending idle updates to the PC & the LCD.

const int buttonPin1 = 3;    // the number of the focus in pushbutton pin
const int buttonPin2 = 4;    // the number of the focus out pushbutton pin
const int buttonPin3 = 5;    // the number of the go fast focuser button
const int ledPin =  13;      // the number of the LED pin for focus button pushes
const int tempPin = 2;   // Defines the Temp pin as analogue pin 2
int button1State = 0;         // variable for reading the pushbutton #1 status
int button2State = 0;         // variable for reading the pushbutton #2 status
int fastFocusButtonState = 0;  // variable for reading the go fast focuser button status

int stepPhase = 1; // The current stepper motor phase (1-4)
LiquidCrystal_I2C lcd(0x27, 16, 2);

void setup() {

  // Set the LCD address to 0x27 for a 16 chars and 2 line display
  

  lcd.begin();
  lcd.backlight();
  lcd.print("Easyfocus v");
  lcd.print(ver);

  //establish motor direction toggle pins
  pinMode(12, OUTPUT); //CH A -- HIGH = forwards and LOW = backwards???
  pinMode(13, OUTPUT); //CH B -- HIGH = forwards and LOW = backwards???
  //establish motor brake pins
  pinMode(9, OUTPUT); //brake (disable) CH A
  pinMode(8, OUTPUT); //brake (disable) CH B

  unsigned long numEEPROMWrites = 0; //contains the number of times the EEPROM has been writtem to

  if (EEPROM.read(6) != 33) {
    //Assume this is the first time this arduino has been used. Need to initialise the EEPROM
    WriteToEEPROM();
  }

  ReadFromEEPROM();



  //setup the serial port on the Arduino board to communicate with a PC at 9600 baud
  Serial.begin(9600);

  delay(2000);


  //Configures the reference voltage used for analog input as the AREF pin.
  //Used to calibrate the temp sensor against an external voltage source.
  //You need to connect the AREF pin to to an external & stable power source.
  //analogReference(EXTERNAL);
}

void loop() {

  // check to see if we have received any commands from the PC
  //

  if (Serial.available() > 0) {
    read_input();

    if (i == 252) {       // Routine to move the focuser in by 1 count
      currentpos --;
      if (!revDir)
      {
        moveIn();
      } else
      {
        moveOut();
      }
      Serial.print("!");
    }

    else if (i == 251) { // Routine to move the focuser out by 1 count
      currentpos ++;
      if (!revDir)
      {
        moveOut();
      } else
      {
        moveIn();
      }
      Serial.print("!");
    }

    else if (i == 199) {  // Connectivity test. Just reply with a "!" (ASCII = 33)
      Serial.print("!");
    }

    else if (i == 198) { // If we receive a BYTE 198 from the PC then send the focuser position back.
      SendToPC(currentpos);
    }

    else if (i == 197) { // If we receive a BYTE 197 from the PC then set the focuser position to the value received in the data packet
      delay(serial_pause);
      currentpos_hi = Serial.read();
      delay(serial_pause);
      currentpos_lo = Serial.read();
      delay(serial_pause);
      currentpos = currentpos_hi * 256 + currentpos_lo;
      Serial.print("!");
      WriteToEEPROM();
    }

    else if (i == 196) { // If we receive a BYTE 196 from the PC then just send the temperature back.
      SendToPC((int)temp*10);
    }
  }

  if ((millis() - idleTimer) > idlePeriod) {
    averageTemp = averageTempSum / averageTempCount;
    temp = averageTemp;
    averageTempSum = 0;
    averageTempCount = 0;
    idleTimer = millis();
    lcd.clear();
    lcd.print("Position = ");
    lcd.print(currentpos);
  }

  if ((millis() - lastMoveTimer) > (lastMoveThreshold)) {
    if (moveWait == true) {
      WriteToEEPROM();
      moveWait = false;
      lastMoveTimer = millis();
    }
    else {
      lastMoveTimer = millis();
    }
  }

  if ((millis() - averageTempReadTimer) > averageTempReadFrequency) {
    ReadTemp();
    averageTempReadTimer = millis();
  }

} // END OF MAIN LOOP //

void read_input() {
  i = Serial.read();
}

int bytes_to_int(byte hi, byte low) {
  int x = (hi << 8) | low;
  return x;
}

void moveIn() {
  stepPhase ++;
  if (stepPhase == 5)stepPhase = 1;
  step(stepPhase);

  if (moveWait == false) {
    WriteToEEPROM();
  }
  lastMoveTimer = millis();
  moveWait = true;
}

void moveOut() {
  stepPhase --;
  if (stepPhase == 0)stepPhase = 4;
  step(stepPhase);

  if (moveWait == false) {
    WriteToEEPROM();
  }
  lastMoveTimer = millis();
  moveWait = true;
}

void step(byte phase) {
  switch (phase) {
    case 1:
      digitalWrite(9, LOW);  //ENABLE CH A
      digitalWrite(8, HIGH); //DISABLE CH B
      digitalWrite(12, HIGH);   //Sets direction of CH A
      analogWrite(3, 255);   //Moves CH A
      break;
    case 2:
      digitalWrite(9, HIGH);  //DISABLE CH A
      digitalWrite(8, LOW); //ENABLE CH B
      digitalWrite(13, LOW);   //Sets direction of CH B
      analogWrite(11, 255);   //Moves CH B
      break;
    case 3:
      digitalWrite(9, LOW);  //ENABLE CH A
      digitalWrite(8, HIGH); //DISABLE CH B
      digitalWrite(12, LOW);   //Sets direction of CH A
      analogWrite(3, 255);   //Moves CH A
      break;
    case 4:
      digitalWrite(9, HIGH);  //DISABLE CH A
      digitalWrite(8, LOW); //ENABLE CH B
      digitalWrite(13, HIGH);   //Sets direction of CH B
      analogWrite(11, 255);   //Moves CH B
      break;
  }
}

void SendToPC(int data) {
  Serial.write(data / 256);
  Serial.write(data % 256);
  idleTimer = millis();
}

void WriteToEEPROM() {
  numEEPROMWrites ++;
  EEPROM.write(0, (byte) (numEEPROMWrites >> 16));
  EEPROM.write(1, (byte) (numEEPROMWrites >> 8));
  EEPROM.write(2, (byte) (numEEPROMWrites));
  EEPROM.write(3, currentpos / 256);
  EEPROM.write(4, currentpos % 256);
  EEPROM.write(5, stepPhase);
  EEPROM.write(6, firstWrite);
}

void ReadFromEEPROM() {
  numEEPROMWrites = EEPROM.read(0);
  numEEPROMWrites = numEEPROMWrites * 256 + EEPROM.read(1);
  numEEPROMWrites = numEEPROMWrites * 256 + EEPROM.read(2);
  currentpos = (EEPROM.read(3) * 256) + EEPROM.read(4);
  stepPhase = EEPROM.read(5);
  firstWrite = EEPROM.read(6);
}

void ReadTemp() {
  reading = analogRead(tempPin);
  temp = (5.0 * reading * 100.0) / 1024.0;
  averageTempSum = averageTempSum + temp;
  averageTempCount ++;
}

void DoNothing() {
}












