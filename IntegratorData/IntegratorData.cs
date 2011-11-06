using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace IntegratorData
{
    public class IntegratorData
    {
        Database integrator;

        public bool GetRepositories(string RepositoryTypeName)
        {
            integrator = DatabaseFactory.CreateDatabase("integrator");

            return true;
        }

    }
}
