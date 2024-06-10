
namespace IMPAR_API.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int? PhotoId { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public Photo? Photo { get; set; }
    }
}
