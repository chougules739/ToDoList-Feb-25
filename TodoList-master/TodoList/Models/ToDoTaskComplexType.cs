using DataModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TodoList.Models
{
    public class ToDoTaskComplexType
    {
        public Project Project { get; set; }
        public List<TodoTaskModel> TodoTasks { get; set; }
        public long TodoTaskCount { get; set; }
        public IEnumerable<SelectListItem> TaskStatus { get; set; }
        public IEnumerable<SelectListItem> TaskPriorities { get; set; }
    }
}
