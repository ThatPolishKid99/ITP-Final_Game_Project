using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Final_Game_Project
{
    class DiscoveryEncounter : Encounter
    {
        string Name;
        SpecialItem specialItem;

        public DiscoveryEncounter(string name, string description, string[] encounterChoices, SpecialItem specialItem, string encounterArt )
            : base(name, description, encounterChoices, encounterArt)
        {
            Name = name;
            this.specialItem = specialItem;
        }
        public override void DisplayEncounter(Player currentPlayer)
        {
            DisplayEncounterArt();

            Menu encounterMenu = new Menu(Description, EncounterChoices, false);
            int index = encounterMenu.Run();
            switch (index)
            {
                case 0:
                    GainSpecialItem(currentPlayer);
                    break;
                case 1:
                    GivePlayerEquipment(currentPlayer);
                    break;
                case 2:
                    BribeOption(currentPlayer);
                    break;
            }
        }

        public override float GivePlayerEquipment(Player currentPlayer)
        {
            WriteLine("\n> You acquire some better equipment! You quickly grab it and head out to continue your journey");
            float newWinChance = currentPlayer.WinChance = currentPlayer.WinChance >= 0.5F ? 0.6F : 0.45F;
            currentPlayer.UpdateArmor();
            return newWinChance;
        }

        private void GainSpecialItem(Player currentPlayer)
        {
            currentPlayer.ItemAcquired(specialItem);
        }

        private void BribeOption(Player currentPlayer)
        {
            WriteLine($"> You approach the {Name} with your palm outstretched, containing some of your precious coin. They jeer a bit, but eventually takes your coin and lets you pass");
            currentPlayer.CoinPouch -= 25;
            currentPlayer.DisplayPlayerStats(false);
        }
    }
}
