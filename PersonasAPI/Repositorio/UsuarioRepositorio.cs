using Microsoft.EntityFrameworkCore;
using PersonasAPI.Data;
using PersonasAPI.Modelos;
using System.Security.Cryptography;

namespace PersonasAPI.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private DataContext _dataContext;
        public UsuarioRepositorio(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<string> Login(string userName, string password)
        {
            var user = await _dataContext.Usuarios.FirstOrDefaultAsync(x =>
            x.UserName.ToLower().Equals(userName.ToLower()));
            if(user == null)
            {
                return "nouser";
            }
            else if(!VerificarPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return "wrongpassword";
            }
            else
            {
                return "ok";
            }
        }

        public async Task<int> Register(Usuarios usuario, string password)
        {
            try
            {
                if(await UserExist(usuario.UserName))
                {
                    return -1;
                }
                CrearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.PasswordHash = passwordHash;
                usuario.PasswordSalt = passwordSalt;
                await _dataContext.Usuarios.AddAsync(usuario);
                await _dataContext.SaveChangesAsync();
                return usuario.Id;
            }
            catch (Exception e)
            {
                return -500;
            }
        }

        public async Task<bool> UserExist(string userName)
        {
            if (await _dataContext.Usuarios.AnyAsync(usuario => usuario.UserName.ToLower().Equals(userName)))
            {
                return true;
            }
            return false;
        }
        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
        private bool VerificarPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i<hash.Length; i++)
                {
                    if (hash[i] != passwordHash[i])
                    {
                        return false;
                    }                    
                }
                return true;
            }
        }
    }
}
