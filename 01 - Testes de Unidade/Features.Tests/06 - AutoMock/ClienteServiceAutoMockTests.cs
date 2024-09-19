using Features.Clientes;
using MediatR;
using Moq;

namespace Features.Tests._06___AutoMock
{
    [Collection(nameof(ClienteAutoMockCollection))]
    public class ClienteServiceAutoMockTests
    {
        private readonly ClientesTestsAutoMockerFixture _clienteAutoMockCollection;
        private readonly ClienteService _clienteService;
        public ClienteServiceAutoMockTests(ClientesTestsAutoMockerFixture fixture)
        {
            _clienteAutoMockCollection = fixture;
            _clienteService = fixture.ObterClienteService();
        }

        [Fact(DisplayName = "Deve Adicionar Com Sucesso")]
        [Trait("Categoria", "ClienteService autoMocker")]
        public void ClienteService_Adicinar_DeveAdicionarComSucesso()
        {
            //Arrange
            var cliente = _clienteAutoMockCollection.GerarClienteValido();
            //Act 
            _clienteService.Adicionar(cliente);
            //Assert
            _clienteAutoMockCollection.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteAutoMockCollection.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "ClienteService autoMocker")]
        public void ClienteService_Adicionar_DeveFalharPoisClienteInvalido()
        {
            //Arrange 
            var cliente = _clienteAutoMockCollection.GerarClienteInvalido();
            //Act
            _clienteService.Adicionar(cliente);
            //Assert 
            _clienteAutoMockCollection.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteAutoMockCollection.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Todos Clientes Ativos")]
        [Trait("Categoria", "ClienteService autoMocker")]
        public void ClienteService_ObterTodosAtivos_DeveObterListaClientesAtivos()
        {
            //Arrange
            _clienteAutoMockCollection.Mocker.GetMock<IClienteRepository>()
                .Setup(repo => repo.ObterTodos()).Returns(_clienteAutoMockCollection.GerarClientes());

            //Act
            var clientes = _clienteService.ObterTodosAtivos();

            //Assert
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);
        }
    }
}
