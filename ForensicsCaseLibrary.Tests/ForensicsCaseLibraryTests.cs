using NUnit.Framework;

namespace ForensicsCaseLibrary.Tests;

[TestFixture]
public class ForensicsCaseLibraryTests
{
    private global::ForensicsCaseLibrary.ForensicsCaseLibrary _library;

    [SetUp]
    public void Setup() => _library = new global::ForensicsCaseLibrary.ForensicsCaseLibrary();

    [Test]
    public void TestCaseCreation()
    {
        var case1 = _library.CreateCase(123, "Detective A", "Homicide");
        Assert.That(case1, Is.Not.Null);
        Assert.That(123, Is.EqualTo(case1.CustomerId));
        Assert.That("Detective A", Is.EqualTo(case1.ResponsiblePerson));
        Assert.That("Homicide", Is.EqualTo(case1.CaseType));
    }

    [Test]
    public void TestAddExhibitToCase()
    {
        var case1 = _library.CreateCase( 456, "Detective B", "Robbery");
        _library.AddExhibitToCase("0001", new Exhibit("BulletShell", DateTime.Now));
        Assert.That(1, Is.EqualTo(case1.Exhibits.Count));
    }

    [Test]
    public void TestCaseApproval()
    {
        var case1 = _library.CreateCase(789, "Detective Approver", "Theft");
        _library.ApproveCase("0001");
        Assert.That(CaseState.Approved, Is.EqualTo(case1.State));
    }
    
    [Test]
    public void TestCaseReject()
    {
        var case1 = _library.CreateCase(789, "Detective Rejecter", "Theft");
        _library.RejectCase("0001");
        Assert.That(CaseState.Rejected, Is.EqualTo(case1.State));
    }

    // This test is not a great idea - prices are subject to change, and it will fail due to them
    // But since cost calculation is a major part of the assignment - here it is.
    [Test]
    public void TestCostCalculationWithDiscounts()
    {
        var case1 = _library.CreateCase(111, "Detective D", "Fraud");

        for (int i = 0; i < 10; i++)
            _library.AddExhibitToCase("0001", new Exhibit("BulletShell", DateTime.Now));
        for (int i = 0; i < 50; i++)
            _library.AddExhibitToCase("0001", new Exhibit("Swab", DateTime.Now));
        Assert.That(382.5m, Is.EqualTo(case1.CalculateTotalCost()));  // Base ( 50 + 10 * 15 + 50 * 5 ) * 0.85 discount
    }
    
    
}