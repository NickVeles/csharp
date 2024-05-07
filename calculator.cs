// TODO: add parentheses evaluation
// TODO: add error handling
// TODO: add user input

internal class Program
{
    private static void Main()
    {
        //string expression = "100 * 2 - 50 / 5 + 30 * 3 - 15 / 3 + 10 * 4 - 20 / 2 + 25 * 2 - 35 / 7 + 40 * 3 - 45 / 9 + 50 * 2 - 55 / 11 + 60 * 3 - 65 / 13 + 70 * 2 - 75 / 5 + 80 * 4 - 85 / 17 + 90 * 3 - 95 / 19 + 100 * 2 - 105 / 7 + 110 * 3 - 115 / 23 + 120 * 4 - 125 / 5 + 130 * 2 - 135 / 3 + 140 * 3 - 145 / 29 + 150 * 2 - 155 / 31 + 160 * 3 - 165 / 11 + 170 * 4 - 175 / 7 + 180 * 2 - 185 / 37 + 190 * 3 - 195 / 13 + 200 * 4 - 205 / 5 + 210 * 2 - 215 / 43 + 220 * 3 - 225 / 15 + 230 * 2 - 235 / 47 + 240 * 3 - 245 / 49 + 250 * 4 - 255 / 17 + 260 * 2 - 265 / 53 + 270 * 3 - 275 / 55 + 280 * 2 - 285 / 19 + 290 * 3 - 295 / 59 + 300 * 4";
        string expression = "11*3+2";

        //Console.WriteLine(Evaluate(expression));
        double result = Evaluate(expression.Replace(" ", ""));

        Console.WriteLine(result);
    }

    /// <summary>
    /// Calculates the result of a binary operation between two numbers.
    /// </summary>
    /// <param name="sign">The operator symbol ('*', '/', '+', or '-').</param>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of the operation.</returns>
    static double Calculate(char sign, double left, double right)
    {
        return sign switch
        {
            '*' => left * right,

            '/' => left / right,

            '+' => left + right,

            '-' => left - right,
        };
    }

    /// <summary>
    /// Iterates through a list of strings representing a mathematical expression in postfix notation and evaluates it.
    /// </summary>
    /// <param name="memory">A list of strings representing the memory stack containing operands and operators.</param>
    /// <returns>The memory stack after evaluating the expression.</returns>
    static List<string> Iterate(List<string> memory)
    {
        char sign;
        double left;
        double right;

        while (memory.Count > 1)
        {
            // Extracting operands and operator from the memory stack
            left = double.Parse(memory[^3]);
            sign = memory[^2][0]; // <-- previous sign
            right = double.Parse(memory[^1]);

            // Removing the operands and operator from the memory stack
            memory.RemoveRange(memory.Count - 2, 2);

            // Calculating the result of the operation and updating the memory stack
            memory[^1] = Calculate(sign, left, right).ToString();

            // DEBUG
            //Console.WriteLine($"memory[{memory.Count - 1}] = {memory[^1]}");
        }

        return memory;
    }

    /// <summary>
    /// Evaluates a mathematical expression given in postfix notation and returns the result.
    /// </summary>
    /// <param name="exp">The mathematical expression in postfix notation.</param>
    /// <returns>The result of the evaluated expression.</returns>
    static double Evaluate(string exp)
    {
        // Memory stack
        List<string> memory = [];

        // Flag indicating a new item
        bool newItem = true;

        // Valid operators
        char[] signs = ['+', '-', '*', '/'];

        foreach (char c in exp)
        {
            // If the character is a valid operator
            if (signs.Contains(c))
            {
                // If there are enough operands in memory and the current
                // operator is not a priority operator, iterate over the
                // memory
                if (memory.Count > 2 && (c == '+' || c == '-'))
                {
                    memory = Iterate(memory);
                }

                // Add the operator to the memory
                memory.Add(c.ToString());

                // Set stage for the new operand
                newItem = true;
            }

            // If the character is a new number, add it to the memory
            else if (newItem)
            {

                memory.Add(c.ToString());
                newItem = false;
            }

            // Else concatinate the character to the latest number
            else
            {
                memory[^1] += c;
            }
        }
        
        // Evaluate the remaining expression in memory
        memory = Iterate(memory);


        // Return the final number left in the memory
        return double.Parse(memory[0]);
    }
}
