using LabSpace2.Models;

namespace LabSpace2.Repositories.Interface
{
    public interface IItemRepository
    {
        IEnumerable<Item> Itens {get;}
        IEnumerable<Item> ItensEmDestaque {get;}
        Item  GetItemById(int itemId);
    }
}