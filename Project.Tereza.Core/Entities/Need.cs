namespace Project.Tereza.Core;
public class Need : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    // add in future updates
    //public NeedType Type { get; set; }
    public int Count { get; set; }
    public bool IsCovered { get; set; }
}
