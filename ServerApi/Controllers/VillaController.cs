using Microsoft.AspNetCore.Http;
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


        [HttpGet]
        public IEnumerable<VillaDto> GetVilla()
        {
            return VillaStore.vlisa;

        }

    }
}
