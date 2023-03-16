using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        [Route("api/Empleado/GetAll")]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result result = BL.Empleado.GetAll(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Empleado/GetAll")]
        public ActionResult GetAll([FromBody] ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.GetAll(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet]
        [Route("api/Empleado/GetById/{NumeroEmpleado}")]
        public ActionResult GetById(string NumeroEmpleado)
        {
            ML.Result result = BL.Empleado.GetById(NumeroEmpleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Empleado/Add")]
        public ActionResult Add([FromBody] ML.Empleado empleado)
        {
            //Empleado.Rol = new ML.Rol();
            //Empleado.Direccion = new ML.Direccion();

            ML.Result result = BL.Empleado.Add(empleado);
            if (result.Correct)
            {

                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpPost]
        [Route("api/Empleado/Update")]
        public ActionResult Update([FromBody] ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Update(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Empleado/Delete/{NumeroEmpleado}")]
        public ActionResult Delete([FromBody] ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Delete(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
