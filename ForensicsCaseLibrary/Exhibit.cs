namespace ForensicsCaseLibrary;

public class Exhibit
{
    public string Type { get; }
    public DateTime DateCollected { get; }

    public Exhibit(string type, DateTime dateCollected)
    {
        Type = type;
        DateCollected = dateCollected;
    }
}