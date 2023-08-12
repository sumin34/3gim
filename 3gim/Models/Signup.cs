using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace _3gim.Models
{
    public class Signup
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
