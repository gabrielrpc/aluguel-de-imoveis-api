namespace aluguel_de_imoveis.Communication.Request
{
    public class RequestAtualizarImovelJson : RequestImovelJson
    {
        public Guid ImovelId { get; set; }
    }
}
