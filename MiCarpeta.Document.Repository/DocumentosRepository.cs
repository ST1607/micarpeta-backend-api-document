using MiCarpeta.Document.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace MiCarpeta.Document.Repository
{
    public class DocumentosRepository: Repository<Documentos>, IDocumentosRepository
    {
        public DocumentosRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
