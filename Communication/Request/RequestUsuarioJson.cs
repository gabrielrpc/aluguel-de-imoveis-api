namespace aluguel_de_imoveis.Communication.Request
{
    public class RequestUsuarioJson
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int Tipo { get; set; }
    }
}
