using FluxoPedidos.Micro.Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace FluxoPedidos.Micro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControllerAplicacaoBase<Aplic> : ControllerBase where Aplic : IAplicBase
    {
        protected readonly Aplic _aplic;

        public ControllerAplicacaoBase(Aplic aplic)
        {
            _aplic = aplic;
        }

        [HttpGet]
        public async Task<IActionResult> Recuperar()
        {
            return await Executar(async () => await _aplic.Recuperar());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorId([FromRoute] int id)
        {
            return await Executar(async () => await _aplic.RecuperarPorId(id));
        }

        protected async Task<IActionResult> Executar(Func<Task<ServiceResult>> acao)
        {
            try
            {
                return RetornoBase(await acao.Invoke());
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
