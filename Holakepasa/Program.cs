// See https://aka.ms/new-console-template for more information

class Program
{
    
    static List<string> results = new List<string>(new string[16]); 
    static Random rnd = new Random();
    static List<string> lanzar = new List<string> { "Rock", "Paper", "Scissors" };


    static void PlayRPS(int playerIndex)
    {
        int index = rnd.Next(0, lanzar.Count);
        string play = lanzar[index];
        results[playerIndex] = play;
        Console.WriteLine("Challenger played: " + lanzar[index]);
    }

    static void Main()
    {
        Console.WriteLine("Welcome to RPS! The biggest competition in the world!!!");
        Thread[] players = new Thread[16];

        for (int i = 0; i < 16; i++)
        {
            int playerIndex = i;
            players[i] = new Thread(() => PlayRPS(playerIndex)); // El hilo ejecuta esta función
            players[i].Start(); // Comienza el hilo
        }

        foreach (var player in players)
        {
            player.Join();
        }

        Console.WriteLine("\nInitial results:");
        for (int i = 0; i < 16; i++) 
        {
            Console.WriteLine($"Jugador {i + 1}: {results[i]}");
        }

        // Competición como tal:

        List<int> listPlayers = new List<int>();
        for (int i = 0; i < 16; i++) listPlayers.Add(i);

        int ronda = 1;
        while (listPlayers.Count > 1)
        {
            Console.WriteLine($"\n--- Ronda {ronda} ---");
            List<int> ganadores = new List<int>();

            for (int i = 0; i < listPlayers.Count; i += 2)
            {
                int player1 = listPlayers[i];
                int player2 = listPlayers[i + 1];

                string winner = CheckResult(results[player1], results[player2]);
                if (winner == "Player 1")
                {
                    Console.WriteLine(
                        $"Player {player1 + 1} (\"{results[player1]}\") defeats {player2 + 1} (\"{results[player2]}\")");
                    ganadores.Add(player1);
                }
                else if (winner == "Player 2")
                {
                    Console.WriteLine(
                        $"Player {player2 + 1} (\"{results[player2]}\") defeats {player1 + 1} (\"{results[player1]}\")");
                    ganadores.Add(player2);
                }
                else
                {
                    Console.WriteLine(
                        $"Tie between {player1 + 1} and {player2 + 1}, choosing randomly.");
                    ganadores.Add(rnd.Next(0, 2) == 0 ? player1 : player2);
                }
            }

            listPlayers = ganadores;
            ronda++;
        }
        Console.WriteLine($"\nThe champion is {listPlayers[0] + 1} with \"{results[listPlayers[0]]}\". Congrats!");
    }

    static string CheckResult(string option1, string option2)
    {
        if (option1 == option2) return "Empate";

        if ((option1 == "Rock" && option2 == "Scissors") ||
            (option1 == "Paper" && option2 == "Rock") ||
            (option1 == "Scissors" && option2 == "Paper"))
        {
            return "Player 1";
        }
        else
        {
            return "Player 2";
        }
    }
}