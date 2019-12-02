using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdGrupo { get; set; }
    }
}