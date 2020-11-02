using System;

namespace MiCarpeta.Document.Domain.Entities
{
    public class Documentos
    {
        public long Id { get; set; }
        public long IdCiudadano { get; set; }

        public string Base64 { get; set; }

        public string NombreArchivo { get; set; }

        public string URLS3 { get; set; }

        public DateTime FechaCarga { get; set; }

        public string Estado { get; set; }

        public DateTime FechaEstado { get; set; }
    }
}
