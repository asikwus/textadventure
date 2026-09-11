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
            switch (hero.location)
            {
                case "new game": NewGame(hero); break;
                case "table room": TableRoom(hero); break;
                case "corridor": Corridor(hero); break;
                case "locked room": LockedRoom(hero); break;
                case "third room": ThirdRoom(hero); break;
                case "back outside": BackOutside(hero); break;
                case "shelter": Shelter(hero); break;
                case "cell": Cell(hero); break;
                case "boss fight": BossFight(hero); break;
                case "win": Win(hero); break;
                case "lose": Lose(hero); break;
                case "game over": GameOver(hero); break;
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
        string path = "";
        bool doorUnlocked = false;
        while (path == "")
        {
            path = Ask("Which path do you choose? ").ToLower();
            if (path == "right")
            {
                if (!doorUnlocked)
                    Console.WriteLine("The door to the room appears to be locked.");
                if (hero.items.Contains("key"))
                {
                    if (!doorUnlocked)
                    {
                        Console.WriteLine("Your key fits in the lock and unlocks the door!");
                        doorUnlocked = true;
                    }

                    if (!AskYesOrNo("Do you want to enter the room? "))
                    {
                        path = "";
                        continue;
                    }

                    Console.WriteLine("You enter the unlocked room.");
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

            if (path == "left")
            {
                if (!AskYesOrNo("Do you want to continue down the hallway? "))
                {
                    path = "";
                    continue;
                }

                Console.WriteLine("You continue down the hallway.");
                hero.location = "third room";
                break;
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
        if (AskYesOrNo("Do you loot the corpse?"))
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
        //hero.location = "boss fight";

        string direction = "";
        do
        {
            direction = Ask(
                "Do you want to stay and face the enemy or flee though a small hole in the ground? (Stay/flee) ");
        } while (!AskYesOrNo($"So you want to to {direction}? "));

        if (direction == "stay")
        {
            Console.WriteLine("You stand your ground and face the charging enemy ");
            hero.location = "boss fight";
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("You flee through the ground ");
            hero.location = "shelter";
            Console.ReadLine();
        }
    }

    static void Shelter(Hero hero)
    {
        Console.Clear();
        Console.WriteLine(
            "Through the hole you find yourself in a large dark space\n In front of you a large spider appears and moves towards you");
        if (RollD6() >= 4)
        {
            Console.WriteLine("The spider slips in front of you and you smash it");
        }
        else
        {
            hero.health = hero.health - 10;
            Console.WriteLine("The spider plunges at you, successfully biting you for some hp before you finish it off. -10hp");
        }
        
        if (AskYesOrNo("Behind spider you've slain, you see a small door on the wall, do you want to go there? "))
        {
            hero.location = "cell";
        }
        else
        {
            Console.WriteLine("There's nothing more in the burrow, you turn around to face the enemy you fled from");
            hero.location = "boss fight";
            Console.ReadLine();
        }

    }

    static void Cell(Hero hero)
    {
        Console.Clear();
        if (AskYesOrNo("In the cell you find an old potion on the floor, do you want to drink it?"))
        {
            if (RollD6() >= 5)
            {
                Console.WriteLine("You take a sip from the potion and feels how it empowers you +25hp");
                hero.health += 25;
            }
            else
            {
                hero.health = hero.health - 10;
                Console.WriteLine("The potion drains your energy -10hp ");
            }
            
        }
        Console.WriteLine("There's nothing more in the cell, you go back to the foe you fled from");
        hero.location ="boss fight";
        Console.ReadLine();
    }

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
                        minotaur.health = heroAttack * 2;
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
        Console.Clear();
        Console.WriteLine("You beat the minotaur and escaped the area!");
        hero.location = "game over";
        Console.ReadLine();
    }

    static void Lose(Hero hero)
    {
        Console.Clear();
        Console.WriteLine("You died!");
        hero.location = "game over";
        Console.ReadLine();
    }

    static void GameOver(Hero hero)
    {
        if (AskYesOrNo("Do you want to play again? "))
        {
            hero.location = "new game";
            hero.health = 100;
            hero.items.Clear();
        }
        else
        {
            hero.location = "quit";
        }
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