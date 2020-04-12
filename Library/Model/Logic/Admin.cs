using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Library.Entities;
using Microsoft.EntityFrameworkCore;


namespace Library.Model.Logic
{
  public  class Admin:IAdmin
    {
        private readonly BookStoreDbContext _bookStoreDb;

        public Admin(BookStoreDbContext bookStoreDb)
        {
            _bookStoreDb = bookStoreDb;
        }
        public async Task CreateBook(Book book)
        {
            var status = await _bookStoreDb.Book.FirstOrDefaultAsync(response =>
                response.Isbn == book.Isbn && book.PublishYear == response.PublishYear && response.Title == book.Title);

            if (status == null) 
            {
                _bookStoreDb.Book.Add(book);
                _bookStoreDb.SaveChangesAsync().Wait();
            }
            else
            {
                status.BookAvalaiblityCount++;
                 _bookStoreDb.SaveChangesAsync().Wait();
            }
           

           
        }
    }
}
