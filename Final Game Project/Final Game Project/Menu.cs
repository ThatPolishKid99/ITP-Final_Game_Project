using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Final_Game_Project
{
    class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;
        public string ReturnString;

        public Menu(string prompt, string[] options, bool isFirstUse)
        {
            Prompt = prompt;
            if (isFirstUse)
            {
                Prompt = prompt + "\n(Use the arrow keys to cycle through options, and hit Enter to select an option)";
            }
            Options = options;
            SelectedIndex = 0;
        }   

        private void DisplayOptions(int x, int y)
        {
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                    ReturnString = Options[i];
                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }

                Console.SetCursorPosition(x, y + i);
                WriteLine($"{prefix} << {currentOption} >>");
            }
            ResetColor();
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            WriteLine("\n" + Prompt);
            int cursorLeft = CursorLeft;
            int cursorTop = CursorTop;

            do
            {
                DisplayOptions(cursorLeft, cursorTop);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                //Update SelectedIndex based on arrow key input.
                if(keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}
