using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreasionSesion { get; set; }
            public List<string > ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _context;
            public Manejador(CarritoContexto context)
            {
                this._context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                CarritoSesion carrito = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreasionSesion,
                   // ListaDetalle = (ICollection<CarritoSesionDetalle>)request.ProductoLista
                };

                this._context.CarritoSesion.Add(carrito);
                var resultado = await _context.SaveChangesAsync();
                if(resultado == 0)
                {
                    throw new Exception("Error al registrar carrito de compra");
                }

                int id = carrito.CarritoSesionId;
                foreach(string libro in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = libro
                    };
                    this._context.CarritoSesionDetalle.Add(detalleSesion);
                }
                resultado = await _context.SaveChangesAsync();
                if(resultado >= 1)
                {
                    return Unit.Value;
                }
                throw new Exception("Error al registrar carrito y los productos seleccionados");
            }
        }
    }
}
