using System.IO;

namespace MiCarpeta.Document.Common
{
    public interface IS3Client
    {
        string UploadStringFile(Stream cuerpoArchivo, string filename, int permission);
    }
}
