using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using Pastel;
using Figgle;

namespace Final_Game_Project
{
    class Game
    {
        Player CurrentPlayer;

        public Game()
        {

            WindowWidth = 180;
            WindowHeight = 50;

            Title = "The Forgotten Knight";


            CombatEncounter trollEncounter = new CombatEncounter("The Troll's Bridge", "Troll", "> As you leave the borders of Camelot, you encounter a rundown bridge. As you go to cross it, a Troll pops out! Do you: ", new string[] {"Attack it" + "  (Enter Combat)".Pastel("#ff0000"), "Bribe it", "Ford the River" + "  (You might drown!)".Pastel("#ff0000") }, 30, false, @"TEMP ART");
            CombatEncounter scottishClan = new CombatEncounter("The Scottish Clan's Camp", "Scottish Clan", "> After you encountered the Troll, you next stumble upon a camp of Scottish Highlanders! Do you: ", new string[] { "Attack them" + "  (Enter Combat)".Pastel("#ff0000"), "Bribe them", "Try to avoid them" + "  (You will probably get spotted!)".Pastel("#ff0000") }, 50, true, @"TEMP ART");
            DiscoveryEncounter ruinedTower = new DiscoveryEncounter("The Ruined Tower", "As you continue on your journey, you come across a Ruined Tower, with one full side of the tower piled in a heap at the bottom. When you enter it you discover a partially intact staircase leading up, and a staircase leading downwards. Both look so fragile, you know you'll only have one chance at entering and leaving, where do you go? ", new string[] {"Climb to the upper room", "Head down to the lower room" }, new SpecialItem("Map", ConsoleColor.DarkMagenta), @"TEMP ART");
            CombatEncounter romanCamp = new CombatEncounter("The Ruined Roman Fort", "Bandits", "> As you leave the Ruined Tower, you happen across a ruined Roman Fort, a leftover from the glory days of the Empire. As you slowly enter the fort, you see a large group of bandits hiding along the crumbling walls! Do you:", new string[] {"Spring the Ambush and fight it out" + "  (Enter Combat)".Pastel("#ff0000"), "Bribe them", "Try and run past them, deeper into the Fort" + "  (You will probably get spotted!)".Pastel("#ff0000") }, 60, true, @"TEMP ART");
            DiscoveryEncounter irishMonks = new DiscoveryEncounter("The Irish Monk's Pilgramage", "> After your encounter with the bandits, you find a group of bedraggled Irish Monks who have clearly been harrassed by the bandits. As you approach them, they begin to yell and scream, beseeching God to protect them. Do you: ", new string[] {"Help them","Give them some coin and direct them away", "Abandon them to their fate!" }, new SpecialItem("Chest Key", ConsoleColor.DarkGreen), @"TEMP ART");
            DiscoveryEncounter wrongCave = new DiscoveryEncounter("The Wrong Cave", "> Having left the Monks, you can feel you are finally nearing your goal, the Piece of the True Cross cries out to you! You climb the large mountains and begin searching the caves for the Piece's location, and finally stumble into one of them as night approaches. You quickly determine this is not the correct cave, but do find a small box", new string[] {"Open the Box"}, new SpecialItem("Chest Key", ConsoleColor.DarkGreen), @"TEMP ART");
            Encounter correctCave = new Encounter("The Hiding Place of the Piece of the True Cross", "> After leaving the Wrong Cave, you finally reach your goal! You have found it, the Hiding Place of the Piece of the True Cross, now all you have to do is get it! As you enter the Cave, you see a path leading to your left and a path to your right, which do you take?", new string[] { "The Left Path", "The Right Path" }, @"TEMP ART");

            RunMainMenu();
            InitializePlayer();

            HandleEncounter(trollEncounter);
            BetweenEncounters();

            HandleEncounter(scottishClan);
            BetweenEncounters();

            HandleEncounter(ruinedTower);
            BetweenEncounters();

            HandleEncounter(romanCamp);
            BetweenEncounters();

            HandleEncounter(irishMonks);
            BetweenEncounters();

            if (!CurrentPlayer.HasMap)
            {
                HandleEncounter(wrongCave);
                BetweenEncounters();
            }

            HandleEncounter(correctCave);

            DisplayWinScreen();
        }

        private void RunMainMenu()
        {
            Clear();
            DisplayTitle();
            Menu mainMenu = new Menu("Welcome to the Forgotten Knight!", new string[] { "Play", "About", "Exit" }, true);
            int index = mainMenu.Run();
            switch (index)
            {
                case 0:
                    DisplayStartLore();
                    break;
                case 1:
                    DisplayAbout();
                    break;
                case 2:
                    ExitGame();
                    break;
            }
        }

        private void HandleEncounter(Encounter encounter)
        {
            encounter.DisplayEncounter(CurrentPlayer);
            PressKeyToContinue();
        }

        private void BetweenEncounters()
        {
            Clear();
            DisplayLandscape();
            Menu betweenEncountersMenu = new Menu("> You continue your journey across the wilderness, and can have a moment of quiet reflection", new string[] {"Do you want to check your belongings and health?","Do you want to continue to the next encounter?"}, false);
            int betweenEncountersIndex = betweenEncountersMenu.Run();
            switch (betweenEncountersIndex)
            {
                case 0:
                    CurrentPlayer.DisplayPlayerStats(false);
                    if (CurrentPlayer.HasMap && !CurrentPlayer.hasSeenMap)
                    {
                        Menu readMapMenu = new Menu($"> Would you like to read the Map?", new string[] { "Yes", "No"}, false);
                        int readMapIndex = readMapMenu.Run();
                        switch (readMapIndex)
                        {
                            case 0:
                                WriteLine("> You read the map and figure out more precisely where you need to go! You head off towards the Cave that contains the Piece of the True Cross!");
                                CurrentPlayer.hasSeenMap = true;
                                break;
                            case 1:
                                WriteLine("> That silly map is just nonsense! You throw it away and carry on your journey");
                                CurrentPlayer.HasMap = false;
                                break;
                        }
                    }
                    break;
                case 1:
                    break;
            }
            PressKeyToContinue();
        }

        private void InitializePlayer()
        {
            Clear();
            DisplayTitle();
            CurrentPlayer = new Player(Player.AcquirePlayerName(), Player.AcquirePlayerColor());
            CurrentPlayer.DisplayPlayerStats(true);

            PressKeyToContinue();
        }

        private void DisplayAbout()
        {
            Clear();
            DisplayTitle();
            WriteLine("\nA Game by Sebastian Chrzanowski");
            WriteLine("     >> Credits <<       ");
            WriteLine("Blah Blah Blah");

            PressKeyToContinue();
            RunMainMenu();
        }

        public void PressKeyToContinue()
        {
            WriteLine("\n\nPress any key to continue...");
            ReadKey(true);
        }

        public void ExitGame()
        {
            Clear();
            DisplayTitle();
            WriteLine("\n\nPress any button to exit...");
            ReadKey(true);
            Environment.Exit(0);
        }

        private void DisplayStartLore()
        {
            Clear();
            DisplayTitle();
            WriteLine("Firness was a Knight of the Round Table, but you’ll never find their story in the Le Morte d’Arthur; " +
                "Arthur never let word of a quest going awry to get out, no, it was much simpler to remove the Knight in question from history.  " +
                "Firness was a proud and skilled knight, not as strong as Lancelot, but probably better than Gawain, poor bugger. " +
                "While Arthur fought his way across England and Northern France, Firness helped keep the peace and defeat his enemies. " +
                "Eventually, word reached Camelot of the discovery of the location of a Piece of the True Cross somewhere in the Scottish Highlands, " +
                "and of course, the Knights of the Round Table hastened to organize a quest to retrieve it. " +
                "Over the Knight’s protests, Firness was determined to achieve fame and glory in their own name, " +
                "and so set out to find the holy relic completely alone but for their horse and armed with a trusty sword and shield.");

            PressKeyToContinue();
        }

        private void DisplayWinScreen()
        {
            Clear();
            WriteLine(FiggleFonts.Larry3d.Render("YOU HAVE WON!!!!"));
            WriteLine("TEMP ART");

            WriteLine("You defeated the Gaurdian, and grab the Piece of the True Cross!!! Your Quest completed, you return to Camelot a Hero!");
            PressKeyToContinue();
        }

        private void DisplayLandscape()
        {
            WriteLine(@"TEMP ART");
        }

        public void DisplayTitle()
        {
            WriteLine(@"
                 _________ __              ________                            _     _                    ___  ____           _         __      _    
                |  _   _  [  |            |_   __  |                          / |_  / |_                 |_  ||_  _|         (_)       [  |    / |_  
                |_/ | | \_|| |--.  .---.    | |_ \_|.--.  _ .--. .--./)  .--.`| |-'`| |-'.---.  _ .--.     | |_/ /   _ .--.  __   .--./)| |--.`| |-' 
                    | |    | .-. |/ /__\\   |  _| / .'`\ [ `/'`\] /'`\;/ .'`\ \ |   | | / /__\\[ `.-. |    |  __'.  [ `.-. |[  | / /'`\;| .-. || |   
                   _| |_   | | | || \__.,  _| |_  | \__. || |   \ \._//| \__. | |,  | |,| \__., | | | |   _| |  \ \_ | | | | | | \ \._//| | | || |,  
                  |_____| [___]|__]'.__.' |_____|  '.__.'[___]  .',__`  '.__.'\__/  \__/ '.__.'[___||__] |____||____[___||__|___].',__`[___]|__]__/  
                                                               ( ( __))                                                         ( ( __))                                                                                                                                                                                                                                                                                                      
           ");
        }
    }
}
