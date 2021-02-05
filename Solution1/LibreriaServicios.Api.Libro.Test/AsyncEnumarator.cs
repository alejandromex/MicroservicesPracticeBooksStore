using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.Test
{
    public class AsyncEnumarator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> enumarator;
        public T Current => enumarator.Current;

        public AsyncEnumarator(IEnumerator<T> enumerator) => this.enumarator = enumarator ?? throw new ArgumentNullException();

                  


        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return await Task.FromResult(enumarator.MoveNext());
        }
    }
}
