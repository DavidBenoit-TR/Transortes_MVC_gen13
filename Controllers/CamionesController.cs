using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transortes_MVC_gen13.Models;

namespace Transortes_MVC_gen13.Controllers
{
    public class CamionesController : Controller
    {
        // GET: Camiones
        public ActionResult Index()
        {
            //crear una lista de camiones del modelo original
            List<Camiones> lista_camiones = new List<Camiones>();
            //lleno la lista con elementos existentes dentro del contexto(BD) utilizando EF y LinQ
            using (TransportesEntities context = new TransportesEntities())
            {
                //lleno mi lista directamente usando LinQ
                lista_camiones = (from camion in context.Camiones select camion).ToList();
                //otra forma de usar LinQ es
                //lista_camiones = context.Camiones.ToList();
                ////otra forma de hacerlo 
                //foreach (Camiones cam in context.Camiones)
                //{
                //    lista_camiones.Add(cam);
                //}
            }

            //ViewBag (forma parte de Razor) se caracteriza por hacer uso de una propiedad arbitraria que sirve para pasar información desde el controlador a la vista
            ViewBag.Titulo = "Lista de Camiones";
            ViewBag.Subtitulo = "Utilizando ASP.NET MVC";

            //ViewData se caracterizaz por hacer uso de un atributo arbitrario y tiene el mismo funcionamiento que el ViewBag
            ViewData["Titulo2"] = "Segundo Título";

            //TempData se cracteriza por permitir crear variables temporales que existen durante la ejecución del Runtime de ASP
            //además, los temdata me permite compartir información nos solo del controlodaor a la vista, sino también entre otras vistas y otros controladores
            //TempData.Add("Clave", "Valor");


            //retorno la vista con los datos del modelo
            return View(lista_camiones);
        }
    }
}