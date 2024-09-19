namespace Features.Tests.Dados_Humanos
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteBogusTests
    {
        private readonly ClienteTestsBogusFixture _clienteTestsBogus;

        public ClienteBogusTests(ClienteTestsBogusFixture clienteTestsFixture)
        {
            _clienteTestsBogus = clienteTestsFixture;
        }

        [Fact(DisplayName = "Novo Cliente Válido (Bogus)")]
        [Trait("Categoria", "Dados Humanos")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            //Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            //Act
            var result = cliente.EhValido();
            //Assert
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
