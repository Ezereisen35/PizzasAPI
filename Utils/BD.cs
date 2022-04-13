using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

using Pizzas.API.Models;

namespace Pizzas.API.Utils {
    public static class BD {
        public static string CONNECTION_STRING = @"Persist Security Info=False;User ID=Pizzas;password=Pizzas;Initial Catalog=DAI-Pizzas;Data Source=A-CEO-19;";
    }
}       