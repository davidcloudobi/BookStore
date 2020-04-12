using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Library.Entities;
using Library.Model.DTO;
using Library.Model.Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace BookStore.Controllers
{
    /// <summary>
    /// Version 1, Admin
    /// </summary>
   
    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;

        /// <summary>
        /// Dependency injection for Admin logic  
        /// </summary>
        /// <param name="admin"></param>
        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }


        /// POST: api/Admin
        /// <summary>
        /// Create a book entity 
        /// </summary>
        /// <param name="book"></param>
        /// <remarks>
        /// Sample request (this request post a new book entity)
        /// </remarks>
        /// <response code="200">Returns Ok response in respect to  the successive creation of a new book entity</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateBook(Book book)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    await _admin.CreateBook(book);

                    return Ok("Successful");

                }
            }
            catch (Exception e)
            {
              Log.Error(e.Message);
            }

            return BadRequest("Invalid Book Details");


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        
        [HttpPost("login")]

        public IActionResult test(string role, string user)
        {

            var User = new List<Claim>()
            {new Claim(ClaimTypes.Role , user),
                
            };

            var UserIdentity = new ClaimsIdentity(User, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { UserIdentity });
            HttpContext.SignInAsync(userPrincipal);

            return Ok();
        }
    

    }
}