using System;

namespace Pizzas.API.Models {
    public class User {
        public int      Id                  { get; set; }
        public string   Nombre              { get; set; }
        public string   Apellido            { get; set; }
        public string   UserName            { get; set; }
        public string   Password            { get; set; }
        public string   Token               { get; set; }
        public int      TokenExpirationDate { get; set; }
    }
}