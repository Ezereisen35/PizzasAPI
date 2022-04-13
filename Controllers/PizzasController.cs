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
    public class PizzasController : ControllerBase {
        [HttpGet]
        public IActionResult GetAll() {
            IActionResult   respuesta = Ok();
            try{
                List<Pizza> entityList = PizzasServices.GetAll();
                respuesta = Ok(entityList);
            }catch{
                Console.WriteLine("No se han podido cargar las pizzas");
            }
            return respuesta;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            IActionResult respuesta = Ok();
            try{
                Pizza entity = PizzasServices.GetById(id);
                if (entity == null) {
                    respuesta = NotFound();
                }else{
                    respuesta = Ok(entity);
                }
           }catch{
                Console.WriteLine("No se ha podido cargar la pizza");
           }
            return respuesta;
        }

        [HttpPost]
        public IActionResult Create(Pizza p) {
            IActionResult respuesta = Ok();;
            try{
                int intRowsAffected = PizzasServices.Insert(p);
                respuesta = CreatedAtAction(nameof(Create), new { id = p.Id }, p);
            }
            catch{
                Console.WriteLine("No se ha podido crear la pizza");
            }
            return respuesta;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza p) {
           
            IActionResult respuesta = Ok();
            try{
                Pizza entity;
                int intRowsAffected;
                if (id != p.Id) {
                    respuesta = BadRequest();
                } else {
                    entity = PizzasServices.GetById(id);
                    if (entity == null){
                        respuesta = NotFound();
                    } else {
                        intRowsAffected = PizzasServices.UpdateById(p);
                        if (intRowsAffected > 0){
                            respuesta = Ok(p);
                        } else {
                            respuesta = NotFound();
                        }
                    }
                }
            }catch{
                Console.WriteLine("No se ha podido actualizar la pizza");
            }
            
            return respuesta;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) {
            
            IActionResult   respuesta = Ok();
            try{
                Pizza entity;
                int intRowsAffected;
                
                entity = PizzasServices.GetById(id);
                if (entity == null){
                    respuesta = NotFound();
                }else{
                    intRowsAffected = PizzasServices.DeleteById(id);
                    if (intRowsAffected > 0){
                        respuesta = Ok(entity);
                    } else {
                        respuesta = NotFound();
                    }
                }
            }catch{
                Console.WriteLine("No se ha podido eliminar la pizza");
            }
            return respuesta;
        }
    }
}
