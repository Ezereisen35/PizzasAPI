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
    public class usersServices{

        public static List<User> GetAll() {
            string sqlQuery;
            List<User>     returnList;

            returnList = new List<User>();
            using (SqlConnection db = new SqlConnection(BD.CONNECTION_STRING)) {
                sqlQuery     = "SELECT Id, Nombre, Apellido, UserName, Password, Token, TokenExpirationDate ";
                sqlQuery    += "FROM Usuarios";
                returnList  = db.Query<User>(sqlQuery).ToList();
            }

            return returnList;
        }
        public static User GetById(int id) {
            string sqlQuery;
            User   returnEntity = null;
            
            sqlQuery  = "SELECT Id, Nombre, Apellido, UserName, Password, Token, TokenExpirationDate ";
            sqlQuery += "FROM User ";
            sqlQuery += "WHERE Id = @idUser";
            using (SqlConnection db = new SqlConnection(BD.CONNECTION_STRING)) {
                returnEntity  = db.QueryFirstOrDefault<User>(sqlQuery, new { idUser = id });
            }
            return returnEntity;
        }
        public static int Insert(User u) {
            string sqlQuery;
            int     intRowsAffected = 0;
                
            sqlQuery  = "INSERT INTO Usuarios (";
            sqlQuery += "Id, Nombre, Apellido, UserName, Password, Token, TokenExpirationDate";
            sqlQuery += ") VALUES (";
            sqlQuery += "@Id, @Nombre, @Apellido, @UserName, @Password, @Token, @TokenExpirationDate";
            sqlQuery += ")";
            using (SqlConnection db = new SqlConnection(BD.CONNECTION_STRING)) {
                intRowsAffected = db.Execute(sqlQuery, new {  
                    nombre      = u.Nombre, 
                    apellido = u.Apellido, 
                    userName     = u.UserName,
                    password = u.Password,
                    token = u.Token,
                    tokenExpirationDate = u.TokenExpirationDate
                });
            }
            return intRowsAffected;
        }

        public static int UpdateById(User u) {
            string sqlQuery;
            int     intRowsAffected = 0;

            sqlQuery  = "UPDATE Usuarios SET ";
            sqlQuery += "    Nombre         = @nombre, ";
            sqlQuery += "    Apellido    = @apellido, ";
            sqlQuery += "    UserName        = @userName, ";
            sqlQuery += "    Password    = @password ";
            sqlQuery += "    Token    = @token ";
            sqlQuery += "    TokenExpirationdate    = @tokenExpirationDate ";
            sqlQuery += "WHERE Id = @idUser";
            using (var db = new SqlConnection(BD.CONNECTION_STRING)) {
                intRowsAffected = db.Execute(sqlQuery, new {     
                    idUser = u.Id, 
                    nombre = u.Nombre, 
                    apellido = u.Apellido, 
                    userName = u.UserName, 
                    password = u.Password,
                    token = u.Token, 
                    tokenExpirationDate = u.TokenExpirationDate,  
                });
            }
            return intRowsAffected;
        }
        public static int DeleteById(int id) {
            string sqlQuery;
            int intRowsAffected = 0;
            
            sqlQuery  = "DELETE ";
            sqlQuery += "FROM Usuarios ";
            sqlQuery += "WHERE Id = @idUser";
            using (SqlConnection db = new SqlConnection(BD.CONNECTION_STRING)) {
                intRowsAffected = db.Execute(sqlQuery, new { idUser = id });
            }
            return intRowsAffected;
        }
    }
}