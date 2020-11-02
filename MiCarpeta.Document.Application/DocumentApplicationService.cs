using AutoMapper;
using MiCarpeta.Document.Common;
using MiCarpeta.Document.Domain;
using MiCarpeta.Document.Domain.Entities;

namespace MiCarpeta.Document.Application
{
    public class DocumentApplicationService: IDocumentApplicationService
    {
        private readonly IDocumentoDomainService DocumentoDomainService;
        private readonly IMapper Mapper;

        public DocumentApplicationService(IDocumentoDomainService documentoDomainService, IMapper mapper)
        {
            DocumentoDomainService = documentoDomainService;
            Mapper = mapper;
        }

        public ResponseViewModel SubirArchivo(string archivoBase64, long idUsuario, string nombreArchivo)
        {
            Response response = DocumentoDomainService.SubirArchivo(archivoBase64, idUsuario, nombreArchivo);

            return Mapper.Map<ResponseViewModel>(response);
        }

        public ResponseViewModel ValidarDocumento(long idDocumento)
        {
            Response response = new Response();

            return Mapper.Map<ResponseViewModel>(response);
        }

        public ResponseViewModel ListarDocumentosPorCiudadano(long idCiudadano)
        {
            Response response = DocumentoDomainService.ListarDocumentosPorCiudadano(idCiudadano);

            return Mapper.Map<ResponseViewModel>(response);
        }
    }
}
