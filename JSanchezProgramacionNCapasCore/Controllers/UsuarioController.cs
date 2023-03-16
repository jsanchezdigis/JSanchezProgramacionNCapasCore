using BL;
using Microsoft.AspNetCore.Mvc;
using ML;
using System.Runtime.InteropServices;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            //ML.Usuario usuario = new ML.Usuario();
            //ML.Result result = BL.Usuario.GetAll(usuario);//EF

            //if (result.Correct)
            //{
            //    usuario.Usuarios = result.Objects;
            //    return View(usuario);
            //}
            //else
            //{
            //    return View(usuario);
            //}

            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {

                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Usuario/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    usuario.Usuarios = result.Objects;
                }

            }
            catch (Exception ex)
            {
            }

            return View(usuario);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpGet]
        //Formulario
        public ActionResult Form(int? IdUsuario)
        {
            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPais = BL.Pais.GetAll();

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            if (resultRol.Correct && resultPais.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                usuario.Rol.Roles = resultRol.Objects;
            }
            if (IdUsuario == null)
            {
                //Add Formulario vacio
                return View(usuario);
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
                        var responseTask = client.GetAsync("Usuario/GetById/" + IdUsuario);
                        responseTask.Wait();

                        var resultService = responseTask.Result;

                        if (resultService.IsSuccessStatusCode)
                        {
                            var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();


                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                            //result.Objects.Add(resultItemList);
                            result.Object = resultItemList;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                            usuario = (ML.Usuario)result.Object;

                            ML.Result resultEstado = BL.Estado.GetById(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                            ML.Result resultMunicipio = BL.Municipio.GetById(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                            ML.Result resultColonia = BL.Colonia.GetById(usuario.Direccion.Colonia.Municipio.IdMunicipio);

                            usuario.Usuarios = resultPais.Objects;
                            usuario.Rol.Roles = resultRol.Objects;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                            return View(usuario);
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se puedo hacer la consulta";
                        }
                    }
                    catch(Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }
                        return View("Modal");
                }

                //GetById
                //ML.Result result = BL.Usuario.GetById(IdUsuario.Value);

                //if (result.Correct) //&& resultPais.Correct)
                //{
                //    usuario = (ML.Usuario)result.Object;
                //    usuario.Rol.Roles = resultRol.Objects;
                //    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                //    ML.Result resultEstado = BL.Estado.GetById(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                //    ML.Result resultMunicipio = BL.Municipio.GetById(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                //    ML.Result resultColonia = BL.Colonia.GetById(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                //    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                //    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                //    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                //    return View(usuario);
                //}
                //else
                //{
                //    ViewBag.Message = "Ocurrio un error al consultar la informacion";
                //    return View("Modal");
                //}
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            //Convirtiendo imagen a byte 
            //HttpPostedFileBase file = Request.Files["fuImage"];
            IFormFile file = Request.Form.Files["fuImage"];
            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);
                usuario.Imagen = Convert.ToBase64String(imagen);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApi"]);
                if (usuario.IdUsuario == 0)
                {
                    //POST
                    var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);
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
                    var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Update", usuario);
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

            //sin utilizar el consumo de servicios
            //ML.Result result = new ML.Result();

            //if (ModelState.IsValid == true)
            //{

            //    if (usuario.IdUsuario == 0)
            //    {
            //        //Add
            //        result = BL.Usuario.Add(usuario);

            //        if (result.Correct)
            //        {
            //            ViewBag.Message = "Se completo el registro satisfactoriamente";
            //        }
            //        else
            //        {
            //            ViewBag.Message = "Ocurrio un error al insertar el registro";
            //        }
            //        return View("Modal");
            //    }
            //    else
            //    {
            //        //Update
            //        result = BL.Usuario.Update(usuario);
            //        if (result.Correct)
            //        {
            //            ViewBag.Message = "Se actualizo el registro satisfactoriamente";
            //        }
            //        else
            //        {
            //            ViewBag.Message = "Ocurrio un error al actualizar el registro";
            //        }
            //        return View("Modal");
            //    }
            //}
            //else
            //{
            //    usuario.Direccion = new ML.Direccion();
            //    usuario.Direccion.Colonia = new ML.Colonia();
            //    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            //    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            //    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            //    ML.Result resultRol = BL.Rol.GetAll();
            //    ML.Result resultPais = BL.Pais.GetAll();

            //    usuario.Rol.Roles = resultRol.Objects;
            //    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

            //    return View(usuario);
            //}
        }

        [HttpGet]
        public ActionResult Delete(ML.Usuario usuario)
        {
            //ML.Result resultUsuario = new ML.Result();
            int IdUsuario = usuario.IdUsuario;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApi"]);

                //POST-Delete
                //var postTask = client.DeleteAsync("Usuario/Delete/" + IdUsuario);
                var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Delete/" + IdUsuario,usuario);
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


            //resultUsuario = BL.Usuario.GetAll(usuario);
            //return View("GetAll", resultListProduct);



            //ML.Result result = new ML.Result();

            //result = BL.Usuario.Delete(usuario);
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

        [HttpPost]
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = new ML.Result();
            result = BL.Estado.GetById(IdPais);

            return Json(result.Objects);
        }

        [HttpPost]
        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            result = BL.Municipio.GetById(IdEstado);

            return Json(result.Objects);
        }

        [HttpPost]
        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            result = BL.Colonia.GetById(IdMunicipio);

            return Json(result.Objects);
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        public JsonResult CambiarStatus(int IdUsuario, bool Status)
        {
            ML.Result result = BL.Usuario.ChangeStatus(IdUsuario, Status);
            return Json(result);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserNombre, string Password)
        {
            ML.Result result = BL.Usuario.GetByName(UserNombre);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (Password == usuario.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Contraseña incorrecta";
                    return PartialView("ModalLogin");
                }
            }
            else
            {
                ViewBag.Message = "Contraseña incorrecta";
                return PartialView("ModalLogin");
            }
        }
    }
}
