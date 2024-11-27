using System.ComponentModel.DataAnnotations;

namespace AssetsApi.Model
{
    public class CategoryRequest
    {        
        [Required]
        public string Name { get; set; }
    }
}
