using AutoMapper;
using FluentValidation;
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
    public class ConsultaFiltro
    {
        public class Ejecutar : IRequest<LibroDto>
        {
            public Guid LibreriaMaterialId;
        }
        public class Validador : AbstractValidator<Ejecutar>
        {
            public Validador()
            {
                RuleFor(x => x.LibreriaMaterialId).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecutar, LibroDto>
        {
            private readonly IMapper mapper;
            private readonly ContextoLibreria _contexto;
            public Manejador(IMapper mapper, ContextoLibreria context)
            {
                this._contexto = context;
                this.mapper = mapper;
            }
            public async Task<LibroDto> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                LibreriaMaterial libro = await _contexto.LibreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibreriaMaterialId).FirstOrDefaultAsync();
                if(libro == null)
                {
                    throw new Exception("Libro no encontrado");
                }
                LibroDto libroDto = mapper.Map<LibreriaMaterial, LibroDto>(libro);
                return libroDto;
            }
        }
    }


}
