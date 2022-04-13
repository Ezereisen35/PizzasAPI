using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Utils;
using Pizzas.API.services;


namespace Pizzas.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase {
        [HttpGet]
        public IActionResult GetAll() {
            IActionResult respuesta = Ok();
            try{
                List<User> entityList = usersServices.GetAll();
                respuesta = Ok(entityList);
            }catch{
                Console.WriteLine("No se han podido cargar los usuario");
            }
            return respuesta;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            IActionResult respuesta = Ok();
           try{
                User entity = usersServices.GetById(id);;
                if (entity == null) {
                    respuesta = NotFound();
                }else{
                    respuesta = Ok(entity);
                }
           }catch{
                Console.WriteLine("No se ha podido cargar el usuario");
           }
            return respuesta;
        }

        [HttpPost]
        public IActionResult Create(User u) {
            IActionResult respuesta = Ok();
            try{
                int intRowsAffected = usersServices.Insert(u);
                respuesta = CreatedAtAction(nameof(Create), new { id = u.Id }, u);
            }
            catch{
                Console.WriteLine("No se ha podido crear el usuario");
            }
            return respuesta;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User u) {
           
            IActionResult respuesta = Ok();
            try{
                User entity;
                int intRowsAffected;
                if (id != u.Id) {
                    respuesta = BadRequest();
                } else {
                    entity = usersServices.GetById(id);
                    if (entity == null){
                        respuesta = NotFound();
                    } else {
                        intRowsAffected = usersServices.UpdateById(u);
                        if (intRowsAffected > 0){
                            respuesta = Ok(u);
                        
                        } else {
                            respuesta = NotFound();
                        }
                    }
                }
            }catch{
                Console.WriteLine("No se ha podido actualizar el usuario");
            }
            
            return respuesta;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) {
            
            IActionResult respuesta = Ok();
            try{
                User entity = usersServices.GetById(id);
                int intRowsAffected;
                if (entity == null){
                    respuesta = NotFound();
                } else {
                    intRowsAffected = usersServices.DeleteById(id);
                    if (intRowsAffected > 0){
                        respuesta = Ok(entity);
                        
                    } else {
                        respuesta = NotFound();
                    }
                }
            }catch{
                Console.WriteLine("No se ha podido eliminar el usuario");
            }
            return respuesta;
        }
    }
}
