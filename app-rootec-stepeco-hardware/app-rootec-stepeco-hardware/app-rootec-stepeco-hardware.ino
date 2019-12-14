#include <DHT.h>

#define DHTPIN 2   

#define DHTTYPE DHT22 
DHT dht(DHTPIN, DHTTYPE);



int chk;
float hum;  
float temp;
int quality;

void setup()
{
    Serial.begin(9600);
  dht.begin();

}

void loop()
{
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
    delay(2000); 
}

void calcQuality(){
   quality = analogRead(A0);
    delay(1000);
}
