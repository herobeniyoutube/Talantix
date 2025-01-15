namespace Talantix.ModelsLibrary.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsComplete { get; set; }
    }
}
