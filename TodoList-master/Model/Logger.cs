using System;

namespace DataModels
{
    public class LoggerModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
