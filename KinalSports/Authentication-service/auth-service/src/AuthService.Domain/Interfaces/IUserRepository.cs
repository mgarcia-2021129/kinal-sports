using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user); // Devuelve el usuario creado con su Id asignada
    Task<User> GetByIdAsync(string id); // Devuelve el usuario con el Id especificado
    Task<User?> GetByEmailAsync(string email); // Devuelve el usuario con el email especificado
    Task<User?> GetByUsernameAsync(string username);// Devuelve el usuario con el username especificado
    Task<User?> GetByEmailVerificationTokenAsync(string token);// Devuelve el usuario con el token de verificación de email especificado
    Task<User?> GetByPasswordResetTokenAsync(string token);// Devuelve el usuario con el token de reseteo de contraseña especificado
    Task<bool> ExistsByEmailAsync(string email);// Verifica si existe un usuario con el email especificado
    Task<bool> ExistsByUsernameAsync(string username);// Verifica si existe un usuario con el username especificado
    Task<User> UpdateAsync(User user);// Actualiza la información del usuario
    Task<bool> DeleteAsync(string id);// Elimina el usuario con el Id especificado
    Task UpdateUserRoleAsync(string userId, string roleId);// Asigna un rol al usuario
}
