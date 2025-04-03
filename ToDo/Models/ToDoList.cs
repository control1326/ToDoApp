namespace ToDo.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int? UserID { get; set; }

        public bool IsComplete { get; set; }
    }
}
