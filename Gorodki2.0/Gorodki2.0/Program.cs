using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
using System.Linq;
using System.Threading;

namespace Gorodki2._0
{
    class Program
    {
        static List<string> allCities = new List<string>();
        static List<string> trash = new List<string>();
        static string LCC = "";
        static string LPC = "";
        static char neededLetter;


        static void FillUp(List<string> abc, string docName)
        {
            string[] allNames = File.ReadAllLines(docName);
            foreach (string each in allNames) abc.Add(each);
        }

        static void NeededLetterListCreate(List<string> nowList, out char needLttr, string direct, List<string> abc)
        {
            char neededLetter = direct[^1];
            needLttr = neededLetter;

            var sequenza = abc.Select(p => new { booley = p.StartsWith(neededLetter), indexey = abc.IndexOf(p) });
            foreach (var el in sequenza)
            {
                if (el.booley) nowList.Add(abc[el.indexey]);
            }
            if (nowList.Count == 0)
            {
                WriteLine("Oops! No more cities for the last letter of this one.\n" +
                "The game is over. Thanks for playing GORODKI!");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }

        static void FirstCpuTurn(List<string> abc, List<string> trash, string LCC, string LPC)
        {
            var rndm = new Random();
            int rndmEl = rndm.Next(0, abc.Count);
            string chosen = abc[rndmEl];

            LCC = chosen;
            WriteLine(Environment.NewLine + LCC + ", your letter : " + LCC[^1]);
            trash.Add(LCC);
            abc.Remove(LCC);

            PlayerTurn(abc, trash, LCC, LPC);
        }

        static void PlayerTurn(List<string> abc, List<string> trash, string LCC, string LPC)
        {
            string plCity = ReadLine().ToUpper();

            var nowList = new List<string>();

            NeededLetterListCreate(nowList, out neededLetter, LCC, abc);

            if (plCity[0] != neededLetter)
            {
                WriteLine("Your city should start with the last letter of my city. Try another one!");
                PlayerTurn(abc, trash, LCC, LPC);
            }
            else
            {
                if (!abc.Contains(plCity))
                {
                    if (trash.Contains(plCity)) WriteLine("This city is already used. Try another one!");
                    else WriteLine("This city doesn\'t exist (or isn\'t in my database). Try another one!");
                    PlayerTurn(abc, trash, LCC, LPC);
                }
                else
                {
                    LPC = plCity;
                    trash.Add(plCity);
                    abc.Remove(plCity);
                    CpuTurn(abc, trash, LCC, LPC);
                }
            }
        }

        static void CpuTurn(List<string> abc, List<string> trash, string LCC, string LPC)
        {
            List<string> nowList = new List<string>();
            char neededLetter = ' ';

            NeededLetterListCreate(nowList, out neededLetter, LPC, abc);

            var rndm = new Random();
            int rndmEl = rndm.Next(0, nowList.Count);
            LCC = nowList[rndmEl];
            WriteLine(Environment.NewLine + LCC + ", your letter : " + LCC[^1]);
            trash.Add(LCC);
            abc.Remove(LCC);

            PlayerTurn(abc, trash, LCC, LPC);
        }

        static void Main(string[] args)
        {
            for (int i = 65; i < 91; i++)
            {
                char ch = (char)i;
                string chh = ch.ToString();
                string neededFile = "citLists\\cit" + chh + ".txt";
                FillUp(allCities, neededFile);
            }

            WriteLine();
            Thread.Sleep(1000);
            WriteLine("It\'s GORODKI time! (over " + allCities.Count + " cities in our database!)" + Environment.NewLine);
            Thread.Sleep(850);

            WriteLine("Hi there! What is your name?");

            string playerName = ReadLine();

            WriteLine("OK, let\'s start the game then, " + playerName + "." + Environment.NewLine);
            Thread.Sleep(1350);
            WriteLine("The main rule is to name cities in such order so that " +
                "the last letter of the previous city" + Environment.NewLine + "is the same as " +
                "the first letter of the next city." + Environment.NewLine);
            Thread.Sleep(1350);
            WriteLine("If a city\'s name contains the \"City\" part, then write the name without this part." +
                Environment.NewLine);
            Thread.Sleep(1350);
            WriteLine("For your convenience, the letter your city must start with " +
                "will be displayed alongside CPU\'s city, like this :" + Environment.NewLine);
            Thread.Sleep(1350);
            WriteLine("GRONINGEN, your letter : N" + Environment.NewLine);
            Thread.Sleep(1350);
            WriteLine("If there is no more cities left for the last letter of given, then the game is over." +
                Environment.NewLine);
            Thread.Sleep(1350);
            WriteLine("Good luck, " + playerName + "! Hope you\'ll enjoy this game ;)");
            Thread.Sleep(1350);

            FirstCpuTurn(allCities, trash, LCC, LPC);
        }
    }
}
