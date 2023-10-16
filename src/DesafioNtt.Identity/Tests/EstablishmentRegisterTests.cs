using DesafioNtt.Application.DTOs.Requests.EstablishmentRequestDTO;
using DesafioNtt.Application.ExternalService;
using DesafioNtt.Application.UseCase.EstablishmentUseCases;
using DesafioNtt.Domain.Entities.Address;
using DesafioNtt.Domain.Entities.Establishment;
using DesafioNtt.Domain.Entities.Phone;
using DesafioNtt.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace DesafioNtt.Identity.Tests;

public class EstablishmentRegisterTests
{
    [Fact]
    public async Task Execute_ValidEstablishmentData_ReturnsSuccessResponse()
    {
        // Arrange
        var establishmentRepoMock = new Mock<IRepository<Establishment>>();
        establishmentRepoMock.Setup(repo => repo.Add(It.IsAny<Establishment>())).ReturnsAsync(true);

        var addressRepoMock = new Mock<IRepository<Address>>();
        addressRepoMock.Setup(repo => repo.Add(It.IsAny<Address>())).ReturnsAsync(true);

        var phoneRepoMock = new Mock<IRepository<Phone>>();
        phoneRepoMock.Setup(repo => repo.Add(It.IsAny<Phone>())).ReturnsAsync(true);

        var viaCepServiceMock = new Mock<ViaCepService>();
        viaCepServiceMock.Setup(service => service.GetCep(It.IsAny<string>())).ReturnsAsync(new ViaCepResponse());
        
        var userManagerMock = new Mock<UserManager<IdentityUser>>();

        var establishmentRegister = new EstablishmentRegister(
            establishmentRepoMock.Object,
            addressRepoMock.Object,
            phoneRepoMock.Object,
            viaCepServiceMock.Object
        );

        var establishmentData = new EstablishmentRegistration
        {
            Name = "Test Establishment",
            EstablishmentStartDate = DateTime.Now,
            EstablishmentPhone = "11-123456789",
            EstablishmentAddress = new EstablishmentAddress
            {
                Cep = "12345678",
                Number = "123",
                Complement = "Apt 1"
            }
        };

        // Act
        var result = await establishmentRegister.Execute(establishmentData, "test@test.com");

        // Assert
        Assert.True(result.Sucess);
    }

    // Add more tests as needed
}