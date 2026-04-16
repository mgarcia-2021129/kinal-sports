using System;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Entities;

public class UserRole
{
    [Key]
    [MaxLength(16)]
    public string Id {get; set;} = string.Empty;

    [Key]
    [MaxLength(16)]
    public String UserId {get; set;} = string.Empty;

    [Key]
    [MaxLength(16)]
    public String RoleId {get; set;} = string.Empty;

    //Esto se realiza por buena practica para auditoria

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;

    public User User {get;  set;} = null!; // SE COLOCA EL SIGNO DE EXLAMACION DE CIERRE POR SI VIENE NULO O NO LO IGNORE

    public Role Role {get; set;} = null!; // SE COLOCA EL SIGNO DE EXLAMACION DE CIERRE POR SI VIENE NULO O NO LO IGNORE
}
