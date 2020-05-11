using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using Figgle;

namespace Final_Game_Project
{
    class Player
    {
        public string Name;
        public int Health;
        public int CoinPouch;
        public float WinChance;
        public bool HasMap;
        public bool hasSeenMap;
        public bool HasChestKey;
        public string Armor;
        public ConsoleColor Color;

        public Player(string name, string color)
        {
            Name = name;
            Health = 100;
            CoinPouch = 100;
            WinChance = 0.25F;
            HasMap = false;
            hasSeenMap = false;
            HasChestKey = false;
            Armor = "Iron Sword and Iron Armor set";
            if (Enum.TryParse(color, out ConsoleColor colorOut)) Color = colorOut;
        }

        public static string AcquirePlayerName()
        {
            WriteLine("> Welcome New Player! Please enter your name: ");
            string playerName;
            playerName = ReadLine();
            return "Knight " + playerName;
        }

        public static string AcquirePlayerColor()
        {
            Menu colorMenu = new Menu("> You can also choose a color to be assoicated with from the following options: ", new string[] { "Red", "Blue", "Green", "Cyan", "Magenta", "Yellow" }, false);
            colorMenu.Run();
            return colorMenu.ReturnString;
        }

        public void DisplayPlayerStats(bool isFirst)
        {
            ForegroundColor = Color;
            if (isFirst)
            {
                WriteLine($"\n> Welcome {Name}! You start your journey with {Health} health points, {CoinPouch} coins in your pouch, and a {Armor} ");
            }
            else if (Health == 0)
            {
                PlayerDeath();
            }
            else
            {
                WriteLine($"\n> You have {Health} health points, {CoinPouch} coins in your pouch, and a {Armor} {WinChance}");
                if (HasChestKey)
                {
                    WriteLine("You also have the Chest Key!");
                }
                else if (HasMap)
                {
                    WriteLine("You also have the Map!");
                }
            }
            ResetColor();
        }

        public void UpdateArmor()
        {
            if (WinChance > 0.25F && WinChance <= 0.5F)
            {
                Armor = "Steel Sword and a Steel Armor set";
            }
            else if (WinChance > 0.5F && WinChance <= 0.75F)
            {
                Armor = "Black Steel Sword and a Black Steel Armor set";
            }
            else if (WinChance > 0.75F)
            {
                Armor = "Mithril Sword and a Mithril Armor set";
            }
        }

        public void ItemAcquired(SpecialItem specialItem)
        {
            specialItem.DisplaySpecialItem();
            if (specialItem.EnumType == SpecialItem.Special_Item.Map)
            {
                HasMap = true;

            }
            if (specialItem.EnumType == SpecialItem.Special_Item.Chest_Key)
            {
                HasChestKey = true;
            }
        }

        public void PlayerDeath()
        {
            Clear();
            WriteLine("\n\n> Oh no! Your poor choices have lead directly to your death!\n\n");
            WriteLine(FiggleFonts.Larry3d.Render("YOU DIED"));
            WriteLine("You feel weak, the light is fading, and you collapse to the ground... Your story not quite finished, and your legend soon forgotten...");
            WriteLine("\n\nPress any button to return to the main menu...");
            ReadKey(true);
            ResetColor();
            Game newGame = new Game();
        }
    }
}
