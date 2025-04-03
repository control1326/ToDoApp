using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDo.ViewModels
{
    public class ToDoListVM
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int? UserID { get; set; }

        public SelectList UserList { get; set; }

        public bool IsComplete { get; set; }

    }
}
