using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicio.Api.Libro.Aplicacion;

namespace TiendaServicio.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMediator mediator;
        public LibroController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateNewBook(Nuevo.Ejecutar data)
        {
            return await this.mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroDto>>> GetAllBooks()
        {
            return await this.mediator.Send(new Consulta.Ejecutar());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDto>> GetBook(Guid id)
        {

            return await this.mediator.Send(new ConsultaFiltro.Ejecutar { LibreriaMaterialId = id });
        }
        

    }
}
