namespace MiCarpeta.Document.Application
{
    public interface IUsuariosApplicationService
    {
        bool ValidarToken(string token, string usuario);
    }
}
