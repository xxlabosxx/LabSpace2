using LabSpace2.Context;
using LabSpace2.Models;
using LabSpace2.Repositories.Interface;
using SQLitePCL;

namespace LabSpace2.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        } 
        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}