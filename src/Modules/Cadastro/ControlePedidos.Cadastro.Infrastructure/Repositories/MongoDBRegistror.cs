using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePedidos.Cadastro.Infrastructure.Repositories
{
    public class MongoDBRigistror
    {
        public static void RegisterDocumentResolver()
        {
            var pack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
            };

            ConventionRegistry.Register("Camel Case", pack, t => t.FullName.Contains(".MongoDB.Models"));
        }
    }
}
