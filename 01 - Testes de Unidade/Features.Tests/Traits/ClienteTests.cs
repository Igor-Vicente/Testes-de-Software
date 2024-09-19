using Features.Clientes;

namespace Features.Tests.Traits
{
    public class ClienteTests
    {
        public ClienteTests()
        {
            //a classe será recriada a cada método de teste
            //assim qualquer objeto criado no construtor será recriado para cada teste
        }


        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            //Arrange 
            var clienteValido = new Cliente(Guid.NewGuid(), "igor", "vicente", DateTime.Now.AddYears(-30), "igor@gmail.com", true, DateTime.Now);
            //Act
            var result = clienteValido.EhValido();
            //Assert
            Assert.True(result);
            Assert.Equal(0, clienteValido.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Trait Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            //Arrange
            var cliente = new Cliente(Guid.NewGuid(), "", "", DateTime.Now, "", true, DateTime.Now);
            //Act
            var result = cliente.EhValido();
            //Assert
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
