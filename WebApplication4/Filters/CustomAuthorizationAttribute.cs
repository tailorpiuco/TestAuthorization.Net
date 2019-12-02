using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebApplication4.Models;

namespace WebApplication4.Filters
{
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        public new int[] Roles { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            CreateSession();

            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }

            var usuario = httpContext.Session["USUARIO"] as Usuario;

            Teste5Entities db = new Teste5Entities();
            var grupo = db.GrupoUsuario.Where(gu => gu.IdUsuario == usuario.Id).SingleOrDefault();
            var listaGrupos = db.GrupoFuncionalidade.Where(gf => gf.IdGrupo == grupo.IdGrupo).ToList();

            if (Roles.Any(x => listaGrupos.Any(y => y.IdGrupo == x)))
            {
                return true;
            }

            return false;
        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            Roles = GetRoles.GetActionRoles(actionName, controllerName);

            base.OnAuthorization(filterContext);
        } 
        
        private Usuario CreateSession()
        {
            CarregarSessao(HttpContext.Current);

            return HttpContext.Current.Session["USUARIO"] as Usuario;
        }

        private void CarregarSessao(HttpContext httpContext)
        {
            var usuario = new Usuario()
            {
                Id = 1,
                Nome = "User1",
                IdGrupo = 1
            };

            httpContext.Session["USUARIO"] = usuario;
        }
    }

    public class GetRoles
    {

        public static int[] GetActionRoles(string action, string controller)
        {
            Teste5Entities db = new Teste5Entities();
            var listaGrupos = db.GrupoFuncionalidade.Where(x => x.Controller == controller && x.Metodo == action).ToList();

            return listaGrupos.Select(x => x.IdGrupo).ToArray();
        }

        public static XElement findElementByAttribute(XElement xElement, string tagName, string attribute)
        {
            return xElement.Elements(tagName).FirstOrDefault(x => x.Attribute("name").Value.Equals(attribute, StringComparison.OrdinalIgnoreCase));
        }
    }
}