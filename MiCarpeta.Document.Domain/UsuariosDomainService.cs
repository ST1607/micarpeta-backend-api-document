using MiCarpeta.Document.Domain.Entities;
using MiCarpeta.Document.Repository;

namespace MiCarpeta.Document.Domain
{
    public class UsuariosDomainService: IUsuariosDomainService
    {
        private readonly IUsuariosRepository UsuariosRepository;

        public UsuariosDomainService(IUsuariosRepository usuariosRepository)
        {
            UsuariosRepository = usuariosRepository;
        }

        public bool ValidarToken(string token, string idUsuario)
        {
            Usuarios usuarioDB = UsuariosRepository.ValidarToken(token, idUsuario);

            return usuarioDB != null;
        }
    }
}
