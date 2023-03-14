using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();//EF
            ML.Aseguradora aseguradora = new ML.Aseguradora();

            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
                return View(aseguradora);
            }
            else
            {
                return View(aseguradora);
            }
        }

        [HttpGet]
        //Formulario
        public ActionResult Form(int? IdAseguradora)
        {
            ML.Usuario usuario = new ML.Usuario(); //agrgar para aceptar el getall
            ML.Result resultUsuario = BL.Usuario.GetAll(usuario);//(usuario) para obtener todos
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();

            if (resultUsuario.Correct)
            {
                aseguradora.Usuario.Usuarios = resultUsuario.Objects;
            }
            if (IdAseguradora == null)
            {
                //Add
                return View(aseguradora);
            }
            else
            {
                //GetById
                ML.Result result = BL.Aseguradora.GetById(IdAseguradora.Value);

                if (result.Correct)
                {
                    aseguradora = (ML.Aseguradora)result.Object;
                    aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                    return View(aseguradora);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar la informacion";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            if (aseguradora.IdAseguradora == 0)
            {
                //Add
                result = BL.Aseguradora.Add(aseguradora);

                if (result.Correct)
                {
                    ViewBag.Message = "Se completo el registro satisfactoriamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al insertar el registro";
                }
                return View("Modal");
            }
            else
            {
                //Update
                result = BL.Aseguradora.Update(aseguradora);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo el registro satisfactoriamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar el registro";
                }
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult Delete(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            result = BL.Aseguradora.Delete(aseguradora);
            if (result.Correct)
            {
                ViewBag.Message = "Se elimino el registro satisfactoriamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar el registro";
            }
            return View("Modal");
        }
    }
}
