using ApiTienda.Models;

namespace ApiTienda.Repository.IRepository
{
    public interface ICategoriaRepository
    {
        ICollection<Categoria> GetCategorias();

        Categoria GetCategoria(long cateogiraId);

        bool ExisteCategoria(long id);

        bool ExisteCategoria(string nombre);

        bool CrearCategoria(Categoria categoria);

        bool ActualizarCategoria(Categoria categoria);

        bool BorrarCategoria(Categoria categoria);

        bool Guardar();

    }
}
