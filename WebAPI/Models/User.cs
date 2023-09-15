using Microsoft.AspNetCore.Identity;

namespace WebAPI.Models
{
    /// <summary>
    /// Represents a user in the system, extending from IdentityUser for authentication.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string? LastName { get; set; }

        public ICollection<Account>? Accounts { get; set; }
    }

}
