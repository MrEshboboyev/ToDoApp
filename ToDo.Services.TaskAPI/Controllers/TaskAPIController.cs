using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Services.TaskAPI.Data;
using ToDo.Services.TaskAPI.Models;
using ToDo.Services.TaskAPI.Models.Dto;
using Task = ToDo.Services.TaskAPI.Models.Task;

namespace ToDo.Services.TaskAPI.Controllers
{
    [Route("api/task")]
    [ApiController]
    [Authorize]
    public class TaskAPIController : ControllerBase
    {
        private ResponseDto _response;
        private IMapper _mapper;
        private readonly AppDbContext _db;

        public TaskAPIController(AppDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet("GetTasks/{userId}")]
        public ResponseDto GetTasks(string userId)
        {
            try
            {
                IEnumerable<TaskDto> tasks = _mapper.Map<IEnumerable<TaskDto>>(_db.Tasks.Where(t => t.UserId == userId));

                _response.Result = tasks;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // create Task
        [HttpPost]
        public ResponseDto Post([FromBody] TaskDto taskDto)
        {
            try
            {
                Task obj = _mapper.Map<Task>(taskDto);
                _db.Tasks.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<TaskDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }
        
        // delete Task
        [HttpDelete]
        public ResponseDto Delete(int taskId)
        {
            try
            {
                Task obj = _db.Tasks.First(t => t.Id == taskId);
                _db.Tasks.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }
        
        // update Task
        [HttpPut]
        public ResponseDto Put([FromBody] TaskDto taskDto)
        {
            try
            {
                Task obj = _mapper.Map<Task>(taskDto);
                _db.Tasks.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<TaskDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }
    }
}
