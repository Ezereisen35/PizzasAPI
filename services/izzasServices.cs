using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Utils;

namespace Pizzas.API.services{
    public class PizzasServices{
        public static List<Pizza> GetAll() {
            string sqlQuery;
            List<Pizza>     returnList;

            returnList = new List<Pizza>();
            using (SqlConnection db = new SqlConnection(BD.CONNECTION_STRING)) {
                sqlQuery     = "SELECT Id, Nombre, LibreGluten, Importe, Descripcion ";
                sqlQuery    += "FROM Pizzas";
                returnList  = db.Query<Pizza>(sqlQuery).ToList();
            }

            return returnList;
        }
        public static Pizza GetById(int id) {
            string sqlQuery;
            Pizza   returnEntity = null;
            
            sqlQuery  = "SELECT Id, Nombre, LibreGluten, Importe, Descripcion ";
            sqlQuery += "FROM Pizzas ";
            sqlQuery += "WHERE Id = @idPizza";
            using (SqlConnection db = new SqlConnection(BD.CONNECTION_STRING)) {
                returnEntity  = db.QueryFirstOrDefault<Pizza>(sqlQuery, new { idPizza = id });
            }
            return returnEntity;
        }
        public static int Insert(Pizza pizza) {
            string sqlQuery;
            int     intRowsAffected = 0;
                
            sqlQuery  = "INSERT INTO Pizzas (";
            sqlQuery += "   Nombre  , LibreGluten   , Importe   , Descripcion";
            sqlQuery += ") VALUES (";
            sqlQuery += "   @nombre , @libreGluten  , @importe  , @descripcion";
            sqlQuery += ")";
            using (SqlConnection db = new SqlConnection(BD.CONNECTION_STRING)) {
                intRowsAffected = db.Execute(sqlQuery, new {  
                    nombre      = pizza.Nombre, 
                    libreGluten = pizza.LibreGluten, 
                    importe     = pizza.Importe,
                    descripcion = pizza.Descripcion 
                });
            }
            return intRowsAffected;
        }

        public static int UpdateById(Pizza pizza) {
            string sqlQuery;
            int     intRowsAffected = 0;

            sqlQuery  = "UPDATE Pizzas SET ";
            sqlQuery += "    Nombre         = @nombre, ";
            sqlQuery += "    LibreGluten    = @libreGluten, ";
            sqlQuery += "    Importe        = @importe, ";
            sqlQuery += "    Descripcion    = @descripcion ";
            sqlQuery += "WHERE Id = @idPizza";
            using (var db = new SqlConnection(BD.CONNECTION_STRING)) {
                intRowsAffected = db.Execute(sqlQuery, new {     
                    idPizza     = pizza.Id, 
                    nombre      = pizza.Nombre, 
                    libreGluten = pizza.LibreGluten, 
                    importe     = pizza.Importe, 
                    descripcion = pizza.Descripcion 
                });
            }
            return intRowsAffected;
        }
        public static int DeleteById(int id) {
            string sqlQuery;
            int     intRowsAffected = 0;
            
            sqlQuery  = "DELETE ";
            sqlQuery += "FROM Pizzas ";
            sqlQuery += "WHERE Id = @idPizza";
            using (SqlConnection db = new SqlConnection(BD.CONNECTION_STRING)) {
                intRowsAffected = db.Execute(sqlQuery, new { idPizza = id });
            }
            return intRowsAffected;
        }
    }
}