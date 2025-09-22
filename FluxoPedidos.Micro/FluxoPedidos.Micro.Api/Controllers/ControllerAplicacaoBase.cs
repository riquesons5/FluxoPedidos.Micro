using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Domain.Base;
using Microsoft.AspNetCore.Mvc;

namespace FluxoPedidos.Micro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControllerAplicacaoBase<T, TView> : ControllerBase where T : ModelBase 
                                                                    where TView : ViewBase 
    {
        protected IActionResult RetornoBase<TView>(ServiceResult<T> resultado, string mensagem = "") 
        {
            //é necessário alterar para consumir a View
            var resposta = resultado;

            return resultado.Sucesso ? Ok(new {Result = resposta, Mensagem = mensagem}) : BadRequest(new {Erro = resultado.Erros, Mensagem = mensagem});
        }
    }
}
