// TODO: add tests
// TODO: add error handling

namespace Calculator
{
    internal class Program
    {
        // Define escape commands to exit the program
        private static readonly HashSet<string> escape = ["exit", "quit", "q"];
        // Define valid operation signs
        private static readonly HashSet<char> signs = ['+', '-', '*', '/', ')'];

        private static void Main()
        {
            string exp;
            Console.WriteLine("Calculator");

            while (true)
            {
                // User input
                Console.Write(">>> ");
                exp = (Console.ReadLine() ?? "").ToLower();

                // If user input is an escape command, break the loop and exit the program
                if (escape.Contains(exp))
                {
                    break;
                }

                // If user input is empty, continue to the next iteration
                else if (exp == "")
                {
                    continue;
                }

                // Evaluate the expression and print the result
                Console.WriteLine($"Result: {Evaluate(exp)}");
            }
        }

        /// <summary>
        /// Performs a calculation based on the provided sign and operands.
        /// </summary>
        /// <param name="sign">The operation to perform.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the calculation.</returns>
        /// <exception cref="ArgumentException">Thrown when an invalid operator is provided.</exception>
        static double Calculate(char sign, double left, double right) =>
            // Use switch expression to perform the operation based on the sign
            sign switch
            {
                '*' => left * right,
                '/' => left / right,
                '+' => left + right,
                '-' => left - right,
                _ => throw new ArgumentException($"Invalid operator '{sign}'"),
            };

        /// <summary>
        /// Iterates through the memory list and performs calculations until only one element is left.
        /// </summary>
        /// <param name="memory">The list of operands and operators to calculate.</param>
        /// <returns>The memory list after performing the calculations.</returns>
        static List<string> Iterate(List<string> memory)
        {
            char sign;
            double left;
            double right;

            // While there are more than one elements in the memory list
            while (memory.Count > 1)
            {
                // Parse the last three elements as left operand, sign, and right operand
                left = double.Parse(memory[^3]);
                sign = memory[^2][0];
                right = double.Parse(memory[^1]);

                // Remove the last three elements from the memory list
                memory.RemoveRange(memory.Count - 3, 2);
                // Calculate the result and add it back to the memory list
                memory[^1] = Calculate(sign, left, right).ToString();
            }

            return memory;
        }

        /// <summary>
        /// Evaluates a mathematical expression and returns the result.
        /// </summary>
        /// <param name="exp">The expression to evaluate.</param>
        /// <returns>The result of the evaluation.</returns>
        internal static double Evaluate(string exp)
        {
            List<string> memory = [];
            bool newItem = true;

            // If the expression starts with a negative number, insert a '0' at the beginning
            if (exp[0] == '-')
            {
                exp = exp.Insert(0, "0");
            }

            // Iterate through each character in the expression
            for (int i = 0; i < exp.Length; i++)
            {
                char c = exp[i];

                // If the character is an open parenthesis
                if (c == '(')
                {
                    int openCount = 1;
                    int closeCount = 0;
                    int j = i + 1;

                    // Find the matching close parenthesis
                    while (openCount > closeCount && j < exp.Length)
                    {
                        if (exp[j] == '(')
                        {
                            openCount++;
                        }
                        else if (exp[j] == ')')
                        {
                            closeCount++;
                        }
                        j++;
                    }

                    // Extract the sub-expression inside the parentheses
                    string subExp = exp.Substring(i + 1, j - i - 2);
                    // Evaluate the sub-expression and add the result to the memory list
                    memory.Add(Evaluate(subExp).ToString());
                    i = j - 1;
                }
                // If the character is a sign
                else if (signs.Contains(c))
                {
                    // If there are more than two elements in the memory list and the sign is '+' or '-'
                    if (memory.Count > 2 && (c == '+' || c == '-'))
                    {
                        // Perform calculations on the memory list
                        memory = Iterate(memory);
                    }

                    // If the character is a close parenthesis, continue to the next iteration
                    if (c == ')')
                    {
                        continue;
                    }

                    // Add the sign to the memory list
                    memory.Add(c.ToString());
                    newItem = true;
                }
                // If the character is a number
                else if (newItem)
                {
                    // Add the number to the memory list
                    memory.Add(c.ToString());
                    newItem = false;
                }
                else
                {
                    // Append the number to the last element in the memory list
                    memory[^1] += c;
                }
            }

            // Perform calculations on the memory list
            memory = Iterate(memory);
            return double.Round(double.Parse(memory[0]), 2);
        }
    }
}
