
/*
## Moje rozumienie problemu:

Przedstawiona klasa może ulec zmianie z dwóch powodów:
1. Zmieni się sposób wytwarzania raportu (modyfikacja GetData) -
    Może nagle pojawi się konieczność przekazywania jakiegoś obiektu formatującego,
    albo decydującego skąd dany raport ma być pobrany.
2. Zmieni się sposób formatowania raportu (FormatDocument).
    Tutaj można rozpatrzeć dwa rodzaje formatowania:
    1. stylistyczne - ustalenie rozmiaru czcionki, marginesów, interlinii itp.
      Wówczas natomiast pewnie używalibyśmy jakiegoś specjalnego typu danych
      a nie standardowego String'a.
    2. konwersja do specyficznego formatu np. pdf, format dla drukarki, kompresja itp.

    Możliwe oczywiście, że metoda robi obie te rzeczy ale to ponownie byłoby
    mocnym naruszeniem zasad dobrego projektowania.

    W obu przypadkach dostarczony interfejs daje bardzo małą elastyczność.
    Nieprzestrzegana jest zasada Open Closed, ponieważ klient nie ma żadnej możliwości
    dostarczenia konfiguracji poprzez wstrzyknięcie funkcjonalności formatowania czy
    algorytmu konwersji.

    Znaczenie metody PrintReport jest powiązane ze znaczeniem metody FormatDocument.
    Dodatkowo odnoszę wrażenie, że dodawanie metod takich jak PrintReport jest słabym pomysłem.
    Chodzi mi tutaj o metody których głównym celem są skutki uboczne taki jak wypisywanie
    na wyjście standardowe, zapis do pliku, drukowanie czy zapis do bazy danych.
    Uważam tak ponieważ:
    1. Operacje IO są mocno związane z medium na którym operują. Dodatkowo potencjalnie wiele
    klas może chcieć korzystać z tych operacji i gdybyśmy implementowali to dla każdej
    z nich osobno prowadziłoby to do dużego powielania tego samego kodu.
    Dużo czystszym rozwiązaniem uważam, byłoby stworzenie jednej klasy odpowiedzialnej za dany rodzaj
    operacji wejścia/wyjścia oraz udostępnić odpowiedni interfejs, który pozwalałby innym klasom
    używać tej operacji poprzez produkowanie danych w odpowiednim formacie dla danego medium
    (odwrócenie zależności w pewnym sensie).

    Taki kod byłby łatwo rozszerzalny oraz sprawiłby, że kod klas byłby bardziej spójny jakoże klasy
    implementowałyby tylko tę część operacji, która faktycznie jest specyficzna do danych na jakich
    klasy operują.

    Dodatkowo operacje IO są jednymi z najkosztowanijszych elemenów aplikacji.
    Przeniesienie zarządania nimi do jednego obiektu pozwoliłoby na implementację
    rozmaitych strategii zarządania tymi operacjami np. buforowanie i wysyłanie danych w seriach
    co jest znacznie szybsze, ponieważ pozwala uniknąć zwieokrotnienia narzutu związanego
    z wykonaniem operacji.


## Propozycja rozwiązania problemu:

Mam dwa pomysły, z których każdy ma pewne wady i zalety.
Oba zakładają stworzenie osobnej klasy Report

 */


mod elementy_programowania_obiektowego_rust {
    /*
    W Rust deklaracja i definicje metod mogą być rozbite na wiele tzw. bloków implementacyjnych.
    pozwala to na np. warunkową kompilację i lepszą modularycjację kodu.
    Implementacja Rust'owych interfejsów nazywanych Cechami (ang. Trait) - będę je nazywał interfejsami ponieważ
    ponieważ uważam, że ta nazwa dobrze oddaje to co kod robi bez niepotrzebnego wprowadzania
    nowej nomenklatury plus spolszczenie słowa Trait na słowo cecha jest w moim odczuciu dość słabe.

    Przykład:
    */

    /// przykładowa klasa
    pub struct Osoba {
        imie: String,
        nazwisko: String,
        wiek: u32,
    }

    /// blok implementacyjny z funkcjonalnością specyficzną dla klasy Osoba
    /// Potencjalnie możemy napisać wiele takich bloków.
    impl Osoba {
        pub fn new(imie: String, nazwisko: String, wiek: u32) -> Self {
            Self { imie, nazwisko, wiek }
        }
    }

    /// możemy zdefiniować publiczny interfejs IDisplay w nastęujący sposób:
    pub trait IDisplay {
        // teraz definiujemy metody interfejsu
        fn display(&self) -> String;  // <- brak klamer oznacza deklarację bez definicji.
    }

    /// teraz możemy zaimplementować zdefiniowany interfejs dla naszej klasy
    impl IDisplay for Osoba {
        fn display(&self) -> String {
            format!("Osoba o imieniu: {}, nazwisku: {} ma wiek: {}", self.imie, self.nazwisko, self.wiek)
        }
    }

    pub fn przyklad() {
        let imie = String::from("Mikołaj");
        let nazwisko = String::from("Depta");
        let wiek = 21;
        let osoba = Osoba::new(imie, nazwisko, wiek);
        println!("{}", osoba.display());
    }
}

/// W tym module zamieściłem pierwsze rozwiązanie.
/// Polega ono na zmianie perspektywy. O tyle o ile nienaturalnym wydaje mi się,
/// że pomocnicza klasa ReportPrinter jest odpowiedzialana za tworzenie raportu (GetData),
/// tak już jestem w stanie przyjąć, że klasa Report, chce móc się formatować czy też konwertować.
/// Taka klasa faktycznie ma jedną odpowiedzialność - obsługę raportu i wszystkie operacja, które
/// na nim wykona są dla niego specyficzne, operacje te można wyabstrachować i opakować w interfejsy
/// np: IFormat czy IConvert (bardziej ogólnie Into<T>) i IDisplay do drukowania.
/// i wówczas wydaje mi się, że byłaby to sytuacja, o której z resztą była mowa na wykłądzie,
/// gdzie klasa implementuje wiele interfejsów ale są one różnymi czynnościami składającymi się
/// na spójną całość. -- możliwe, że się mylę, tę zasadę jest mi zwłaszcza cieżko zrozumieć bo są
/// rozwiązania które jej nie przestrzegają a mimo to są bardzo przyjmene do pracy z np. dużo klas
/// z biblioteki standardowej implementuje wiele interfejsów.
pub mod pierwsza_implementacja {

    /// Ten interfejs pozwala klasie implementującej go na formatowanie siebie w jakiś sposób.
    /// W otakiej formie interfejs ten nieprzestrzega zasady open/closed bo klient, nie ma możliwości
    /// wpływu na formatowanie - pomijam dla prostoty.
    pub trait Format {
        fn format(&mut self);
    }

    pub struct Report {
        data: String
    }

    impl Report {
        pub fn new(data: String) -> Self {
            Self { data }
        }
    }

    // region tworzenie instancji Report
    // generyczny interfejs From<T> wymaga implementacji metody from<T>(_: T) -> Self
    // która pozwala na tworzenie nowych instacji Self (klasy implemetującej) z typu T.

    impl From<&str> for Report {
        fn from(data: &str) -> Self {
            Report::new(data.into())
        }
    }

    impl From<String> for Report {
        fn from(data: String) -> Self {
            Report::new(data)
        }
    }
    // endregion

    // region formatowanie

    // W razie potrzeby można stworzyć nowe metody i odpowiednio delegować wywołania.
    impl Format for Report {
        fn format(&mut self) {
            println!("Tutaj raport jest formatowany.");
            self.data.replace(" ", " <to jest nowe> ");
        }
    }

    // endregion

    // region konwersja do napisu

    // Rust w bibliotece standardowej posiada interfejs Display, który jest analogiczny
    // do IDisplay z C# - pozwala on na konwersję obiekty do String'a w formalny/elegancki sposób.
    use std::fmt::{ Formatter, Display };

    impl Display for Report {
        fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
            write!(f, "{}", self.data)
        }
    }

    // endregion

    pub fn przyklad() {
        let mut report = Report::from("Tworzenie nowego raportu.");
        report.format();
        println!("{}", report);
    }
}

/// To rozwiązanie zakłada stworzenie osobnej klasy formatera, który zajmuje się formatowaniem
/// raportu, to pozwala potencjalnie na lepsze reużywanie kodu i potencjalnie implementację wielu
/// strategii formatowania. Nie dokońca rozumiem zaletę takiego rozwiązania.
/// Ponieważ, jakby nie patrzeć i tak będziemy musieli zmienić jakąś klasę.
/// A takie rozbijanie klas z mojego aktualnego punktu widzenia jedynie rozspaja trochę na siłę,
/// klasę żeby sztucznie miała mniej metod, mimo tego że wszystkie operują na jednych danych.
pub mod druga_implementacja {

    struct Report {
        data: String
    }

    impl Report {
        pub fn new(data: String) -> Self {
            Self { data }
        }
    }

    /// ogólny interfejs pozwalający na tworzenie nowych formaterów - zgodnie z zasadą open closed.
    pub trait Formatter {
        /// formatuj przekazany tekst.
        fn format(&self, data: &mut String);
    }

    struct SomeFormatter {
        format_config: Vec<String>  // Jakiś tam odpowiedni format informacji o formatowaniu.
    }

    impl SomeFormatter {
        pub fn new(format_config: Vec<String>) -> Self {
            SomeFormatter { format_config }
        }
    }

    impl Formatter for SomeFormatter {
        fn format(&self, data: &mut String) {
            // tutaj formatuj dane.
            println!("Formatowanie {} prze formatera.", data);
            data.replace(" ", " <to jest nowe> ");
        }
    }

    pub fn przyklad() {
        let mut report = Report::new(String::from("Jakaś zawartość raportu."));
        let some_formatter = SomeFormatter::new(vec!["jakaś".into(), "specyfikacja".into(), "formatowania".into()]);
        some_formatter.format(&mut report.data);
    }
}