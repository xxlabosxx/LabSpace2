using LabSpace2.Models;

namespace LabSpace2.ViewModel
{
    public class PedidoItensViewModel
    {
        public Pedido Pedidos { get; set; }
        public IEnumerable<PedidoItem> PedidoMoveis { get; set; }
    }
}