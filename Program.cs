namespace TextAdventure;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("### Welcome to Text Adventure! ###");
        Hero hero = new Hero();
        while (hero.location != "quit")
        {
            if (hero.location == "new game")
            {
                NewGame(hero);
            }
            else if (hero.location == "table room")
            {
                TableRoom(hero);
            }
            else if (hero.location == "corridor")
            {
                Corridor(hero);
            }
            else if (hero.location == "locked room")
            {
                LockedRoom(hero);
            }
            else if (hero.location == "third room")
            {
                ThirdRoom(hero);
            }
            else if (hero.location == "back outside")
            {
                BackOutside(hero);
            }
            else
            {
                Console.Error.WriteLine($"You forgot to implement '{hero.location}'!");
            }
        }
    }

// ******* FUNCTIONS *******//

    // ** ROOMS **

    static void NewGame(Hero hero)
    {
        Console.Clear();
        string name = "";
        do
        {
            name = Ask("What is your name, adventurer? ");
        } while (!AskYesOrNo($"So, {name} it is? "));

        hero.name = name;
        hero.location = "table room";
    }

    static void TableRoom(Hero hero)
    {
        Console.Clear();
        hero.items.Add("wooden sword");
        Console.WriteLine("You are equipped with one wooden sword, and your task ");
        Console.WriteLine("is to slay the monster at the end of the adventure. ");
        Console.WriteLine("");
        Console.WriteLine("In front of you is a stone table with two items on it, ");
        Console.WriteLine("a knife and a key.");
        Console.WriteLine("");
        Console.WriteLine("You can only pick up one of these items.");
        while (true)
        {
            string item = Ask("Which item do you choose? ").ToLower();
            if (item == "key")
            {
                AskYesOrNo("Do you want to pick up the key? ");
                hero.items.Add("key");
                Console.WriteLine("You pick up the key");
                break;
            }

            if (item == "knife")
            {
                AskYesOrNo("Do you want to pick up the knife? ");
                hero.items.Add("knife");
                Console.WriteLine("You pick up the knife");
                break;
            }
        }

        Console.ReadLine();
        hero.location = "corridor";
    }

    static void Corridor(Hero hero)
    {
        Console.Clear();
        Console.WriteLine("You exit the room and find yourself standing in a dark ");
        Console.WriteLine("hallway. You can either enter another room on your right ");
        Console.WriteLine("side, or continue down the hallway on your left.");
        while (true)
        {
            string path = Ask("Which path do you choose? ").ToLower();
            if (path == "right")
            {
                Console.WriteLine("The room appears to be locked.");
                if (hero.items.Contains("key"))
                {
                    Console.WriteLine("Your key fits in the lock and unlocks the door!");
                    hero.location = "locked room";
                    hero.items.Remove("key");
                    break;
                }
                else
                {
                    Console.WriteLine("You have no key to unlock the door, so you proceed to ");
                    Console.WriteLine("continue down the hallway.");
                    hero.location = "third room";
                    break;
                }
            }
        }

        Console.ReadLine();
    }

    static void LockedRoom(Hero hero)
    {
        Console.Clear();
        Console.WriteLine("Inside the locked room ");
        Console.WriteLine("you find a shiny sword!");
        if (AskYesOrNo("Do you want it instead of " +
                       "your wooden sword? "))
        {
            Console.WriteLine("You tossed away your wooden sword for the new shiny one.");
            hero.items.Remove("wooden sword");
            hero.items.Add("shiny sword");
        }
        else
        {
            Console.WriteLine("You let the shiny sword be.");
        }
        
        Console.ReadLine();
        hero.location = "third room";
    }

    static void ThirdRoom(Hero hero)
    {
        Console.WriteLine("On the floor before you lies a lifeless corpse.\n" + "Its hand is clasped around something shiny.\n");
        if (AskYesOrNo("Do you loot the corpse or leave it?"))
        {
            Console.WriteLine("You pick up an old silver necklace.");
            if (RollD6() >= 3) // 67% chance
            {
                Console.WriteLine("A warm feeling spreads over your body.");
                hero.items.Add("blessed amulet");
            }
            else
            {
                Console.WriteLine("A cold shiver runs down your spine.");
                hero.items.Add("cursed amulet");
            }
        }

        Console.WriteLine("You leave the corpse and continue into the \n" + "next room.");
        Console.ReadLine();
        hero.location = "back outside";
    }

    static void BackOutside(Hero hero)
    {
        Console.WriteLine("A minotaur appears and charges towards you!");
        string defence = Ask("")
        Console.ReadLine();
    }

    static void BossFight(Hero hero)
    {
    }

    static void Win(Hero hero)
    {
    }

    static void Lose(Hero hero)
    {
    }

    static void GameOver(Hero hero)
    {
    }

    // ** QUESTIONS **

    static string Ask(string question)
    {
        string response;
        do
        {
            Console.Write(question);
            response = Console.ReadLine().Trim();
        } while (response == "");

        return response;
    }

    static bool AskYesOrNo(string question)
    {
        while (true)
        {
            string response = Ask(question).ToLower();
            switch (response)
            {
                case "yes":
                case "ok":
                    return true;
                case "no":
                    return false;
            }
        }
    }

    static int RollD6()
    {
        Random random = new Random();
        int roll = random.Next(1, 7);
        return roll;
    }
}