using MiCarpeta.Document.Domain.Entities;

namespace MiCarpeta.Document.Repository
{
    public interface IUsuariosRepository : IRepository<Usuarios>
    {
        Usuarios ValidarToken(string token, string idUsuario);
    }
}
