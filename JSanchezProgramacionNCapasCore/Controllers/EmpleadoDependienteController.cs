using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;

namespace PL.Controllers
{
    public class EmpleadoDependienteController : Controller
    {
        [HttpGet]
        public ActionResult GetByIdEmpleado()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result result = BL.Empleado.GetAll(empleado);
            ML.Result resultEmpresa = BL.Empresa.GetAll();

            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
                empleado.Empresa.Empresas = resultEmpresa.Objects;
                return View(empleado);
            }
            else
            {
                return View(empleado);
            }
        }
        [HttpPost]
        public ActionResult GetByIdEmpleado(ML.Empleado empleado)
        {
            //empleado.Empresa = new ML.Empresa();
            ML.Result result = BL.Empleado.GetAll(empleado);
            ML.Result resultEmpresa = BL.Empresa.GetAll();

            if (result.Correct)
            {
                //empleado = (ML.Empleado)result.Object;
                empleado.Empresa.Empresas = resultEmpresa.Objects;
                empleado.Empleados = result.Objects;
                return View(empleado);
            }
            else
            {
                return View(empleado);
            }
        }

        [HttpGet]
        public ActionResult DependienteGetByIdEmpleado(string? NumeroEmpleado)
        {
            ML.Result result = BL.Dependiente.GetAll(NumeroEmpleado);
            ML.Result resultEmpleado = BL.Empleado.GetById(NumeroEmpleado);
            ML.Dependiente dependiente = new ML.Dependiente();
            if (result.Correct)
            {

                dependiente.Dependientes = result.Objects;
                dependiente.Empleado = (ML.Empleado)resultEmpleado.Object;

                return View(dependiente);
            }
            else
            {
                return View(dependiente);
            }

            

        }

        //chacr para la creaion de la session


        [HttpGet]
        //Formulario
        public ActionResult Form(int? IdDependiente)
        {
            ML.Result resultDependienteTipo = BL.DependienteTipo.GetAll();


            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.DependienteTipo = new ML.DependienteTipo();

            dependiente.Empleado = new ML.Empleado();


            if (resultDependienteTipo.Correct)
            {
                dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;
            }
            if (IdDependiente == null)
            {
                //Add Formulario vacio
                return View(dependiente);
            }
            else
            {
                //GetById
                ML.Result result = BL.Dependiente.GetById(IdDependiente.Value);

                if (result.Correct)
                {
                    dependiente = (ML.Dependiente)result.Object;
                    dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;

                    return View(dependiente);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar la informacion";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();

            if (ModelState.IsValid == true)
            {

                if (dependiente.IdDependiente == 0)
                {
                    //Add
                    result = BL.Dependiente.Add(dependiente);

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
                    result = BL.Dependiente.Update(dependiente);
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
            else
            {
                dependiente.Empleado = new ML.Empleado();

                ML.Result resultDependienteTipo = BL.DependienteTipo.GetAll();

                dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;

                return View(dependiente);
            }
        }

        [HttpGet]
        public ActionResult Delete(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();

            result = BL.Dependiente.Delete(dependiente);
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
