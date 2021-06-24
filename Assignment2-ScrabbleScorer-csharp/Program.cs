using System;
using System.Collections.Generic;

namespace Assignment2_ScrabbleScorer_csharp
{
    class Program
    {
        //Here is the original OldPointStructure dictionay
        public static Dictionary<int, string> oldPointStructure = new Dictionary<int, string>()
        {
            {1, "A, E, I, O, U, L, N, R, S, T"},
            {2, "D, G"},
            {3, "B, C, M, P" },
            {4, "F, H, V, W, Y" },
            {5, "K" },
            {8, "J, X" },
            {10, "Q, Z" }
        };

        //newPointStructure dictionary for use in methods
        public static Dictionary<char, int> newPointStructure = Transform();


        //Code your Transform method here
        static Dictionary<char, int> Transform()
        {
            Dictionary<char, int> transformed = new Dictionary<char, int>();
            foreach (KeyValuePair<int, string> pair in oldPointStructure)
            {
                foreach (char letter in pair.Value.ToLower())
                {
                    if (letter >= 'a' && letter <= 'z')
                        transformed.Add(letter, pair.Key);
                }
            }
            transformed.Add(' ', 0);
            return transformed;
        }





        //Code your Scoring Option methods here

        //SimpleScorer-----
        public static void SimpleScorer(string word)
        {
            int score = word.Length;
            Console.WriteLine($"Your word \'{word}\' is worth {score} points.");
        }


        //BonusVowels-----
        public static void BonusVowels(string word)
        {
            int score = 0;
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            foreach (char letter in word)
            {
                if (Array.IndexOf(vowels, letter) == -1) //Consonants
                {
                    score++;
                }
                else //Vowels
                {
                    score+=3;
                }
            }
            Console.WriteLine($"Your word \'{word}\' is worth {score} points.");
        }



        //ScrabbleScorer-----
        public static void ScrabbleScorer(string word)
        {
            int score = 0;
            foreach (char letter in word)
            {
                score += newPointStructure[letter];
            }
            Console.WriteLine($"Your word \'{word}\' is worth {score} points.");
        }




        //Code your ScoringAlgorithms method here
        public static void ScoringAlgorithms(int method, string word)
        {
            switch (method)
            {
                case 1:
                default:
                    ScrabbleScorer(word);
                    break;
                case 2:
                    SimpleScorer(word);
                    break;
                case 3:
                    BonusVowels(word);
                    break;
            }
        }




        //Code your InitialPrompt method here
        static int InitialPrompt()
        {
            Console.WriteLine("Welcome to the Scrabble Score Calculator!");
            Console.WriteLine("\nWhich scoring algorithm would you like to use?");
            Console.WriteLine("\n1 - Scrabble: The traditional scoring algorithm.");
            Console.WriteLine("2 - Simple Score: Each letter is worth 1 point.");
            Console.WriteLine("3 - Bonus Vowels: Vowels are worth 3 pts, and consonants are 1 pt.");
            Console.WriteLine("\nEnter 1, 2, or 3:");
            string inputString = Console.ReadLine();
            int inputInt;
            while (!int.TryParse(inputString, out inputInt))
            {
                Console.WriteLine("\nEnter 1, 2, or 3:");
                inputString = Console.ReadLine();
            }
            inputInt = int.Parse(inputString);
            return inputInt;
        }





        //Code your RunProgram method here
        public static void RunProgram()
        {
            const string endPrompt = "stop";
            int methodChoice = InitialPrompt();
            Console.WriteLine($"\nEnter a word to score or enter \'{endPrompt}\' to end scoring:  ");
            string wordChoice = Console.ReadLine();
            bool validWord = IsWordValid(wordChoice);
            while (!validWord)
            {
                Console.WriteLine("\nWords can only contain letters from A to Z");
                Console.WriteLine($"Enter a word to score or enter \'{endPrompt}\' to end scoring:  ");
                wordChoice = Console.ReadLine();
                validWord = IsWordValid(wordChoice);
            }
            do
            {
                Console.Clear();
                ScoringAlgorithms(methodChoice, wordChoice.ToLower());
                Console.WriteLine($"\nEnter a word to score or enter \'{endPrompt}\' to end scoring");
                wordChoice = Console.ReadLine();
                validWord = IsWordValid(wordChoice);
                while (!validWord)
                {
                    Console.WriteLine("\nWords can only contain letters from A to Z");
                    Console.WriteLine($"Enter a word to score or enter \'{endPrompt}\' to end scoring:  ");
                    wordChoice = Console.ReadLine();
                    validWord = IsWordValid(wordChoice);
                }
            }
            while (wordChoice.ToLower() != endPrompt && validWord);
        }


        public static bool IsWordValid(string word)
        {
            bool isValid = true;
            foreach (char letter in word.ToLower())
            {
                if (!(letter >= 'a' && letter <= 'z') || letter == ' ')
                {
                    isValid = false;
                }
            }
            if (string.IsNullOrEmpty(word))
                isValid = false;
            return isValid;
        }


        static void Main(string[] args)
        {
            //Call your RunProgram method here
            RunProgram();
        }
    }
}
