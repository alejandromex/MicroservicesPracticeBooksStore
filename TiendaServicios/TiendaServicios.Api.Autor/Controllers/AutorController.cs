using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Aplication;
using TiendaServicios.Api.Autor.Model;

namespace TiendaServicios.Api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator mediator;

        public AutorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> AddNewAutor(Nuevo.Ejecutar data)
        {
            return await this.mediator.Send(data);
        }

        [HttpGet("Autores")]
        public async Task<ActionResult<List<AutorDto>>> GetAutorList()
        {
            return await this.mediator.Send(new Consulta.ListaAutor());
        }

        [HttpGet("Autores/{id}")]
        public async Task<ActionResult<AutorDto>> GetAutor(string id)
        {
            return await this.mediator.Send(new ConsultaFiltro.ConsultaAutor { AutorGuid = id });
        }


    }
}
