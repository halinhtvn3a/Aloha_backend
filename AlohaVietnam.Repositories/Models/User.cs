using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlohaVietnam.Repositories.Models
{
    public class User : IdentityUser
    {
        public string? Fullname { get; set; }

        public decimal Balance { get; set; } = 0;

        [StringLength(int.MaxValue)]
        public string? ProfilePictureUrl { get; set; }
    }
}
