namespace Insurance.Models
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        // Active or Inactive
        public bool IsActive { get; set; } 

        // To store roles of the user
        public IList<string>? Roles { get; set; } 

    }
}
