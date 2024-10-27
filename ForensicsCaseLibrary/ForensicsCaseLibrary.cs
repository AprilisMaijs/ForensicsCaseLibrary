namespace ForensicsCaseLibrary;

public class ForensicsCaseLibrary
{
    private readonly List<Case> cases = new();

    public Case CreateCase(string caseNumber, int customerID, string responsiblePerson, string caseType)
    {
        var newCase = new Case(caseNumber, customerID, responsiblePerson, caseType);
        cases.Add(newCase);
        return newCase;
    }

    public void AddExhibitToCase(string caseNumber, Exhibit exhibit)
    {
        var selectedCase = cases.FirstOrDefault(c => c.CaseNumber == caseNumber);
        selectedCase?.AddExhibit(exhibit);
    }

    public IEnumerable<Case> ListAllCases() => cases;

    public Case GetCaseByNumber(string caseNumber) =>
        cases.FirstOrDefault(c => c.CaseNumber == caseNumber);

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
