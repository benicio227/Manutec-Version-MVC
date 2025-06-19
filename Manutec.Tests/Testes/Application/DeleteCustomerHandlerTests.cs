using Manutec.Application.Commands.CustomerEntity;
using Manutec.Core.Entities;
using Manutec.Core.Repositories;
using Moq;
using NSubstitute;

namespace Manutec.Tests.Testes.Application
{
    public class DeleteCustomerHandlerTests
    {
        [Fact]
        public async Task CustomerExists_Delete_Success()
        {
            //Cria um cliente de mentira
            var customer = new Customer(1, "Josefa", "josefa@hotmail.com", "(82)88765-7654");

            //Aqui eu crio um repositório falso, que vai se comportar do jeito que eu quiser.
            //Isso é útil para não depender do banco de dados de verdade
            var repository = Substitute.For<ICustomerRepository>();

            //Aqui eu digo para o repositório falso o que ele deve devolver
            //Toda vez que alguém chamar GetById, devolva esse cliente aqui que eu criei
            repository.GetById(Arg.Any<int>(), Arg.Any<int>()).Returns(Task.FromResult(customer));

            //Esse é o "músculo" que vai tentar deletar o cliente. Ele vai usar o repositório falso
            var handler = new DeleteCustomerHandler(repository);

            //Cria o comando para deletar o cliente
            //Quero deletar o cliente com ID 1, na oficina ID 1
            var command = new DeleteCustomerCommand
            (
                20,
                15
            );

            //Aqui eu mando o handler processar o comando
            //Ele vai: Chamar o repositório(GetById), Verificar se o cliente existe, deletar o cliente
            // devolver um resultado
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess); //A operação foi um sucesso?
            Assert.NotNull(result.Data); //O retorno tem os dados do cliente?
            Assert.Equal("Josefa", result.Data.Name); //O nome é o esperado?

            await repository.Received(1).Delete(customer); //A função delete foi mesmo chamada?

        }
    }
}
