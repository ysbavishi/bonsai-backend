using AutoMapper;
using BonsaiBackend.DTO;
using BonsaiBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BonsaiBackend.Controllers {
    [Route("/api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase {
        protected readonly GenericeRepository<Clients> _repo;
        protected readonly GenericeRepository<Users> _userRepo;
        protected readonly IMapper _mapper;
        public ClientController(DBBonsaiContext context,IMapper mapper) {
            _repo = new GenericeRepository<Clients>(context);
            _userRepo = new GenericeRepository<Users>(context);
            _mapper = mapper;
        }
        // Create a new Client
        // Client:
            // User ID (FK)
            // Name
            // Email
        [HttpPost]
        public IActionResult CreateClient([FromBody] ClientDTO client) {
            try {
                int id = client.UserId;
                Users user = _userRepo.Find(x => x.Id == id).FirstOrDefault<Users>();
                if (user == null) {
                    return BadRequest("Bad Request");
                }
                Clients mainClient = new Clients {
                    Name = client.Name,
                    Email = client.Email,
                    UserId = client.UserId,
                    Users = user
                };
                _repo.Add(mainClient);
                return Ok(client);
            } catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
        [HttpGet]
        public IActionResult GetClient([FromBody] int Id) {
            try {
                Clients client =   _repo.GetById(Id);
                return Ok(client);
            } catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest("Error");
            }
        }

        [HttpGet]
        [Route("/all")]
        public IActionResult GetAllClient() {
            try {
                IEnumerable<Clients> client =   _repo.GetAll();
                return Ok(client);
            } catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] Clients client) {
            try {
                var temp = _repo.Find(acc => acc.Email == client.Email);
                Clients clientTemp = _mapper.Map<Clients>(temp.FirstOrDefault());
                _repo.Remove(clientTemp);
                bool status = _repo.CompleteChanges();
                if (!status) {
                    return Ok("Not working");
                }
                return Ok(clientTemp);
           } catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest(e.Message);
           }
        }
        [HttpPut]
        public IActionResult Update([FromBody] ClientDTO client) {
            try {
                var tempClient = _repo.Find(x => x.Email == client.Email);
                Clients cli = tempClient.First();
                cli.UserId = client.UserId;
                cli.Name = client.Name;
                if(ModelState.IsValid) {
                    _repo.Update(cli);
                    return Ok(cli);
                }
                return Ok(cli);
            } catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest(e.Message);
            }
        }
    }
}