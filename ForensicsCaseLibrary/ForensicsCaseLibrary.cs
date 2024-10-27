namespace ForensicsCaseLibrary;

public class ForensicsCaseLibrary
{
    private int _nextCaseNumber = 1;
    private readonly List<Case> _cases = new();

    public Case CreateCase(int customerId, string responsiblePerson, string caseType)
    {
        string caseNumber = GenerateCaseNumber(); // Automatically generated case number
        var newCase = new Case(caseNumber, customerId, responsiblePerson, caseType);
        _cases.Add(newCase);
        return newCase;
    }

    private string GenerateCaseNumber()
    {
        return (_nextCaseNumber++).ToString("D4");
    }
    
    public void AddExhibitToCase(string caseNumber, Exhibit exhibit)
    {
        var selectedCase = _cases.FirstOrDefault(c => c.CaseNumber == caseNumber);
        selectedCase?.AddExhibit(exhibit);
    }

    public IEnumerable<Case> ListAllCases() => _cases;

    public Case GetCaseByNumber(string caseNumber) =>
        _cases.FirstOrDefault(c => c.CaseNumber == caseNumber)!;

    public void ApproveCase(string caseNumber)
    {
        var caseToApprove = GetCaseByNumber(caseNumber);
        caseToApprove?.Approve();
    }

    public void RejectCase(string caseNumber)
    {
        var caseToReject = GetCaseByNumber(caseNumber);
        caseToReject?.Reject();
    }
}
