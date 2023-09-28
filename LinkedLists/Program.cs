namespace LinkedLists
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Monster monster = new Monster("Type", 0, 0, 0, 0);
            StartGame();
            Console.Clear();


            // Read monster data from the Stats.txt file
            List<Monster> monsters = ReadMonsterData("Stats.txt");


            // Create a doubly linked list to record results
            LinkedList<string> results = new LinkedList<string>();

            //Create rooms
            List<Rooms> rooms = new List<Rooms>();
            rooms.Add(new Rooms("1.Living Room", true, 200));
            rooms.Add(new Rooms("2.Basement", false, 400));
            rooms.Add(new Rooms("3.Penthouse", true, 5000));

            Console.WriteLine("\nHere is your rooms:");

            // Display the list of rooms

            foreach (Rooms r in rooms)
            {
                Console.WriteLine($"\nName: {r.Name}, Light: {r.Light}, Size: {r.Size}");
            }

            Console.WriteLine("\nEnter your room number");



            //handle choices
            int choice1;
            if (int.TryParse(Console.ReadLine(), out choice1))
            {

                switch (choice1)
                {
                    case 1:
                        string result1 = $"You entered {rooms[0].Name} with Size of {rooms[0].Size} and Light is {rooms[2].Light}";
                        results.AddFirst(result1);
                        Console.WriteLine(result1);
                        break;
                    case 2:
                        string result2 = $"You entered {rooms[1].Name} with Size of {rooms[1].Size} and Light is {rooms[1].Light} ";
                        results.AddFirst(result2);
                        Console.WriteLine(result2);
                        break;
                    case 3:
                        string result3 = $"You entered {rooms[2].Name} with Size of {rooms[2].Size} and Light is {rooms[2].Light} ";
                        results.AddFirst(result3);
                        Console.WriteLine(result3);
                        break;
                    case 0:
                        // Handle quitting the game
                        Console.WriteLine("Goodbye!");

                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please start a game again and enter a number.");
                StartGame();
            }




            while (monsters.Count > 0)
            {
                Console.WriteLine(" Choose a monster to face:");
                Console.WriteLine(" ");


                for (int i = 0; i < monsters.Count; i++)
                {

                    Console.WriteLine($"{i + 1}. Type: {monsters[i].Type} MP {monsters[i].MP} HP:{monsters[i].HP} AP: {monsters[i].AP} DEF {monsters[i].DEF}");
                }

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= monsters.Count)
                {
                    Monster selectedMonster = monsters[choice - 1];
                    Console.WriteLine($"You face a {selectedMonster.Type}!");

                    // Simulate a battle (you can implement your own battle logic here)
                    bool playerWins = Battle(selectedMonster);

                    if (playerWins)
                    {
                        results.AddLast($"You defeated a {selectedMonster.Type}");


                        monsters.RemoveAt(choice - 1);
                    }
                    else
                    {
                        results.AddLast($"You were defeated by a {selectedMonster.Type}");
                        break; // Game over
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }


            Console.WriteLine("Game over. Here are your results:");

            // Print results to a file
            using (StreamWriter writer = new StreamWriter("Results.txt"))
            {
                foreach (string result in results)
                {
                    Console.WriteLine(result);
                    writer.WriteLine(result);
                }
            }


        }

        static List<Monster> ReadMonsterData(string fileName)
        {
            fileName = Path.GetFileName("Stats.txt");

            List<Monster> monsters = new List<Monster>();

            try
            {
                string[] lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                {
                    string[] data = line.Split(' ');
                    if (data.Length == 5)
                    {
                        string type = data[0];
                        int hp = int.Parse(data[1]);
                        int mp = int.Parse(data[2]);
                        int ap = int.Parse(data[3]);
                        int def = int.Parse(data[4]);
                        monsters.Add(new Monster(type, hp, mp, ap, def));
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Stats.txt file not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return monsters;
        }

        static void StartGame()
        {


            Console.WriteLine("Would you like to start a game? Yes or No");
            string input = Console.ReadLine();
            if (input == "yes")
            {

                Console.WriteLine("Welcome to the Dungeon Crawler Game! Choose a room below to eneter!");
                Console.ReadLine();
                Console.Clear();

            }
            else
            {
                Console.WriteLine("Game over. Here are your results:");
            }
        }

        static bool Battle(Monster monster)
        {

            // For simplicity, let's assume the player always wins
            Player player = new Player();
            if (monster.DEF == player.DF)
            {
                Console.WriteLine("Sorry your Defence powers are equal you cant deafeat");
            }
            else if (monster.DEF > player.DF) { }
            {
                Console.WriteLine($"Your defence power are much more then {monster.Type}");
                Console.WriteLine($"You defeated the {monster.Type}!");
                Console.WriteLine($"\vYou can defeat another one!");
            }



            Console.ReadLine();
            //Console.WriteLine($"You defeated the {monster.Type}!");
            return true;
        }

    }
}
