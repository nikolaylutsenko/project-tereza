using Project.Tereza.Core.Entities;

namespace Project.Tereza.Core;
public class Need : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    // add in future updates
    //public NeedType Type { get; set; }
    public int Count { get; set; }
    public bool IsCovered { get; set; }

    public Need()
    {

    }

    public Need(string name, string description, int count, bool isCovered)
    {
        Name = name;
        Description = description;
        Count = count;
        IsCovered = isCovered;
    }
}
