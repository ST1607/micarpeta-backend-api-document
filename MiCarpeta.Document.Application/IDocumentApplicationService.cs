using MiCarpeta.Document.Common;

namespace MiCarpeta.Document.Application
{
    public interface IDocumentApplicationService
    {
        ResponseViewModel SubirArchivo(string archivoBase64, long idUsuario, string nombreArchivo);
    }
}
