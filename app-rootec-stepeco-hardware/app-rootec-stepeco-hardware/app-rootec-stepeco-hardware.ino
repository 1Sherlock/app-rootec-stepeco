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


int chk;
float hum;  
float temp;
int quality;
float lattitude;
float longitude;


void setup()
{
    Serial.begin(9600);
      gpsSerial.begin(GPSBaud);
  dht.begin();

}

void loop()
{
  getLocation();
  calcQuality();
  calcTemp();
}


void calcTemp(){
  hum = dht.readHumidity();
    temp= dht.readTemperature();
    
    Serial.print("Namlik: ");
    Serial.print(hum);
    Serial.print(" %, Harorat: ");
    Serial.print(temp);
    Serial.print(" °C, ");
    Serial.print("Havo sifati = ");
    Serial.print(quality);
    Serial.println("*PPM");
    Serial.print("Latitude: ");
    Serial.print(lattitude);
     Serial.print("Longitude: ");
      Serial.print(longitude);
    delay(2000); 
}

void calcQuality(){
   quality = analogRead(A0);
    delay(1000);
}

void getLocation()
{
  if (gps.location.isValid())
  {
   
    lattitude = gps.location.lat();
   
    longitude = gps.location.lng();
  }
  else
  {
    Serial.println("Location: Not Available");
  }
  
  delay(1000);
}
