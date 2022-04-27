using Microsoft.AspNetCore.Identity;

namespace Project.Tereza.Core.Entities.Identity;
public class AppUserRoles : IdentityUserRole<Guid>
{
    public override Guid UserId { get; set; }
    public override Guid RoleId { get; set; }

    // navigation prop
    public virtual AppUser? User { get; set; }
    public virtual AppRole? Role { get; set; }
}
