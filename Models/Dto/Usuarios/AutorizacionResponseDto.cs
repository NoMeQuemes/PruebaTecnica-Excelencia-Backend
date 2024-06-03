namespace Prueba_Tecnica_Api.Models.Dto.Usuarios
{
    public class AutorizacionResponseDto
    {

        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public bool Resultado {  get; set; }
        public string Msg {  get; set; }

    }
}
