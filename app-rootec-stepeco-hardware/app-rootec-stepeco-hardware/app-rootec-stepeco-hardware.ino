#include <DHT.h>

#define DHTPIN 2   
#define DHTTYPE DHT22 
DHT dht(DHTPIN, DHTTYPE);



int chk;
float hum;  
float temp;

void setup()
{
    Serial.begin(9600);
  dht.begin();

}

void loop()
{
  calcTemp();
}


void calcTemp(){
  hum = dht.readHumidity();
    temp= dht.readTemperature();
    
    Serial.print("Namlik: ");
    Serial.print(hum);
    Serial.print(" %, Harorat: ");
    Serial.print(temp);
    Serial.println(" °C");
    delay(2000); 
}
