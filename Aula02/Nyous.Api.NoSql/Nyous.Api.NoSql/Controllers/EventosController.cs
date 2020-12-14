using Microsoft.AspNetCore.Mvc;
using Nyous.Api.NoSql.Domains;
using Nyous.Api.NoSql.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Nyous.Api.NoSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;

        public EventosController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        [HttpPost]
        public ActionResult<EventoDomain> Post(EventoDomain evento)
        {
            try
            {
                _eventoRepository.Adicionar(evento);
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<EventoDomain>> Get()
        {
            try
            {
                var eventos = _eventoRepository.Listar();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                _eventoRepository.Remover(id);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

        [HttpGet("{id}")]
        public ActionResult<List<EventoDomain>> GetById(string id)
        {
            try
            {
                var evento = _eventoRepository.BuscarPorId(id);

                return Ok(evento); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPut]
        public ActionResult Put(string id, EventoDomain evento) 
        {
            try
            {
                _eventoRepository.Atualizar(id, evento);

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}



    