using System.Collections.Generic;
using unicampo.entities;

namespace unicampo.Views
{
    public interface IActividadesView
    {
        void MostrarActividades(IList<ActividadDto> actividades);
        void MostrarMensaje(string mensaje);
        string TituloActividad { get; }
        string DescripcionActividad { get; }
        string FechaInicioActividad { get; }
        string LugarActividad { get; }
        string CuposActividad { get; }
        void LimpiarFormulario();
        string ActividadIdSeleccionada { get; set; }
        void LlenarFormularioEdicion(ActividadDto actividad);
        int CalificacionSeleccionada { get; }
        string ComentarioResena { get; }

        void MostrarPanelResena(string tituloActividad);
        void OcultarPanelResena();
    }

}