using MiCarpeta.Document.Common;
using MiCarpeta.Document.Domain.Entities;
using MiCarpeta.Document.Repository.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace MiCarpeta.Document.Repository
{
    public class DocumentosRepository: Repository<Documentos>, IDocumentosRepository
    {
        public DocumentosRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Documentos ObtenerPorId(long idDocumento)
        {
            List<FilterQuery> filter = new List<FilterQuery>()
            {
                new FilterQuery {
                    AtributeName = "Id",
                    Operator = (int)Enumerators.QueryScanOperator.Equal,
                    ValueAtribute = idDocumento
                }
            };

            Documentos documento = GetByList(filter).FirstOrDefault();

            return documento;
        }

        public List<Documentos> ListarDocumentosCiudadano(long idCiudadano)
        {
            List<FilterQuery> filter = new List<FilterQuery>()
            {
                new FilterQuery {
                    AtributeName = "IdCiudadano",
                    Operator = (int)Enumerators.QueryScanOperator.Equal,
                    ValueAtribute = idCiudadano
                }
            };

            List<Documentos> documentos = GetByList(filter);

            return documentos;
        }
    }
}
