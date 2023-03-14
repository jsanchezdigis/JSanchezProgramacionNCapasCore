using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result result = BL.Empleado.GetAll(empleado);//EF

            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
                return View(empleado);
            }
            else
            {
                return View(empleado);
            }
        }

        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {
            empleado.Empresa = new ML.Empresa();
            ML.Result result = BL.Empleado.GetAll(empleado);

            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
                return View(empleado);
            }
            else
            {
                return View(empleado);
            }
        }

        [HttpGet]
        //Formulario
        public ActionResult Form(string NumeroEmpleado)
        {
            ML.Result resultEmpresa = BL.Empresa.GetAll();

            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();

            if (resultEmpresa.Correct)
            {
                empleado.Empresa.Empresas = resultEmpresa.Objects;
            }
            if (NumeroEmpleado == null)
            {
                //Add Formulario vacio
                empleado.Action = "Add";
                return View(empleado);
            }
            else
            {
                empleado.Action = "Update";
                //GetById
                ML.Result result = BL.Empleado.GetById(NumeroEmpleado);

                if (result.Correct)
                {
                    empleado = (ML.Empleado)result.Object;
                    empleado.Empresa.Empresas = resultEmpresa.Objects;
                    return View(empleado);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar la informacion";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            //Convirtiendo imagen a byte 


            IFormFile file = Request.Form.Files["fuImage"];
            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);
                empleado.Foto = Convert.ToBase64String(imagen);
            }

            ML.Result result = new ML.Result();

            if (empleado.Action == "Add")//Tenia empleado.NumeroEmpleado ==0
            {
                //Add
                result = BL.Empleado.Add(empleado);

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
                result = BL.Empleado.Update(empleado);
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
        public ActionResult Delete(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            result = BL.Empleado.Delete(empleado);
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

        public static byte[] ConvertToBytes(IFormFile Foto)
        {
            using var fileStream = Foto.OpenReadStream();
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

    }
}
