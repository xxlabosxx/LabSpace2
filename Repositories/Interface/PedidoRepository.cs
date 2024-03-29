using LabSpace2.Context;
using LabSpace2.Models;

namespace LabSpace2.Repositories.Interface
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        private readonly Carrinho _carrinho;
        public PedidoRepository(AppDbContext context, Carrinho carrinho)
        {
            _context = context;
            _carrinho = carrinho;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
            var carrinhoItens = _carrinho.CarrinhoItens;
            foreach (var carI in carrinhoItens)
            {
                var pm = new PedidoItem()
                {
                    Quantidade = carI.Quantidade,
                    ItemId = carI.Item.ItemId,
                    PedidoId = pedido.PedidoId,
                    Preco = Convert.ToDecimal(carI.Item.Preco)
                };
                _context.PedidoItens.Add(pm);
            }
            _context.SaveChanges();
        }
    }
}