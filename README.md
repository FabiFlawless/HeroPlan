# HeroPlan - Projektmanagement-Tool

HeroPlan ist ein Trello/Planka-ähnliches Projektmanagement-Tool, das mit WPF (Windows Presentation Foundation) entwickelt wurde. Es ermöglicht Benutzern, Aufgaben in Boards, Listen und Karten zu organisieren.

## Funktionen

- Benutzerauthentifizierung
- Board-Verwaltung
- Erstellung von Aufgabenlisten
- Aufgaben- (Karten-) Verwaltung
- Deadline-Verfolgung
- Admin-Benutzerverwaltung

## Funktionsweise

### 1. Benutzerauthentifizierung

- Die Anwendung startet mit einem Anmeldefenster (`LoginWindow`).
- Benutzer geben ihre Anmeldedaten ein, die gegen die Datenbank überprüft werden.
- Ein Admin-Benutzer ist standardmäßig voreingestellt (Benutzername: admin, Passwort: admin).

### 2. Hauptoberfläche

Nach erfolgreicher Anmeldung wird den Benutzern das Hauptfenster (`MainWindow`) präsentiert, das besteht aus:

- Einer Seitenleiste, die alle Boards anzeigt
- Dem Hauptbereich, der Listen und Aufgaben für das ausgewählte Board zeigt

### 3. Board-Verwaltung

- Benutzer können Boards erstellen, bearbeiten und löschen.
- Jedes Board enthält mehrere Aufgabenlisten.

### 4. Aufgabenlisten-Verwaltung

- Innerhalb jedes Boards können Benutzer mehrere Aufgabenlisten erstellen.
- Listen können umbenannt oder gelöscht werden.

### 5. Aufgaben- (Karten-) Verwaltung

- Jede Liste kann mehrere Aufgaben (Karten) enthalten.
- Aufgaben können erstellt, bearbeitet und gelöscht werden.
- Ein Klick auf eine Aufgabe öffnet ein Detailfenster (`CardDetailWindow`), in dem Benutzer:
  - Den Aufgabennamen bearbeiten können
  - Eine Beschreibung hinzufügen oder bearbeiten können
  - Eine Deadline setzen oder ändern können

### 6. Deadline-Verfolgung

- Aufgaben mit Deadlines werden mit ihrem Fälligkeitsdatum angezeigt.
- Überfällige Aufgaben werden rot hervorgehoben.

### 7. Benutzerverwaltung (nur Admin)

- Admin-Benutzer haben Zugriff auf einen Benutzerverwaltungs-Button.
- Dieser öffnet ein Fenster (`UserRegistrationWindow`), in dem neue Benutzer zum System hinzugefügt werden können.

## Technische Details

- Die Anwendung verwendet Entity Framework Core für Datenbankoperationen.
- Daten werden in einer lokalen SQL Server-Datenbank gespeichert.
- Die `DataService`-Klasse behandelt alle Datenoperationen.
- ObservableCollections werden für Echtzeit-UI-Updates verwendet.

## Erste Schritte

1. Stellen Sie sicher, dass .NET Framework installiert ist.
2. Klonen Sie das Repository.
3. Öffnen Sie die Lösung in Visual Studio.
4. Erstellen und starten Sie die Anwendung.
5. Melden Sie sich mit den Standard-Admin-Anmeldedaten an (Benutzername: admin, Passwort: admin).

## Hinweis

Diese Anwendung ist für den persönlichen Gebrauch und als Demonstrationsprojekt konzipiert. Für den Produktionseinsatz sind möglicherweise zusätzliche Sicherheitsmaßnahmen erforderlich.