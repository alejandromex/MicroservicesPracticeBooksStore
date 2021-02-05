using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;

namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecutar : IRequest<List<LibroDto>>
        {

        }

        public class Manejador : IRequestHandler<Ejecutar, List<LibroDto>>
        {
            private readonly ContextoLibreria _context;
            private readonly IMapper mapper;
            public Manejador(ContextoLibreria context, IMapper mapper)
            {
                this._context = context;
                this.mapper = mapper;
            }

            public async Task<List<LibroDto>> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                List<LibreriaMaterial> libros = await _context.LibreriaMaterial.ToListAsync();
                List<LibroDto> librosDto = this.mapper.Map<List<LibreriaMaterial>, List<LibroDto>>(libros);
                return librosDto;
            }
        }
    }
}
