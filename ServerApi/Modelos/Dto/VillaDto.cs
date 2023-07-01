using System.ComponentModel.DataAnnotations;

namespace ServerApi.Modelos.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Nombre { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
    }
}
