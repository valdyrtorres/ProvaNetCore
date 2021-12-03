using Microsoft.AspNetCore.Mvc;
using Prova.Application.DTOs;
using Prova.Application.Extensions;
using Prova.Application.Validadores;
using Prova.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prova.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        // Injeção de dependência
        private IContatoRepository _contatoRepository;
        public ContatosController(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContatoDTO>> GetContatos() 
        {
            var contatos = _contatoRepository.GetContatos();

            var resultado = contatos.Select(c => c.AsDto());

            return Ok(new
            {
                success = true,
                data = resultado
            });
            
        }

        [HttpGet("ativos")]
        public ActionResult<IEnumerable<ContatoDTO>> GetContatosAtivos()
        {
            var contatos = _contatoRepository.GetContatosAtivos();

            var resultado = contatos.Select(c => c.AsDto());

            return Ok(new
            {
                success = true,
                data = resultado
            });

        }

        [HttpGet("{id}")]
        public ActionResult<ContatoDTO> GetContatoById(int id)
        {
            var contato = _contatoRepository.GetContato(id);

            if (contato == null)
            {
                return NotFound();
            }

            var validacaoContato = HelperContato.ValidarDados(contato.AsDto());
            if (!validacaoContato.Valido)
            {
                var resultado = new ContatoDTO();
                resultado.MsgErro = "Erro ao consutar dados! " + validacaoContato.Erro;
                return BadRequest(resultado);
            }

            return Ok(new
            {
                success = true,
                data = contato.AsDto()
            });

        }

        [HttpPost]
        public ActionResult<ContatoDTO> CreateContato([FromBody] ContatoDTO contatoDTO)
        {
            var resultado = new ContatoDTO();

            contatoDTO.Idade = HelperContato.CalcularIdade(contatoDTO);
            var validacaoContato = HelperContato.ValidarDados(contatoDTO);

            if (!validacaoContato.Valido)
            {
                resultado.MsgErro = "Erro ao incluir dados! " + validacaoContato.Erro;
                return BadRequest(resultado);
            }
            else
            {
                try
                {
                    _contatoRepository.CreateContato(contatoDTO.AsContato());
                    resultado = contatoDTO;
                }
                catch (Exception ex)
                {
                    resultado.MsgErro = "Erro ao incluir dados! " + ex.Message;
                }
            }


            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContato(int id, [FromBody] ContatoDTO contatoDTO)
        {
            if (contatoDTO == null)
            {
                return BadRequest("Objeto Contato é nulo");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto de modelo inválido");
            }
            var dbcontato = _contatoRepository.GetContato(id);
            if(!dbcontato.Id.Equals(id))
            {
                return NotFound();
            }

            try
            {
                _contatoRepository.UpdateContato(contatoDTO.AsContatoUpdate());
                contatoDTO.Valido = true;
            }
            catch (Exception ex)
            {
                contatoDTO.Valido = false;
                contatoDTO.MsgErro = "Erro ao atualizar dados! " + ex.Message;
            }

            return Ok(contatoDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContato(int id)
        {
            var resultado = new ContatoDTO();

            try
            {
                var dbcontato = _contatoRepository.GetContato(id);

                if (dbcontato == null)
                {
                    resultado.Valido = false;
                    resultado.MsgErro = "Contato não encontrado!";
                    return BadRequest(resultado);
                }
                else
                {
                    _contatoRepository.DeleteContato(dbcontato);
                    resultado.Valido = true;
                }
            }
            catch (Exception ex)
            {
                resultado.Valido = false;
                resultado.MsgErro = "Erro ao excluir Contato! " + ex.Message;

            }

            return NoContent();

        }

        [HttpPatch("{id}")]
        public IActionResult DesativarContato(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto de modelo inválido");
            }
            var dbcontato = _contatoRepository.GetContato(id);
            if (!dbcontato.Id.Equals(id))
            {
                return NotFound();
            }

            ContatoDTO contatoDTO = dbcontato.AsDto();
            contatoDTO.IsAtivo = false;  

            try
            {
                _contatoRepository.UpdateContato(contatoDTO.AsContato());
                contatoDTO.Valido = true;
            }
            catch (Exception ex)
            {
                contatoDTO.Valido = false;
                contatoDTO.MsgErro = "Erro ao desativar o contato! " + ex.Message;
            }

            return Ok(contatoDTO);
        }

    }
}
