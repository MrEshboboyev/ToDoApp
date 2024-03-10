using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ToDo.Web.Models
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }

        private DateTime? _dueDate { get; set; }

        public DateTime? DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value?.ToUniversalTime(); }
        }

        public PriorityLevel Priority { get; set; }
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }
}

