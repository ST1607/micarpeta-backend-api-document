using MiCarpeta.Document.Common;
using MiCarpeta.Document.Domain.Entities;
using MiCarpeta.Document.Repository.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace MiCarpeta.Document.Repository
{
    public class CiudadanoRepository: Repository<Ciudadano>, ICiudadanoRepository
    {
        public CiudadanoRepository(IConfiguration configuration) : base(configuration) { }

        public Ciudadano ObtenerPorId(long idCiudadano)
        {
            List<FilterQuery> filter = new List<FilterQuery>()
            {
                new FilterQuery {
                    AtributeName = "Id",
                    Operator = (int)Enumerators.QueryScanOperator.Equal,
                    ValueAtribute = idCiudadano
                }
            };

            Ciudadano ciudadano = GetByList(filter).FirstOrDefault();

            return ciudadano;
        }
    }
}
