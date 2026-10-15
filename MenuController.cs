namespace JobApplicationTracker
{
    public class MenuController
    {
        // private innebär att bara MenuController har tillgång till JobManager-objektet via den här variabeln
        private JobManager manage = new JobManager();
        
        public void Start()
        {
            manage.TestData();
            
            //kör programmet tills användaren väljer att avsluta
            bool running = true;

            while (running)
            {
                MenuHelper.ShowMenu();
                
                int userInput = int.Parse(Console.ReadLine()!);

                //rensar text i console för ett renare program
                Console.Clear();

                //Hoppar till användarens val i switch case
                switch(userInput)
                {
                    case 1:
                    manage.AddJob();
                    break;

                    case 2:
                    manage.ShowAll();
                    break;

                    case 3:
                    manage.ShowByStatus();
                    break;

                    case 4:
                    manage.SortByDate();
                    break;

                    case 5:
                    manage.ShowStatistics();
                    break;

                    case 6:
                    manage.UpdateStatus();
                    break;

                    case 7:
                    manage.RemoveApplication();
                    break;

                    case 0:
                    Console.WriteLine("Exiting program..");
                    running = false; //Avbryter loopen
                    break;

                    default:
                    Console.ForegroundColor = ConsoleColor.Red;     //Sätter färg
                    Console.WriteLine("Invalid option, please choose again");   //Felmeddelande om användaren matar in ogiltigt val
                    Console.ResetColor(); //Återställer färgen
                    break;
                }
            }
        }
    }
}