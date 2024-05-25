using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ProductoController : ControllerBase
{
    private readonly AplicationDbContext _context;

    public ProductoController(AplicationDbContext context)
    {
        _context = context;
    }

    // Obtener todos los productos
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var productos = await _context.Productos.ToListAsync();

        if (productos == null || !productos.Any())
        {
            return NoContent();
        }

        return Ok(productos);
    }

    // Crear un nuevo producto
    [HttpPost("Crear")]
    public async Task<IActionResult> Crear([FromBody] Producto producto)
    {
        if (producto == null || !ModelState.IsValid)
        {
            return BadRequest("Los datos del producto son inválidos.");
        }
        var existingProducto = await _context.Productos.FirstOrDefaultAsync(p => p.SKU == producto.SKU || p.Nombre == producto.Nombre);
        if (existingProducto != null)
        {
            return Conflict("Ya existe un producto con el mismo SKU o Nombre.");
        }

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return StatusCode((int)HttpStatusCode.Created);
    }

    // Mostrar detalles de un producto por su Id
    [HttpGet("Mostrar/{id}")]
    public async Task<IActionResult> Mostrar(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null)
        {
            return NotFound();
        }
        return Ok(producto);
    }

    // Eliminar un producto por su Id
    [HttpDelete("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null)
        {
            return NotFound();
        }
        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return Ok();
    }

    // Actualizar un producto
    [HttpPut("Actualizar")]
    public async Task<IActionResult> Actualizar([FromBody] Producto producto)
    {
        if (producto == null || !ModelState.IsValid)
        {
            return BadRequest("Los datos del producto son inválidos.");
        }

        var existingProducto = await _context.Productos.FindAsync(producto.IdProducto);
        if (existingProducto == null)
        {
            return NotFound("El producto no existe.");
        }

        existingProducto.SKU = producto.SKU;
        existingProducto.Nombre = producto.Nombre;
        existingProducto.Costo = producto.Costo;
        existingProducto.PrecioVenta = producto.PrecioVenta;
        existingProducto.ClaveSAT = producto.ClaveSAT;
        existingProducto.ClaveKey = producto.ClaveKey;

        await _context.SaveChangesAsync();
        return Ok("El producto se actualizó correctamente.");
    }
}
