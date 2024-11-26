using System.ComponentModel.DataAnnotations;

namespace AssetsApi.Model
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
