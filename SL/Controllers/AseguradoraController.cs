using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class AseguradoraController : Controller
    {
        [HttpGet]
        [Route("api/Aseguradora/GetAll")]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();
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
        [Route("api/Aseguradora/GetById/{IdAseguradora}")]
        public ActionResult GetById(int IdAseguradora)
        {
            ML.Result result = BL.Aseguradora.GetById(IdAseguradora);
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
        [Route("api/Aseguradora/Add")]
        public ActionResult Add([FromBody] ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Add(aseguradora);
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
        [Route("api/Aseguradora/Update")]
        public ActionResult Update([FromBody] ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Update(aseguradora);
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
        [Route("api/Aseguradora/Delete/{IdAseguradora}")]
        public ActionResult Delete([FromBody] ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Delete(aseguradora);
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
