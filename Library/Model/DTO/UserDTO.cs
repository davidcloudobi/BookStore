using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.DTO
{
  public  class UserDTO
    {
      
        public string FullName { get; set; }
        public string Email { get; set; }

        public int PhoneNumber { get; set; }
        public int NationalIdentificationNumber { get; set; }
       // public static DateTime DateOfCheckOut { get; set; } 
        public string DateNow{ get; set; }
       public  string BookTitle { get; set; }

        public  string BookIsbn { get; set; }


        public static DateTime DateTimeReturn(DateTime DateOfCheckOut ,  int days)
        {
            for (int index = 0; index < days; index++)
            {
                switch (DateOfCheckOut.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        DateOfCheckOut = DateOfCheckOut.AddDays(3);
                        break;
                    case DayOfWeek.Saturday:
                        DateOfCheckOut = DateOfCheckOut.AddDays(2);
                        break;
                    default:
                        DateOfCheckOut = DateOfCheckOut.AddDays(1);
                        break;
                }
            }
            return DateOfCheckOut;
        }

      
    }
}
