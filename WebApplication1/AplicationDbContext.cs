using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    public DbSet<Tbusuario> Usuarios { get; set; }
    public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        // Datos para la entidad Tbusuario
        modelBuilder.Entity<Tbusuario>().HasData(
                new Tbusuario
                {
                    IdUsuario = 1,
                    Nombre = "Usuario1",
                    Contraseña = "password1", // Recuerda encriptar la contraseña antes de agregarla
                    Usuario = "user1"
                },
                new Tbusuario
                {
                    IdUsuario = 2,
                    Nombre = "Usuario2",
                    Contraseña = "password2",
                    Usuario = "user2"
                }
            );

        modelBuilder.Entity<Producto>().HasData(
          new Producto
          {
              IdProducto = 1,
              SKU = "SKU1",
              Nombre = "Producto1",
              Costo = 10.50m,
              PrecioVenta = 20.00m,
              ClaveSAT = "ClaveSAT1",
              ClaveKey = "ClaveKey1"
          },
          new Producto
          {
              IdProducto = 2,
              SKU = "SKU2",
              Nombre = "Producto2",
              Costo = 15.75m,
              PrecioVenta = 25.00m,
              ClaveSAT = "ClaveSAT2",
              ClaveKey = "ClaveKey2"
              }
          );
            base.OnModelCreating(modelBuilder);
        }
    }
