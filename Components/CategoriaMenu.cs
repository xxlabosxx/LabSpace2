using LabSpace2.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LabSpace2.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaMenu(ICategoriaRepository categoriaRespository)
        {
            _categoriaRepository = categoriaRespository;
        }
        public IViewComponentResult Invoke()
        {
            var categoria = _categoriaRepository.Categorias.OrderBy(c => c.Nome);

            return View(categoria);
        }
    }
}