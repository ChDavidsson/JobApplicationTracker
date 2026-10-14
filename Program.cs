namespace JobApplicationTracker;

class Program
{
    static void Main(string[] args)
    {
        JobManager test = new JobManager();
        MenuHelper.ShowMenu();
        test.AddJob();
        test.UpdateStatus();
        test.ShowByStatus();
        test.ShowAll();
        test.ShowStatistics();
        test.SortByDate();
        test.RemoveApplication();
    }
}
