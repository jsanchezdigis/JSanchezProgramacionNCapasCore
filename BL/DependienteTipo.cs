using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DependienteTipo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.DependienteTipos.FromSqlRaw($"DependienteTipoGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.DependienteTipo dependienteTipo = new ML.DependienteTipo();

                            dependienteTipo.IdDependienteTipo = obj.IdDependienteTipo;
                            dependienteTipo.Nombre = obj.Nombre;

                            result.Objects.Add(dependienteTipo);
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
