using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Entities;
using Library.Model.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Serilog;


namespace Library.Model.Logic
{
  public  class User:IUser
  {
      private readonly BookStoreDbContext _bookStoreDb;

      public User(BookStoreDbContext bookStoreDb)
      {
          _bookStoreDb = bookStoreDb;
      }

      public static DateTime DateTimeReturn(DateTime DateOfCheckOut, int days)
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

    
        public async Task<IEnumerable<BookDTO>> GetBooks()
      {
          
            var ListOfBooks = await _bookStoreDb.Book.Select(h => new {
                    Id = h.Id,
                    Title = h.Title,
                    BookAvalaiblityCount = h.BookAvalaiblityCount,
                    PublishYear=h.PublishYear,
                    CoverPrice=h.CoverPrice,
                    Status=h.Status,
                    Isbn = h.Isbn,
                }).OrderBy(x=> x.BookAvalaiblityCount).ToListAsync();

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

      public async Task<IEnumerable<BookDTO>> GetBooksByStatus(bool newStatus)
      {
         

          var ListOfBooks = await _bookStoreDb.Book.Where(response => response.Status == newStatus).ToListAsync();

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

      public async  Task<bool> BorrowBook(UserDTO userDto)
      {

          try
          {

              var user = await _bookStoreDb.User.FirstOrDefaultAsync(response =>
                  response.NationalIdentificationNumber == userDto.NationalIdentificationNumber);

          

              var newUser = new Entities.User();

          
         
          

              if (user == null)
              {

                  newUser.FullName = userDto.FullName;
                  newUser.NationalIdentificationNumber = userDto.NationalIdentificationNumber;
                  newUser.History = new List<History>();
                  newUser.Email = userDto.Email;
                  newUser.Role = "user";
                  newUser.PhoneNumber = userDto.PhoneNumber;

                  _bookStoreDb.User.Add(newUser);
                  _bookStoreDb.SaveChangesAsync().Wait();

                  user = await _bookStoreDb.User.FirstOrDefaultAsync(response =>
                      response.NationalIdentificationNumber == userDto.NationalIdentificationNumber);

              }
              
              var history = await _bookStoreDb.History.FirstOrDefaultAsync(response => response.PenaltyFee > 0 && response.UserId == user.Id);


              if (history == null)
              {
                  var book = await _bookStoreDb.Book.FirstOrDefaultAsync(response =>
                      response.Title == userDto.BookTitle && response.Isbn == userDto.BookIsbn);

                  if (book == null || book.BookAvalaiblityCount == 0)
                  {

                      return false;
                  }
                  else
                  {
                      var count =  --book.BookAvalaiblityCount;
                      if (count == 0)
                      {

                          book.Status = false;
                      }

                      var newHistory = new History();
                      newHistory.DateOfCheckOut = Convert.ToDateTime(userDto.DateNow);
                      newHistory.DateOfReturn = DateTimeReturn(DateTime.Now, 10);
                      newHistory.NumberOfDays = 10;
                      newHistory.PenaltyFee = 0;
                      newHistory.UserId = user.Id;
                      newHistory.BookId = book.Id;
                      _bookStoreDb.History.Add(newHistory);

                      _bookStoreDb.SaveChangesAsync().Wait();

                  }
              }
              else
              {
                  return false;
              }
          }
          catch (Exception e)
          {
            Log.Error(e.Message);
          }
       

       
          return true;
      }

      public async Task<bool> CheckIn(UserDTO userDto)
      {
          try
          {
              var book = await _bookStoreDb.Book.FirstOrDefaultAsync(response =>
                  response.Title == userDto.BookTitle && response.Isbn == userDto.BookIsbn);


              if (book != null)
              {

                  var user = await _bookStoreDb.User.FirstOrDefaultAsync(response =>
                      response.Email == userDto.Email ||
                      response.NationalIdentificationNumber == userDto.NationalIdentificationNumber ||
                      response.PhoneNumber == userDto.PhoneNumber);
                  var history =
                      await  _bookStoreDb.History.FirstOrDefaultAsync(response =>
                          response.BookId == book.Id && response.UserId == user.Id && response.NumberOfDays > 0);
        
                  book.BookAvalaiblityCount++;
                  book.Status = true;

                  _bookStoreDb.SaveChangesAsync().Wait();

                  var currentDate = DateTime.Now;
                  DateTime returnDate = Convert.ToDateTime(history.DateOfReturn);

                  if (currentDate > history.DateOfReturn)
                  {
                      var days = currentDate.Subtract(returnDate).TotalDays;
                      history.NumberOfDays = Convert.ToInt32(days);
                      history.PenaltyFee += 200;

                      _bookStoreDb.SaveChangesAsync().Wait();

                      return false;
                  }

                  history.NumberOfDays = 0;
                  book.BookAvalaiblityCount++;
                  book.Status = true;

                  _bookStoreDb.SaveChangesAsync().Wait();
                  return true;
              }
          }
          catch (Exception e)
          {
             Log.Error(e.Message);
          }

          return false;

      }

      public async Task<bool> PaymentFee(int phonenumber)
      {
          try
          {
              var user = await _bookStoreDb.User.FirstOrDefaultAsync(response => response.PhoneNumber == phonenumber);

              if (user != null)
              {
                  var history = await _bookStoreDb.History.FirstOrDefaultAsync(response =>
                      response.UserId == user.Id && response.NumberOfDays > 0);
                  history.NumberOfDays = 0;
                  _bookStoreDb.SaveChangesAsync().Wait();
                  return true;
              }
          }
          catch (Exception e)
          {
               Log.Error(e.Message);
          }

          return false;

      }
  }
}
