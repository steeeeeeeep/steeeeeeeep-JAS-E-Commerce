using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace JASData.Models;

public class ApplicationRole : IdentityRole
{
    [JsonIgnore]
    public ICollection<ApplicationUser> Users { get; set; }
}
