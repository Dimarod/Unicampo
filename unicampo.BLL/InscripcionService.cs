using System;
using unicampo.DAL;

namespace unicampo.BLL
{
    public class InscripcionService
    {
        private readonly InscripcionRepository _repository;

        public InscripcionService()
        {
            _repository = new InscripcionRepository();
        }

        public void InscribirEstudiante(int actividadId, int usuarioId)
        {
            if (actividadId <= 0 || usuarioId <= 0)
                throw new Exception("Datos de inscripción inválidos.");

            _repository.RegistrarInscripcion(actividadId, usuarioId);
        }
    }
}
