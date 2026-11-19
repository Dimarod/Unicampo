using System;
using unicampo.BLL;
using unicampo.entities;
using unicampo.Views;

namespace unicampo.Presenters
{
    public class ActividadesPresenter
    {

        private readonly IActividadesView _view;
        private readonly ActividadService _service;
        private readonly unicampo.BLL.InscripcionService _inscripcionService;
        private readonly unicampo.BLL.ResenaService _resenaService;

        public ActividadesPresenter(IActividadesView view)
        {
            _view = view;
            _service = new ActividadService();
            _inscripcionService = new unicampo.BLL.InscripcionService();
            _resenaService = new unicampo.BLL.ResenaService();
        }

        public void Cargar()
        {
            try
            {
                var actividades = _service.ListarActividades();
                _view.MostrarActividades(actividades);
            }
            catch (Exception)
            {
                
                _view.MostrarMensaje("No fue posible cargar las actividades. Intente nuevamente.");
            }
        }
        public void Guardar()
        {
            try
            {
                var actividad = new ActividadDto
                {
                    Titulo = _view.TituloActividad,
                    Descripcion = _view.DescripcionActividad,
                    FechaInicio = DateTime.Parse(_view.FechaInicioActividad),
                    Lugar = _view.LugarActividad,
                    Cupos = int.Parse(_view.CuposActividad)
                };

                if (!string.IsNullOrEmpty(_view.ActividadIdSeleccionada))
                {
                    actividad.ActividadId = int.Parse(_view.ActividadIdSeleccionada);
                    _service.ModificarActividad(actividad);
                    _view.MostrarMensaje("¡Actividad actualizada con éxito!");
                }
                else
                {
                    _service.RegistrarNuevaActividad(actividad);
                    _view.MostrarMensaje("¡Actividad registrada con éxito!");
                }

                _view.LimpiarFormulario();
                Cargar(); 
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje("Error al guardar: " + ex.Message);
            }
        }

        public void SeleccionarParaEditar(ActividadDto actividad)
        {
            _view.LlenarFormularioEdicion(actividad);
        }

        public void Cancelar(int actividadId)
        {
            try
            {
                _service.CancelarActividad(actividadId);
                _view.MostrarMensaje("Actividad cancelada y removida del listado.");
                Cargar(); 
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje("Error al cancelar: " + ex.Message);
            }
        }
        public void Inscribir(int actividadId, int usuarioId)
        {
            try
            {
                _inscripcionService.InscribirEstudiante(actividadId, usuarioId);
                _view.MostrarMensaje("¡Inscripción exitosa! Tu cupo ha sido reservado.");
                Cargar(); 
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje("No se pudo inscribir: " + ex.Message);
            }
        }
        public void PrepararResena(int actividadId)
        {
            var actividades = _service.ListarActividades();
            var actividad = actividades.Find(a => a.ActividadId == actividadId);
            if (actividad != null)
            {
                _view.ActividadIdSeleccionada = actividad.ActividadId.ToString();
                _view.MostrarPanelResena(actividad.Titulo);
            }
        }

        public void GuardarResena(int usuarioId)
        {
            try
            {
                var resena = new ResenaDto
                {
                    ActividadId = int.Parse(_view.ActividadIdSeleccionada),
                    UsuarioId = usuarioId,
                    Puntuacion = _view.CalificacionSeleccionada,
                    Comentario = _view.ComentarioResena
                };

                _resenaService.RegistrarResena(resena);
                _view.MostrarMensaje("¡Gracias! Tu reseña ha sido guardada.");
                _view.OcultarPanelResena();
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje("Error al guardar reseña: " + ex.Message);
            }
        }
    }
}