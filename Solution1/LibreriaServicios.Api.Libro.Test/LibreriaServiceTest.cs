using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TiendaServicio.Api.Libro.Aplicacion;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;
using Xunit;

namespace LibreriaServicios.Api.Libro.Test
{
    public class LibreriaServiceTest
    {

        private IEnumerable<LibreriaMaterial> GetLibrosDataPruebas()
        {
            A.Configure<LibreriaMaterial>().Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;
            return lista;
        }

        private Mock<ContextoLibreria> CreateContext()
        {
            var dataPrueba = GetLibrosDataPruebas().AsQueryable();
            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());
            
        }

        [Fact]
        public async void GetLibros()
        {
            var mockContexto = new Mock<ContextoLibreria>();
            var mockMapper = new Mock<IMapper>();

            //Instaciamos el manejador y pasamos como parametro los mocks

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mockMapper.Object);
            
        }
    }
}
