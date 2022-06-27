
Operation mySum = Functions.Sum;

//Console.WriteLine(mySum(2,3));
mySum = Functions.Mul;
//Console.WriteLine(mySum(2, 3));

Show cw = Console.WriteLine;
cw += Functions.ConsoleShow;
// cw("hola mundo");
// Functions.Some("Juan", "Guevara", cw);

Console.WriteLine("###############");

decimal amount = 100;
Func<decimal, decimal> iva = (decimal amount) => amount + (amount * 0.16m);
 decimal iva2 (decimal amount)
{
    return amount + (amount * 0.16m);
}
var discount = (decimal amount) => amount - (amount * 0.1m);
var surcharge = (decimal amount) => amount + (amount * 0.2m);
var annualPartialities = (decimal amount) => amount / 12;

//Pip gets as argument a first order function
Console.WriteLine(annualPartialities(discount(iva(surcharge(amount)))));
Console.WriteLine(amount.Pipe(iva));
var total = amount
    .Pipe(surcharge)
    .Pipe(iva)
    .Pipe(discount)
    .Pipe(annualPartialities);
Console.WriteLine("TOTAL: " + total);


var reversse = (string s) =>
{
    var charArray = s.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
};
Func<string,string> upper = (string s) => s.ToUpper();
var name = "tony";
Console.WriteLine(name.PipeStr(reversse).PipeStr(upper));

var upperList = (List<string> l) => l.Select(e => e.ToUpper()).ToList();    
var orderedList = (List<string> l) => l.OrderBy(o => o.ToUpper()).ToList();    
Func<List<string>, List<string>> upperList2 = (List<string> l) => l.Select(e => e.ToUpper()).ToList();
var beers = new List<string>()
{
    "asd","corona","colima","stella"
};

var readyBeers = beers.PipeStr(upperList2).PipeStr(orderedList);
readyBeers.ForEach(rb => Console.WriteLine(rb));


Console.WriteLine("##############");






#region Action
string hi = "Hola";
Action<string> showMessage = Console.WriteLine;
Action<string, string> showMessage2 = (a, b) => { 
    Console.WriteLine($"{hi} {a} {b}"); 
};
Action<string,string,string> showMessage3= (a,b,c) => Console.WriteLine($"{a} {b} {c}");

/*
showMessage2("Héctor", "De León");
showMessage3("Héctor", "De León", "Dev");
Functions.SomeAction("Héctor", "De León", (a) =>
{
    Console.WriteLine("soy una expression lambda "+a);
});
Functions.SomeAction("Héctor", "De León", showMessage);
*/

#endregion

#region Func
Func<int> numberRandom = () => new Random().Next(0, 100);
Func<int,string> numberRandomLimit = (limit) => new Random().Next(0, limit).ToString();

// Console.WriteLine(numberRandom());
// Console.WriteLine(numberRandomLimit(10000));
#endregion

#region Predicate

Predicate<string> hasSpaceOrA = (word) => word.Contains(" ") || word.ToUpper().Contains("A");
Console.WriteLine(hasSpaceOrA("beear"));
Console.WriteLine(hasSpaceOrA("p ati to"));

var words = new List<string>()
{
    "beer",
    "patito",
    "sandia",
    "hola mundo",
    "c#"
};
var wordsNew = words.FindAll(w => !hasSpaceOrA(w));
foreach (var w in wordsNew) Console.WriteLine(w);
#endregion


#region Delegados
delegate int Operation(int a, int b);
public delegate void Show(string message);
public delegate void Show2(string message, string message2);
public delegate void Show3(string message, string message2, string message3);

#endregion

public class Functions
{
    public static int Sum(int x, int y) => x + y;
    public static int Mul(int num1, int num2) => num1 * num2;
    public static void ConsoleShow(string m) => Console.WriteLine(m.ToUpper());

    public static void Some(string name, string lastName, Show fn)
    {
        Console.WriteLine("Hago algo al inicio");
        fn($"Hola {name} {lastName}");
        Console.WriteLine("Hago algo al final");
    }

    public static void SomeAction(string name, string lastName, Action<string> fn)
    {
        Console.WriteLine("Hago algo al inicio");
        fn($"Hola {name} {lastName}");
        Console.WriteLine("Hago algo al final");
    }

    
}

public static class Methods
{
    public static TOut Pipe<TIn,TOut>(this TIn v, Func<TIn, TOut> fnOut ) 
        where TIn : struct
        where TOut : struct
    {
        return fnOut(v);
    }

    public static TOut PipeStr<TIn, TOut>(this TIn v, Func<TIn, TOut> fnOut)
        where TIn : class
        where TOut : class
    {
        return fnOut(v);
    }
}
