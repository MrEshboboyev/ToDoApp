using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using ToDo.Web.Models;
using ToDo.Web.Service.IService;

namespace ToDo.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
            
        }

        [Authorize]
        public async Task<IActionResult> TaskIndex()
        {
            return View(await LoadTasksBasedOnLoggedInUser());
        }

		#region Create Task
		public async Task<IActionResult> TaskCreate()
        {
            ViewData["PriorityLevels"] = GetPriorityLevels();

			return View();
        }

        [HttpPost]
        public async Task<IActionResult> TaskCreate(TaskDto model)
        {
            if(ModelState.IsValid)
            {
                model.UserId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value;

                ResponseDto? response = await _taskService.CreateTaskAsync(model);

                if(response != null && response.IsSuccess)
                {
                    TempData["success"] = "Task Created Successfully!";
                    return RedirectToAction(nameof(TaskIndex));
                }
                else
                {
					TempData["error"] = response?.Message;
				}
			}

			return View(model);
        }
        #endregion

        #region Delete Task
		public async Task<IActionResult> TaskDelete(int id)
        {
            ResponseDto? response = await _taskService.GetTaskAsync(id);
            if(response != null && response.IsSuccess)
            {
                TaskDto obj = JsonConvert.DeserializeObject<TaskDto>(Convert.ToString(response.Result));
                return View(obj);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> TaskDelete(TaskDto taskDto)
        {
			ResponseDto? response = await _taskService.DeleteTaskAsync(taskDto.Id);
			if (response != null && response.IsSuccess)
			{
				TempData["success"] = "Task deleted successfully";
				return RedirectToAction(nameof(TaskIndex));
			}
			else
			{
				TempData["error"] = response?.Message;
			}

			return View(taskDto);
		}
		#endregion

		#region Update Task
		public async Task<IActionResult> TaskUpdate(int id)
        {
            ResponseDto? response = await _taskService.GetTaskAsync(id);
            if(response != null && response.IsSuccess)
            {
                ViewData["PriorityLevels"] = GetPriorityLevels();

                TaskDto obj = JsonConvert.DeserializeObject<TaskDto>(Convert.ToString(response.Result));
                return View(obj);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> TaskUpdate(TaskDto taskDto)
        {
            taskDto.UserId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value;

            ResponseDto? response = await _taskService.UpdateTaskAsync(taskDto);
			if (response != null && response.IsSuccess)
			{
				TempData["success"] = "Task updated successfully";
				return RedirectToAction(nameof(TaskIndex));
			}
			else
			{
				TempData["error"] = response?.Message;
			}
            ViewData["PriorityLevels"] = GetPriorityLevels();

            return View(response);
		}
        #endregion


		#region Private Actions

        // private methods
        private async Task<List<TaskDto>> LoadTasksBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value;
            ResponseDto? response = await _taskService.GetTasksByUserIdAsync(userId);
            if(response != null && response.IsSuccess)
            {
                List<TaskDto> taskDtos = JsonConvert.DeserializeObject<List<TaskDto>>(Convert.ToString(response.Result));
                return taskDtos;
            }

            return new List<TaskDto>();
        }
		private List<SelectListItem> GetPriorityLevels()
		{
			var priorityLevels = Enum.GetValues(typeof(PriorityLevel))
									 .Cast<PriorityLevel>()
									 .Select(level => new SelectListItem
									 {
										 Value = level.ToString(),
										 Text = level.ToString()
									 })
									 .ToList();

			return priorityLevels;
		}

		#endregion
	}
}
