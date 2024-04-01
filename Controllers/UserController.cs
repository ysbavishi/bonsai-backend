using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BonsaiBackend.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using BonsaiBackend.DTO;
using AutoMapper;
using System.Runtime.InteropServices.Marshalling;
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
        public IActionResult GetUserById(int id) {
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
                return Ok(user);
           } catch (Exception e) {
            Console.WriteLine(e);
            return Ok("Not working");
           }
            
        }
        [HttpPut]
        public IActionResult Update([FromBody] UserDto UserDt) {
            Console.WriteLine("CAlled");
            try {
                var temp = _repo.Find(acc => acc.Email == UserDt.Email);
                Users user = _mapper.Map<Users>(temp.FirstOrDefault());
                user.Name = UserDt.Name;
                _repo.Update(user);
                var temp2 = _repo.Find(acc => acc.Email == UserDt.Email);
                return Ok(temp2);
            } catch (Exception e) {
                Console.WriteLine(e);
                return Ok("Not working");
            }
        }
    }
}