using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public EmpleadoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {

                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Empleado/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    empleado.Empleados = result.Objects;
                }

            }
            catch (Exception ex)
            {
            }

            return View(empleado);

            //ML.Empleado empleado = new ML.Empleado();
            //empleado.Empresa = new ML.Empresa();
            //ML.Result result = BL.Empleado.GetAll(empleado);//EF

            //if (result.Correct)
            //{
            //    empleado.Empleados = result.Objects;
            //    return View(empleado);
            //}
            //else
            //{
            //    return View(empleado);
            //}
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
                ML.Result result = new ML.Result();

                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(_configuration["urlApi"]);
                        //GET
                        var responseTask = client.GetAsync("Empleado/GetById/" + NumeroEmpleado);
                        responseTask.Wait();

                        var resultService = responseTask.Result;

                        if (resultService.IsSuccessStatusCode)
                        {
                            var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();


                            ML.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(readTask.Result.Object.ToString());
                            //result.Objects.Add(resultItemList);
                            result.Object = resultItemList;

                            empleado = (ML.Empleado)result.Object;
                            empleado.Empresa.Empresas = resultEmpresa.Objects;
                            return View(empleado);
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
                //ML.Result result = BL.Empleado.GetById(NumeroEmpleado);

                //if (result.Correct)
                //{
                //    empleado = (ML.Empleado)result.Object;
                //    empleado.Empresa.Empresas = resultEmpresa.Objects;
                //    return View(empleado);
                //}
                //else
                //{
                //    ViewBag.Message = "Ocurrio un error al consultar la informacion";
                //    return View("Modal");
                //}
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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApi"]);
                if (empleado.Action == "Add")
                {
                    //POST
                    var postTask = client.PostAsJsonAsync<ML.Empleado>("Empleado/Add", empleado);
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
                    var postTask = client.PostAsJsonAsync<ML.Empleado>("Empleado/Update", empleado);
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

            //if (empleado.Action == "Add")//Tenia empleado.NumeroEmpleado ==0
            //{
            //    //Add
            //    result = BL.Empleado.Add(empleado);

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
            //    result = BL.Empleado.Update(empleado);
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
        public ActionResult Delete(ML.Empleado empleado)
        {
            string NumeroEmpleado = empleado.NumeroEmpleado;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApi"]);

                //POST-Delete
                //var postTask = client.DeleteAsync("Usuario/Delete/" + IdUsuario);
                var postTask = client.PostAsJsonAsync<ML.Empleado>("Empleado/Delete/" + NumeroEmpleado, empleado);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se elimino el registro satisfactoriamente";
                    //resultUsuario = BL.Usuario.GetAll(usuario);
                    //return RedirectToAction("GetAll", resultUsuario);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al eliminar el registro";
                }
                return View("Modal");
            }

            //ML.Result result = new ML.Result();

            //result = BL.Empleado.Delete(empleado);
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

        public static byte[] ConvertToBytes(IFormFile Foto)
        {
            using var fileStream = Foto.OpenReadStream();
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

    }
}
