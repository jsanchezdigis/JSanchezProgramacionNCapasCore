using DL;
using Microsoft.EntityFrameworkCore;
using ML;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioAdd " +
                        $"'{usuario.UserNombre}'," +
                        $"'{usuario.Nombre}'," +
                        $"'{usuario.ApellidoPaterno}'," +
                        $"'{usuario.ApellidoMaterno}'," +
                        $"'{usuario.Email}'," +
                        $"'{usuario.Password}'," +
                        $"'{usuario.FechaNacimiento}'," +
                        $"'{usuario.Sexo}'," +
                        $"'{usuario.Telefono}'," +
                        $"'{usuario.Celular}'," +
                        $"'{usuario.CURP}'," +
                        $"'{usuario.Imagen}'," +
                        $"'{usuario.Rol.IdRol}'," +
                        $"'{usuario.Direccion.Calle}'," +
                        $"'{usuario.Direccion.NumeroInterior}'," +
                        $"'{usuario.Direccion.NumeroExterior}'," +
                        $"'{usuario.Direccion.Colonia.IdColonia}'");
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

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioUpdate " +
                        $"'{usuario.IdUsuario}'," +
                        $"'{usuario.UserNombre}'," +
                        $"'{usuario.Nombre}'," +
                        $"'{usuario.ApellidoPaterno}'," +
                        $"'{usuario.ApellidoMaterno}'," +
                        $"'{usuario.Email}'," +
                        $"'{usuario.Password}'," +
                        $"'{usuario.FechaNacimiento}'," +
                        $"'{usuario.Sexo}'," +
                        $"'{usuario.Telefono}'," +
                        $"'{usuario.Celular}'," +
                        $"'{usuario.CURP}'," +
                        $"'{usuario.Imagen}'," +
                        $"'{usuario.Rol.IdRol}'," +
                        $"'{usuario.Direccion.Calle}'," +
                        $"'{usuario.Direccion.NumeroInterior}'," +
                        $"'{usuario.Direccion.NumeroExterior}'," +
                        $"'{usuario.Direccion.Colonia.IdColonia}'");
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

        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioDelete '{usuario.IdUsuario}'");
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

        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll " +
                        $"'{usuario.Nombre}', " +
                        $"'{usuario.ApellidoPaterno}', " +
                        $"'{usuario.ApellidoMaterno}'").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            usuario = new ML.Usuario();

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserNombre = obj.UserNombre;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Password = obj.Password;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.CURP = obj.Curp;
                            usuario.Imagen = obj.Imagen;

                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.NombreRol;
                            usuario.Status = obj.Status.Value;
                            //agrgar los campos faltantes y en byid
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion;//.Value;
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;//.Value;
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;//.Value;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;//.Value;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            result.Objects.Add(usuario);
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

        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById '{IdUsuario}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();
                        var obj = query;
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserNombre = obj.UserNombre;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Password = obj.Password;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.CURP = obj.Curp;
                            usuario.Imagen = obj.Imagen;

                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.NombreRol;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion;//.Value;
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;//.Value;
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;//.Value;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;//.Value;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;//.Value;

                            result.Object = usuario;
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

        public static ML.Result ChangeStatus(int IdUsuario, bool Status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioChangeStatus " +
                        $"'{IdUsuario}'," +
                        $"'{Status}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo realizar la consulta";
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

        public static ML.Result GetByName(string UserNombre)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezProgramacionNcapasContext context = new DL.JsanchezProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName '{UserNombre}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();
                        var obj = query;
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.UserNombre = obj.UserNombre;
                            usuario.Password = obj.Password;

                            result.Object = usuario;
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

        public static ML.Result ConvertXSLXtoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.UserNombre = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.FechaNacimiento = row[6].ToString();
                                usuario.Sexo = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.CURP = row[10].ToString();

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = byte.Parse(row[11].ToString());

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = int.Parse(row[15].ToString());

                                //usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                                //usuario.Direccion.Colonia.Municipio.IdMunicipio = int.Parse(row[16].ToString());

                                //usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                                //usuario.Direccion.Colonia.Municipio.Estado.IdEstado = int.Parse(row[17].ToString());

                                //usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                                //usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = int.Parse(row[18].ToString());

                                result.Objects.Add(usuario);
                            }

                            result.Correct = true;

                        }

                        result.Object = tableUsuario;

                        if (tableUsuario.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;


        }
        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (usuario.UserNombre == "")
                    {
                        error.Mensaje += "Ingresar el UserNombre,  ";
                    }
                    if (usuario.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el nombre,  ";
                    }
                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el apellido paterno, ";
                    }
                    if (usuario.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el apellido materno, ";
                    }
                    if (usuario.Email == "")
                    {
                        error.Mensaje += "Ingresar el email, ";
                    }
                    if (usuario.Password == "")
                    {
                        error.Mensaje += "Ingresar el password, ";
                    }
                    if (usuario.FechaNacimiento == "")
                    {
                        error.Mensaje += "Ingresar la fecha nacimiento, ";
                    }
                    if (usuario.Sexo == "")
                    {
                        error.Mensaje += "Ingresar el sexo, ";
                    }
                    if (usuario.Telefono == "")
                    {
                        error.Mensaje += "Ingresar el telefono, ";
                    }
                    if (usuario.Celular == "")
                    {
                        error.Mensaje += "Ingresar el celular, ";
                    }
                    if (usuario.CURP == "")
                    {
                        error.Mensaje += "Ingresar el CURP, ";
                    }
                    if (usuario.Rol.IdRol.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el IdRol, ";
                    }
                    if (usuario.Direccion.Calle == "")
                    {
                        error.Mensaje += "Ingresar la calle, ";
                    }
                    if (usuario.Direccion.NumeroInterior == "")
                    {
                        error.Mensaje += "Ingresar el numero interior, ";
                    }
                    if (usuario.Direccion.NumeroExterior == "")
                    {
                        error.Mensaje += "Ingresar el numero exterior, ";
                    }
                    if (usuario.Direccion.Colonia.IdColonia.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el IdColonia, ";
                    }
                    //if (usuario.Direccion.Colonia.Municipio.IdMunicipio.ToString() == "")
                    //{
                    //    error.Mensaje += "Ingresar el IdMunicipio ";
                    //}
                    //if (usuario.Direccion.Colonia.Municipio.Estado.IdEstado.ToString() == "")
                    //{
                    //    error.Mensaje += "Ingresar el IdEstado ";
                    //}
                    //if (usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais.ToString() == "")
                    //{
                    //    error.Mensaje += "Ingresar el IdPais ";
                    //}

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
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