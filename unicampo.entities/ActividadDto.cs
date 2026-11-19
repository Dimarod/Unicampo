namespace unicampo.entities
{
    public class ActividadDto
    {
        public int ActividadId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public string Lugar { get; set; }
        public int Cupos { get; set; }
        public string Estado { get; set; }
    }
}