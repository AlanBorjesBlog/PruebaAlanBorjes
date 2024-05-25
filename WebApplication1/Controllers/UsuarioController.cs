using System;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly AplicationDbContext _context;

    public UsuarioController(AplicationDbContext context)
    {
        _context = context;
    }

    // Obtener todos los usuarios
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var usuarios = await _context.Usuarios.ToListAsync();

        if (usuarios == null || !usuarios.Any())
        {
            return NoContent();
        }

        return Ok(usuarios);
    }

    // Crear un nuevo usuario
    [HttpPost("Crear")]
    public async Task<IActionResult> Crear([FromBody] Tbusuario usuario)
    {
        if (usuario == null)
        {
            return BadRequest();
        }

        var existingUsuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Usuario == usuario.Usuario);
        if (existingUsuario != null)
        {
            return Conflict();
        }

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return StatusCode((int)HttpStatusCode.Created);
    }

    // Mostrar detalles de un usuario por su Id
    [HttpGet("Mostrar/{id}")]
    public async Task<IActionResult> Mostrar(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }

    // Eliminar un usuario por su Id
    [HttpDelete("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return Ok();
    }

    // Actualizar un usuario
    [HttpPut("Actualizar")]
    public async Task<IActionResult> Actualizar([FromBody] Tbusuario usuario)
    {
        if (usuario == null)
        {
            return BadRequest();
        }
        var existingUsuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Usuario == usuario.Usuario && u.IdUsuario != usuario.IdUsuario);
        if (existingUsuario != null)
        {
            return Conflict("Ya existe un usuario con el mismo nombre.");
        }
        var usuarioToUpdate = await _context.Usuarios.FindAsync(usuario.IdUsuario);
        if (usuarioToUpdate == null)
        {
            return NotFound();
        }
        usuarioToUpdate.Nombre = usuario.Nombre;
        usuarioToUpdate.Contraseña = usuario.Contraseña;
        usuarioToUpdate.Usuario = usuario.Usuario;

        await _context.SaveChangesAsync();
        return Ok();
    }
}