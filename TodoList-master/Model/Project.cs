using System;

namespace DataModels
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }//1- Agile, 2- Normal Proj.
        public bool IsActive { get; set; }
    }
}
