# FleetMangementApp
Projectwerk - Graduaatsproef

| Roles            | Naam            | Email                              |
| ---------------- | --------------- | ---------------------------------- |
| Organisatie-rol  | Luca Joos       | luca.joos@student.hogent.be        |
| Business-rol     | Arnout Droesbeke| arnout.droesbeke@student.hogent.be |
| Testing-rol      | Olivier De Neef | olivier.deneef@student.hogent.be   |
| Architectuur-rol | Olivier De Neef | olivier.deneef@student.hogent.be   |



## Todo's 

- Classen diagram van de data access layer
- Classdiagram uitbreiden van domain laag VoertuigManager, BestuurderManager, TankkaartManager BrandstofTypeManager en VoertuigTypeManager 
- ERD diagram ontwerpen (databank)
- Userinterface ontwerpen



## Vragen

- Wat is de minimumleeftijd van een bestuurder en maximum leeftijd? 18
  - Geen check nodig bij max



## opmerkingen van Docent

##### 		07/Oktober

- Maak klasse Adres van Bestuurder **Done**
- IsDeleted moet andere en betere naam krijgen
- Pincode geen INT maar string (bv code is 0014, int geeft 14)
- HasBrandstofType -> Heeft... Betere naam ervoor geven
- Hoort rijksreg. nummer bij "Bestuurder"? -> static class rijksgestisterChecker en static class chassisnummerChecker **Done**
- Controleren of bestuurder voertuig heeft voor het voertuig dat is toegewezen

- Vermijdt Engels en Nederlands door elkaar

