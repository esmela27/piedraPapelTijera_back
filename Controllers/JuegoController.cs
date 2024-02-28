using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("NuevaPolitica")]
    public class JuegoController : Controller
    {
     
        private static int puntos1 = 0;
        private static int puntos2 = 0;
        private static bool ganador1 = false;
        private static bool ganador2 = false;
        [HttpGet]
        public IActionResult Jugar(string jugadaUsuario1, string jugadaUsuario2)
        {


            if (string.IsNullOrEmpty(jugadaUsuario1) || string.IsNullOrEmpty(jugadaUsuario2))
            {
                return BadRequest("La jugada del usuario no puede estar vacía.");
            }


            var resultadoFinal = DeterminarResultado(jugadaUsuario1, jugadaUsuario2);


            return Ok(new { resultadoFinal, jugadaUsuario1, jugadaUsuario2 });
        }

        private string ObtenerJugadaComputadora()
        {


            string[] jugadasPosibles = { "piedra", "papel", "tijera" };
            var indiceJugadaComputadora = new Random().Next(0, jugadasPosibles.Length);

            return jugadasPosibles[indiceJugadaComputadora];
        }

        private string DeterminarResultado(string jugadaUsuario, string jugadaComputadora)
        {
            string resultado;

            if (jugadaUsuario == jugadaComputadora)
            {
                resultado = "Empate";
            }
            else if ((jugadaUsuario == "piedra" && jugadaComputadora == "tijera") ||
                     (jugadaUsuario == "papel" && jugadaComputadora == "piedra") ||
                     (jugadaUsuario == "tijera" && jugadaComputadora == "papel"))
            {

                resultado = "Gana jugador 1";
                ++ puntos1;
                if (puntos1 ==3) 
                {
                    ganador1 = true;
                }
            }
            else
            {
                resultado = "Gana jugador 2";
                 ++puntos2 ;
                if (puntos2 == 3)
                {
                    ganador2 = true;
                }
            }
            string resultadoJson = JsonConvert.SerializeObject(new { resultado, puntos1, puntos2, ganador1, ganador2 });
            return resultadoJson;
        }



        [HttpPost("reiniciar")]
        public IActionResult ReiniciarContador()
        {
           puntos1 = 0;
          puntos2 = 0;
           ganador1 = false;
           ganador2 = false;
            return Ok(new { mensaje = "Iniciado correctamente" });
        }
    }
}
