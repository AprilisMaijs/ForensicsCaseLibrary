namespace ForensicsCaseLibrary;

public class Case
{
    private const decimal BasePrice = 50;
    private static readonly Dictionary<string, decimal> ExhibitPricing = new()
    {
        { "BulletShell", 15 },
        { "Swab", 5 },
        { "Luggage", 150 }
    };

    public string CaseNumber { get; }  // Now set by the library, not manually
    public CaseState State { get; private set; }
    public int CustomerId { get; }
    public string ResponsiblePerson { get; }
    public string CaseType { get; }
    
    private readonly List<Exhibit> _exhibits = new();
    public IReadOnlyList<Exhibit> Exhibits => _exhibits.AsReadOnly();
    public Case(string caseNumber, int customerId, string responsiblePerson, string caseType)
    {
        CaseNumber = caseNumber;
        CustomerId = customerId;
        ResponsiblePerson = responsiblePerson;
        CaseType = caseType;
        State = CaseState.OnHold;
    }

    public void AddExhibit(Exhibit exhibit) => _exhibits.Add(exhibit);

    public void Approve() => State = CaseState.Approved;

    public void Reject() => State = CaseState.Rejected;

    public decimal CalculateTotalCost()
    {
        decimal totalCost = BasePrice;
        int totalExhibitCount = _exhibits.Count;

        foreach (var exhibit in _exhibits)
        {
            var costPerItem = ExhibitPricing.GetValueOrDefault(exhibit.Type, 0);
            totalCost += costPerItem;
        }

        decimal discountMultiplier = 1;
        
        if (totalExhibitCount >= 100)
        {
            discountMultiplier = 0.8m;
        }
        else
        {
            var exhibitCountByType = _exhibits
                .GroupBy(e => e.Type)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var exhibitType in exhibitCountByType)
            {
                var count = exhibitType.Value;

                if (count >= 50)
                {
                    discountMultiplier = Math.Min(discountMultiplier, 0.85m);
                }
                else if (count >= 10)
                {
                    discountMultiplier = Math.Min(discountMultiplier, 0.95m);
                }
            }
        }

        totalCost *= discountMultiplier;
        return totalCost;
    }
}
