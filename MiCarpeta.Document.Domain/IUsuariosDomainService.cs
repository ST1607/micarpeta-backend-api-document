namespace MiCarpeta.Document.Domain
{
    public interface IUsuariosDomainService
    {
        bool ValidarToken(string token, string idUsuario);
    }
}
