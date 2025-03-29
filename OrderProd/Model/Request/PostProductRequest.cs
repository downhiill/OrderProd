using System.ComponentModel.DataAnnotations;

namespace OrderProd.Model.Request
{
    public class PostProductRequest
    {
        [Required(ErrorMessage = "Название продукта обязательно.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Цена продукта обязательна.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена продукта должна быть больше нуля.")]
        public decimal Price { get; set; }
    }
}
