# Presentation Outline

## Introduction

- Visitrack è un software commissionato alla nostra software house da GlobalCorp
- L'obbiettivo è quello di digitalizzare tutta la gestione dei visitatori nell'HQ

## Problem Statement

- GlobalCorp è un azienda di grandi dimensioni, gli utenti che dovranno interagire con il software sono molteplici, per cui è necessario individuare un piccolo gruppo che sia significativo per l'analisi e lo studio dei requisiti che Visitrack dovrà avere

## Solution Approach

- Prima di sviluppare il prodotto, è stata svolta un analisi (visualizzare discovery.md)

## Architecture

- L'architettura del sistema è strutturata in strati: il livello Core contiene la logica di business, il livello Infrastructure gestisce le risorse esterne, il livello Services coordina le attività, e il livello Web gestisce le interazioni utente.
- Le tecnologie utilizzate sono ASP.NET Core, Entity Framework, MVC, Vue, and SignalR

## Implementation Details

- Le funzionalità principali sono state sviluppate come Features, poichè queste funzionalità non si diramano in altri sottolivelli, mentre l'Admin Area è stata sviluppata come Area
- Metodo AGILE, suddivisione del lavoro, stand-up ogni settimana.
- Si è lavorato per avere una demo funzionante il prima possibile, tralasciando qualche funzionalità accessoria, in modo da poter avere feedback dal cliente costante.
- L'interfaccia deve essere essenziale e contenere solo gli elementi veramente necessari, in modo da ridurre i tempi di sviluppo e migliorare la UX
- Shift-Left Security Paradigm dove possibile, scansione del codice per vulnerabilità su GitHub e necessaria autenticazione per utilizzare le funzionalità di visitrack.

## Demo

- http://gerardocipriano-001-site1.htempurl.com/
- Mostra Check-In, VisitorsList (CheckOut + Search), Reports (Grafico) e Admin Area

## Results and Evaluation

- Avere cicli di sviluppo brevi ci ha consentito di ottenere molti feedback dal cliente

- Tutte le funzionalità previste obbligatorie sono state implementate e sono funzionanti

- I feedback del cliente hanno consentito di migliorare la UX di Visitrack, aggiungendo

  - Semantica dei colori corretta sui banner di Success/Failure e sui pulsanti
  - Label maggiormente parlanti per guidare maggiormente l'utente durante l'uso
  - Inserimento del logo aziendale in tutte le schermate dell'applicativo
  - Funzione di ricerca dei visitatori attualmente presenti
  - Mini guida utente in inglese (README.md)

- L'interfaccia, risulta piacevole agli occhi e facile da usare. L'applicativo è molto veloce e permette di risparmiare molto tempo.

## Future Work

- Buona parte dei feedback raccolti dal cliente sono stati implementati. Durante lo sviluppo, sono state richieste nuove funzionalità non inizialmente previste durante la fase di analisi. Queste funzionalità saranno oggetto di uno sviluppo futuro, in quanto per rispettare le tempistiche iniziali con il cliente, non è stato possibile implementarle. Queste sono:

  - una pagina di Storico, in cui visualizzare e ricercare visitatori di altri giorni;
  - ricerca degli utenti abilitati al login nell'admin area
  - differenziare le autorizzazioni fra Reception e Ufficio Sicurezza

- Per quanto riguarda il codice, sarà necessario aggiornare alcune librerie e utilizzare TypeScript al posto di JS.

## Conclusion

- Pur considerando qualche difetto nel codice e di progettazione, siamo molto soddisfatti di come abbiamo realizzato il progetto
- Abbiamo lavorato con efficacia in team utilizzando Git
- Domande?
