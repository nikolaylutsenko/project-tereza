using Microsoft.AspNetCore.Identity;

namespace Project.Tereza.Core.Entities.Identity;

public class AppUser : IdentityUser<Guid>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public override string? UserName { get; set; }

    // navigation prop
    public virtual ICollection<AppUserRoles> UserRoles { get; set; }

    public AppUser()
    {
        UserRoles = new List<AppUserRoles>();
    }

}