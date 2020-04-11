using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.EF
{
  public  class Book
    {
        public  int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public string PublishYear { get; set; }
    public double CoverPrice { get; set; }
    public bool Status { get; set; }


    }
}

