using System;
using System.Collections.Generic;
using System.Text;
//using Windows.Media.SpeechSynthesis;
using Plugin.TextToSpeech;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NCalc;

/* 
 * This class handles all the logic. Computation, getting random numbers, etc. is all done here.
 */

namespace MM
{
    class GameLogicClass
    {

        //User-entered length of computation and user-entered maximum value passes into this method (delay currently unused). This method creates a list of random numbers with either a '+' or '-' 
        //  in between each random number. The Speech function is used to read out this list so the user can hear. Method returns the calculated answer. 
        public int LoopThroughComputation(int length, int range, int delay)
        {
            List<string> RandomNumberList_string = new List<string>();
            string ConcatenatedString = string.Empty;

            for (int i = 1; i <= length; i++)
            {
                int randomNumber = GetRandomNumber(1, range + 1);
                RandomNumberList_string.Add(randomNumber.ToString());

                if (i == length)
                    RandomNumberList_string.Add("equals");
                else
                    RandomNumberList_string.Add(GetOperation());
            }

            foreach (string numberOrOperation in RandomNumberList_string)
            {
                if (numberOrOperation == "-")
                    CrossTextToSpeech.Current.Speak("minus", speakRate: 1.25f);
                else
                    CrossTextToSpeech.Current.Speak(numberOrOperation, speakRate: 1.25f);

                if (numberOrOperation != "equals")
                    ConcatenatedString += numberOrOperation;
            }

            Expression e = new Expression(ConcatenatedString);
            System.Diagnostics.Debug.WriteLine(e.Evaluate());
            int Result = Convert.ToInt32(e.Evaluate());
            return Result;

        }

        //Passes in user-entered answer and the correct answer determined from LoopThroughComputation. Returns true if they are the same. 
        public bool IsAnswerCorrect(int userAnswerToCheck, int correctAnswer)
        {
            if (userAnswerToCheck == correctAnswer)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Function to get operation (+, -)
        public string GetOperation()
        {
            // 1 is + , 2 is -
            int randomNumber = GetRandomNumber(1, 2 + 1);
            if (randomNumber == 1)
                return "+";
            else if (randomNumber == 2)
                return "-";
            else
                return "error";


        }

        //Function to get random number
        private static readonly Random getrandom = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize {
                return getrandom.Next(min, max);
        }
    }
}

