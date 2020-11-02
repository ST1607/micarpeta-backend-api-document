using MiCarpeta.Document.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MiCarpeta.Document.Domain.Entities;
using MiCarpeta.Document.Common;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace MiCarpeta.Document.Presentation.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IDocumentApplicationService DocumentApplicationService;
        private readonly IUsuariosApplicationService UsuariosApplicationService;

        public DocumentosController(IConfiguration config, IDocumentApplicationService documentApplicationService,
            IUsuariosApplicationService usuariosApplicationService)
        {
            Configuration = config;
            DocumentApplicationService = documentApplicationService;
            UsuariosApplicationService = usuariosApplicationService;
        }

        [Authorize(Roles = "Ciudadano")]
        [HttpPost("subirDocumento")]
        public async System.Threading.Tasks.Task<IActionResult> SubirDocumentoAsync([FromBody] Documentos documento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseViewModel
                {
                    Estado = 400,
                    Errores = new List<string>() { "Todos los campos son obligatorios" }
                });
            }

            string token = await HttpContext.GetTokenAsync("access_token");

            Claim claimIdUsuario = User.Claims.FirstOrDefault(x => x.Type.Equals("IdUsuario", StringComparison.InvariantCultureIgnoreCase));

            if (claimIdUsuario != null)
            {
                string idUsuario = claimIdUsuario.Value;

                if (UsuariosApplicationService.ValidarToken(token, idUsuario))
                {
                    documento.IdCiudadano = long.Parse(idUsuario);
                    ResponseViewModel response = DocumentApplicationService.SubirArchivo(documento.Base64, documento.IdCiudadano, documento.NombreArchivo);

                    return Ok(response);
                }
            }

            return BadRequest(new ResponseViewModel
            {
                Estado = 401,
                Errores = new List<string>() { "Unauthorized" }
            });
        }
    }
}
