using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities
{
    public partial class Book
    {
        public Book()
        {
            History = new HashSet<History>();
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [Column("ISBN")]
        public string Isbn { get; set; }
        public string PublishYear { get; set; }
        public double CoverPrice { get; set; }
        public bool Status { get; set; }

        [InverseProperty("Book")]
        public virtual ICollection<History> History { get; set; }
    }
}
