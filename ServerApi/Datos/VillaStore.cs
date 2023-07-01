using ServerApi.Modelos.Dto;

namespace ServerApi.Datos
{
    public class VillaStore
    {

        public static  List<VillaDto> vlisa =  new List<VillaDto> {

             new VillaDto { Id = 1, Nombre = "casa campo" , MetrosCuadrados = 50 , Ocupantes = 30},
             new VillaDto { Id = 2, Nombre = "los campanales", MetrosCuadrados = 50 , Ocupantes = 30}

        };
    }
}
