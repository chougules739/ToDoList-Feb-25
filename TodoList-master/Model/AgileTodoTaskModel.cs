namespace DataModels
{
    public class AgileTodoTaskModel : TodoTaskModel
    {
        public int Efforts { get; set; }
        public int StoryPoints { get; set; }
        public float BurnedHours { get; set; }
    }
}
