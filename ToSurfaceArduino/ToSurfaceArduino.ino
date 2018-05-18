// MUST USE BAUD RATE 115200 WITH BLUNO BOARD

// motor pins
int dirPinA1 = 6; // N
int dirPinA2 = 9; // E
int dirPinB1 = 10; // W
int dirPinB2 = 11;  // S

// led pins
int pinLed1 = 2;
int pinLed2 = 3;
int pinLed3 = 4;
int pinLed4 = 5;

// incoming data
int mode = 0;
int dist = 0;

// motor values
int motN = 0;
int motE = 0;
int motW = 0;
int motS = 0;

// refer to unity and set same thresh settings
int thresh = 12;
int threshNear = 6;

// vibration pattern pre-set
unsigned int patterns[5][4] = { {1,0,1,0},
                                {0,1,1,1},
                                {1,0,0,0},
                                {0,0,0,1},
                                {0,0,1,0}};
bool isStart = false;
unsigned long startMillis = 0;
unsigned int index = 0;
unsigned int duration = 100;

// test buz
bool isRunning = false;

void setup() 
{
  // pin declarations
  pinMode(dirPinA1, OUTPUT);
  pinMode(dirPinA2, OUTPUT);  
  pinMode(dirPinB1, OUTPUT);
  pinMode(dirPinB2, OUTPUT); 
  pinMode(pinLed1, OUTPUT);
  pinMode(pinLed2, OUTPUT);  
  pinMode(pinLed3, OUTPUT);
  pinMode(pinLed4, OUTPUT); 

  // init to zero
  analogWrite(dirPinA1, 0);
  analogWrite(dirPinA2, 0);
  analogWrite(dirPinB1, 0);
  analogWrite(dirPinB2, 0);
  digitalWrite(pinLed1, 0);
  digitalWrite(pinLed2, 0);
  digitalWrite(pinLed3, 0);
  digitalWrite(pinLed4, 0);

  // Serial settings
  Serial.begin(115200);
}

void loop() 
{ 
  if (Serial.available() > 1)  
  {
    GetData();
    DebugData();
    // test
    if (mode == 33) TestBuz();
  }

  if (!isStart || millis() - startMillis > duration)
  {
    // shower mode
    if (mode == 1) 
    {
      ComputeMotor(50);
      duration = 200;
    }
    // heavy rain mode
    else if (mode == 2) 
    {
      ComputeMotor(120);
      duration = 100;
    }
    // no feedback mode
    else if (mode == 0) 
    {
      ComputeMotor(0);
      duration = 100;
    }

    if (index < 4) index++;
    else index = 0;
    
    startMillis = millis();
    isStart = true;   
  }
}

void ComputeMotor(int mag)
{
  // very close so run in maximum
  if (mag == 0) 
  {
    analogWrite(dirPinA1, 0);
    analogWrite(dirPinA2, 0);
    analogWrite(dirPinB1, 0);
    analogWrite(dirPinB2, 0);
    digitalWrite(pinLed1, LOW);
    digitalWrite(pinLed2, LOW);
    digitalWrite(pinLed3, LOW);
    digitalWrite(pinLed4, LOW);
  }
  else if (dist == 0) 
  {
    RandomFeedback(mag, 0, 6, 2);
    RandomFeedback(mag, 1, 9, 3);
    RandomFeedback(mag, 2, 10, 4);
    RandomFeedback(mag, 3, 11, 5);
    Serial.println("near");
  }
  // scale depending on position
  else 
  {
    int temp = map(dist, thresh, threshNear, 30, mag);
    RandomFeedback(temp, 0, 6, 2);
    RandomFeedback(temp, 1, 9, 3);
    RandomFeedback(temp, 2, 10, 4);
    RandomFeedback(temp, 3, 11, 5);
    Serial.println("approaching");
  }
}

void RandomFeedback(int motM, int i, int mot, int led) 
{
  if (patterns[index][i] == 1) 
  {
    analogWrite(mot, motM);
    digitalWrite(led, HIGH);
  }
  else 
  {
    analogWrite(mot, 0);
    digitalWrite(led, LOW);
  }
}

void GetData()
{
  mode = Serial.read();
  dist = Serial.read();
  //Serial.flush();
}

void DebugData()
{
  Serial.print("1:");
  Serial.println(mode);
  Serial.print("2:");
  Serial.println(dist);
}

void TestBuz()
{
  if (isRunning) {
    analogWrite(dirPinA1, 70);
    analogWrite(dirPinA2, 70);
    analogWrite(dirPinB1, 70);
    analogWrite(dirPinB2, 70);
    digitalWrite(pinLed1, HIGH);
    digitalWrite(pinLed2, HIGH);
    digitalWrite(pinLed3, HIGH);
    digitalWrite(pinLed4, HIGH);
    isRunning = false;
  }
  else {
    analogWrite(dirPinA1, 0);
    analogWrite(dirPinA2, 0);
    analogWrite(dirPinB1, 0);
    analogWrite(dirPinB2, 0);
    digitalWrite(pinLed1, LOW);
    digitalWrite(pinLed2, LOW);
    digitalWrite(pinLed3, LOW);
    digitalWrite(pinLed4, LOW);
    isRunning = true;
  }
}

