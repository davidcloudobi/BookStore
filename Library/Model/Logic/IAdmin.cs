using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Entities;



namespace Library.Model.Logic
{
    public interface IAdmin
    {

         Task CreateBook(Book book);


    }
}