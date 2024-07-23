namespace ApiTienda.Exceptions
{
    public class ProductoOutStockException : Exception
    {
        public ProductoOutStockException(string message) : base(message)
        { }
    }
}
