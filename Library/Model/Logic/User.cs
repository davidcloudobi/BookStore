using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Entities;
using Library.Model.DTO;
using Microsoft.EntityFrameworkCore;


namespace Library.Model.Logic
{
  public  class User:IUser
  {
      private readonly BookStoreDbContext _bookStoreDb;

      public User(BookStoreDbContext bookStoreDb)
      {
          _bookStoreDb = bookStoreDb;
      }
      public async Task<IEnumerable<BookDTO>> GetBooks()
      {
          var ListOfBooks = await _bookStoreDb.Book.ToListAsync();

          IEnumerable<BookDTO> bookDto = ListOfBooks.Select(response => new BookDTO
          {
              Title = response.Title,
              ISBN =  response.Isbn,
              PublishYear = response.PublishYear,
              CoverPrice = response.CoverPrice,
              Status = response.Status ? "Available": "Not Available"
          });

          return bookDto;

      }

      public async Task<IEnumerable<BookDTO>> GetBookByTitle(string title)
      {
          var ListOfBooks = await _bookStoreDb.Book.Where(response=> response.Title == title).ToListAsync();

          IEnumerable<BookDTO> bookDto = ListOfBooks.Select(response => new BookDTO
          {
              Title = response.Title,
              ISBN = response.Isbn,
              PublishYear = response.PublishYear,
              CoverPrice = response.CoverPrice,
              Status = response.Status ? "Available" : "Not Available"
          });

          return bookDto;
        }

      public async Task<IEnumerable<BookDTO>> GetBookByIsbn(string isbn)
      {
        
          var ListOfBooks = await _bookStoreDb.Book.Where(response => (response.Isbn).Replace("-", "") == isbn.Replace("-", "")).ToListAsync();

          IEnumerable<BookDTO> bookDto = ListOfBooks.Select(response => new BookDTO
          {
              Title = response.Title,
              ISBN = response.Isbn,
              PublishYear = response.PublishYear,
              CoverPrice = response.CoverPrice,
              Status = response.Status ? "Available" : "Not Available"
          });

          return bookDto;
        }

      public async Task<List<Book>> GetBooksByStatus()
      {
          throw new NotImplementedException();
      }

      public bool BorrowBook()
      {
          throw new NotImplementedException();
      }

      public bool BorrowBooks()
      {
          throw new NotImplementedException();
      }
  }
}
