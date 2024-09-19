using Features.Clientes;

namespace Features.Tests.Fixtures
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsBogusFixture> { }

    public class ClienteTestsBogusFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            return new Cliente(Guid.NewGuid(), "igor", "vicente", DateTime.Now.AddYears(-30), "igor@gmail.com", true, DateTime.Now);
        }
        public Cliente GerarClienteInvalido()
        {
            return new Cliente(Guid.NewGuid(), "", "", DateTime.Now, "", true, DateTime.Now);
        }

        public void Dispose()
        {
        }
    }
}
