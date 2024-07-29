using AutoMapper;
using BonsaiBackend.DTO;
using BonsaiBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonsaiBackend.Controllers {
    [Route("/api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase {
        protected readonly GenericeRepository<Clients> _clientRepo;
        protected readonly GenericeRepository<Users> _userRepo;
        protected readonly GenericeRepository<Tasks> _repo;
        protected readonly IMapper _mapper;

        public TaskController(DBBonsaiContext context, IMapper mapper) {
            _mapper = mapper;
            _clientRepo = new GenericeRepository<Clients>(context);
            _userRepo = new GenericeRepository<Users>(context);
            _repo = new GenericeRepository<Tasks>(context);
        }

    [HttpPost]
    public IActionResult CreateTask([FromBody] TaskDTO task) {
        try {
            Users user = _userRepo.Find(x => x.Id == task.UserId).FirstOrDefault();
            Clients clients = _clientRepo.Find(x => x.Id == task.ClientId).FirstOrDefault();
            if (user == null || clients == null) {
                return BadRequest("Err");
            }
            Tasks mainTask = new Tasks {
                UserId = task.UserId,
                ClientId = task.ClientId,
                Users = user,
                Clients = clients,
                Description = task.Description,
                Status = task.Status
            };
            _repo.Add(mainTask);
            Ok(task);

        } catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest("Bad code");
        }
        return Ok("He");
    }

    [HttpGet]
    public IActionResult GetTask([FromBody] int Id) {
        try {
            Tasks task =   _repo.GetById(Id);
            return Ok(task);
        } catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest("Error");
        }
    }

    [HttpGet]
    [Route("/api/[controller]/all")]
    public IActionResult GetAllClient() {
        try {
            IEnumerable<Tasks> tasks =   _repo.GetAll();
            return Ok(tasks);
        } catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest("Error");
        }
    }

    [HttpPut]
    public IActionResult UpdateTask([FromBody] TaskDTO task) {
        try {
            Tasks mainTask = _repo.Find(x => x.Description == task.Description).FirstOrDefault<Tasks>();
            mainTask.Description = task.newDescription;
            _repo.Update(mainTask);
            return Ok(task);
        } catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest("Error");
        }
    }

    [HttpDelete]
    public IActionResult DeleteTask([FromBody] TaskDTO task) {
        try {
            Tasks mainTask = _repo.Find(x => x.Description == task.Description).FirstOrDefault<Tasks>();
            _repo.Remove(mainTask);
            return Ok(task);
        } catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest("Error");
        }
    }

    }


    }

