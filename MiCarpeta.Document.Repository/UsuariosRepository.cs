using MiCarpeta.Document.Common;
using MiCarpeta.Document.Domain.Entities;
using MiCarpeta.Document.Repository.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiCarpeta.Document.Repository
{
    public class UsuariosRepository : Repository<Usuarios>, IUsuariosRepository
    {
        public UsuariosRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Usuarios ValidarToken(string token, string idUsuario)
        {
            List<FilterQuery> filters = new List<FilterQuery>()
            {
                new FilterQuery {
                    AtributeName = "Token",
                    Operator = (int)Enumerators.QueryScanOperator.Equal,
                    ValueAtribute = token
                },
                new FilterQuery {
                    AtributeName = "IdUsuario",
                    Operator = (int)Enumerators.QueryScanOperator.Equal,
                    ValueAtribute = long.Parse(idUsuario)
                },
                new FilterQuery {
                    AtributeName = "VencimientoToken",
                    Operator = (int)Enumerators.QueryScanOperator.GreaterThan,
                    ValueAtribute = DateTime.UtcNow
                }
            };

            Usuarios usuarioDB = GetByList(filters).FirstOrDefault();

            return usuarioDB;
        }
    }
}
