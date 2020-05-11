using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using Pastel;

namespace Final_Game_Project
{
    class CombatEncounter : Encounter
    {
        string Name;
        int Difficulty;
        string EnemyName;
        bool DropEquipment;


        public CombatEncounter(string name, string enemyName, string description, string[] encounterChoices, int difficulty, bool dropEquipment, string encounterArt)
            : base(name, description, encounterChoices, encounterArt)
        {
            Name = name;
            EnemyName = enemyName;
            Difficulty = difficulty;
            DropEquipment = dropEquipment;
        }
        public override void DisplayEncounter(Player currentPlayer)
        {
            DisplayEncounterArt();

            Menu encounterMenu = new Menu(Description, EncounterChoices, false);
            int index = encounterMenu.Run();
            switch (index)
            {
                case 0:
                    EnterCombat(currentPlayer);
                    break;
                case 1:
                    BribeOption(currentPlayer);
                    break;
                case 2:
                    DeathOption(currentPlayer);
                    break;
            }
        }

        public void EnterCombat(Player currentPlayer)
        {
            ForegroundColor = currentPlayer.Color;
            WriteLine($"> You engage the {EnemyName} in furious mortal combat, striving mightly with your trusty {currentPlayer.Armor}!");

            Random random = new Random();
            if ((currentPlayer.WinChance) * 100 >= random.Next(0, Difficulty))
            {
                WriteLine($"> \n> You have come out on top, and have succesfully beaten the {EnemyName}! Nice work {currentPlayer.Name}!");
                if (DropEquipment)
                {
                    currentPlayer.WinChance += GivePlayerEquipment(currentPlayer);
                    currentPlayer.UpdateArmor();
                }
                WriteLine(">\n> You can now continue on your journey!");
            }
            else
            {
                WriteLine($"> Despite your best efforts, the {EnemyName} manages to score a hit on you! Ouch! That looked like it hurt! \n> You manage to slink away in disgrace, nursing your wounds.");
                currentPlayer.Health -= 25;
                currentPlayer.DisplayPlayerStats(false);
            }
            ResetColor();
        }
        public override float GivePlayerEquipment(Player currentPlayer)
        {
            WriteLine("\n> With your victory, you have managed to scavange some better equipment!");
            float newWinChance = 0.25F;
            return newWinChance;
        }

        private void BribeOption(Player currentPlayer)
        {
            Menu confirmBribeMenu = new Menu($"> Are you sure you want to bribe your way through? You have {currentPlayer.CoinPouch} gold coins left.", new string[] { "Yes", "No" + $"(You will have to fight the {EnemyName}!)".Pastel("#ff0000") }, false);
            int index = confirmBribeMenu.Run();
            switch (index)
            {
                case 0:
                    WriteLine($"> You approach the {EnemyName} with your palm outstretched, containing some of your precious coin. The {EnemyName} jeers a bit, but eventually takes your coin and lets you pass");
                    currentPlayer.CoinPouch -= 25;
                    currentPlayer.DisplayPlayerStats(false);
                    break;
                case 1:
                    EnterCombat(currentPlayer);
                    break;
            }
        }

        public void DeathOption(Player currentPlayer)
        {
            currentPlayer.Health = 0;
            currentPlayer.DisplayPlayerStats(false);
        }
    }
}
