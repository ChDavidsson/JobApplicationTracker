namespace JobApplicationTracker
{
    public class MenuController
    {
        private JobManager manage = new JobManager();

        public void Start()
        {
            bool running = true;

            while (running)
            {
                MenuHelper.ShowMenu();
                
                int userInput = int.Parse(Console.ReadLine()!);

                Console.Clear();

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
                    running = false;
                    break;

                    default:
                    Console.WriteLine("Please choose again");
                    break;
                }
            }
        }
    }
}