using aluguel_de_imoveis.Models;

namespace aluguel_de_imoveis.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> CadastrarUsuario(Usuario usuario);

        Task<bool> VerificarEmailExistente(string email);

        Task<Usuario?> ObterUsuarioPorEmail(string email);
    }
}
