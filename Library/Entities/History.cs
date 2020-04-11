using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities
{
    public partial class History
    {
        [Key]
        public int Id { get; set; }
        public int? BookId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? DateOfCheckOut { get; set; }
        public DateTime? DateOfReturn { get; set; }
        public double PenaltyFee { get; set; }
        public int NumberOfDays { get; set; }

        [ForeignKey(nameof(BookId))]
        [InverseProperty("History")]
        public virtual Book Book { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("History")]
        public virtual User User { get; set; }
    }
}
