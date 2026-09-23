using Microsoft.AspNetCore.Identity;

namespace BUA_project.Models
{
	public class ApplicationUser : IdentityUser
	{
		public int? BusinessUserId { get; set; }
	}
}