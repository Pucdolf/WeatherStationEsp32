#include <Arduino.h>
#include <DHT.h>

#define DHTPIN 15
#define DHTTYPE DHT11

const int ledRed = 25;
const int ledYellow = 26;
const int ledGreen = 27;

DHT dht(DHTPIN, DHTTYPE);

// put function declarations here:
int myFunction(int, int);

void setup() {
  // put your setup code here, to run once:
  // int result = myFunction(2, 3);

  Serial.begin(115200);
  Serial.println("Start");

  dht.begin();

  pinMode(ledRed, OUTPUT);
  pinMode(ledYellow, OUTPUT);
  pinMode(ledGreen, OUTPUT);

}

void loop() {
  // put your main code here, to run repeatedly:

  float h = dht.readHumidity();
  float t = dht.readTemperature();

  Serial.print("Wilgotnosc: ");
  Serial.print(h);
  Serial.print(" %\tTemperatura: ");
  Serial.print(t);
  Serial.println(" *C");

  if(isnan(h) || isnan(t)){

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