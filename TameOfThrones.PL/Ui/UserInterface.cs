using TameOfThrones.BL.Interfaces;
using TameOfThrones.PL.Providers;

namespace TameOfThrones.PL.Ui
{
    using System;

    /// <summary>
    /// Main class of the exe
    /// </summary>
    public class UserInterface
    {
        private const char MainMenuChoiceQuestion = '1';
        private const char MainMenuChoiceMessage = '2';
        private const char MainMenuChoiceBallot = '3';
        private const char MainMenuChoiceReset = '4';
        private const char MainMenuChoiceExit = '5';

        private const char ChoiceRuler = '1';
        private const char ChoiceAllies = '2';
        private const char ChoiceGoback = '3';
        private const string ExitCode = "exit";

        private static int _ballotRounds = 1;

        /// <summary>
        /// The main function of console app
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            string userInputValue = string.Empty;
            char mainMenuChoice = 'A';
            while (true)
            {
                bool clearConsole = false;
                if (userInputValue == string.Empty)
                {
                    mainMenuChoice = MenuProvider.DoGetMainMenuChoice();
                    clearConsole = true;
                }

                userInputValue = GetUserInputAsPerChoice(mainMenuChoice, clearConsole);
                if (mainMenuChoice.Equals(MainMenuChoiceExit))
                {
                    break; // Exit main while loop
                }

                if ((mainMenuChoice.Equals(MainMenuChoiceQuestion) &&
                    userInputValue.Equals(ChoiceGoback.ToString())) || userInputValue.Equals(ExitCode))
                {
                    userInputValue = string.Empty;
                    continue;
                }

                ProcessUserInput(mainMenuChoice, userInputValue);
            }
        }


        /// <summary>
        /// Gets the input from user as per choice e.g. Ballot
        /// </summary>
        /// <param name="userChoice"></param>
        /// <param name="clearConsole"></param>
        /// <returns></returns>
        private static string GetUserInputAsPerChoice(char userChoice, bool clearConsole = true)
        {
            string userInputValue = string.Empty;
            switch (userChoice)
            {
                case MainMenuChoiceQuestion:
                    userInputValue = MenuProvider.DoGetQuestionInput();
                    break;
                case MainMenuChoiceMessage:
                    userInputValue = MenuProvider.DoGetInputMessage(clearConsole);
                    break;
                case MainMenuChoiceBallot:
                    userInputValue = MenuProvider.DoGetBallotInput(clearConsole);
                    break;
                case MainMenuChoiceReset:
                    break;
                case MainMenuChoiceExit:
                    userInputValue = MainMenuChoiceExit.ToString();
                    break;
            }

            return userInputValue;
        }

        /// <summary>
        /// Send the user input to BL for processing and display results
        /// </summary>
        /// <param name="userChoice"></param>
        /// <param name="userInputValue"></param>
        private static void ProcessUserInput(char userChoice, string userInputValue)
        {
            bool processMessageResult = true;
            bool reset = false;
            switch (userChoice)
            {
                case MainMenuChoiceQuestion:
                    ProcessQuestionOutput(userInputValue);
                    break;
                case MainMenuChoiceMessage:
                    processMessageResult = BlAccessor.ProcessMessageInput(userInputValue, reset);
                    reset = false;
                    break;
                case MainMenuChoiceBallot:
                    reset = true;
                    _ballotRounds = 1;
                    ProcessBallot(userInputValue);
                    break;
                case MainMenuChoiceReset:
                    BlAccessor.ResetGlobals();
                    break;
                case MainMenuChoiceExit:
                    break;
            }

            if (!processMessageResult)
            {
                DisplayError();
            }
        }

        /// <summary>
        /// Display the answers to the questions
        /// </summary>
        /// <param name="userInputValue"></param>
        private static void ProcessQuestionOutput(string userInputValue)
        {
            IOutputDataProvider outputData = BlAccessor.GetOutputDataProvider();
            char userChoice = userInputValue.ToCharArray()[0];
            switch (userChoice)
            {
                case ChoiceRuler:
                    DisplayOutput(outputData.RulerName);
                    break;
                case ChoiceAllies:
                    DisplayOutput(outputData.AlliesList);
                    break;
            }            
        }

        /// <summary>
        /// Send the user input to BL for Ballot processing
        /// </summary>
        /// <param name="userInputValue"></param>
        private static void ProcessBallot(string userInputValue)
        {
            var result = BlAccessor.ProcessBallotInput(userInputValue);
            if (result.IsSuccess)
            {
                DisplayBallotResult(result, _ballotRounds++);
            }
            else
            {
                DisplayError();
                return;
            }

            if (result.IsTieExist)
            {
                // Repeat the ballot with Tie
                ProcessBallot(result.TieListString);
            }
        }

        /// <summary>
        /// Display the result of Ballot process
        /// </summary>
        /// <param name="result"></param>
        /// <param name="ballotRounds"></param>
        private static void DisplayBallotResult(BallotResult result, int ballotRounds)
        {
            Console.WriteLine("\nResults after round " + ballotRounds + " ballot count");
            foreach (var kingdomName in result.TieList)
            {
                IKingdom kingdom = BlAccessor.GetKingdom(kingdomName);
                Console.WriteLine("Output: Allies for " + kingdom.KingdomName + ": " + kingdom.AlliesNameList.Count);
            }
        }

        /// <summary>
        /// Display the error message
        /// </summary>
        private static void DisplayError()
        {
            Console.WriteLine("Invalid or Duplicate input");
        }
        
        /// <summary>
        /// Display the result on console and wait
        /// </summary>
        /// <param name="outputResult">String which needs to be displayed</param>
        private static void DisplayOutput(string outputResult)
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine("Output: " + outputResult);
            Console.Out.WriteLine();
            Console.Out.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }
    }
}
