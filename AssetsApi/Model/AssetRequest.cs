using System.ComponentModel.DataAnnotations;

namespace AssetsApi.Model
{
    public class AssetRequest
    {        
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public long CategoryId { get; set; }
    }
}
