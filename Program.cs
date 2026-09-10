namespace TextAdventure;
//Det här är en kommentar.
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
            else if (hero.location == "shelter")
            {
                //Shelter(hero);
            }
            else if (hero.location == "boss fight")
            {
                BossFight(hero);
            }
            else if (hero.location == "win")
            {
                Win(hero);
            }
            else if (hero.location == "lose")
            {
                Lose(hero);
            }
            else if (hero.location == "game over")
            {
                GameOver(hero);
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
        string tableroomItem = "";
        do
        {
            tableroomItem = Ask("Which item do you choose? ");
            switch (tableroomItem)
            {
                case "key":
                    tableroomItem = "key";
                    break;
                case "knife":
                    tableroomItem = "knife";
                    break;
                case "none":
                    tableroomItem = "none";
                    break;
                default:
                    tableroomItem = "";
                    continue;
            }

            if (tableroomItem == "none")
            {
                if (!AskYesOrNo("Do you want to proceed without picking an item? "))
                    tableroomItem = "";
            }
            else
            {
                if (!AskYesOrNo($"Do you want to pick up the {tableroomItem}? "))
                    tableroomItem = "";
                else
                    Console.WriteLine($"You picked up the {tableroomItem}");
            }
        } while (tableroomItem == "");
        
        hero.items.Add(tableroomItem);

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
        Console.Clear();
        Console.WriteLine("On the floor before you lies a lifeless corpse.\n" +
                          "Its hand is clasped around something shiny.\n");
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
        Console.Clear();
        Console.WriteLine("A minotaur appears and charges towards you!");
        Console.ReadLine();
        hero.location = "boss fight";
        
       /* string direction = "";
        do
        { direction = Ask("Do you want to stay and face the enemy or flee though a small hole in the ground? (Stay/flee) ");
        } while (!AskYesOrNo($"So you want to to {direction}? "));

        if (direction == "stay")
        {
            Console.WriteLine("You stand your ground and face the charging enemy ");
            hero.Location = "bossfight";
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine(" You flee through the ground ");
            hero.Location = "shelter";
            Console.ReadLine();
        }*/
    }
    
  /*  static void Shelter(Hero hero)
    {
        Console.Clear();
        Console.WriteLine("Through the hole you find yourself in a large dark space\n In front of you a large spider appears and moves towards you");
        //Enemy spider = new Enemy("Spider", 25);
        //Battle(hero, spider);
    
        Console.WriteLine("The spider drops a special potion, you use it to regain som strength\n There's nothing more in the ground and must face the enemy you fled from");
        //hero.Health += 50;
        //hero.Location = "bossfight";
        Console.ReadLine();
    }*/

    static void BossFight(Hero hero)
    {
        Console.Clear();
        // Set up enemy
        Enemy minotaur = new Enemy();
        minotaur.name = "Minotaur";
        minotaur.health = 100;
        // Set up hero
        string heroAction = "";
        int heroAttack = 1;
        if (hero.items.Contains("shiny sword"))
            heroAttack += 8;
        if (hero.items.Contains("wooden sword"))
            heroAttack += 1;
        if (hero.items.Contains("knife"))
            heroAttack += 2;
        if (hero.items.Contains("blessed amulet"))
            heroAttack += 1;
        if (hero.items.Contains("cursed amulet"))
            heroAttack -= 1;

        do
        {
            Console.WriteLine($"{hero.name}s health: {hero.health} || Minotaurs health {minotaur.health}");

            Console.Write("The minotaur prepares to attack : ");
            if (heroAction != "paralyzed")
            {
                do
                {
                    heroAction = Ask("do you want to dodge/jump/parry? ");
                    if (heroAction != "dodge" || heroAction != "jump" || heroAction != "parry")
                    {
                        continue;
                    }
                } while (heroAction == "" || !AskYesOrNo($"Are you sure you want to {heroAction}? "));
            }
            else
            {
                Console.WriteLine($"you are recovering from the {minotaur.name}'s last attack. ");
                heroAction = "";
            }

            if (RollD6() >= 4)
            {
                Console.WriteLine($"The {minotaur.name} swings its club.");
                switch (heroAction)
                {
                    case "dodge":
                        Console.WriteLine("You dodge towards the powerful but slow swing, \n" +
                                          "and manage to do a counter-attack with your weapons");
                        // Enemy attacks
                        hero.health -= 5;
                        // Hero attacks
                        tellAmuletEffect(hero);
                        minotaur.health -= heroAttack;
                        break;
                    case "jump":
                        Console.WriteLine("You jumped in your stand, and didn't dodge the swing of the club.\n" +
                                          "You take critical damage!");
                        // Enemy attacks
                        hero.health -= 25;
                        break;
                    case "parry":
                        Console.WriteLine(
                            "You parry the attack with your weapons. It doesn't prevent the minotaur's club \n" +
                            "but you manage to inflict some damage to the minotaur. ");
                        // Enemy attacks
                        hero.health -= 25;
                        // Hero attacks
                        tellAmuletEffect(hero);
                        minotaur.health -= heroAttack;
                        break;
                    default:
                        Console.WriteLine("It lands a critical hit on you!");
                        // Enemy attacks
                        hero.health -= 25;
                        break;
                }
            }
            else
            {
                Console.WriteLine("The minotaur beat its club in the ground, and the ground shakes heavily. ");
                switch (heroAction)
                {
                    case "dodge":
                        Console.WriteLine(
                            "There's nowhere to dodge from the ground. You lose your stance on your feet, \n" +
                            "and fall on your back");
                        // Minotaur attacks
                        heroAction = "paralyzed";
                        break;
                    case "jump":
                        Console.WriteLine("You jump and avoid the earthquake from the club,\n" +
                                          "and manage to counter-attack minotaur whom is caught off guard. \n"
                                          + "You deal a lot of damage with your weapons.");
                        // Hero attacks
                        minotaur.health = heroAttack*2;
                        break;
                    case "parry":
                        Console.WriteLine("You run towards the minotaur, but lose balance on the shaking ground. \n" +
                                          "You trip with your weapons on the minotaur who lifts its club and flings you back.");
                        // Minotaur attacks
                        hero.health -= 5;
                        // Hero attacks
                        minotaur.health -= 5;
                        break;
                    default:
                        Console.WriteLine("The ground bounces you back on your feet!");
                        break;
                }
            }

            Console.ReadLine();
        } while (hero.health > 0 && minotaur.health > 0);
        
        if (hero.health > 0)
            hero.location = "win";
        else
            hero.location = "lose";
    }

    static void Win(Hero hero)
    {
        hero.location = "game over";
    }

    static void Lose(Hero hero)
    {
        hero.location = "game over";
    }

    static void GameOver(Hero hero)
    {
        hero.location = "new game";
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

    // ** ITEM EFFECTS **

    static void tellAmuletEffect(Hero hero)
    {
        if (hero.items.Contains("blessed amulet") && !hero.items.Contains("cursed amulet"))
            Console.WriteLine(
                "The amulet makes your aim with your weapons confident, \n" +
                "and you deal extra damage!");
        else if (hero.items.Contains("cursed amulet") && !hero.items.Contains("blessed amulet"))
            Console.WriteLine(
                "The amulet makes you feel uncertain where to aim on the monster. \n" +
                "You deal less damage than normal!");
    }

    // ** DIES **
    static int RollD6()
    {
        Random random = new Random();
        int roll = random.Next(1, 7);
        return roll;
    }
}