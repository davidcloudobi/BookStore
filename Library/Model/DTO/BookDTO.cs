using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.DTO
{
  public  class BookDTO
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublishYear { get; set; }
        public double CoverPrice { get; set; }
        public string Status { get; set; }

    } 
}
