
namespace ToDoApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsHidden { get; set; } = false;

        internal static object FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
