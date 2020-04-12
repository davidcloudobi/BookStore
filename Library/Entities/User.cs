using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities
{
    public partial class User
    {
        
        public User()
        {
            History = new HashSet<History>();
        }

        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int NationalIdentificationNumber { get; set; }
        public string Role { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<History> History { get; set; }
    }
}
