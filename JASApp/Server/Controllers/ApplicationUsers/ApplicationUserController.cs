//using JAS.Shared.Dto.ApplicationUser;
//using JASApi.Data;
//using JASData.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace JASApp.Api.Controllers.ApplicationUsers;

//public class ApplicationUserController(AppDbContext context) : Controller
//{
//    private readonly AppDbContext _dbContext;

//    [HttpPost]
//    public async Task<ActionResult<bool>> CreateApplicationUserAsync(AddOrEditApplicationUserDto user)
//    {
//        try
//        {
//            List<ApplicationUser> users = await _dbContext.ApplicationUsers.Where(c => !c.IsDeleted).ToListAsync();

//            if(users.FirstOrDefault(c => c.Email == user.Email) != null)
//            {
//                return BadRequest("Email is already taken.");
//            }

//            if (users.FirstOrDefault(c => c.PhoneNumber == user.PhoneNumber) != null)
//            {
//                return BadRequest("Phone number is already taken.");
//            }

//            if (users.FirstOrDefault(c => c.Username == user.Username) != null)
//            {
//                return BadRequest("Username is already taken.");
//            }

//            ApplicationUser addUser = new()
//            {
//                Username = user.Username,
//                Email = user.Email,
//                Password = user.Password,
//                PasswordHash = user.PasswordHash,
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                PhoneNumber = user.PhoneNumber,
//                Street = user.Street,
//                City = user.City,
//                State = user.State,
//                PostalCode = user.PostalCode,
//                Country = user.Country,
//                IsActive = true
//            };

//            return true;

//        }
//        catch (Exception ex) 
//        {
//            return BadRequest(ex.Message);
//        }
//    }
//}
