using System.ComponentModel.DataAnnotations;

namespace AssetsApi.Model
{
    public class Asset
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public long CategoryId { get; set; }
    }
}
