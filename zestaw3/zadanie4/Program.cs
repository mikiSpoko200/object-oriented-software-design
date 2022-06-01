/* Opis problemu:
 *
 * Problem polega na tym, że klasa Square nie ma takich samych niezmienników jak klasa Rectangle.
 * Zgodnie z zasadą LSP klasy potomne powinny być semantycznie poprawne zawsze tam gdzie poprawna jest klasa macierzysta.
 * W naszym przypadku brak poprawności jest spowodowany tym jak zdefiniowane są właściwości Width oraz Height.
 * 
 * Właściwości ogólności nie powinni mieć skutków ubocznych innych niż modyfikacja zmiennej, której one dotyczą - od tego są funkcje.
 * 
 * Błąd wynika ze tego, jak zdefiniowany jest interfejs klasy Rectangle, a mianowicie daje klientowi możliwość modyfikowania wysokości i szerokości
 * prostokąta, co ma sens dla prostokąta ale nie koniecznie dla kwadratu.
 * 
 * Problem jaki to powoduje jest demonstrowany przez załączony framgent kodu gdzie AreaCalculator oczekuje od prostokąta 
 * i liczy odpowiednią dla niego funckją pole. Mimo tego, że tak naprawdę przekazujemy zrzutowany obiekt klasy Square.
 * 
 * Rozwiązanie:
 * Proponuję stowrzenie interfejsu IArea, który dostarcza metodę CalculateArea() obliczającą pole figury.
 * To w jaki sposób pole to ma być obliczone będzie specyficznie wyznaczone dla każdej klasy.
 * Problem: (W przypadku jakiegoś bardziej skomplikowanego systemu)
 * Nie wiem czy C# udostępnia możliwość nadania metodzie w interfejsie domyślnej implementacji (np Rust na to pozwala),
 * moglibyśmy wówczas stworzyć opakowujące interfejsy dla IArea, które dostarczałyby odpowiedniej funkcjonalności i pozwoliłby na
 * wtórne wykorzystanie kodu.
 * 
 * Alternatywnie, możemy zaimplementować interfejs, dla klas AreaCalculator i zagregować je wewnątrz obiektu.
 * Byłoby to zgodne z zasadami
 * - OCP: bo klient, mógłby dostarczyć swoje obiekty implementujące nasz interfejs.
 * - DIP: nasza klasa Rectangle zależałaby na abstrakcji naszego interfejsu a nie implementacji (?).
 * - SRP: nasza klasa delegowałaby obliczanie pola do innego obiektu a tym samym sama zajmowałaby się tylko danymi dla AreaCalculator.
 * 
 * Zrezygnowałbym też z relacji dziedziczenia ponieważ powyższe rozwiązanie daje wszystkie (i nawet więcej) zalety dziedziczenia,
 * bez dziedziczenia a tym samym automatycznie rozwiązuje problem niezgodności z LSP.
 * 
 * Wada: jest to dość skomplikowane. Czy nie można by czegoś uprościć?
 */

public interface IArea<D>
{
    /// <summary>
    /// Metoda obliczająca pole figury.
    /// </summary>
    /// <returns>pole figury</returns>
    public double Area(D args);
}

/// <summary>
/// Structure for Area method argument passing.
/// </summary>
public struct RectangleAreaArgs
{
    readonly public double width;
    readonly public double height;

    public RectangleAreaArgs(double width, double height)
    {
        this.width = width;
        this.height = height;
    }
}

/// <summary>
/// Class that represents a Rectangle.
/// </summary>
/// <typeparam name="T">Area calculator that should be used</typeparam>
public class Rectangle<T>
    where T : IArea<RectangleAreaArgs>, new()
{
    public double Width { get; set; }
    public double Height { get; set; }
    private T areaCalculator;

    public Rectangle()
    {
        this.areaCalculator = new T();
    }

    Rectangle(T areaCalculator) {
        this.areaCalculator = areaCalculator;
    }

    /// <summary>
    /// Calculates Area by delegating AreaCalculation to areaCalculator.
    /// </summary>
    /// <returns>Area of the rectangle</returns>
    public double CalculateArea()
    {
        return areaCalculator.Area(new RectangleAreaArgs(this.Width, this.Height));
    }
}

/// <summary>
/// Structure for Area method argument passing.
/// </summary>
public struct SquareAreaArgs
{
    readonly public double width;

    public SquareAreaArgs(double width)
    {
        this.width = width;
    }
}

/// <summary>
/// Class that represents a Square.
/// </summary>
/// <typeparam name="T">Area calculator that should be used</typeparam>
public class Square<T>
    where T : IArea<SquareAreaArgs>, new()
{
    public double Width { get; set; }
    private T areaCalculator;

    public Square()
    {
        this.areaCalculator = new T();
    }

    public Square(T areaCalculator)
    {
        this.areaCalculator = areaCalculator;
    }


    /// <summary>
    /// Calculates Area by delegating AreaCalculation to areaCalculator.
    /// </summary>
    /// <returns>Area of the rectangle</returns>
    public double CalculateArea()
    {
        return areaCalculator.Area(new SquareAreaArgs(this.Width));
    }
}


/// <summary>
/// An example AreaCalculator for the Rectangle.
/// </summary>
public class RectangleAreaCalculator: IArea<RectangleAreaArgs>
{
    public double Area(RectangleAreaArgs args)
    {
        return args.width * args.height;
    }
}


/// <summary>
/// An example AreaCalculator for the Square.
/// </summary>
public class SquareAreaCalculator : IArea<SquareAreaArgs>
{
    public double Area(SquareAreaArgs args)
    {
        return args.width * args.width;
    }
}

namespace solution
{
    public class program
    {
        static void Main()
        {
            int w = 4, h = 5;
            var calculator = new SquareAreaCalculator();
            var rect = new Rectangle<RectangleAreaCalculator>();  // default constructor
            rect.Width = w;
            rect.Height = h;
            var square = new Square<SquareAreaCalculator>(calculator);  // custom area calculator
            square.Width = w;
            Console.WriteLine("prostokąt o wymiarach {0} na {1} ma pole: {2}", rect.Width, rect.Height, rect.CalculateArea());
            Console.WriteLine("kwadrat o boku {0} ma pole: {1}", square.Width, square.CalculateArea());

        }
    }
}