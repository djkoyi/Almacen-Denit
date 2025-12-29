namespace Almacen.Data
{
    public class AuthenticationService
    {
        public async Task<bool> Login(string email, string password)
        {
            // Aquí debes verificar las credenciales en tu base de datos.
            // Por simplicidad, solo estoy verificando contra valores codificados.

            if (email == "test@example.com" && password == "password123")
            {
                // En un escenario real, aquí establecerías una cookie de sesión o cualquier otro mecanismo de autenticación.
                return true;
            }
            return false;
        }
    }
}
