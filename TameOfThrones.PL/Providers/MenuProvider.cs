namespace TameOfThrones.PL.Providers
{
    using System;

    /// <summary>
    /// Class provides the different menus for user input
    /// </summary>
    internal class MenuProvider
    {
        private static  string m_HeaderLines = "==========================";
        private static  string m_MainMenuLines = "==========";
        private static  string m_QuestionMenuLines = "=============";
        private static  string m_InputMenuLines = "==========================================";
        private static  string m_BallotMenuLines = "============================================";
        private static  string m_WelcomeMessage = "Welcome to Tame of Thrones";
        private static  string m_MainMenuText = "Main Menu";
        private static  string m_ChoseOptionText = "Choose from below options:";
        private static  string m_YourChoiceText = "Your Choice: ";
        private static  int m_DivideBy = 2;
        private static  int m_YPos0 = 0;
        private static  int m_YPos1 = 1;
        private static  int m_YPos2 = 2;

        /// <summary>
        /// Display the main program heder
        /// </summary>
        private static void DisplayHeader()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / m_DivideBy, m_YPos0);
            Console.Out.WriteLine(m_HeaderLines);
            Console.SetCursorPosition(Console.WindowWidth / m_DivideBy, m_YPos1);
            Console.Out.WriteLine(m_WelcomeMessage);
            Console.SetCursorPosition(Console.WindowWidth / m_DivideBy, m_YPos2);
            Console.Out.WriteLine(m_HeaderLines);
        }

        /// <summary>
        /// Display main menu
        /// </summary>
        internal static char DoGetMainMenuChoice()
        {
            DisplayHeader();
            Console.Out.WriteLine(m_MainMenuLines);
            Console.Out.WriteLine(m_MainMenuText);
            Console.Out.WriteLine(m_MainMenuLines);
            Console.Out.WriteLine(m_ChoseOptionText);
            Console.Out.WriteLine("1. Ask Questions");
            Console.Out.WriteLine("2. Input Message");
            Console.Out.WriteLine("3. Do Ballot");
            Console.Out.WriteLine("4. Reset All Previous Selections");
            Console.Out.WriteLine("5. Exit");
            Console.Out.Write(m_YourChoiceText);
            return Console.ReadKey(true).KeyChar;
        }

        /// <summary>
        /// Display menu to ask for questions
        /// </summary>
        internal static string DoGetQuestionInput()
        {
            DisplayHeader();
            Console.Out.WriteLine(m_QuestionMenuLines);
            Console.Out.WriteLine("Question Menu");
            Console.Out.WriteLine(m_QuestionMenuLines);
            Console.Out.WriteLine("Choose from below options:");
            Console.Out.WriteLine("1. Who is the ruler of Southeros?");
            Console.Out.WriteLine("2. Allies of Ruler?");
            Console.Out.WriteLine("3. Go back");
            Console.Out.Write(m_YourChoiceText);
            return Console.ReadKey().KeyChar.ToString();
        }

        /// <summary>
        /// Menu for user to input message for allies
        /// </summary>
        internal static string DoGetInputMessage(bool showMenu = true)
        {
            if (showMenu)
            {
                DisplayHeader();
                Console.Out.WriteLine(m_InputMenuLines);
                Console.Out.WriteLine("Input Messages to kingdoms from King Shan:");
                Console.Out.WriteLine(m_InputMenuLines);
            }

            return DoGetUserInput();
        }

        /// <summary>
        /// Menu for user to input list of kingdoms competing to be ruler
        /// </summary>
        internal static string DoGetBallotInput(bool showMenu = true)
        {
            if (showMenu)
            {
                DisplayHeader();
                Console.Out.WriteLine(m_BallotMenuLines);
                Console.Out.WriteLine("Enter the kingdoms competing to be the ruler:");
                Console.Out.WriteLine(m_BallotMenuLines);
            }

            return DoGetUserInput();
        }

        /// <summary>
        /// Read the input from user
        /// </summary>
        /// <returns></returns>
        private static string DoGetUserInput()
        {
            Console.Out.WriteLine("Input [type exit to stop]: ");
            return Console.ReadLine();
        }
    }
}
