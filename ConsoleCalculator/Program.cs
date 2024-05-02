Console.WriteLine("Console Calculator App");
Console.WriteLine("----------------------");
Console.WriteLine();

List<string> memory = new();

Calc.AskOperator();
string? operatorInput = Console.ReadLine();
while(operatorInput != "a" && operatorInput != "s" && operatorInput != "m" && operatorInput != "d")
{
    Console.WriteLine("Invalid choice.");
    Calc.AskOperator();
    operatorInput = Console.ReadLine();
}

double num1 = Calc.AskNumber();
double num2 = Calc.AskNumber();
double result = Calc.Evaluate(operatorInput, num1, num2);
Calc.AddToMemory(memory,operatorInput, num1, num2, result);
Console.WriteLine(memory.Last());


class Calc
{
    public static void AskOperator()
    {
        Console.WriteLine("Please select from the following");
        Console.WriteLine("\ta - Addition");
        Console.WriteLine("\ts - Subtraction");
        Console.WriteLine("\tm - Multiplication");
        Console.WriteLine("\td - Division");
    }

    public static double AskNumber()
    {
        Console.WriteLine("Please enter a number:");
        string? input = Console.ReadLine();
        double num;
        while (!Double.TryParse(input, out num))
        {
            Console.WriteLine("Please enter a VALID number:");
            input = Console.ReadLine();
        }
        return num;
    }
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

    public static double Evaluate(string operatorInput, double x, double y)
    {
        switch(operatorInput)
        {
            case "a":
                return Add(x, y);
            case "s":
                return Subtract(x, y);
            case "m":
                return Multiply(x, y);
            case "d":
                return Divide(x, y);
            default:
                Console.WriteLine("Error occured during evaluation");
                return 0;
        }
    }

    public static void AddToMemory(List<string> list, string operatorInput, double x, double y, double result)
    {
        string op = operatorInput == "a" ? "+" : operatorInput == "s" ? "-" : operatorInput == "m" ? "*" : "/";
        list.Add($"{x} {op} {y} = {result}");
    }
}


