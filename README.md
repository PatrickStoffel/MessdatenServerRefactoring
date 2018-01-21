# MessdatenServerRefactoring

###VSTS
![Build Status](https://messdatenserver.visualstudio.com/_apis/public/build/definitions/64214a36-dc6b-424c-a981-e178b335810d/1/badge)

## Entwicklungsteam

 * Patrick Stoffel
 * Hans-J�rg Nett


## Einleitung

F�r die Beschreibung der Ausgangslage und des Inhalts der Semesterarbeit verweisen wir auf die Projektskizze.pdf im 
Verzeichnis App_Data des Projekts.

Das Refactoring behandelt mehrheitlich den Teil der Applikation, in der die einzelnen Messmittel konfiguriert und 
verwaltet werden.

Eine Demonstration des Auslesens der Messwerte aus den konfigurierten Ger�ten ist hier nicht vorgesehen, da dies Teil 
der letzten Semesterarbeit war.

##Umsetzung

Der Verlauf des Refactorings kann �ber die History der Commits in GitHub nachvollzogen werden.

Continuous Integration wurde mit VSTS umgesetzt und ist �ber untenstehenden Link einsehbar.
 
https://messdatenserver.visualstudio.com 

Die Applikation wurde zur Ausf�hrung von Gui-Tests in einer Testumgebung mit Hilfe von Azure ver�ffentlicht.

Die Konfiguration und Verwaltung der Messmittel kann von hier aus auch explorativ getestet werden.

http://messdatenserver.azurewebsites.net/View/index.html
