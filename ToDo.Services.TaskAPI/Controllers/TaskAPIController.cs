using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Services.TaskAPI.Data;
using ToDo.Services.TaskAPI.Models.Dto;

namespace ToDo.Services.TaskAPI.Controllers
{
    [Route("api/task")]
    [ApiController]
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
    }
}
