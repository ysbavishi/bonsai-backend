using AutoMapper;
using BonsaiBackend.DTO;
using BonsaiBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BonsaiBackend.Controllers {
    [Route("/api/[controller]")]
    [ApiController]
    public class TaskTimeController : ControllerBase {
        protected readonly GenericeRepository<Clients> _clientRepo;
        protected readonly GenericeRepository<Users> _userRepo;
        protected readonly GenericeRepository<Tasks> _taskRepo;
        protected readonly GenericeRepository<TaskTimes> _repo;
        public IMapper _mapper; 
        public TaskTimeController(DBBonsaiContext context, IMapper mapper) {
            _mapper = mapper;
            _clientRepo = new GenericeRepository<Clients>(context);
            _userRepo = new GenericeRepository<Users>(context);
            _taskRepo = new GenericeRepository<Tasks>(context);
            _repo = new GenericeRepository<TaskTimes>(context);
        }

        [HttpPost]
        public IActionResult CreateTaskTime([FromBody] TaskTimeDTO tt) {
            try {
                Users user = _userRepo.Find(x => x.Id == tt.UserId).FirstOrDefault();
                Clients client = _clientRepo.Find(x => x.Id == tt.ClientId).FirstOrDefault();
                Tasks task = _taskRepo.Find(x => x.TaskId == tt.TaskId).FirstOrDefault();
                DateTime startDate = DateTime.Now;
                Console.WriteLine(user.Email);
                Console.WriteLine(task.Description);
                Console.WriteLine(client.Email);
                DateTime currDate = DateTime.Now;
                Console.WriteLine(currDate);
                TaskTimes sad = new()
                {
                   TaskId = task.TaskId,
                   UserId = user.Id,
                   ClientId  = client.Id,
                   Tasks = task,
                   Users = user,
                   Clients = client,
                   StartDate = startDate,
                   CurrentDate = currDate,
                };
                if(object.ReferenceEquals(sad,null)) {
                    BadRequest("Object error");
                } else {
                    _repo.Add(sad); 
                    Ok(tt);
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
            return Ok("Something went wrong");
        }
        [HttpPut]
        [Route("/api/[controller]/taskComplete")]
        public IActionResult UpdateEndDate([FromBody] TaskCloseDto TaskInfo) {
            try{
                Tasks task = _taskRepo.Find(x => x.TaskId == TaskInfo.TaskId).FirstOrDefault<Tasks>();
                TaskTimes tt = _repo.Find(x => x.Id == TaskInfo.TaskTimeId).FirstOrDefault<TaskTimes>();
                tt.EndDate = DateTime.Now;
                _repo.Update(tt);
                return Ok(tt);
            } catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(e.Data);
            }
        }
        [HttpPut]
        [Route("/api/[controller]/timeUpdate")]
        public IActionResult UpdateCurrDate([FromBody] int TaskTimeId) {
            try{
                TaskTimes tt = _repo.Find(x => x.Id == TaskTimeId).FirstOrDefault<TaskTimes>();
                tt.CurrentDate = DateTime.Now;
                _repo.Update(tt);
                return Ok(tt);
            } catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(e.Data);
            }
        }
    }
}