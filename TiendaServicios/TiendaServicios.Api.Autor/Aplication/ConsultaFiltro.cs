using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Model;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplication
{
    public class ConsultaFiltro
    {
        public class ConsultaAutor : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<ConsultaAutor, AutorDto>
        {
            private readonly ContextoAutor _context;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<AutorDto> Handle(ConsultaAutor request, CancellationToken cancellationToken)
            {
                var autor = await this._context.AutorLibro.Where(autor => autor.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                if(autor == null)
                {
                    throw new Exception("Autor dont exists");
                }
                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);
                return autorDto;
            }
        }
    }
}
