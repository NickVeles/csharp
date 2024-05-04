public enum Choice
{
    Rock,
    Paper,
    Scissors,
    Invalid,
    Quit
}

internal class Program
{
    private static void Main(string[] args)
    {
        static Choice GetChoice()
        {
            // Format user input
            Console.Write("\nRock/Paper/Scissors? ");
            string input = (Console.ReadLine() ?? "").Trim().ToLower();

            // Handle user input
            return input switch
            {
                "rock" => Choice.Rock,
                "r" => Choice.Rock,

                "paper" => Choice.Paper,
                "p" => Choice.Paper,

                "scissors" => Choice.Scissors,
                "s" => Choice.Scissors,

                "quit" => Choice.Quit,
                "q" => Choice.Quit,

                _ => Choice.Invalid,
            };
        }

        // Init Vals
        Choice playerChoice;
        Choice botChoice;

        int playerScore = 0;
        int botScore = 0;

        Random random = new();

        // Start game
        while ((playerChoice = GetChoice()) != Choice.Quit)
        {
            // Bot picks one of 3 valid choices
            botChoice = (Choice)(random.Next(3));

            // Handle invalid character
            if (playerChoice == Choice.Invalid)
            {
                Console.WriteLine("Invalid choice!");
                continue;
            }

            // Handle a draw
            else if (playerChoice == botChoice)
            {
                Console.WriteLine();
                Console.WriteLine($"A Draw!");
                Console.WriteLine($"(YOU) {playerChoice} {playerScore} - {botScore} {botChoice} (BOT)");
            }

            // Handle player win
            else if (
                (playerChoice == Choice.Rock && botChoice == Choice.Paper) ||
                (playerChoice == Choice.Paper && botChoice == Choice.Scissors) ||
                (playerChoice == Choice.Scissors && botChoice == Choice.Rock)
            )
            {
                Console.WriteLine();
                Console.WriteLine($"You Win!");
                Console.WriteLine($"(YOU) {playerChoice} {++playerScore} - {botScore} {botChoice} (BOT)");
            }

            // Handle player lose
            else
            {
                Console.WriteLine();
                Console.WriteLine($"You Lose!");
                Console.WriteLine($"(YOU) {playerChoice} {playerScore} - {++botScore} {botChoice} (BOT)");
            }
        }
    }
}
