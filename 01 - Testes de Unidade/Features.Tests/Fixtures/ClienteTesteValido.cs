namespace Features.Tests.Fixtures
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTesteValido
    {
        private readonly ClienteTestsBogusFixture _clienteTestsFixture;

        public ClienteTesteValido(ClienteTestsBogusFixture clienteTestsFixture)
        {
            //usando fixture mantemos o estado do objeto para todo teste que usar da coleção,
            //por exemplo toda classe que usar a coleção 'ClienteCollection' compartilhará o mesmo Cliente
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            //Arrange 
            var clienteValido = _clienteTestsFixture.GerarClienteValido();
            //Act
            var result = clienteValido.EhValido();
            //Assert
            Assert.True(result);
            Assert.Equal(0, clienteValido.ValidationResult.Errors.Count);
        }
    }
}
