Console.WriteLine("Console Calculator App");
Console.WriteLine("----------------------");
Console.WriteLine();

Console.WriteLine(Calc.Add(1, 5));



class Calc
{
      
    public static double Add(double x, double y) => x + y;
    public static double Subtract(double x, double y) => x - y;
    public static double Multiply(double x, double y) => x * y;
    public static double Divide(double x, double y)
    {
        try
        {
            return x / y;
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }
    }
}


