//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GrupoFuncionalidade
    {
        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public string Controller { get; set; }
        public string Metodo { get; set; }
    
        public virtual Grupo Grupo { get; set; }
    }
}
