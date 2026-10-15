namespace JobApplicationTracker
{
    public class JobManager
    {
        List<JobApplication> JobApplications = new List<JobApplication>();


        //Testdata för jag orkade inte mata in massa olika företag varje gång jag skulle testa programmet :)
        public void TestData()
        {
            JobApplications.Add(new JobApplication
            {
                CompanyName = "Gekås",
                PositionTitle = "IT-support",
                Status = ApplicationStatus.Applied,
                ApplicationDate = new DateTime(2025, 06, 05),
                SalaryExpectation = 32000
            });

            JobApplications.Add(new JobApplication
            {
                CompanyName = "IKEA",
                PositionTitle = "IT-tekniker",
                Status = ApplicationStatus.Interviewed,
                ApplicationDate = new DateTime(2026, 10, 12),
                ResponseDate = new DateTime(2026, 10, 14),
                SalaryExpectation = 38000
            });

            JobApplications.Add(new JobApplication
            {
                CompanyName = "Discord",
                PositionTitle = "Operator",
                Status = ApplicationStatus.Applied,
                ApplicationDate = new DateTime(2026, 10, 10),
                SalaryExpectation = 29000
            });

            JobApplications.Add(new JobApplication
            {
                CompanyName = "Google",
                PositionTitle = "Software Developer",
                Status = ApplicationStatus.Rejected,
                ApplicationDate = new DateTime(2026, 10, 14),
                ResponseDate = new DateTime(2026, 10, 15),
                SalaryExpectation = 39000
            });

            JobApplications.Add(new JobApplication
            {
                CompanyName = "Microsoft",
                PositionTitle = "IT-Engineer",
                Status = ApplicationStatus.Interviewed,
                ApplicationDate = new DateTime(2026, 06, 30),
                ResponseDate = new DateTime(2026, 07, 09),
                SalaryExpectation = 33500
            });
        }
        
        //Metod för att lägga till ansökan
        public void AddJob()
        {
            Console.WriteLine("Company name: ");
            string companyName = Console.ReadLine()!;
            Console.WriteLine("Position: ");
            string position = Console.ReadLine()!;
            Console.WriteLine("Status (1: Applied, 2: Interview, 3: Offer, 4: Rejected): ");
            int statusNumber = int.Parse(Console.ReadLine()!);
            ApplicationStatus status = (ApplicationStatus)statusNumber;
            Console.WriteLine("Application date (yyyy-mm-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine()!);
            Console.WriteLine("Salary expectation: ");
            int salary = int.Parse(Console.ReadLine()!);
            
            JobApplication job = new JobApplication
            {
                CompanyName = companyName,
                PositionTitle = position,
                Status = status,
                ApplicationDate = date,
                SalaryExpectation = salary,
            };

            JobApplications.Add(job);
            Console.ForegroundColor = ConsoleColor.Green; 
            Console.WriteLine("Job application added!");
            Console.ResetColor();                        

        }

        //Metod för att uppdatera en status från listan och sätter färg beroende på status.
        public void UpdateStatus()
        {
            if (JobApplications.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There are no applications to update.");
                Console.ResetColor();
                return;
            }

            bool run = true;
            while(run)
            {
                for (int number = 0; number < JobApplications.Count; number++)
                {
                    StatusColor(JobApplications[number].Status);
                    Console.WriteLine($"{number + 1}.{JobApplications[number].GetSummary()}");  //Skriver ut alla ansökningar
                    Console.ResetColor();
                }

                Console.WriteLine("Number of the application you want to update: ");
                int.TryParse(Console.ReadLine(),out int inputNumber);
                
                if (inputNumber < 1 || inputNumber > JobApplications.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice, please try again.");
                    Console.ResetColor();
                    continue;
                }

                JobApplication job = JobApplications[inputNumber - 1];
                Console.WriteLine("Enter the new status (1. Applied, 2. Interviewed, 3. Offer, 4.Rejected)");
                if (!int.TryParse(Console.ReadLine(),out int statusNumber) || statusNumber < 1 || statusNumber > 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice, please try again");
                    Console.ResetColor();
                    continue;
                }

                job.Status = (ApplicationStatus)statusNumber;

                if (job.Status != ApplicationStatus.Applied)
                {
                    job.ResponseDate = DateTime.Today;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Status for {job.CompanyName} has been updated");
                    Console.ResetColor();
                    run = false;
                }
            }
    
        }

        //Metod för att visa alla ansökningar i listan, sätter färg beroende på status.
        public void ShowAll()
        {
            if (JobApplications.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No available application");
                Console.ResetColor();
                return;
            }
            
            //Sorterar listan från nyast till äldst
            List<JobApplication> sortedby = JobApplications.OrderByDescending(app => app.ApplicationDate).ToList();

            foreach(var application in sortedby)
            {
                StatusColor(application.Status);
                Console.WriteLine(application.GetSummary());
                Console.WriteLine($"{application.GetDaysSinceApplied()} days since applied ");
                Console.ResetColor();
            }

        }

        //LINQ-metod som visar en lista beroende vilken status du har valt att visa.
        public void ShowByStatus()
        {

            Console.WriteLine("Status: 1: Applied 2: Interview 3: Offer 4: Rejected");
            
            if (!int.TryParse(Console.ReadLine(), out int statusNumber))
            {
                Console.WriteLine("Choose an option (1-4)");
                return;
            }

            ApplicationStatus status = (ApplicationStatus)statusNumber;
            
            List<JobApplication> sortedlist = JobApplications.Where(app => app.Status == status).ToList();

            if(sortedlist.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No application found");
                Console.ResetColor();
                return;
            }

            foreach (var application in sortedlist)
            {
                StatusColor(application.Status);
                Console.WriteLine(application.GetSummary());
                Console.ResetColor();
            }
        }

        //LINQ-metod som visar statistik på antal: ansökningar, intervjuer, erbjudanden och avslag.
        public void ShowStatistics()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("///STATS///");
            Console.WriteLine($"Total applications: {JobApplications.Count}");

            int applied = JobApplications.Count(job => job.Status == ApplicationStatus.Applied);
            int interview = JobApplications.Count(job => job.Status == ApplicationStatus.Interviewed);
            int offer = JobApplications.Count(job => job.Status == ApplicationStatus.Offer);
            int rejected = JobApplications.Count(job => job.Status == ApplicationStatus.Rejected);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Applied: {applied}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Interviewed: {interview}");   //Färger visas beroende på positivt eller negativ statistik
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Offer: {offer}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Rejected: {rejected}");
            Console.ResetColor();

            //LINQ tagen från inlämingsuppgiften
            var averageResponseTime = JobApplications
                .Where(job => job.ResponseDate != null)
                .Average(job => (job.ResponseDate - job.ApplicationDate)?.TotalDays);

            if (averageResponseTime.HasValue)
            {
                Console.WriteLine($"Average response time: {averageResponseTime.Value:F1} days");
            }
            else
            {
                Console.WriteLine("Currently no average response time.");
            }
        }

        //LINQ-metod som sorterar efter datum samt visar det med färg
        public void SortByDate()
        {
            //Sorterar datan från äldst till nyast
            List<JobApplication> sortedByDate = JobApplications.OrderBy(job => job.ApplicationDate).ToList();

            if (sortedByDate.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Currently no applications in list");
                Console.ResetColor();
                return;
            }

            foreach (var application in sortedByDate)
            {
                StatusColor(application.Status);
                Console.WriteLine(application.GetSummary());
                Console.ResetColor();
            }
        }
        //Metod för att ta bort en ansökan
        public void RemoveApplication()
        {
            if (JobApplications.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No applications to remove.");
                Console.ResetColor();
                return;
            }

            for (int number = 0; number < JobApplications.Count; number++)
            {
                StatusColor(JobApplications[number].Status);
                Console.WriteLine($"{number + 1}.{JobApplications[number].GetSummary()}");
                Console.ResetColor();
            }

            Console.WriteLine("Number of the application you want to remove: ");
            int inputNumber = int.Parse(Console.ReadLine()!);

            if (inputNumber < 1 || inputNumber > JobApplications.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number.");
                Console.ResetColor();
                return;
            }

            JobApplications.RemoveAt(inputNumber - 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Application removed successfully!");
            Console.ResetColor();
        }

        public void StatusColor(ApplicationStatus status)
        {
            switch (status)  //Tog lite hjälp av Claude för att sätta färg beroende på vilken status ansökan har.
            {
                case ApplicationStatus.Applied:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case ApplicationStatus.Interviewed:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case ApplicationStatus.Offer:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                
                case ApplicationStatus.Rejected:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
        }
    }
}