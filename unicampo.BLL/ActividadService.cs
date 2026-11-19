using System;
using System.Collections.Generic;
using unicampo.DAL;
using unicampo.entities;

namespace unicampo.BLL
{
    public class ActividadService
    {
        private readonly ActividadRepository _repository;

        public ActividadService()
        {
            _repository = new ActividadRepository();
        }

        public List<ActividadDto> ListarActividades()
        {
            return _repository.ObtenerPublicadas();
        }

        public bool PuedeSolicitarInscripcion(ActividadDto actividad)
        {
            if (actividad == null) return false;
            if (actividad.Estado != "Publicada") return false;
            if (actividad.FechaInicio <= DateTime.Now) return false;
            if (actividad.Cupos <= 0) return false;
            return true;
        }
        public void RegistrarNuevaActividad(ActividadDto actividad)
        {
            
            if (string.IsNullOrWhiteSpace(actividad.Titulo))
                throw new Exception("El título es obligatorio.");

            if (actividad.Cupos < 0)
                throw new Exception("Los cupos no pueden ser negativos.");

            if (actividad.FechaInicio < DateTime.Now)
                throw new Exception("La fecha de inicio debe ser en el futuro.");

            _repository.GuardarActividad(actividad);
        }
        public void ModificarActividad(ActividadDto actividad)
        {
            if (actividad.ActividadId <= 0)
                throw new Exception("ID de actividad inválido.");

            if (string.IsNullOrWhiteSpace(actividad.Titulo))
                throw new Exception("El título es obligatorio.");

            if (actividad.Cupos < 0)
                throw new Exception("Los cupos no pueden ser negativos.");

            _repository.ActualizarActividad(actividad);
        }

        public void CancelarActividad(int actividadId)
        {
            if (actividadId <= 0)
                throw new Exception("ID de actividad inválido.");

            _repository.CancelarActividad(actividadId);
        }
    }


}
