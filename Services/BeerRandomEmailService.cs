
using Beers.Services;

namespace Beer_API.Services
{
    public class BeerRandomEmailService : BackgroundService
    {
        private readonly IPunkApiService _punkApiService;
        private readonly string _filePath;

        public BeerRandomEmailService(IPunkApiService punkApiService)
        {
            _punkApiService = punkApiService;
            _filePath = "beer_details.txt";
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
       
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
                {
                    var randomBeer = await _punkApiService.GetRandomBeer();

                    var emailList = GetPrePopulatedEmailList();

                    foreach (var email in emailList)
                    {
                        // Write beer details to file
                        using (StreamWriter writer = File.AppendText(_filePath))
                        {
                            writer.WriteLine($"{email}Random Beer of the Week:");
                            writer.WriteLine($"Name: {randomBeer.Name}");
                            writer.WriteLine($"Tagline: {randomBeer.Tagline}");
                            writer.WriteLine($"Description: {randomBeer.Description}");
                            writer.WriteLine();
                        }

                        Console.WriteLine("Beer details written to file.");

                    }

                    // Wait for 2 weeks before checking again
                    await Task.Delay(TimeSpan.FromDays(14), stoppingToken);
                }
            }

        }

        private IEnumerable<string> GetPrePopulatedEmailList()
        {
            return new List<string>
            {
                "justinkokolo5@gmail1.com",
                "justinkokolo5@gmail2.com",
                "justinkokolo5@gmail3.com",
            };
        }
    }
}
