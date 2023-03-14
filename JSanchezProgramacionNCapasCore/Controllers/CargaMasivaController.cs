using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult UsuarioCargaMasiva()
        {
            ML.Result result = new ML.Result();
            return View(result); 
        }

        [HttpPost]
        public ActionResult UsuarioCargaMasiva(ML.Usuario usuario)
        {
            IFormFile archivo = Request.Form.Files["FileExcel"];
            //Session
            //GetString = Obtener la session
            //SetString = Crear una session
            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                //Si el archivo trae información
                if (archivo != null)
                {
                    if (archivo.Length > 0)
                    {
                        //obtener el nombre de nuestro archivo
                        string fileName = Path.GetFileName(archivo.FileName);

                        string folderPath = _configuration["PathFolder:value"];
                        string extensionArchivo = Path.GetExtension(archivo.FileName).ToLower();
                        string extensionModulo1 = _configuration["TipoArchivo:xlsx"];
                        string extensionModulo2 = _configuration["TipoArchivo:xls"];
                        string extensionModulo3 = _configuration["TipoArchivo:csv"];

                        if (extensionArchivo == extensionModulo1 || extensionArchivo == extensionModulo2 || extensionArchivo == extensionModulo3)
                        {
                            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                            if (!System.IO.File.Exists(filePath))
                            {
                                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                                {
                                    archivo.CopyTo(stream);
                                }

                                string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;
                                ML.Result resultUsuarios = BL.Usuario.ConvertXSLXtoDataTable(connectionString);

                                if (resultUsuarios.Correct)
                                {
                                    ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultUsuarios.Objects);
                                    if (resultValidacion.Objects.Count == 0)
                                    {
                                        resultValidacion.Correct = true;
                                        HttpContext.Session.SetString("PathArchivo", filePath);//Crear una sesion
                                    }

                                    return View(resultValidacion);
                                }
                                else
                                {
                                    ViewBag.Message = "El excel no contiene registros";
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Favor de seleccionar un archivo .xlsx, Verifique su archivo";
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "No selecciono ningun archivo, Seleccione uno correctamente";
                }
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertXSLXtoDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Usuario.Add(usuarioItem);
                        if (!resultAdd.Correct)
                        {
                            //Modificar de acuerdo a los datos
                            resultErrores.Objects.Add("No se insertó el Usuario con nombre: " + usuarioItem.Nombre + 
                                " Apellido paterno:" + usuarioItem.ApellidoPaterno + "Apellido materno:" + usuarioItem.ApellidoMaterno + " Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"~\Files\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los Usuarios No han sido registrados correctamente";
                    }
                    else
                    {
                        //terminar session
                        HttpContext.Session.Remove("PathArchivo");
                        ViewBag.Message = "Los Usuarios han sido registrados correctamente";
                    }

                }

            }
            return PartialView("Modal");
        }
    }    
}
