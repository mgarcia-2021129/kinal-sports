namespace AuthService.Application.Interfaces;


//Esta interfaz define los métodos para hashear y verificar contraseñas.
public interface IPasswordHashService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
