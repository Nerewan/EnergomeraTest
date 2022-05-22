using System.ComponentModel.DataAnnotations;

namespace Task3.Model.DTOs.Requests.Item
{
    public class CreateItemRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
