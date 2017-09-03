
const int TrigPin1 = 2; 
const int EchoPin1 = 3;
int i=5;
int cm1,cm2,cm3;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600); 
  pinMode(TrigPin1, OUTPUT); 
  pinMode(EchoPin1, INPUT); 

}

void loop() {
  // put your main code here, to run repeatedly:
  digitalWrite(TrigPin1, LOW); //低高低电平发一个短时间脉冲去TrigPin 
  delayMicroseconds(2); 
  digitalWrite(TrigPin1, HIGH); 
  delayMicroseconds(10); 
  digitalWrite(TrigPin1, LOW); 

cm1 = pulseIn(EchoPin1, HIGH) / 58.0; //将回波时间换算成cm 

delay(200);

  digitalWrite(TrigPin1, LOW); //低高低电平发一个短时间脉冲去TrigPin 
  delayMicroseconds(2); 
  digitalWrite(TrigPin1, HIGH); 
  delayMicroseconds(10); 
  digitalWrite(TrigPin1, LOW); 

cm2 = pulseIn(EchoPin1, HIGH) / 58.0; //将回波时间换算成cm 

delay(200);

 digitalWrite(TrigPin1, LOW); //低高低电平发一个短时间脉冲去TrigPin 
  delayMicroseconds(2); 
  digitalWrite(TrigPin1, HIGH); 
  delayMicroseconds(10); 
  digitalWrite(TrigPin1, LOW); 

cm3 = pulseIn(EchoPin1, HIGH) / 58.0; //将回波时间换算成cm 

delay(200);

if(cm1>0&cm1<30&cm2>0&cm2<30&cm3>0&cm3<30)
{

if(cm1>cm2&cm2>cm3)
{
  Serial.print("0");   

}
else if(cm1<cm2&cm2<cm3)
{
     Serial.print("1"); 


}
else
{
   Serial.print("2"); 

  }
}
else
{
   Serial.print("2"); 

  }

delay(1000);





 
}
