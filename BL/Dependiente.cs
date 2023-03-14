using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
        public static ML.Result Add(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"DependienteAdd " +
                        $"'{dependiente.NumeroEmpleado}'," +
                        $"'{dependiente.Nombre}'," +
                        $"'{dependiente.ApellidoPaterno}'," +
                        $"'{dependiente.ApellidoMaterno}'," +
                        $"'{dependiente.EstadoCivil}'," +
                        $"'{dependiente.Genero}'," +
                        $"'{dependiente.Telefono}'," +
                        $"'{dependiente.RFC}'," +
                        $"'{dependiente.DependienteTipo.IdDependienteTipo}'," +
                        $"'{dependiente.FechaNacimiento}'"
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

        public static ML.Result Update(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"DependienteUpdate " +
                        $"'{dependiente.IdDependiente}'," +
                        $"'{dependiente.NumeroEmpleado}'," +
                        $"'{dependiente.Nombre}'," +
                        $"'{dependiente.ApellidoPaterno}'," +
                        $"'{dependiente.ApellidoMaterno}'," +
                        $"'{dependiente.EstadoCivil}'," +
                        $"'{dependiente.Genero}'," +
                        $"'{dependiente.Telefono}'," +
                        $"'{dependiente.RFC}'," +
                        $"'{dependiente.DependienteTipo.IdDependienteTipo}'," +
                        $"'{dependiente.FechaNacimiento}'"
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

        public static ML.Result Delete(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{dependiente.IdDependiente}'");
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

        public static ML.Result GetAll(string? NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.Dependientes.FromSqlRaw($"DependienteGetAll '{NumeroEmpleado}'").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();

                            dependiente.NumeroEmpleado = obj.NumeroEmpleado;
                            dependiente.IdDependiente = obj.IdDependiente;

                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.Nombre = obj.NombreEmpleado;
                            dependiente.Empleado.ApellidoPaterno = obj.ApellidoPaternoEmpleado;
                            dependiente.Empleado.ApellidoMaterno = obj.ApellidoMaternoEmpleado;

                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.EstadoCivil = obj.EstadoCivil;
                            dependiente.Genero = obj.Genero;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.RFC = obj.Rfc;

                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.IdDependienteTipo = obj.IdDependienteTipo.Value;
                            dependiente.DependienteTipo.Nombre = obj.Tipo;
                            dependiente.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");

                            result.Objects.Add(dependiente);
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

        public static ML.Result GetById(int? IdDependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.Dependientes.FromSqlRaw($"DependienteGetById '{IdDependiente}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();
                        var obj = query;
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();

                            dependiente.NumeroEmpleado = obj.NumeroEmpleado;

                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.Nombre = obj.NombreEmpleado;
                            dependiente.Empleado.ApellidoPaterno = obj.ApellidoPaternoEmpleado;
                            dependiente.Empleado.ApellidoMaterno = obj.ApellidoMaternoEmpleado;

                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.EstadoCivil = obj.EstadoCivil;
                            dependiente.Genero = obj.Genero;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.RFC = obj.Rfc;

                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.IdDependienteTipo = obj.IdDependienteTipo.Value;
                            dependiente.DependienteTipo.Nombre = obj.Tipo;
                            dependiente.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");

                            result.Object = dependiente;
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
