using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Entities;
using Library.Model.DTO;


namespace Library.Model.Logic
{
    public interface IUser
    {
        Task<IEnumerable<BookDTO>> GetBooks();
         Task<IEnumerable<BookDTO>> GetBookByTitle(string title);
         Task<IEnumerable<BookDTO>> GetBookByIsbn(string isbn);
        Task<IEnumerable<BookDTO>> GetBooksByStatus(bool status);
        Task<bool> BorrowBook(UserDTO userDto);
        Task<bool> CheckIn(UserDTO userDto);
        Task<bool> PaymentFee(int phonenumber);


    }
}