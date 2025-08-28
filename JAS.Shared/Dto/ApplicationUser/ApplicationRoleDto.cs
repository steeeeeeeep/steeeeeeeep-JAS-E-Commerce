using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace JAS.Shared.Dto.ApplicationUser
{
    public class ApplicationRoleDto : IdentityRole
    {
        [JsonIgnore]
        public ICollection<ApplicationUserDto> Users { get; set; }
    }
}