# Zad 1.

link do dokumentu: http://www.wfosgw.poznan.pl/wp-content/uploads/2016/11/strona-www-opis-przedmiotu-zam%C3%B3wienia.pdf

Listing kategorii **FURPS**:

## **Functional:**

1. System musi być wyposażony w panel administracyjny dostępny dla
administratorów i redaktorów serwisu, zawierający wszystkie funkcje
administracyjne i redakcyjne systemu.

  **Ocena SMART**:
   - Simple: Tak,
   - Measurable: Tak,
   - Achievable: Tak,
   - Relevant: Tak,
   - Time-specific: Tak,

  **Pytanie:** Czy jedno konto może być jednocześnie administratorem i redaktorem?


2. System musi posiadać pracujący w trybie on-line edytor WYSIWYG.
  Edytor musi posiadać co najmniej takie funkcje jak:
 - a) Pole format zawierające predefiniowane elementy strukturalne
treści (P, H1, H2, H3, H4)
 - b) Pole styl – zawierające predefiniowane style CSS
 - c) Opcje: Wytnij, Kopiuj, Wklej, Wklej jako czysty tekst, Wklej z Worda
 - d) Opcje: Znajdź, Zamień, Zaznacz wszystko, Usuń formatowanie
 - e) Opcje: Pogrubienie, Kursywa, Podkreślenie, Przekreślenie, Indeks
dolny, Indeks górny
 - f) Opcje: Wstaw/Usuń numerowanie listy, Wstaw/Usuń
wypunktowanie listy
 - g) Opcje: Zmniejsz/Zwiększ wcięcie, Wyrównaj do lewej, środka,
prawej, lewej i prawej
 - h) Opcje: Wstaw/Edytuj/Usuń załącznik, grafikę, Flash, hiperłącze,
kotwicę, embedowanie treści z serwisu YouTube
 - i) Opcje: Wstaw/Edytuj tabelę
 - j) Opcje: Zmień kolor czcionki, Zmień kolor tła
 - k) Opcje: Pokaż/Edytuj kod źródłowy
 - l) Podgląd strony
 - m) Podział strony (stronicowanie)

  **Ocena SMART**:
    - Simple: Nie,
    - Measurable: Tak,
    - Achievable: Tak,
    - Relevant: Tak,
    - Time-specific: Tak,

  **Pytanie**: Czy muszą być wspierane skróty klawiszowe?

## Supportability:
1. System musi umożliwiać automatyczne tworzenie kopii bezpieczeństwa
wszystkich elementów składających się na serwis (baza danych,
aplikacje, pliki) z częstotliwością określoną przez administratora.

  **Ocena SMART**:
   - Simple: Tak,
   - Measurable: Nie,
   - Achievable: Tak,
   - Relevant: Tak,
   - Time-specific: Tak,

  **Pytanie**: Ile zasobów pamięciowych można przeznaczyć na kopie zapasowe? Ile kopii w tył ma być zapis przechowywany?

2. System musi dawać możliwość ustalenia przez administratora miejsca
przechowywania kopii bezpieczeństwa, w tym na innych serwerach.

  **Ocena SMART**:
   - Simple: Nie,
   - Measurable: Tak,
   - Achievable: Tak,
   - Relevant: Tak,
   - Time-specific: Tak,

  **Pytanie**: Czy serwery muszą podlegać walidacji?

3. System musi umożliwiać tworzenie i zmianę reguł dotyczących długości
oraz stopnia skomplikowania haseł przechowywanych w bazie danych
systemu, a także umożliwiać określenie czasu, po którym konieczna
będzie zmiana hasła.

  **Ocena SMART**:
   - Simple: Tak,
   - Measurable: Tak,
   - Achievable: Tak,
   - Relevant: Tak,
   - Time-specific: Tak,

  **Pytanie**: Czy przy zmianie wymagań dotyczących haseł baza haseł powinna zostać przeskanowana i sprawdzona pod kontem istnienia starych haseł niezgodnych z nowymi wymaganiami?

4. Hasła użytkowników nie mogą być przechowywane w bazie systemu w
postaci jawnej, lecz z wykorzystaniem bezpiecznej funkcji skrótu (np.
SHA - Secure Hash Algorithm).

  **Ocena SMART**:
   - Simple: Tak,
   - Measurable: Tak,
   - Achievable: Tak,
   - Relevant: Tak,
   - Time-specific: Tak,

  **Pytanie**: Czy jakiś konkretny system bazodanowy będzie w użyciu?


3. Implementation:

- Zgodność z HTML5 i CSS3

**Ocena SMART**:
 - Simple: Tak,
 - Measurable: Tak,
 - Achievable: Tak,
 - Relevant: Tak,
 - Time-specific: Nie,

**Pytanie**: Na jakich przeglądarkach ma działać serwis?

- Prawidłowa walidacja kodu HTML i CSS za pomocą walidatora: http://validator.w3.org/

**Ocena SMART**:
 - Simple: Tak,
 - Measurable: Tak,
 - Achievable: Tak,
 - Relevant: Tak,
 - Time-specific: Nie,

**Pytanie**: Co powinno się dziać w przypadku awarii wskazanego serwisu?
