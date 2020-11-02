using MiCarpeta.Document.Common;
using MiCarpeta.Document.Domain.Entities;
using MiCarpeta.Document.Repository;
using System;
using System.Collections.Generic;
using System.IO;

namespace MiCarpeta.Document.Domain
{
    public class DocumentoDomainService: IDocumentoDomainService
    {
        private readonly IS3Client S3Client;
        private readonly IDocumentosRepository DocumentosRepository;

        public DocumentoDomainService(IS3Client s3Client, IDocumentosRepository documentosRepository)
        {
            S3Client = s3Client;
            DocumentosRepository = documentosRepository;
        }

        public Response SubirArchivo(string archivoBase64, long idUsuario, string nombreArchivo)
        {
            string _nombreArchivo = $"{idUsuario}-{nombreArchivo}";
            byte[] bytes = Convert.FromBase64String(archivoBase64);

            Stream ms = new MemoryStream(bytes);
            string ruta = S3Client.UploadStringFile(ms, _nombreArchivo, (int)Enumerators.StoragePermissions.PublicWrite);

            if (string.IsNullOrEmpty(ruta))
            {
                return new Response { 
                    Estado = 201,
                    Errores = new List<string>() { "Se ha presentado un error al intentar cargar el archivo al S3." }
                };
            }

            Documentos documento = new Documentos
            {
                IdCiudadano = idUsuario, 
                Estado = "Temporal",
                FechaCarga = DateTime.UtcNow,
                FechaEstado = DateTime.UtcNow,
                URLS3 = ruta,
                NombreArchivo = nombreArchivo
            };

            DocumentosRepository.Add(documento);

            return new Response { 
                Estado = 200,
                Mensaje = "El archivo ha sido cargado exitosamente."
            };
        }
    }
}
