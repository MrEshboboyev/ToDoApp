using ToDo.Web.Models;

namespace ToDo.Web.Service.IService
{
    public interface ITaskService
    {
        Task<ResponseDto?> GetTasksByUserIdAsync(string userId);
        Task<ResponseDto?> CreateTaskAsync(TaskDto taskDto);
        Task<ResponseDto?> DeleteTaskAsync(int taskId);
        Task<ResponseDto?> UpdateTaskAsync(TaskDto taskDto);
    }
}
