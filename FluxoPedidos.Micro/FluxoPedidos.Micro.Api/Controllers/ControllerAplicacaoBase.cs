using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Domain.Base;
using Microsoft.AspNetCore.Mvc;

namespace FluxoPedidos.Micro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControllerAplicacaoBase<Aplic> : ControllerBase where Aplic : IAplicBase
    {
        private readonly Aplic _aplic;

        public ControllerAplicacaoBase(Aplic aplic)
        {
            _aplic = aplic;
        }

        [HttpGet]
        public IActionResult Recuperar()
        {
            return Executar(() => _aplic.Recuperar());
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarPorId([FromRoute] int id)
        {
            return Executar(() => _aplic.RecuperarPorId(id));
        }

        protected IActionResult Executar(Func<ServiceResult> acao)
        {
            try
            {
                return RetornoBase(acao.Invoke());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Erro = "Ocorreu um erro inesperado.",
                    Detalhes = ex.Message
                });
            }
        }

        protected IActionResult RetornoBase(ServiceResult resultado, string mensagem = "") 
        {
            return resultado.Sucesso ? 
                            Ok(new {Result = resultado, Mensagem = mensagem}) : 
                            BadRequest(new {Erro = resultado.Erros, Mensagem = mensagem});
        }
    }
}
