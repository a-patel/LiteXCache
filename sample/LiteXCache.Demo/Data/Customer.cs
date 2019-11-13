#region Imports
using System; 
#endregion

namespace LiteXCache.Demo.Data
{
    public class Customer
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
