namespace Features.Tests.Order
{
    /* Testes de Unidade não necessitam de ordenação, visto que testam uma única unidade do código 
     * se um teste de unidade depende de outro teste, então, ou não é teste de unidade (e sim de integração)
     * ou seu teste foi escrito errado.
     */
    [TestCaseOrderer("Features.Tests.Order.PriorityOrderer", "Features.Tests")]
    public class OrdemTestes
    {
        public static bool Teste1Chamado;
        public static bool Teste2Chamado;
        public static bool Teste3Chamado;

        [Fact(DisplayName = "Teste 03"), TestPriority(3)]
        [Trait("Categoria", "Ordem dos Testes")]
        public void Teste03()
        {
            Teste3Chamado = true;
            Assert.True(Teste1Chamado);
            Assert.True(Teste2Chamado);
            Assert.True(Teste3Chamado);
        }

        [Fact(DisplayName = "Teste 02"), TestPriority(2)]
        [Trait("Categoria", "Ordem dos Testes")]
        public void Teste02()
        {
            Teste2Chamado = true;
            Assert.True(Teste1Chamado);
            Assert.True(Teste2Chamado);
            Assert.False(Teste3Chamado);
        }

        [Fact(DisplayName = "Teste 01"), TestPriority(1)]
        [Trait("Categoria", "Ordem dos Testes")]
        public void Teste01()
        {
            Teste1Chamado = true;
            Assert.True(Teste1Chamado);
            Assert.False(Teste2Chamado);
            Assert.False(Teste3Chamado);
        }
    }
}
