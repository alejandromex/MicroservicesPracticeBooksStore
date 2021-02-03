using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Model;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplication
{
    public class Nuevo
    {

        public class Ejecutar : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
            

        }

        public class Validador : AbstractValidator<Ejecutar>
        {
            public Validador()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();

            }
        }

        public class Manejador : IRequestHandler<Ejecutar>
        {

            private readonly ContextoAutor _context;

            public Manejador(ContextoAutor context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Guid.NewGuid().ToString()
                };
                this._context.Add(autorLibro);
                var AutorWasInsert = await _context.SaveChangesAsync();
                if(AutorWasInsert > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Error trying to save the autor");
            }
        }


    }
}
