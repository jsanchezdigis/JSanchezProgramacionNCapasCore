using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required]
        //[RegularExpression("/s/",ErrorMessage = "Ingrese el UserNombre")] este no
        public string UserNombre { get; set; }
        //[Required(ErrorMessage = "Ingrese el Nombre")]
        //[RegularExpression("[a-zA-Z]{1,20}")]
        public string Nombre { get; set; }
        //[Required(ErrorMessage = "Ingrese el App")]
        //[RegularExpression("[a-zA-Z]{1,20}")]
        public string ApellidoPaterno { get; set; }
        //[Required(ErrorMessage = "Ingrese el Apm")]
        //[RegularExpression("[a-zA-Z]{1,20}")]
        public string ApellidoMaterno { get; set; }
        //[Required(ErrorMessage = "Ingrese correctamente el Email")]
        //[RegularExpression("^\\S+@\\S+\\.\\S+$")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Ingrese correctamente el password")]
        //[RegularExpression("(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,10}")]
        public string Password { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        //[Required(ErrorMessage = "Ingrese correctamente el telefono")]
        //[RegularExpression("[0-9]{10}")]
        public string Telefono { get; set; }
        //[Required(ErrorMessage = "Ingrese correctamente el celular")]
        //[RegularExpression("[0-9]{10}")]
        public string Celular { get; set; }
        //[Required(ErrorMessage = "Ingrese correctamente el CURP")]
        //[RegularExpression("[a-zA-Z]{18}")]
        public string CURP { get; set; }
        public string Imagen { get; set; }
        public bool Status { get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }
        public ML.Aseguradora Aseguradora { get; set; }
        public List<object> Usuarios { get; set; }
    }
}