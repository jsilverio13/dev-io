namespace SOLID.DIP.Violacao
{
    public class ClienteService
    {
        public static string AdicionarCliente(Cliente cliente)
        {
            if (!cliente.Validar())
                return @"Dados inválidos";

            var repo = new ClienteRepository();
            ClienteRepository.AdicionarCliente(cliente);

            EmailServices.Enviar(@"empresa@empresa.com", cliente.Email.Endereco, @"Bem Vindo", @"Parabéns está Cadastrado");

            return @"Cliente cadastrado com sucesso";
        }
    }
}