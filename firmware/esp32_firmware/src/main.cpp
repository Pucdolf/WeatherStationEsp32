#include <Arduino.h>
#include <DHT.h>
#include<OneWire.h>
#include<DallasTemperature.h>

//DHT11
#define DHTPIN 15
#define DHTTYPE DHT11
DHT dht(DHTPIN, DHTTYPE);

//DS18B20
#define ONE_WIRE_BUS 4
OneWire oneWire(ONE_WIRE_BUS);
DallasTemperature ds18b20(&oneWire);

//LEDS
const int ledRed = 25;
const int ledYellow = 26;
const int ledGreen = 27;


// put function declarations here:
int myFunction(int, int);

void setup() {
  // put your setup code here, to run once:
  // int result = myFunction(2, 3);

  Serial.begin(115200);
  Serial.println("Weather Station is starting...");

  dht.begin();
  ds18b20.begin();

  pinMode(ledRed, OUTPUT);
  pinMode(ledYellow, OUTPUT);
  pinMode(ledGreen, OUTPUT);

}

void loop() {
  // put your main code here, to run repeatedly:

  float hDHT = dht.readHumidity();
  float tDHT = dht.readTemperature();

  ds18b20.requestTemperatures();
  float tDS = ds18b20.getTempCByIndex(0);

  Serial.print("\n-----------DHT11------------\n");
  Serial.print("Wilgotnosc: ");
  Serial.print(hDHT);
  Serial.print(" %\tTemperatura: ");
  Serial.print(tDHT);
  Serial.println(" *C");

  Serial.print("\n----------DS18B20----------\n");
  Serial.print("Temperatura: ");
  Serial.print(tDS);
  Serial.println(" *C");


  if(isnan(hDHT) || isnan(tDHT) || tDS <= -100.0){
    digitalWrite(ledRed, HIGH);
    digitalWrite(ledYellow,LOW);
    digitalWrite(ledGreen,LOW);
  }
  else{

    digitalWrite(ledRed, HIGH);
    delay(2000);
    digitalWrite(ledRed,LOW);
    digitalWrite(ledYellow, HIGH);
    delay(2000);
    digitalWrite(ledYellow,LOW);
    digitalWrite(ledGreen, HIGH);
    delay(2000);
    digitalWrite(ledGreen,LOW);
  }
  delay(2000);
}

// // put function definitions here:
// int myFunction(int x, int y) {
//   return x + y;
// }