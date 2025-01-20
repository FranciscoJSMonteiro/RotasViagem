using Moq;
using RotasViagem;
using RotasViagem.Aplication.Services;
using RotasViagem.Entidade;
using RotasViagem.Infra.Persistence;
using RotasViagem.Aplication.Interfaces;

namespace RotasViagem.Test
{
    public class RotasServiceTest
    {

        private readonly Mock<IRotaRepository> _mockRepository;
        private readonly RotaService _service;
      

        public RotasServiceTest()
        {

            _mockRepository = new Mock<IRotaRepository>();  // Configurando Mock
            _service = new RotaService(_mockRepository.Object);  // Injetando Mock no serviço
           
        }

        [Fact(DisplayName = "Deve retornar a rota mais barata para GRU-CDG")]


        public void FindBestRoute_WhenCalledWithValidRoutes_ShouldReturnCheapestRoute()
        {

           // Arrange
            _mockRepository.Setup(repo => repo.PegarTodasRotas()).Returns(new List<Rotas>
            {
                new Rotas("GRU", "BRC", 10),
                new Rotas("BRC", "SCL", 5),
                new Rotas("SCL", "CDG", 20),
                new Rotas("GRU", "CDG", 75)
            });
            _mockRepository.Setup(repo => repo.EncontrarMelhorRota("GRU", "CDG"))
                  .Returns("GRU - BRC - SCL - CDG ao custo de $35");
            //act

            var result =  _service.EncontrarMelhorRota("GRU", "CDG");
            
            Assert.NotNull(result); // Garante que o resultado não é nulo
            
            //assert
            Assert.Equal("GRU - BRC - SCL - CDG ao custo de $35", result);

            

        }
    }
}