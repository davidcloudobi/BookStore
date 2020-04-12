using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Model.DTO;
using Library.Model.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
        private readonly UserDTO _userDto;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public UserController(IUser user, UserDTO userDto)
        {
            _user = user;
            _userDto = userDto;
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
                if (listOfBooks.Any())
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
                Log.Error(e.Message);
               
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
                if (listOfBooks.Any())
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
                Log.Error(e.Message);

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

                if (listOfBooks.Any())
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
                Log.Error(e.Message);

            }

            return BadRequest();

        }


        // GET: api/User/books/status
        /// <summary>
        ///  Returns the list of books with the same status
        /// </summary>
        /// <remarks>
        /// Sample request (this request get the list of all the books)
        /// GET /User/books/status
        /// 
        /// [ 
        ///     {
        ///         
        ///         "status": "available or not availiable" 
        ///     } 
        /// ]
        /// </remarks>
        /// <param name="newstatus"></param>
        /// <response code="200">Returns the list of books with the same status</response>

        [HttpGet("books/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetBookByStatus(string newstatus)
        {

            try
            {
                bool boolStatus = false;

                var status = newstatus.ToLower();
                if (status == "available")
                {
                    boolStatus = true;
                }
                else if (status == "not available")
                {
                    boolStatus = false;
                }
                else
                {
                    return BadRequest("Invalid Details, enter 'available' or 'not available' ");
                }

                var listOfBooks = await _user.GetBooksByStatus(boolStatus);

                if (listOfBooks.Any())
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
                Log.Error(e.Message);

            }

            return BadRequest();

        }

        /// <summary>
        /// post request
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost("borrow")]
        public async Task<IActionResult> Borrow(UserDTO user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool res = await _user.BorrowBook(user);

                    if (res == true)
                    {
                        return Ok("Successful");
                    }

                    return BadRequest("Not Successful");

                }

            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
            return BadRequest("Not Successful");

        }

        /// <summary>
        /// Post CheckIn
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>


        [HttpPost("checkIn")]
        public async Task<IActionResult> CHeckIn(UserDTO user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var status =  await _user.CheckIn(user);

                    if (status)
                    {
                        return Ok("Successful");
                    }
                    else
                    {
                        return BadRequest("Something went wrong!");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

            return BadRequest("Something went wrong!");
        }

        /// <summary>
        /// Post Payment
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>


        [HttpPost("payment")]
        public async Task<IActionResult> PaymentFee(int phoneNumber)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var status = await _user.PaymentFee(phoneNumber);

                    if (status )
                    {
                        return Ok("Successful");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
               
            }

            return BadRequest("Something went wrong!");
        }
    }
}

