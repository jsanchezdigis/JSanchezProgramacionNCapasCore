using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.Empresas.FromSqlRaw($"EmpresaGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();

                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.Email = obj.Email;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Logo = obj.Logo;

                            result.Objects.Add(empresa);
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
