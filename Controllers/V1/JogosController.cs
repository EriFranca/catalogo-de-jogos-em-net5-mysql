using catalogoJogosAPI.Services;
using catalogoJogosAPI.Exceptions;
using System.ComponentModel.DataAnnotations;
using catalogoJogosAPI.ViewModel;
using catalogoJogosAPI.InputModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Runtime.Versioning;
using System.Collections.Generic;


namespace catalogoJogosAPI.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoServices _JogoServices;

        public JogosController(IJogoServices iJogosServices)
        {
            this._JogoServices = iJogosServices;
        }

        [HttpGet]   // [FromQuery] : backend data comes from a DB query
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var l_jogos = await _JogoServices.Obter(1, 5);

            if (l_jogos.Count() == 0)
                return NoContent();

            return Ok(l_jogos);
        }

        [HttpGet("{idJogo:guid}")]  // [FromQuery] : backend data comes from the route string
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            try
            {
                var jogo = await _JogoServices.Obter(idJogo);
                return Ok();
            }
            catch (JogoNaoCadastradoException e)
            {
                return UnprocessableEntity(e.ToString());
            }
        }

        [HttpPost]  // [FromBody] : data goes from front-end to the back-end in POST request
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = _JogoServices.Inserir(jogoInputModel);
                return Ok(jogo);
            }
            catch (JogoJaCadastradoException e)
            {
                return UnprocessableEntity(e.ToString());
            }
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, JogoInputModel jogoInputModel)
        {
            try
            {
                await _JogoServices.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            catch (JogoNaoCadastradoException e)
            {
                return UnprocessableEntity(e.ToString());
            }
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _JogoServices.Atualizar(idJogo, preco);
                return Ok();
            }
            catch (JogoNaoCadastradoException e)
            {
                return UnprocessableEntity(e.ToString());
            }
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _JogoServices.Remover(idJogo);
                return Ok();
            }
            catch (JogoNaoCadastradoException e)
            {
                return UnprocessableEntity(e.ToString());
            }
            
        }

    }
}