using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using Pastel;

namespace Final_Game_Project
{
    class Encounter
    {
        string Name;
        protected string Description;
        protected string[] EncounterChoices;
        protected string EncounterArt;

        public Encounter(string name, string description, string[] encounterChoices, string encounterArt)
        {
            Name = name;
            Description = description;
            EncounterChoices = encounterChoices;
            EncounterArt = encounterArt;
        }

        public virtual void DisplayEncounter(Player currentPlayer)
        {
            DisplayEncounterArt();
            
            Menu encounterMenu = new Menu(Description, EncounterChoices, false);
            int index = encounterMenu.Run();
            switch (index)
            {
                case 0:
                    Menu leftPathMenu = new Menu("> As you turn left, you see a narrow passage way. On one side is a dark pit, and you cannot see the bottom. On the other side, you see a dangerous snake. What do you do? ", new string[] { "Fight the Snake" + "  (Enter Combat)".Pastel("#ff0000"), "Try and jump over the pit" + "  (You will probably fall!)".Pastel("#ff0000") }, false );
                    int leftPathIndex = leftPathMenu.Run();
                    switch (leftPathIndex)
                    {
                        case 0:
                            CombatEncounter snakeEncounter = new CombatEncounter("Snake fight", "Snake", "You move toward the Snake!", new string[] { "Attack the Snake!" }, 50, false, @"TEMP ART");
                            snakeEncounter.DisplayEncounter(currentPlayer);
                            WriteLine("After you defeat the Snake, you follow the path down as it heads deeper into the Cave, and ahead of you, you see the Piece of the True Cross! But just behind it is the Gaurdian!! \n> Press any key to continue...");
                            ReadKey(true);

                            GaurdianFight(currentPlayer);
                            break;
                        case 1:
                            currentPlayer.PlayerDeath();
                            break;
                    }
                    break;
                case 1:
                    WriteLine("As you turn to the right, you see the large Gaurdian, and behind it, the Piece of the True Cross!");
                    GaurdianFight(currentPlayer);
                    break;

            }
        }

        public void GaurdianFight(Player currentPlayer)
        {
            CombatEncounter guardianFight = new CombatEncounter("Gaurdian Fight", "Gaurdian", "You move toward the Gaurdian!", new string[] { "Attack the Gaurdian!" }, 70, true, @"TEMP ART");
            int preFightHealth = currentPlayer.Health;
            do
            {
                preFightHealth = currentPlayer.Health;
                guardianFight.DisplayEncounter(currentPlayer);
                if (!(preFightHealth == currentPlayer.Health))
                {
                    WriteLine("> Ouch! Looks like that didn't go too well! Better gear up and try again! Press any key to continue...");
                    ReadKey();
                }
            } while (!(preFightHealth == currentPlayer.Health));
        }

        public virtual float GivePlayerEquipment(Player currentPlayer)
        {
            WriteLine("\n> You gain equipment");
            float newWinChance = 1.0F;
            currentPlayer.UpdateArmor();
            return newWinChance;
        }

        public void DisplayEncounterArt()
        {
            Clear();
            WriteLine(EncounterArt);
        }
    }
}
