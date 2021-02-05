using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContexto _contexto;
            private readonly ILibrosService _libroService;
            public Manejador(CarritoContexto contexto, ILibrosService libroService)
            {
                this._contexto = contexto;
                this._libroService = libroService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _contexto.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await _contexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDto>();
                
                foreach(var libro in carritoSesionDetalle)
                {
                    var response = await this._libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if(response.resultado)
                    {
                        var objectoLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDto
                        {
                            TituloLibro = objectoLibro.Titulo,
                            FechaPublicacion = objectoLibro.FechaPublicacion,
                            LibroId = objectoLibro.LibreriaMaterialId

                        };
                        listaCarritoDto.Add(carritoDetalle);
                    }
                }
                var carrito = new CarritoDto        
                {        
                    CarritoDetalleDto = listaCarritoDto,
                    CarritoId = carritoSesion.CarritoSesionId,           
                    FechaCreacionSesion = carritoSesion.FechaCreacion            
                };

                return carrito;
            }
        }
    }
}
