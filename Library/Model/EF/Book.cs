using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Model.EF
{
  public  class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public  Guid Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int PublishYear { get; set; }
    public double CoverPrice { get; set; }
    public bool Status { get; set; } 

    public int BookAvalaiblityCount { get; set; } 


    }
}

