using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Model.EF
{
  public  class History
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public  Guid Id { get; set; }
      public  Book Book { get; set; }
      public  User User { get; set; }
      public  DateTime? DateOfCheckOut { get; set; }
      public DateTime? DateOfReturn { get; set; }
      public double PenaltyFee { get; set; }
      public int NumberOfDays { get; set; }

    }
}

//[UserId]
//,[BookId]
//,[DateOfCheckOut]
//,[DateOfReturn]
//,[PenaltyFee]
//,[NumberOfDays] 