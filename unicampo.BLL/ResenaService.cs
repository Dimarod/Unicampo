using System;
using unicampo.DAL;
using unicampo.entities;

namespace unicampo.BLL
{
    public class ResenaService
    {
        private readonly ResenaRepository _repository;

        public ResenaService()
        {
            _repository = new ResenaRepository();
        }

        public void RegistrarResena(ResenaDto resena)
        {
            if (resena.Puntuacion < 1 || resena.Puntuacion > 5)
                throw new Exception("La calificación debe estar entre 1 y 5 estrellas.");

            _repository.GuardarResena(resena);
        }
        public System.Collections.Generic.List<ResenaDto> ListarResenas(int actividadId)
        {
            if (actividadId <= 0) return new System.Collections.Generic.List<ResenaDto>();
            return _repository.ObtenerResenasPorActividad(actividadId);
        }
    }

}