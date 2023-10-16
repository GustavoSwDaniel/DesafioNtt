using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesafioNtt.Application.DTOs.Requests.EstablishmentRequestDTO;
using DesafioNtt.Application.ExternalService;
using DesafioNtt.Application.Interfaces.IViaCepService;
using DesafioNtt.Application.UseCase.EstablishmentUseCases;
using DesafioNtt.Domain.Entities.Address;
using DesafioNtt.Domain.Entities.Phone;
using DesafioNtt.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace DesafioNtt.Tests.Applications.Establishment
{
    [TestClass]
    public class EstablishmentRegisterTests
    {
        [TestMethod]
        public async Task Execute_WithValidData_ShouldReturnSuccessfulResponse()
        {
            // Arrange
            var establishmentRepositoryMock = new Mock<IRepository<Domain.Entities.Establishment.Establishment>>();
            establishmentRepositoryMock.Setup(x => x.Add(It.IsAny<Domain.Entities.Establishment.Establishment>()))
                .ReturnsAsync(true);
            var addressRepositoryMock = new Mock<IRepository<Domain.Entities.Address.Address>>();
            var phoneRepositoryMock = new Mock<IRepository<Phone>>();
            var viaCepServiceMock = new Mock<IViaCepService>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>();
            

            viaCepServiceMock.Setup(x => x.GetCep(It.IsAny<string>()))
                .ReturnsAsync(new ViaCepResponse { Cep = "12345-678", Logradouro = "Rua Teste", Bairro = "Bairro Teste", Localidade = "Cidade Teste", UF = "UF" });

            var establishmentRegister = new EstablishmentRegister(
                establishmentRepositoryMock.Object,
                addressRepositoryMock.Object,
                phoneRepositoryMock.Object,
                viaCepServiceMock.Object);

            var establishmentData = new EstablishmentRegistration
            {
                Name = "Test Establishment",
                EstablishmentPhone = "11-987654321",
                EstablishmentStartDate = DateTime.Now,
                EstablishmentAddress = new EstablishmentAddress
                {
                    Cep = "12345-678",
                    Number = "123",
                    Complement = "Complemento"
                }
            };

            // Act
            var result = await establishmentRegister.Execute(establishmentData, "test@test.com");

            // Assert
            Assert.IsTrue(result.Sucess);
            establishmentRepositoryMock.Verify(x => x.Add(It.IsAny<Domain.Entities.Establishment.Establishment>()), Times.Once);
            addressRepositoryMock.Verify(x => x.Add(It.IsAny<Address>()), Times.Once);
            phoneRepositoryMock.Verify(x => x.Add(It.IsAny<Phone>()), Times.Once);
        }
    }
}
