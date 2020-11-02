using MiCarpeta.Document.Domain.Entities;

namespace MiCarpeta.Document.Repository
{
    public interface ICiudadanoRepository: IRepository<Ciudadano>
    {
        Ciudadano ObtenerPorId(long idCiudadano);
    }
}
