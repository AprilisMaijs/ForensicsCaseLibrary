using NUnit.Framework;

namespace ForensicsCaseLibrary.Tests;

[TestFixture]
public class CaseStateTests
{
    [Test]
    public void PlaceOnHold_ShouldSetStateToOnHold()
    {
        var testCase = new Case("0001", 123, "Detective A", "Theft");
        testCase.PlaceOnHold();
        Assert.That(CaseState.OnHold, Is.EqualTo(testCase.State));
    }

    [Test]
    public void PlaceOnHold_ShouldThrowException_WhenStateIsRejected()
    {
        var testCase = new Case("0001", 123, "Detective A", "Theft");
        testCase.Reject();
        Assert.Throws<InvalidOperationException>(() => testCase.PlaceOnHold());
    }

    [Test]
    public void Complete_ShouldSetStateToCompleted_WhenStateIsApproved()
    {
        var testCase = new Case("00001", 123, "Detective A", "Theft");
        testCase.Approve();
        testCase.Complete();
        Assert.That(CaseState.Completed, Is.EqualTo(testCase.State));
    }

    [Test]
    public void Complete_ShouldThrowException_WhenStateIsRejected()
    {
        var testCase = new Case("00001", 123, "Detective A", "Theft");
        testCase.Reject();
        Assert.Throws<InvalidOperationException>(() => testCase.Complete());
    }

    [Test]
    public void Complete_ShouldThrowException_WhenStateIsNew()
    {
        var testCase = new Case("0001", 123, "Detective A", "Theft");
        Assert.Throws<InvalidOperationException>(() => testCase.Complete());
    }
    
    [Test]
    public void Approve_ShouldSetStateToApproved_WhenStateIsNew()
    {
        var caseItem = new Case("0001", 111, "Detective A", "Theft");
        caseItem.Approve();
        Assert.That(CaseState.Approved, Is.EqualTo(caseItem.State));
    }

    [Test]
    public void Reject_ShouldSetStateToRejected_WhenStateIsNew()
    {
        var caseItem = new Case("00001", 111, "Detective A", "Theft");
        caseItem.Reject();
        Assert.That(CaseState.Rejected, Is.EqualTo(caseItem.State));
    }

    [Test]
    public void Approve_ShouldThrowException_WhenCaseIsAlreadyRejected()
    {
        var caseItem = new Case("0001", 111, "Detective A", "Theft");
        caseItem.Reject();

        Assert.Throws<InvalidOperationException>(() => caseItem.Approve());
    }
    
    [Test]
    public void CaseState_ShouldNotChange_WhenAlreadyComplete()
    {
        var caseItem = new Case("0001", 111, "Detective A", "Theft");
        caseItem.Approve();
        caseItem.Complete();
        Assert.Throws<InvalidOperationException>(() => caseItem.Reject());
        Assert.Throws<InvalidOperationException>(() => caseItem.Approve());
        Assert.Throws<InvalidOperationException>(() => caseItem.PlaceOnHold());
    }
}