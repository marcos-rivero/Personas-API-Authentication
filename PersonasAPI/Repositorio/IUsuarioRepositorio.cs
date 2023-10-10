using PersonasAPI.Modelos;

namespace PersonasAPI.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<int> Register(Usuarios usuario, string password);
        Task<string> Login(string userName, string password);
        Task<bool> UserExist(string userName);
    }
}
