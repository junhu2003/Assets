using System.ComponentModel.DataAnnotations;

namespace AssetsApi.Model
{
    public class Asset
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
