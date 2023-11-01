namespace Model;

public class SampleRecordGenerator {
    private static string[] Countries = { "IE", "GB", "FR", "DE", "NL", "ES", "IT", "CH", "PL" };
    private static string[] FirstNames = {
        "John", "William", "James", "Charles", "George", "Frank", "Joseph", "Thomas",
        "Henry", "Robert", "Edward", "Harry", "Walter", "Arthur", "Fred", "Albert",
        "David", "Joe", "Charlie", "Richard", "Andrew",
        "Mary", "Anna", "Emma", "Elizabeth", "Margaret", "Alice", "Sarah", "Annie",
        "Ciara", "Ella", "Laura", "Carrie", "Julia", "Edith", "Mattie", "Catherine",
        "Helen", "Louise", "Eva", "Frances", "Lucy"
    };
    private static string[] LastNames = { 
        "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Wilson",
        "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Thompson",
        "White", "Harris", "Clark", "Lewis", "Robinson", "Young", "King", "Hill"
    };


    private readonly Random _random;

    public SampleRecordGenerator(){
        _random = new Random();
    }

    // ----------------------------------------------------------------------------------------------------
    // Create the sample data for this test
    public List<SimpleRecord> CreateRecords(int count = 10000)
    {
        var response = new List<SimpleRecord>();

        for (var ctr = 0; ctr < count; ctr++)
        {
            var incorporationDate = generateDate(2010);

            var testRecord = new SimpleRecord
            {
                CompanyName = $"Test Company {ctr}",
                IncorporationDate = incorporationDate,
                IncorporationCountryCode = GenerateCountryCode(),
                RegistrationNumber = generateRegistrationNumber(),
                TaxRegistration = generateRegistrationNumber(),
                CreditRating = (Decimal)_random.Next(10,95),
                ManagingDirector = generateName()
            };

            response.Add(testRecord);
        }
        return response;
    }

    // ----------------------------------------------------------------------------------------------------
    // Generators
    private string GenerateCountryCode()
    {
        var country = _random.Next(Countries.Length);
        return Countries[country];
    }

    private string generateRegistrationNumber () {
        var companyNumber = _random.Next(12345, 99999);
        return string.Format("{0:00000}-G", companyNumber);
    }

    private DateTime generateDate (int startFrom = 1980) {
        var distYear = generateYearNormalDist(2015, 2.5);
        var incorporationYear = _random.Next(startFrom, DateTime.Now.Year);
        var incorporationMonth = _random.Next(1, 13);

        return new DateTime(distYear, incorporationMonth, 1);
    }

    private string generateName (){
        var firstNameIdx = _random.Next(FirstNames.Length);
        var lastNameIdx = _random.Next(LastNames.Length);

        var name = string.Format("{0} {1}", FirstNames[firstNameIdx], LastNames[lastNameIdx]);

        return name;
    }

    // ----------------------------------------------------------------------------------------------------
    // Use a Box-Muller transform to generate a distribution of years centred around the meanYear
    private int generateYearNormalDist(double meanYear = 2000, double stdDev = 10){
        double u1 = 1.0 - _random.NextDouble(); 
        double u2 = 1.0 - _random.NextDouble();

        double z1 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); 
        double randNormal = meanYear + stdDev * z1; 

        return (int)Math.Floor(randNormal);
    }
}