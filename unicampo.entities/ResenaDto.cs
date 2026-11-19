using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unicampo.entities
{
    public class ResenaDto
    {
        public int ActividadId { get; set; }
        public int UsuarioId { get; set; }
        public int Puntuacion { get; set; }
        public string Comentario { get; set; }
        public string NombreEstudiante { get; set; }
    }
}
