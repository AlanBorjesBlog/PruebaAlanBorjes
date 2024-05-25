using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

public class Tbusuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int IdUsuario { get; set; }
    public string Nombre { get; set; }
    public string Usuario { get; set; }
    private string _contraseña;

    public string Contraseña
    {
        get => _contraseña;
        set => _contraseña = EncriptarContraseña(value);
    }

    private string EncriptarContraseña(string contraseña)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(contraseña);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
