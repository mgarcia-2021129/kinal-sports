using System;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Entities;

public class User
{
    [Key]
    [MaxLength(16)]
    public String Id {get; set;} = string.Empty; // Se iniicializa con un strinf vacio para evitar nulls

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(25, ErrorMessage = "El nombre no puede tener mas de 25 caracteres")]
    public String Name {get; set;} = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [MaxLength(50, ErrorMessage = "El apellido no puede tener mas de 50 caracteres")]
    public String Surname {get; set;} = string.Empty;

    [Required(ErrorMessage = "El username es obligatorio")]
    [MaxLength(25, ErrorMessage = "El username no puede tener mas de 25 caracteres")]
    public String Username {get; set;} = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato valido")]
    [MaxLength(150, ErrorMessage = "El email no puede tener mas de 150 caracteres")]
    public String Email {get; set;} = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
    [MaxLength(255, ErrorMessage = "La contraseña no puede tener mas de 50 caracteres")]
    public String Password {get; set;} = string.Empty;

    public bool Status {get; set;} = false; // Por defecto el usuario se mantiene inactivo

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;

    public ICollection<UserRole> UserRoles {get; set;} = [];  // de esta manera se coloca cuando es de uno a muchos y es por eso mismo que recibe un array.

    public  UserProfile UserProfile{get; set;} = null!;

    public UserEmail UserEmail {get; set;} = null!;

    public UserPasswordReset UserPasswordReset {get; set;} = null!; // de esta manera se coloca cuando es de uno a uno ya que solamente recibe un objeto.
}

    
