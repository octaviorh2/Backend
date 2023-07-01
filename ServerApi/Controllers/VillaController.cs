using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Datos;
using ServerApi.Modelos;
using ServerApi.Modelos.Dto;

namespace ServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        public VillaController(ILogger<VillaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVilla()
        {
            _logger.LogInformation("obtener la lista ");
            return Ok(VillaStore.vlisa);
        }

        [HttpGet("id:int", Name = "Getvilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDto> GetVilla(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.vlisa.FirstOrDefault(x => x.Id == id);
            //villa is null ? NotFound: Ok(villa);
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]//cabeceras
        //docuemntacion de estados
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villaDto) {

            //validacio de los modelos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //validaciones personalizadas
            if (VillaStore.vlisa.FirstOrDefault(v => v.Nombre?.ToLower() == villaDto.Nombre?.ToLower()) != null)
            {
                ModelState.AddModelError("Nombre Existente", "El nombre ya se ecuentra registrado");
                return BadRequest(ModelState);
            }
            if (villaDto == null) {
                return BadRequest(villaDto);
            }
            if (villaDto.Id > 0) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDto.Id = VillaStore.vlisa.Count + 1;
            VillaStore.vlisa.Add(villaDto);
            //retirno de la ruta
            return CreatedAtRoute("Getvilla", new { id = villaDto.Id }, villaDto);
        }
        
        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id) {

            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.vlisa.FirstOrDefault(x => x.Id == id);
            if (villa == null) {
                return NotFound();
            }
            VillaStore.vlisa.Remove(villa);
            return NoContent();
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PutVilla( int id , [FromBody]VillaDto villaDto) {
            if (villaDto ==null || id != villaDto.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.vlisa.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            villa.Nombre = villaDto.Nombre;
            villa.Ocupantes = villaDto.Ocupantes;
            villa.MetrosCuadrados = villaDto.MetrosCuadrados;  
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PathVilla(int id, JsonPatchDocument<VillaDto> pathDto)
        {
            if (pathDto == null || id == 0 )
            {
                return BadRequest();
            }
            var villa = VillaStore.vlisa.FirstOrDefault(x => x.Id == id);
            pathDto.ApplyTo(villa , ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
         
            return NoContent();
        }
    }
}
