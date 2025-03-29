namespace OrderProd.Model.Request
{
    public class PutProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public decimal Price { get; set; }
    }
}
