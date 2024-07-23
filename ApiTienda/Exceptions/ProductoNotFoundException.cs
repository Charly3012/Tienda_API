namespace ApiTienda.Exceptions
{
    public class ProductoNotFoundException : Exception
    {
        public ProductoNotFoundException(string message) : base(message)
        { 
        }
    }
}
