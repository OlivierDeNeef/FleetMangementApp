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

- ERD diagram ontwerpen (databank)
- Userinterface ontwerpen



## Vragen

- Wanneer men een rijbewijs type wilt toevoegen aan een bestuurder maar deze bestuurder heeft dit type al in zijn lijst, wat doen we dan ? 
  1. Exception geven omdat men iets wilt toevoegen dat er al is. ---DEZE----
  2. Het programma gewoon laten lopen zonder dat er iets veranderd.
- Mogen we dependency injection en  Mok-items gebruiken ? --Ja--
- Wat is de minimumleeftijd van een bestuurder en maximum leeftijd? 18--- geen check nodig bij max



opmerkingen van Docent

- Maak klasse Adres van Bestuurder
- IsDeleted moet andere en betere naam krijgen
- Pincode geen INT maar string (bv code is 0014, int geeft 14)
- HasBrandstofType -> Heeft...
- Hoort rijksreg. nummer bij "Bestuurder"? -> static class rijksgestisterChecker en static class chassisnummerChecker
- Controleren of bestuurder voertuig heeft voor het voertuig dat is toegewezen

