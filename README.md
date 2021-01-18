# PizzerianLab3

Städat och modifierat annan students projekt. Original koden var uppladdad först för att du ska enkelt kunna se de förändring jag gjort. 
Första intrycket var att det var väldigt många foreach loopar och som repeterades vid flera tillfällen och fick koden att se rörig ut. För att reducera och göra det allmänt lättare för ögonen så skapade jag extensions för diverse modeller som utför samma eller likvärdig uppgift. nu skriver man istället något i stil med:
> exempel.ToViewModel()
> exempel[].ToViewModels()
> exempel.CopyToNew()

Elevens val av design patterns tycker jag var väldigt passande för uppgiften och valde att bevara alla 3. 

1. För sin shopping-cart så har hen använt sig av singleton, detta är deklarerat i apins startup. Anledningen till detta är solklar då det snabbt hade blivit rörig att passa runt ett ID mellan de olika kontrollerna och som hen själv nämner i sin original text så är det smidigt att komma åt den var som i kontrollerna och att det bara behövs en. 

2. Eleven har också använt sig av DTOs för att passa data mellan databasen och APIn detta är standard i liknande situationer för att inte passa rå data till användaren eller att validera datan innan den sparas. Det är därför bl.a. en mycket bra och flexibel säkerhetslösning.

3. Iterator design pattern, solklart val då drickor, pizzor och tillbehör är sparat i diverse listor och är inte nödvändigtvis bundna till varandra i databasen. Detta går mycket bra ihop med DTO. Jag tycker dock att eleven hade överdrivit med foreach loopar och hade kunnat ersätta många av dessa för att göra koden lättare för ögonen och minska repeterande kod, vilket jag gjort nu. 


#Tldr
* Skapat extensions för diverse modeller för att reducera mängden kod i kontroller.

* Städat kontroller.

* Fixat namespaces.

* skapat copy funktioner åt diverse modeller. Återigen för att göra koden lite renare.  (ligger i extensions).

* Sqlite databas istället för sqlserveer. då jag använder mig av linux utan M$sql



_____Original text_____

Singleton, för shopping cart, då det endast behöver finnas 1. Blir också enkelt att komma åt överallt i koden. 

Transfer Object Pattern, DTO för att få datan smidigt från client till server.

Iterator design pattern, används mycket för att loopa igenom flera collections i koden. 
