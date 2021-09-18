namespace MinAPIDemo.Models
{
    public record Product
    {
        public Guid Id {get;init;} = Guid.NewGuid();
        public string Name {get;init;} = default!;
        public int Quantity {get;init;}
        public decimal Price {get;init;}
    }
}