# 2aHIF Projekt von ToS4 und mirko4001

## [Projekttagebuch](https://htlr-my.sharepoint.com/:x:/g/personal/mohamad_ayo_student_htl-rankweil_at/EbcGpkWvtWhIq2CpxRiq5dkBmaxSItsFSd3aY-lm92mrJw?e=Ucpsdn)

## Projektplanung

### Beschreibung
* Inventar-Management
* für kleine Unternehmen
* Funktionen zur Verwaltung von Produkten

### Must-Have
* Produkte hinzufügen, bearbeiten und löschen
* Artikeln anzeigen 
* Benachrichtigungen bei geringer Menge
* Databank-Integration

### Nice-To-Have
* Auftragsverwaltung
* MVVM-Struktur (irgendwie)
* Export (PDF)
* Email-Benachrichtigungen
* Logging
* Dashboard

### No-Have
* Cloud
* Multi-Language
* Multiplatform

## Umsetzungsdetails
**Umsetzung**
Aufgaben wurden aufgeteilt und wir haben angefangen, keep it simple.

mirko4001: zuständig für Dashboard, auch für UI Design (Frontend)

ToS4: zuständig für Products und Orders und für weitere Features (Backend)

Wir haben in eine Woche alles für das Projekt gemacht.

**Softwarevoraussetzungen**
Funktioniert gut auf Windows 10 & 11.

**Verwendete Technologien**
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [Visual Studio Code](https://code.visualstudio.com/)
- Internet
    - [Google](https://www.google.at/)
    - [ChatGPT](https://chatgpt.com/)

## Anmerkungen

* Da wir durch das PoS Projekt Druck hatten, kann man merken, dass das Projekt in der letzten Woche vor der Abgabe gemacht wurde.
* Wir haben wie gewohnt im Unterricht SQlite verwendet, um die Daten unserer Inventar-Management-Applikation zu speichern.
* Es wurden 2 Tabellen erstellt: Produkt- und Ordertabelle
* Alle CRUD Abfragen wurden verwendet
* Da unser Programm beim Laden der Applikation alle Daten aus der Datenbank in einer Collection speichert und Instanzen daraus erzeugt, haben wir uns dafür entschieden, keine Join Abfrage zu benutzen.
* Wir haben die externe Library WPF UI verwendet, um mehrere Controls zu benutzen