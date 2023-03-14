using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetById(int IdMunicipio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio '{IdMunicipio}'").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {

                            ML.Colonia colonia = new ML.Colonia();

                            colonia.IdColonia = obj.IdColonia;
                            colonia.Nombre = obj.Nombre;

                            colonia.Municipio = new ML.Municipio();
                            colonia.Municipio.IdMunicipio = obj.IdMunicipio.Value;

                            result.Objects.Add(colonia);
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
