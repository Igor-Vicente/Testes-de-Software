using Bogus;
using Bogus.DataSets;
using Features.Clientes;
using static Bogus.DataSets.Name;

namespace Features.Tests.Dados_Humanos
{
    [CollectionDefinition(nameof(ClienteBogusCollection))]
    public class ClienteBogusCollection : ICollectionFixture<ClienteTestsBogusFixture> { }
    public class ClienteTestsBogusFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            return GerarClientes(1, true).FirstOrDefault();
        }
        public Cliente GerarClienteInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(1, DateTime.Now.AddYears(1)),
                    "",
                    false,
                    DateTime.Now));

            return cliente;
        }
        public IEnumerable<Cliente> GerarClientes(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Gender>();
            var clientes = new Faker<Cliente>("pt_BR").CustomInstantiator(f => new Cliente(
                Guid.NewGuid(),
                f.Name.FirstName(genero),
                f.Name.LastName(genero),
                f.Date.Past(80, DateTime.Now.AddYears(-18)),
                "",
                ativo,
                DateTime.Now
                )).RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

            return clientes.Generate(quantidade);
        }

        public IEnumerable<Cliente> GerarClientes()
        {
            var clientes = new List<Cliente>();
            clientes.AddRange(GerarClientes(50, true));
            clientes.AddRange(GerarClientes(50, false));
            return clientes;
        }

        public void Dispose()
        {
        }
    }
}
