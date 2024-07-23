using ApiTienda.Data;
using ApiTienda.Models;
using ApiTienda.Repository.IRepository;
using System.Reflection.Metadata.Ecma335;

namespace ApiTienda.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {

        //Inyección de dependencias
        public readonly ApplicationDBContext _bd;

        public CategoriaRepository(ApplicationDBContext bd)
        {
            _bd = bd;
        }


        //Implementación de los métodos de la categoría
        public bool ActualizarCategoria(Categoria categoria)
        {
            var categoriaExistente = _bd.Categoria.Find(categoria.Id);
            if (categoriaExistente != null)
            {
                _bd.Entry(categoriaExistente).CurrentValues.SetValues(categoria);
            }
            else
            {
                _bd.Categoria.Update(categoria);
            }
  
            return Guardar();
        }

        public bool BorrarCategoria(Categoria categoria)
        {
            _bd.Categoria.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(Categoria categoria)
        {
            _bd.Categoria.Add(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(long id)
        {
            if (id == 0) return false;

            return _bd.Categoria.Any(c => c.Id == id);
        }

        public bool ExisteCategoria(string nombre)
        {
            if(nombre == null ) return false;

            bool valor = _bd.Categoria.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public Categoria GetCategoria(long cateogiraId)
        {
            return _bd.Categoria.FirstOrDefault(c => c.Id == cateogiraId);
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _bd.Categoria.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false ;
        }
    }
}
