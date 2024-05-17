Console.WriteLine("Console Calculator App");
Console.WriteLine("----------------------");
Console.WriteLine();

Calc calc = new Calc();

bool runAgain = false;
do
{
    Calc.AskOperator();
    string? operatorInput = Console.ReadLine();
    while (operatorInput != "a" && operatorInput != "s" && operatorInput != "m" && operatorInput != "d" && operatorInput != "p")
    {
        Console.WriteLine("Invalid choice.");
        Calc.AskOperator();
        operatorInput = Console.ReadLine();
        if (operatorInput != null) operatorInput = operatorInput.Trim().ToLower();

    }

    if(operatorInput == "p")
    {
        if (calc.memory.Count > 0)
            calc.DisplayMemory();
        else
        {
            Console.WriteLine("Currently no operations in memory :(");
        }
    }
    else
    {
        double num1 = calc.AskNumber();
        double num2 = calc.AskNumber();
        double result = Calc.Evaluate(operatorInput, num1, num2);
        Calc.AddToMemory(calc.memory, operatorInput, num1, num2, result);
        Console.WriteLine(calc.memory.Last());
        Console.WriteLine($"Operations performed this session: {calc.memory.Count}");
    }
    
    Console.WriteLine("Would you like to perform another operation? y/n");
    string? response = Console.ReadLine();
    if(response != null) response = response.Trim().ToLower();
    runAgain = response == "y";
} while(runAgain);
Console.WriteLine("Goodbye!");



class Calc
{
    public List<string> memory = new();
    public static void AskOperator()
    {
        Console.WriteLine("Please select from the following");
        Console.WriteLine("\ta - Addition");
        Console.WriteLine("\ts - Subtraction");
        Console.WriteLine("\tm - Multiplication");
        Console.WriteLine("\td - Division");
        Console.WriteLine();
        Console.WriteLine("\tp - List previous operations");
    }

    public double AskNumber()
    {
        Console.WriteLine("Please enter a number or enter 'a' to display current operations or 'p' to use previous operation:");
        string? input = Console.ReadLine();
        double num;
        while (!Double.TryParse(input, out num) && input != "a" && input != "p")
        {
            Console.WriteLine("Please enter a VALID number:");
            input = Console.ReadLine();
        }
        if(input == "a")
        {
            Console.WriteLine("Please select from the following:"); 
            DisplayMemory();
            string? indexInput = Console.ReadLine();
            int memoryIndex;
            while(int.TryParse(indexInput, out memoryIndex) && (memoryIndex < 1 || memoryIndex > memory.Count))
            {
                Console.WriteLine("Please pick a valid option: ");
                indexInput = Console.ReadLine();
            }
            int equalIndex = memory[memoryIndex - 1].IndexOf("=");
            num = double.Parse(memory[memoryIndex - 1].Substring(equalIndex + 2));
        } else if(input == "p")
        {
            int equalIndex = memory.Last().IndexOf("=");
            num = double.Parse(memory.Last().Substring(equalIndex + 2));
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

    public void DisplayMemory()
    {
        if(memory.Count > 0)
        for(int i = 0; i < memory.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {memory[i]}");
        }
    }
}


