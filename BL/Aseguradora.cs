using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Aseguradora
    {
        public static ML.Result Add(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"AseguradoraAdd " +
                        $"'{aseguradora.Nombre}'," +
                        $"'{aseguradora.Usuario.IdUsuario}'");
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

        public static ML.Result Update(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"AseguradoraUpdate " +
                        $"'{aseguradora.IdAseguradora}'," +
                        $"'{aseguradora.Nombre}'," +
                        $"'{aseguradora.Usuario.IdUsuario}'");
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

        public static ML.Result Delete(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"AseguradoraDelete '{aseguradora.IdAseguradora}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Elimino el registro";
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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.Aseguradoras.FromSqlRaw($"AseguradoraGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();

                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = obj.FechaCreacion.ToString();
                            aseguradora.FechaModificacion = obj.FechaModificacion.ToString();//Actualizar sql

                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = obj.IdUsuario.Value;
                            aseguradora.Usuario.Nombre = obj.NombreUsuario;

                            result.Objects.Add(aseguradora);
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

        public static ML.Result GetById(int IdAseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.Aseguradoras.FromSqlRaw($"AseguradoraGetById '{IdAseguradora}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();
                        var obj = query;
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();

                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = obj.FechaCreacion.ToString();
                            aseguradora.FechaModificacion = obj.FechaModificacion.ToString();//Actualizar sql

                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = obj.IdUsuario.Value;
                            aseguradora.Usuario.Nombre = obj.NombreUsuario;

                            result.Object = aseguradora;
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
