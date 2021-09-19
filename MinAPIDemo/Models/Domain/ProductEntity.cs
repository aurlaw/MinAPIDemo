namespace MinAPIDemo.Models.Domain
{
    public class ProductEntity
    {
        public string Id {get;set;} = default!;
        public string Name {get;set;} = default!;
        public int Quantity {get;set;}
        public decimal Price {get;set;}
        
    }
}