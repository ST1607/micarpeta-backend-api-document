﻿using MiCarpeta.Document.Domain.Entities;

namespace MiCarpeta.Document.Domain
{
    public interface IDocumentoDomainService
    {
        Response SubirArchivo(string archivoBase64, long idUsuario, string nombreArchivo);

        Response ListarDocumentosPorCiudadano(long idCiudadano);
    }
}
