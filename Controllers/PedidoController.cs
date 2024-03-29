using LabSpace2.Context;
using LabSpace2.Models;
using LabSpace2.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace LabSpace2.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoR;
        private readonly Carrinho _carrinho;
        private readonly AppDbContext _context;
        public PedidoController(IPedidoRepository pedidoR, Carrinho carrinho, AppDbContext context)
        {
            _pedidoR = pedidoR;
            _carrinho = carrinho;
            _context = context;
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;
            //obtem os itens do carrinho de compra do cliente
            List<CarrinhoItem> items = _carrinho.GetCarrinhoCompraItems();
            _carrinho.CarrinhoItens = items;
            //verifica se existem itens de pedido
            if (_carrinho.CarrinhoItens.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um lanche...");
            }
            //calcula o total de itens e o total do pedido
            foreach (var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (Convert.ToDecimal(item.Item.Preco) * item.Quantidade);
            }
            //atribui os valores obtidos ao pedido
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;
            //valida os dados do pedido
            if (ModelState.IsValid)
            {
                //cria o pedido e os detalhes
                _pedidoR.CriarPedido(pedido);
                //define mensagens ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                ViewBag.TotalPedido = _carrinho.GetCarrinhoCompraTotal();
                //limpa o carrinho do cliente
                _carrinho.LimparCarrinho();
                ViewBag.UrlWhatsApp = EnviarMensagem(pedido.PedidoId);
                //exibe a view com dados do cliente e do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }

            return View(pedido);
        }
        public string EnviarMensagem(int pedidoId)
        {
            var pedido = _context.Pedidos.Include(p =>
        p.PedidoItens).ThenInclude(p => p.Item).FirstOrDefault(p => p.PedidoId == pedidoId);

            string numeroDestinatario = "+55991514879";

string mensagem = "Pedido: " + pedido.PedidoId.ToString() + " Cliente: "+pedido.Nome.ToString();

// Formate o número removendo caracteres não numéricos
string numeroFormatado = new string(numeroDestinatario.Where(char.IsDigit).ToArray());

            // Construa o link do WhatsApp
            string urlWhatsApp = $"https://web.whatsapp.com/send?phone=+{numeroFormatado}&text={Uri.EscapeDataString(mensagem)}";

            Console.WriteLine(urlWhatsApp);
            // Retorne a View que contém o script JavaScript
            return urlWhatsApp;
        }
    }
}