using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public AseguradoraController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult GetAll()
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {

                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Aseguradora/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Aseguradora resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    aseguradora.Aseguradoras = result.Objects;
                }

            }
            catch (Exception ex)
            {
            }

            return View(aseguradora);

            //ML.Result result = BL.Aseguradora.GetAll();//EF
            //ML.Aseguradora aseguradora = new ML.Aseguradora();

            //if (result.Correct)
            //{
            //    aseguradora.Aseguradoras = result.Objects;
            //    return View(aseguradora);
            //}
            //else
            //{
            //    return View(aseguradora);
            //}
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
                ML.Result result = new ML.Result();

                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(_configuration["urlApi"]);
                        //GET
                        var responseTask = client.GetAsync("Aseguradora/GetById/" + IdAseguradora);
                        responseTask.Wait();

                        var resultService = responseTask.Result;

                        if (resultService.IsSuccessStatusCode)
                        {
                            var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();


                            ML.Aseguradora resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(readTask.Result.Object.ToString());
                            //result.Objects.Add(resultItemList);
                            result.Object = resultItemList;

                            aseguradora = (ML.Aseguradora)result.Object;
                            aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                            return View(aseguradora);
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se puedo hacer la consulta";
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }
                    return View("Modal");
                }


                //GetById
                //ML.Result result = BL.Aseguradora.GetById(IdAseguradora.Value);

                //if (result.Correct)
                //{
                //    aseguradora = (ML.Aseguradora)result.Object;
                //    aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                //    return View(aseguradora);
                //}
                //else
                //{
                //    ViewBag.Message = "Ocurrio un error al consultar la informacion";
                //    return View("Modal");
                //}
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApi"]);
                if (aseguradora.IdAseguradora == 0)
                {
                    //POST
                    var postTask = client.PostAsJsonAsync<ML.Aseguradora>("Aseguradora/Add", aseguradora);
                    postTask.Wait();

                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
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
                    var postTask = client.PostAsJsonAsync<ML.Aseguradora>("Aseguradora/Update", aseguradora);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        //return RedirectToAction("GetAll");
                        ViewBag.Message = "Se actualizo el registro satisfactoriamente";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al actualizar el registro";
                    }
                    return View("Modal");
                }
                return View("Modal");
            }


            //ML.Result result = new ML.Result();

            //if (aseguradora.IdAseguradora == 0)
            //{
            //    //Add
            //    result = BL.Aseguradora.Add(aseguradora);

            //    if (result.Correct)
            //    {
            //        ViewBag.Message = "Se completo el registro satisfactoriamente";
            //    }
            //    else
            //    {
            //        ViewBag.Message = "Ocurrio un error al insertar el registro";
            //    }
            //    return View("Modal");
            //}
            //else
            //{
            //    //Update
            //    result = BL.Aseguradora.Update(aseguradora);
            //    if (result.Correct)
            //    {
            //        ViewBag.Message = "Se actualizo el registro satisfactoriamente";
            //    }
            //    else
            //    {
            //        ViewBag.Message = "Ocurrio un error al actualizar el registro";
            //    }
            //    return View("Modal");
            //}
        }

        [HttpGet]
        public ActionResult Delete(ML.Aseguradora aseguradora)
        {
            int IdAseguradora = aseguradora.IdAseguradora;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApi"]);

                //POST-Delete
                //var postTask = client.DeleteAsync("Aseguradora/Delete/" + IdAseguradora);
                var postTask = client.PostAsJsonAsync<ML.Aseguradora>("Aseguradora/Delete/" + IdAseguradora,aseguradora);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se elimino el registro satisfactoriamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al eliminar el registro";
                }
                return View("Modal");
            }


            //ML.Result result = new ML.Result();

            //result = BL.Aseguradora.Delete(aseguradora);
            //if (result.Correct)
            //{
            //    ViewBag.Message = "Se elimino el registro satisfactoriamente";
            //}
            //else
            //{
            //    ViewBag.Message = "Ocurrio un error al eliminar el registro";
            //}
            //return View("Modal");
        }
    }
}
