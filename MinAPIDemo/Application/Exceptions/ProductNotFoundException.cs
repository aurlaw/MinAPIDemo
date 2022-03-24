namespace MinAPIDemo.Application.Exceptions
{
    public class ProductNotFoundException : ApplicationException
    {
        public ProductNotFoundException(string title, string message) : base(title, message)
        {
        }
    }
}