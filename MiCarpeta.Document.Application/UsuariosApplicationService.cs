using MiCarpeta.Document.Domain;

namespace MiCarpeta.Document.Application
{
    public class UsuariosApplicationService: IUsuariosApplicationService
    {
        private readonly IUsuariosDomainService UsuariosDomainService;
        public UsuariosApplicationService(IUsuariosDomainService usuariosDomainService)
        {
            UsuariosDomainService = usuariosDomainService;
        }
        public bool ValidarToken(string token, string usuario)
        {
            return UsuariosDomainService.ValidarToken(token, usuario);
        }
    }
}
