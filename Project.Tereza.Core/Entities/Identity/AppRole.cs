using Microsoft.AspNetCore.Identity;

namespace Project.Tereza.Core.Entities.Identity;
public class AppRole : IdentityRole<Guid>
{
    public override Guid Id { get; set; }
    public override string? Name { get; set; }
    public override string? NormalizedName { get; set; }
    public override string? ConcurrencyStamp { get; set; }
    public string? RoleDescription { get; set; }

    //navigation prop
    public virtual ICollection<AppUserRoles>? UserRoles { get; set; }

    public AppRole()
    {
        UserRoles = new List<AppUserRoles>();
    }

    public AppRole(string name, string roleDescription)
    {
        Id = Guid.NewGuid();
        Name = name;
        NormalizedName = name.ToUpper();
        ConcurrencyStamp = name.ToUpper();
        RoleDescription = roleDescription;
    }
}
