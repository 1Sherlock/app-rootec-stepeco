#include <DHT.h>
#include <TinyGPS++.h>
#include <SoftwareSerial.h>

#define DHTPIN 2

int RXPin = 4;
int TXPin = 3;
int GPSBaud = 9600;

#define DHTTYPE DHT22 
DHT dht(DHTPIN, DHTTYPE);
TinyGPSPlus gps;

SoftwareSerial gpsSerial(TXPin, RXPin);
SoftwareSerial sim(10, 11);


int chk;
float hum = 0;  
float temp = 0;
int quality = 0;
float lattitude = 0;
float longitude = 0;
int noise = 0;

void setup()
{
  delay(10000);
  Serial.begin(9600);
  gpsSerial.begin(GPSBaud);
  sim.begin(9600);
  dht.begin();
  connectGPRS();
}

void loop()
{
  getLocation();
  calcQuality();
  calcTemp();
  calcNoise();
  postData();
  delay(60000);
}

void calcTemp(){
    hum = dht.readHumidity();
    temp= dht.readTemperature();
    delay(2000); 
}

void calcQuality(){
   quality = analogRead(A0);
    delay(1000);
}

void calcNoise(){
   noise = analogRead(A1);
    delay(1000);
}

void getLocation()
{
//  if (gps.location.isValid())
//  {
   
    lattitude = gps.location.lat();
   
    longitude = gps.location.lng();
//  }
//  else
//  {
//    Serial.println("Location: Not Available");
//  }
//  
  delay(1000);
}


void connectGPRS() {
  sim.println("AT");
  delay(1000);
  
  sim.println("AT+CGATT?");
  delay(1000);
  
  sim.println("AT+SAPBR=3,1,\"CONTYPE\",\"GPRS\"");
  delay(1000);
  sim.println("AT+SAPBR=3,1,\"APN\",\"internet\"");
  delay(1000);
  
  sim.println("AT+SAPBR=3,1,\"USER\",\"\"");
  delay(1000);
  
  sim.println("AT+SAPBR=3,1,\"PWD\",\"\"");
  delay(1000);
  
  sim.println("AT+SAPBR=1,1");
  delay(1000);
  
  sim.println("AT+SAPBR=2,1");
  delay(1000);
}

void postData(){
  sim.println("AT+HTTPINIT");
  delay(1000);


  sim.println("AT+HTTPPARA=\"CID\",1");
  delay(1000);


//  sim.println("AT+HTTPSSL=1");
//  delay(1000);

  sim.println("AT+HTTPPARA=\"URL\",\"http://stepeco.somee.com/api/EnvironmentRecord\"");
  delay(3000);

  sim.println("AT+HTTPPARA=\"CONTENT\",\"application/json\"");
  delay(1000);

  String data = "{\"latitude\":\""+ String(lattitude) +"\",\"longitude\":\""+ String(longitude) +"\",\"temperature\":\""+ String(temp) +"\",\"quality\":\""+ String(quality) +"\",\"humidity\":\""+ String(hum) +"\",\"pressure\":\"0\",\"noiseLevel\":\""+ noise +"\",\"keyword\":\"qwerty\"}";
  int k = 0;
  for (int i = 0; i < data.length(); i++){
    if(data[i] != '\\')
      k++;
  }
  String count = String(k);
  sim.println("AT+HTTPDATA="+ count +",5000");
  delay(1000);
  
  sim.println(data);
  delay(10000);
  
  sim.println("AT+HTTPACTION=1");
  delay(10000);
}
