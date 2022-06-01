### Wybrane problemy:
 - Obsługa magazynu sklepu internetowego.

## 1. Wypłacanie pieniędzy z bankomatu:

### brief:

Użytkownik rozpoczyna interakcję poprzez włożenie karty.
Bankomat następnie prosi o wybranie języka w jakim mają być wyświetlane komunikaty
oraz o wybranie rodzaju operacji jaka ma być wykonana.
Może to być wypłacenie gotówki, sprawdzenie stanu konta lub aktywacja karty.
Po dokonaniu wyboru użytkownik proszony jest o podanie kodu pin.
Bankomat wykonuję operację a następnie zwraca gotówkę i/lub wydruk zawierający detale transakcji.

## 2. Obsługa magazynowa zamówień ze sklepu internetowego:

### Brief:

**Upoważniony pracownik** *drukuje* z systemu **manifest aktualnych zamówień**.
Zamówienia obsługiwane są zgodnie z ich **priorytetem**. Pracownik przenosi po kolei
produkty zawarte w zamówieniu z magazynu do swojego stanowiska pracy.
Każdy **produkt** jest *skanowany* czytnikiem kodów kreskowych po czym system drukuje
**faktury**. **Pracownik** *pakuje* produkty wraz z wydrukowanymi dokumentami zgodnie
z **wymaganiami** wybranej w zamówieniu **firmy kurierskiej** lub
zgodnie z **zaleceniami** zamieszczonymi w **sekcji uwagi** zamówienia.
Na paczki naklejane są dane pocztowe wydrukowane przez system.
Finalnie paczki przenoszone są do strefy odbioru paczek.

### Fully dressed:

- **Nazwa:** **Przetwarzanie zamówień**
- **Poziom ważności:** Wysoki
- **Numer:** 1
- **Twórca:** Mikołaj Depta
- **Typ przypadku użycia:** Ogólny, niezbędny
- **Aktor pierwszoplanowy:** Upoważniony pracownik
- **Aktor drugoplanowy:** Poprawnie funkcjonujący serwer zamówień
- **Warunki wstępne:**
  1. Upoważniony pracownik zalogowany do poprawnie funkcjonującego systemu.
  2. Niepusta lista zamówień.''
- **Warunki końcowe:**
  1. Poprawnie przygotowane paczki znajdują się w ustalonej strefie odbioru.
  2. System został zaktualizowany bądź zgłosił problemy z zamówieniem i wycofał
     zmiany wykonane w związku z zamówieniem.
- **Główny scenariusz sukcesu:**
  1. Pracownik pobiera manifest aktualnych zamówień.
  2. Pracownik obsługuje zamówienia zgodnie z ich priorytetem:
    1. Pracownik odnajduje w magazynie kolejne produkty zawarte w zamówieniu
     oraz przenosi je do swojej stacji roboczej.
     4. Pracownik skanuje produkty czytnikiem kodów kreskowych.
    5. System weryfikuje zeskanowany produkt w kontekście zamówienia.
  3. Po zeskanowaniu wszystkich produktów z zamówienia, system drukuje faktury.
  4. Pracownik pakuje zamówienie zgodnie z wytycznymi zawartymi w zamówieniu:
    1. Domyślnie pakowanie powinno być zgodne z wytycznymi firmy kurierskiej
       wybranej w zamówieniu.
    2. W przypadku gdy sekcja uwagi zamówienia zawiera wytyczne co do pakowania,
       należy postąpić z godnie z nimi zamiast zaleceniami firmy kurierskiej.
  5. Po zakończeniu pakowania pracownik drukuje z systemu dane pocztowe, które
     zamieszcza na paczkach.
  6. Paczki przenoszone są do strefy odbioru paczek.
- **Alternatywne przepływy zdarzeń:**
1.) W przypadku braku aktywnych zamówień system wyświetla odpowiedni komunikat.

2.1.a.) Jeśli pracownik nie jest w stanie zlokalizować produktu powinien powiadomić o tym zarządcę magazynu

2.1.b.) Jeśli przedmiot jest widocznie uszkodzony należy zgłosić to w systemie.

2.2.) W przypadku gdy naklejka z kodem kreskowym jest nieczytelna dla skanera
 kod produktu należy wpisać ręcznie wybierając odpowiednią opcję w systemie.

2.3.) W przypadku gdy system zgłosi, że skanowany produkt jest niedostępny należy
     odnieść produkt do magazynu i ponowić kroki 2.1.-2.2.

3.) W przypadku problemów z drukarką system powinien powiadomić administratora systemu i polecić pracownikowi czekać.

5). Tak samo jak w pkt 3.
- **Wyjątki w przepływach:** W przypadku błędu systemu pracownik powinien przerwać
i przejść do innych zadań.
