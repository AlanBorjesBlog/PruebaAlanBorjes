using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Producto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int IdProducto { get; set; }
    public string SKU { get; set; }
    public string Nombre { get; set; }
    public decimal Costo { get; set; }
    public decimal PrecioVenta { get; set; }
    public string ClaveSAT { get; set; }
    public string ClaveKey { get; set; }
}
