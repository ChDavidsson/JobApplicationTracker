namespace JobApplicationTracker
{
    public static class MenuHelper
    {
        public static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("// Welcome to Job Application Tracker //");
            Console.WriteLine("1. Add application");
            Console.WriteLine("2. Show all applications");
            Console.WriteLine("3. Filter applications by status");
            Console.WriteLine("4. Sort applications by date");
            Console.WriteLine("5. Show statistics");
            Console.WriteLine("6. Update an application");
            Console.WriteLine("7. Remove application");
            Console.WriteLine("0. Exit program");
            Console.ResetColor();
            Console.WriteLine("Choose an option:");
        }
    }
}