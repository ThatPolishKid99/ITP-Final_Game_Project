using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Final_Game_Project
{
    class SpecialItem
    {
        public string Name;
        public Special_Item EnumType;
        ConsoleColor Color;
        
public SpecialItem(string name, ConsoleColor color)
        {
            Name = name;
            Color = color;
            if (name == "Map")
            {
                EnumType = Special_Item.Map;
            }
            else if (name == "ChestKey")
            {
                EnumType = Special_Item.Chest_Key;
            }
        }

        public enum Special_Item
        {
            Chest_Key,
            Map
        }

        public void DisplaySpecialItem()
        {
            ForegroundColor = Color;
            WriteLine($"You see a {Name} during your search, it will probably be useful on your journey");
            WriteLine($"You grab the {Name} and make your escape, to continue on your Journey");
            ResetColor();
        }
    }
}
