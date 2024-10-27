

using NUnit.Framework;
using System;
using ForensicsCaseLibrary;

[TestFixture]
public class ForensicsCaseLibraryTests
{
    private ForensicsCaseLibrary.ForensicsCaseLibrary library;

    [SetUp]
    public void Setup() => library = new ForensicsCaseLibrary.ForensicsCaseLibrary();

    [Test]
    public void TestCaseCreation()
    {
        var case1 = library.CreateCase("001", 123, "Detective A", "Homicide");
        Assert.That(case1, Is.Not.Null);
        Assert.That("001", Is.EqualTo(case1.CaseNumber));
    }

    //TODO add more tests
}