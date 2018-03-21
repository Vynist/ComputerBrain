using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/* 
 * This class controls the button presses from the XAML file and calls different functions/classes depending on which button is pressed.
 */

namespace MM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {

        public GamePage()
        {
            InitializeComponent();
        }

        //This is the return variable from GameLogic.LoopThroughComputation() that stores the answer from the random computation
        public int correctAnswer;

        //User presses this button after entering in values and this button starts the speaking
        public void Btn_StartGame(object sender, EventArgs e)
        {
            //These are user-entered values, stores how long the computation is and what the max number will be
            string x = LengthOfComputation.Text;
            string y = RangeOfComputation.Text;

            //TO DO
            int Delay = 1;

            //Check user input and make sure it is an int and greater than 0
            if ((int.TryParse(x, out int Length)) && (Convert.ToInt32(x) > 0) && ((int.TryParse(y, out int Range)) && (Convert.ToInt32(y) > 0)))
            {
                GameLogicClass GameLogic = new GameLogicClass();
                correctAnswer = GameLogic.LoopThroughComputation(Length, Range, Delay);
            }
            else
            {
                DisplayAlert("Error", "Please enter correct values", "OK");
            }

        }

        //Button that user presses to check that their entered value is correct or not
        public void Btn_CheckAnswer(object sender, EventArgs e)
        {
            //User input, what they think the answer is
            string x = userAnswer.Text;

            GameLogicClass GameLogic = new GameLogicClass();

            //Check if user input is an int
            if (int.TryParse(x, out int _userAnswer))
            {
                //Passes in the user input and checks if it is correct
                bool correct = GameLogic.IsAnswerCorrect(Convert.ToInt32(_userAnswer), correctAnswer);

                if (correct)
                {
                    System.Diagnostics.Debug.WriteLine("correct");
                    DisplayAlert("Congratulations!", "That is correct!", "I got lucky... lemme try again");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("wrong");
                    DisplayAlert("WRONG!", "That is incorrect!", "I have failed... lemme try again");
                }
            }
            else
            {
                DisplayAlert("Error", "Please enter correct values", "OK");
            }
        }
    }
}