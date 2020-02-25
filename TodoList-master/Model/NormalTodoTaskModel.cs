using System;

namespace DataModels
{
    public class NormalTodoTaskModel : TodoTaskModel
    {
        public int Priority { get; set; }
        public DateTime EstimatedCompletionDate { get; set; }
        public string CreatedDateString { get; set; }
    }
}
