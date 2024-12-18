using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transortes_MVC_gen13.Models;
using System.Data.Entity;

namespace Transortes_MVC_gen13.Controllers
{
    public class RutasController : Controller
    {
        //creo una instancia Global de mi contexto
        private TransportesEntities context = new TransportesEntities();
        // GET: Rutas
        public ActionResult Index()
        {
            //retorno el modelo exascto de ViewRutas Directamente usando LinQ
            return View(context.View_Rutas.ToList());
        }

        //GET: View_LinQ
        public ActionResult View_LinQ()
        {
            //la idea de este controlador es poder crear objetos y datos a partir de datos ya existentes de la BD
            //Creao una lista de Objetos DTO
            List<View_Rutas_DTO> lista_view = new List<View_Rutas_DTO>();

            //lleno mi lista a partir de los datos del contexto usando LinQ
            lista_view = (from r in context.Rutas //el orgine del dato, donde 'r' representa cada renglón de la tabla
                          join carg in context.Cargamentos on r.ID_Ruta equals carg.Ruta_ID //unir las tablas usando Join del LinQ es similar a los Join en SQL.
                          //en este caso, 'carg' representa cada renglón de la tabla 'cargamentos', 'on' puedo traducirlos como 'dónde' r.ID_Ruta sea igual (equals) a carg.Ruta_ID
                          join cam in context.Camiones on r.Camion_ID equals cam.ID_Camion
                          join cho in context.Choferes on r.Chofer_ID equals cho.ID_Chofer
                          join dir_o in context.Direcciones on r.Direccionorigen_ID equals dir_o.ID_Direccion
                          join dir_d in context.Direcciones on r.Direcciondestino_ID equals dir_d.ID_Direccion
                          //seleciono los datos que necesito para modelar la clase DTO
                          select new View_Rutas_DTO()
                          {
                              C_ = r.ID_Ruta,
                              ID_Cargamento = carg.ID_Cargamento,
                              cargamento = carg.Descripcion,
                              ID_Direccion_Origen = dir_o.ID_Direccion,
                              Origen = "Calle: " + dir_o.Calle + " # " + dir_o.Numero + " Col. " + dir_o.Colonia + " C.P. " + dir_o.CP,
                              Estado_Origen = dir_o.Estado,
                              ID_Direccion_Destino = dir_d.ID_Direccion,
                              Destino = "Calle: " + dir_d.Calle + " # " + dir_d.Numero + " Col. " + dir_d.Colonia + " C.P. " + dir_d.CP,
                              Estado_Destino = dir_d.Estado,
                              ID_Chofer = cho.ID_Chofer,
                              Chofer = cho.Nombre + " " + cho.Apellido_Paterno + " " + cho.Apellido_Materno,
                              ID_Camion = cam.ID_Camion,
                              Camión = "Marca: " + cam.Marca + " Modelo: " + cam.Modelo + " Matrícula: " + cam.Matricula,
                              Salida = (DateTime)r.Fecha_salida,
                              LLegada_Estimada = (DateTime)r.Fecha_llegadaestimada,
                          }
                       ).ToList();
            ViewBag.Title = "Vista creada con LinQ";
            return View(lista_view);
        }

        //GET: EF_Nav

        public ActionResult EF_Nav()
        {
            //agregar importación de 'using System.Data.Entity;'
            //.Include() crea una posible navegación en función de los campos virtuales de los modelos de EF
            var rutas = context.Rutas.Include(r => r.Camiones)
                                     .Include(r => r.Choferes)
                                     .Include(r => r.Direcciones)
                                     .Include(r => r.Direcciones1)
                                     .Include(r => r.Cargamentos);
            ViewBag.Tituto = "Vista de Rutas con Navegación EF";
            return View(rutas.ToList());
        }

        // GET: Rutas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rutas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rutas/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rutas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rutas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rutas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rutas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
