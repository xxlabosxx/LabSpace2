using LabSpace2.Models;

namespace LabSpace2.ViewModel
{
    public class ItemListViewModel
    {
        public IEnumerable<Item> Itens { get; set; }
        public string CategoriaAtual {get; set;}
    }
}