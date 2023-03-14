using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoAdd " +
                        $"'{empleado.NumeroEmpleado}','{empleado.RFC}'," +
                        $"'{empleado.Nombre}'," +
                        $"'{empleado.ApellidoPaterno}'," +
                        $"'{empleado.ApellidoMaterno}'," +
                        $"'{empleado.Email}'," +
                        $"'{empleado.Telefono }'," +
                        $"'{empleado.FechaNacimiento}'," +
                        $"'{empleado.NSS}'," +
                        $"'{empleado.Foto}'," +
                        $"'{empleado.Empresa.IdEmpresa}'"
                        //empleado.Empresa.Nombre,
                        //empleado.Empresa.Telefono,
                        //empleado.Empresa.Email,
                        //empleado.Empresa.DireccionWeb,
                        //empleado.Empresa.Logo
                        );
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate " +
                        $"'{empleado.NumeroEmpleado}'," +
                        $"'{empleado.RFC}'," +
                        $"'{empleado.Nombre}'," +
                        $"'{empleado.ApellidoPaterno}'," +
                        $"'{empleado.ApellidoMaterno}'," +
                        $"'{empleado.Email}'," +
                        $"'{empleado.Telefono}'," +
                        $"'{empleado.FechaNacimiento}'," +
                        $"'{empleado.NSS}'," +
                        $"'{empleado.Foto}'," +
                        $"'{empleado.Empresa.IdEmpresa}'"
                        //empleado.Empresa.Nombre,
                        //empleado.Empresa.Telefono,
                        //empleado.Empresa.Email,
                        //empleado.Empresa.DireccionWeb,
                        //empleado.Empresa.Logo
                        );
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Actualizo el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{empleado.NumeroEmpleado}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Actualizo el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            //empleado.Empresa = new ML.Empresa();//CHECAR esto ya que pierde el dato
            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetAll " +
                        $"'{empleado.Nombre}'," +
                        $"'{empleado.Empresa.IdEmpresa}'").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            empleado = new ML.Empleado();

                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.RFC = obj.Rfc;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Telefono = obj.Telefono;
                            empleado.Email = obj.Email;
                            empleado.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");//("dd -MM-yyyy")
                            empleado.NSS = obj.Nss;
                            empleado.FechaIngreso = obj.FechaIngreso.Value.ToString("dd-MM-yyyy");
                            empleado.Foto = obj.Foto;

                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.Nombre = obj.NombreEmpresa;

                            result.Objects.Add(empleado);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetById '{NumeroEmpleado}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();
                        var obj = query;
                        {
                            ML.Empleado empleado = new ML.Empleado();

                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.RFC = obj.Rfc;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Telefono = obj.Telefono;
                            empleado.Email = obj.Email;
                            empleado.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");//("dd-MM-yyyy")
                            empleado.NSS = obj.Nss;
                            empleado.FechaIngreso = obj.FechaIngreso.Value.ToString("dd-MM-yyyy");
                            empleado.Foto = obj.Foto;

                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.Nombre = obj.NombreEmpresa;

                            result.Object = empleado;
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
