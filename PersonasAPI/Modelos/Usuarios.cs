using System.ComponentModel.DataAnnotations;

namespace PersonasAPI.Modelos
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public Byte[] PasswordHash { get; set; }
        public Byte[] PasswordSalt { get; set; }
    }
}
