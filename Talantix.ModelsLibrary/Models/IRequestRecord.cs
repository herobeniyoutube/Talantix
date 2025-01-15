namespace Talantix.ModelsLibrary.Models
{
    public interface IRequestRecord
    {
        public int Id { get; set; }
        public int TaskId {  get; set; }
        public string IP {  get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime RequestPath { get; set; }
    }
}
