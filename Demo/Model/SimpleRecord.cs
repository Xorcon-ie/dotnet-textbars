using System.ComponentModel;

namespace Model;


// ----------------------------------------------------------------------------------------------------
// Sample record for holding information to be evaluated by the rules
public record SimpleRecord {
    public SimpleRecord (){
        Id = Guid.NewGuid().ToString();
   }

    public string Id {get; set;}
    public required string CompanyName {get;init;}
    public required DateTime IncorporationDate {get;init;}
    public required string IncorporationCountryCode {get;init;}
    public string? RegistrationNumber {get;set;}
    public string? TaxRegistration {get;set;}
    public decimal CreditRating {get;set;}
    public string? ManagingDirector {get;set;}
    
}
