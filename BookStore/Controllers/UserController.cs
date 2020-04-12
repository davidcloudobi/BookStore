using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Model.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    /// <summary>
    /// Version 1, User
    /// </summary>

    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "User")]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public UserController(IUser user)
        {
            _user = user;
        }

        // GET: api/User/books
        /// <summary>
        ///  Return the list of the book entities
        /// </summary>
        /// <remarks>
        /// Sample request (this request get the list of all the books)
        /// </remarks>
        /// <response code="200">Returns the list of books</response>
        [HttpGet("books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetBook()
        {
            var listOfBooks = await _user.GetBooks();
            try
            {
                if (listOfBooks.Count() > 0)
                {
                    return Ok(listOfBooks);
                }
                else
                {
                    return BadRequest("No record found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }

            return BadRequest();

        }

        // GET: api/User/books/title
        /// <summary>
        ///  Returns the list of books with the same title
        /// </summary>
        /// <remarks>
        /// Sample request (this request get the list of all the books)
        /// </remarks>
        /// <param name="title"></param>
        /// <response code="200">Returns the list of books with the same title</response>

        [HttpGet("books/title")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            var listOfBooks = await _user.GetBookByTitle(title);
            try
            {
                if (listOfBooks.Count() > 0)
                {
                    return Ok(listOfBooks);
                }
                else
                {
                    return BadRequest("No record found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return BadRequest();

        }



        // GET: api/User/books/isbn
        /// <summary>
        ///  Returns the list of books with the same Isbn
        /// </summary>
        /// <remarks>
        /// Sample request (this request get the list of all the books)
        /// </remarks>
        /// <param name="isbn"></param>
        /// <response code="200">Returns the list of books with the same Isbn</response>

        [HttpGet("books/isbn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetBookByIsbn(string isbn)
        {
          
            try
            {
                if (isbn.Length < 10 || isbn ==null)
                {
                    return BadRequest("Invalid request, ISBN length less than 10");
                }


                var listOfBooks = await _user.GetBookByIsbn(isbn);

                if (listOfBooks.Count() > 0)
                {
                    return Ok(listOfBooks);
                }
                else
                {
                    return BadRequest("No record found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return BadRequest();

        }
    }
}