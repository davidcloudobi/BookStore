using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.EF
{
   public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public int PhoneNumber { get; set; }
        public int NationalIdentificationNumber { get; set; }
        public string Role { get; set; }
        
    }
}

//[FullName]
//,[Email]
//,[PhoneNumber]
//,[NationalIdentificationNumber]
//,[DateOfCheckOut] 