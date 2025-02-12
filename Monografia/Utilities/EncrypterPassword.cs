

namespace Monografia.Utils
{
    public static class EncrypterPassword
    {

        public static string GenerarHash(string contrasena)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasena);
        }

        public static bool VerificarContraseña(string contrasenaIngresada, string hashAlmacenado)
        {
            return BCrypt.Net.BCrypt.Verify(contrasenaIngresada, hashAlmacenado);
        }
    }
}