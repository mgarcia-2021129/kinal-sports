using System;
using AuthService.Domain.Entities;
namespace AuthService.Domain.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string roleName); //Esto va buscar el rol por nombre (pueda que exista puede que no).

    Task<int> CountUsersInRoleAsync(string roleName); //Esto va devolver la cantidad de usuarios que esten asignados a un rol.

    Task<IReadOnlyList<User>> GetUsersByRolAsync(string roleName); //Obtiene todos los usuario que pertecen a un rol especifico

    Task<IReadOnlyList<string>> GetUserRoleNameAsync(string userId); // Este obtiene los nombre de los roles que estan asignados a un usuario.
}

