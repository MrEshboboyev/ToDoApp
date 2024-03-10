using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ToDo.Services.TaskAPI.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }

        [Required(ErrorMessage = "Title field is required!")]
        [StringLength(100, ErrorMessage = "Title should be between {2} and {1} characters", MinimumLength = 3)]
        public string Title { get; set; }
        [StringLength(500, ErrorMessage = "Description should be less than {1} characters")]
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }


        private DateTime? _dueDate { get; set; }

        [DisplayName("Due Date")]
        [DataType(DataType.Date)]
        public DateTime? DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value?.ToUniversalTime(); }
        }

        [Display(Name = "Priority")]
        public PriorityLevel Priority { get; set; }
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }
}
