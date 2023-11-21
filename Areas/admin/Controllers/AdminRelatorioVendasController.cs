using LabSpace2.Areas.admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabSpace2.Areas.admin.Controllers
{
    public class AdminRelatorioVendasController : Controller
    {
        private readonly RelatorioVendasServices _relatorio;
        public AdminRelatorioVendasController(RelatorioVendasServices relatorio)
        {
            _relatorio = relatorio;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Relatorio(DateTime? di, DateTime? df)
        {
            if (!di.HasValue)
            {
                di = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (df.HasValue)
            {
                df = DateTime.Now;
            }
            ViewData["di"] = di.Value.ToString("yyyy-mm-dd");
            ViewData["df"] = df.Value.ToString("yyyy-mm-dd");
            var result = await _relatorio.BuscaPorData(di, df);
            return View(result);
        }
    }
}