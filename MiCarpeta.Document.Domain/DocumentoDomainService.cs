using MiCarpeta.Document.Common;
using MiCarpeta.Document.Domain.Entities;
using MiCarpeta.Document.Repository;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;

namespace MiCarpeta.Document.Domain
{
    public class DocumentoDomainService : IDocumentoDomainService
    {
        private readonly IConfiguration Configuration;
        private readonly IS3Client S3Client;
        private readonly IDocumentosRepository DocumentosRepository;
        private readonly ICiudadanoRepository CiudadanoRepository;

        public DocumentoDomainService(IConfiguration configuration, IS3Client s3Client, IDocumentosRepository documentosRepository, ICiudadanoRepository ciudadanoRepository)
        {
            Configuration = configuration;
            S3Client = s3Client;
            DocumentosRepository = documentosRepository;
            CiudadanoRepository = ciudadanoRepository;
        }

        public Response SubirArchivo(string archivoBase64, long idUsuario, string nombreArchivo)
        {
            string _nombreArchivo = $"{idUsuario}-{nombreArchivo}";
            byte[] bytes = Convert.FromBase64String(archivoBase64);

            Stream ms = new MemoryStream(bytes);
            string ruta = S3Client.UploadStringFile(ms, _nombreArchivo, (int)Enumerators.StoragePermissions.PublicWrite);

            if (string.IsNullOrEmpty(ruta))
            {
                return new Response
                {
                    Estado = 201,
                    Errores = new List<string>() { "Se ha presentado un error al intentar cargar el archivo al S3." }
                };
            }

            Documentos documento = new Documentos
            {
                Id = DateTime.UtcNow.Ticks,
                IdCiudadano = idUsuario,
                Estado = "Temporal",
                FechaCarga = DateTime.UtcNow,
                FechaEstado = DateTime.UtcNow,
                URLS3 = ruta,
                NombreArchivo = nombreArchivo
            };

            DocumentosRepository.Add(documento);

            return new Response
            {
                Estado = 200,
                Mensaje = "El archivo ha sido cargado exitosamente."
            };
        }

        public Response ValidarDocumento(long idDocumento)
        {
            Documentos documento = DocumentosRepository.ObtenerPorId(idDocumento);

            if (documento != null)
            {
                Ciudadano ciudadano = CiudadanoRepository.ObtenerPorId(documento.IdCiudadano);

                IRestResponse respuesta = ValidarDocumentoCentralizador(ciudadano.Identificacion, documento.URLS3, documento.NombreArchivo);

                if (respuesta.StatusCode.Equals(HttpStatusCode.OK))
                {
                    return new Response()
                    {
                        Estado = 200,
                        Mensaje = respuesta.Content
                    };
                }
                else
                {
                    return new Response()
                    {
                        Estado = 400,
                        Errores = new List<string>()
                        {
                            respuesta.Content
                        }
                    };
                }

            }
            else
            {
                return new Response
                {
                    Estado = 201,
                    Errores = new List<string>()
                    {
                        "El documento no se encuentra registrado."
                    }
                };
            }
        }

        public Response ListarDocumentosPorCiudadano(long idCiudadano)
        {
            List<Documentos> documentos = DocumentosRepository.ListarDocumentosCiudadano(idCiudadano);

            if (documentos.Count > 0)
            {
                return new Response
                {
                    Estado = 200,
                    Data = documentos
                };
            }

            return new Response
            {
                Estado = 200,
                Mensaje = "El ciudadano no tiene documentos guardados actualmente."
            };
        }

        private IRestResponse ValidarDocumentoCentralizador(string identificacion, string urlS3, string nombreArchivo)
        {
            string url = HttpUtility.UrlEncode(urlS3);

            var client = new RestClient($"{Configuration["MiCarpeta:URL"]}apis/authenticateDocument/{identificacion}/{url}/{nombreArchivo}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
