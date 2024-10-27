namespace ForensicsCaseLibrary;

public class Case
{
    // Existing fields
    private const decimal BasePrice = 50;
    private static readonly Dictionary<string, decimal> ExhibitPricing = new()
    {
        { "BulletShell", 15 },
        { "Swab", 5 },
        { "Luggage", 150 }
    };

    public string CaseNumber { get; }
    public CaseState State { get; private set; }
    public int CustomerId { get; }
    public string ResponsiblePerson { get; }
    public string CaseType { get; }
    
    //had to be added for tests - TODO surely there is a better way? 
    public IReadOnlyList<Exhibit> Exhibits => exhibits.AsReadOnly();
    private readonly List<Exhibit> exhibits = new();

    public Case(string caseNumber, int customerId, string responsiblePerson, string caseType)
    {
        CaseNumber = caseNumber;
        CustomerId = customerId;
        ResponsiblePerson = responsiblePerson;
        CaseType = caseType;
        State = CaseState.OnHold;
    }

    public void AddExhibit(Exhibit exhibit) => exhibits.Add(exhibit);

    public void Approve() => State = CaseState.Approved;

    public void Reject() => State = CaseState.Rejected;

    public decimal CalculateTotalCost()
    {
        decimal totalCost = BasePrice;
        //TODO pricing logic

        return totalCost;
    }
}
