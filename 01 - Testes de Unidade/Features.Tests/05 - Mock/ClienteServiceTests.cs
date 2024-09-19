using Features.Clientes;
using Features.Tests.Dados_Humanos;
using MediatR;
using Moq;

namespace Features.Tests.Mock
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceTests
    {
        private readonly ClienteTestsBogusFixture _clienteTestsBogus;

        public ClienteServiceTests(ClienteTestsBogusFixture clienteTestsBogusFixture)
        {
            _clienteTestsBogus = clienteTestsBogusFixture;
        }


        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "ClienteService Mock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            var clienteRepository = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();
            var clienteService = new ClienteService(clienteRepository.Object, mediatr.Object);

            //Act
            clienteService.Adicionar(cliente);

            //Assert
            clienteRepository.Verify(r => r.Adicionar(cliente), Times.Once);
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "ClienteService Mock Tests")]
        public void ClienteService_Adicionar_DeveFalharPoisClienteInvalido()
        {
            //Arrange 
            var cliente = _clienteTestsBogus.GerarClienteInvalido();
            var clienteRepository = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();
            var clienteService = new ClienteService(clienteRepository.Object, mediatr.Object);
            //Act
            clienteService.Adicionar(cliente);
            //Assert 
            clienteRepository.Verify(r => r.Adicionar(cliente), Times.Never);
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Todos Clientes Ativos")]
        [Trait("Categoria", "ClienteService Mock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveObterListaClientesAtivos()
        {
            //Arrange
            var clienteRepository = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();
            var clienteService = new ClienteService(clienteRepository.Object, mediatr.Object);
            clienteRepository.Setup(repo => repo.ObterTodos()).Returns(_clienteTestsBogus.GerarClientes());

            //Act
            var clientes = clienteService.ObterTodosAtivos();

            //Assert
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);
        }
    }
}
