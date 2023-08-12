using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace _3gim.Models
{
    public class _3gimMember : IdentityUser
    {
        public string Name { get; set; }
    }
}
