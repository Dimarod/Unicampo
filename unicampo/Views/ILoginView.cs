using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unicampo.Views
{
    public interface ILoginView
    {
        string CorreoUsuario { get; }
        void MostrarError(string mensaje);
        void RedirigirAInicio();
    }
}   
