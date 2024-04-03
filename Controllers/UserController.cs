using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BonsaiBackend.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using BonsaiBackend.DTO;
using AutoMapper;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace BonsaiBackend.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class UserController : Controller {
        private readonly IMapper _mapper;
        protected readonly GenericeRepository<Users> _repo; 
        public UserController(DBBonsaiContext context, IMapper mapper) {
            _mapper = mapper;
            _repo = new GenericeRepository<Users>(context);
            
        }
        // Get user by Id
        [HttpGet]
        public IActionResult GetUserById([FromBody] int id) {
            Console.WriteLine(id);
            try {
                var user = _repo.GetById(Convert.ToInt32(id));
               return Ok(user);
           } catch (Exception e) {
            Console.WriteLine(e);
            return Ok("Not working");
           }
        }

        // Add User to Database
        [HttpPost]
        public IActionResult AddUser([FromBody] UserDto UserDt) {
        Console.WriteLine(UserDt.Email);
        Console.WriteLine(UserDt.Password);
        Console.WriteLine(UserDt.Name);
        try {
            Users user = _mapper.Map<Users>(UserDt);
            _repo.Add(user);
            return Ok("Done");
        } catch (Exception err) {
            Console.WriteLine(err);
            return Ok("Failed");
        }
        return Ok("DONE");
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] UserDto UserDt) {
            try {
                var temp = _repo.Find(acc => acc.Email == UserDt.Email);
                Users user = _mapper.Map<Users>(temp.FirstOrDefault());
                _repo.Remove(user);
                bool status = _repo.CompleteChanges();
                if (!status) {
                    return Ok("Not working");
                }
                return Ok(user);
           } catch (Exception e) {
            Console.WriteLine(e);
            return Ok("Not working");
           }
            
        }
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserDto UserDt) {
            try {
                var user = _repo.Find(acc => acc.Email == UserDt.Email);
                Users use = user.FirstOrDefault();
                use.Email = UserDt.Email;
                use.Name = UserDt.Name;
                use.Password = UserDt.Password;
                if(ModelState.IsValid) {
                    _repo.Update(use);
                    return Ok("Passed");
                } 
                return Ok("Mistake"); 
            } catch (Exception err) {
                Console.WriteLine(err);
                return NotFound("Error");
            }  
        }
    }
}