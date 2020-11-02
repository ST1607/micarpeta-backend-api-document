using MiCarpeta.Document.Domain.Entities;
using System.Collections.Generic;

namespace MiCarpeta.Document.Repository
{
    public interface IDocumentosRepository : IRepository<Documentos>
    {
        Documentos ObtenerPorId(long idDocumento);

        List<Documentos> ListarDocumentosCiudadano(long idCiudadano);
    }
}
