using System.Collections.Generic;

namespace Web.Users.Models
{
    public class Purpose
    {
        public string GroupName { get; set; }
        public IList<string> Options { get; set; }

        public Purpose()
        {
            Options = new List<string>();
        }
    }
}