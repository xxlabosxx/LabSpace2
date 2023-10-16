using LabSpace2.Models;

namespace LabSpace2.Repositories.Interface
{
    public interface ICategoriaRepository
    {
        public IEnumerable<Categoria> Categorias {get;}
    }
}