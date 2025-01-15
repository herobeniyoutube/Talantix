namespace Talantix.ModelsLibrary.Models
{
    public interface ITodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsComplete { get; set; }
    }
}
