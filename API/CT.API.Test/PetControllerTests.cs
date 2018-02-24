using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

using ABS = CT.Service.Abstract;
using MD = CT.Models;
using VMD = CT.API.ViewModels;

namespace CT.API.Test
{
    public class PetControllerTests
    {
        [Fact]
        public async Task GetOwnerGengerPetsByPetType_Return_Values_For_PetType()
        {
            // Arrange
            var mockSvc = new Mock<ABS.IPetService>();
            mockSvc.Setup(svc => (svc.GetPetsByPetTypeAsync("Cat"))).Returns(Task.FromResult(await GetPetsForCat()));

            var logger = new Mock<ILogger<Controllers.PetsController>>();

            var controller = new Controllers.PetsController(mockSvc.Object, logger.Object);

            // Act
            var result = await controller.GetOwnerGengerPetsByPetType("Cat");

            // Assert
            //Is return OK result
            var okResult = Assert.IsType<OkObjectResult>(result);

            var actual = okResult.Value as List<VMD.OwnerViewModel>;
            var expected = GetOwnerGenderViewModelList(await GetPetsForCat());

            //Is return the accepted object list
            Assert.Equal(expected.Count, actual.Count);

            Assert.Equal(expected[0].Gender, actual[0].Gender);

            Assert.Equal(expected[0].Pets.ToList()[0].Type, actual[0].Pets.ToList()[0].Type);
            Assert.Equal(expected[0].Pets.ToList()[0].Name, actual[0].Pets.ToList()[0].Name);

            Assert.Equal(expected[0].Pets.ToList()[1].Type, actual[0].Pets.ToList()[1].Type);
            Assert.Equal(expected[0].Pets.ToList()[1].Name, actual[0].Pets.ToList()[1].Name);

            Assert.Equal(expected[0].Pets.ToList()[2].Type, actual[0].Pets.ToList()[2].Type);
            Assert.Equal(expected[0].Pets.ToList()[2].Name, actual[0].Pets.ToList()[2].Name);

            Assert.Equal(expected[1].Gender, actual[1].Gender);

            Assert.Equal(expected[1].Pets.ToList()[0].Type, actual[1].Pets.ToList()[0].Type);
            Assert.Equal(expected[1].Pets.ToList()[0].Name, actual[1].Pets.ToList()[0].Name);

        }

        [Fact]
        public async Task GetOwnerGengerPetsByPetType_BadRequest_For_Empty_PetType()
        {
            // Arrange
            var mockSvc = new Mock<ABS.IPetService>();
            mockSvc.Setup(svc => (svc.GetPetsByPetTypeAsync("Cat"))).Returns(Task.FromResult(await GetPetsForCat()));

            var logger = new Mock<ILogger<Controllers.PetsController>>();

            var controller = new Controllers.PetsController(mockSvc.Object, logger.Object);

            // Act
            var result = await controller.GetOwnerGengerPetsByPetType("");

            // Assert
            //Is return BadRequest result
            Assert.IsType<BadRequestObjectResult>(result);
            
        }

        [Fact]
        public async Task GetOwnerGengerPetsByPetType_BadRequest_For_Null_PetType()
        {
            // Arrange
            var mockSvc = new Mock<ABS.IPetService>();
            mockSvc.Setup(svc => (svc.GetPetsByPetTypeAsync("Cat"))).Returns(Task.FromResult(await GetPetsForCat()));

            var logger = new Mock<ILogger<Controllers.PetsController>>();

            var controller = new Controllers.PetsController(mockSvc.Object, logger.Object);

            // Act
            var result = await controller.GetOwnerGengerPetsByPetType(null);

            // Assert
            //Is return BadRequest result
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task GetOwnerGengerPetsByPetType_EmpltyList_For_Unknown_PetType()
        {
            // Arrange
            var mockSvc = new Mock<ABS.IPetService>();
            mockSvc.Setup(svc => (svc.GetPetsByPetTypeAsync("Unknown_PetType"))).Returns(Task.FromResult(await GetPetsForUnknownPetType()));

            var logger = new Mock<ILogger<Controllers.PetsController>>();

            var controller = new Controllers.PetsController(mockSvc.Object, logger.Object);

            // Act
            var result = await controller.GetOwnerGengerPetsByPetType("Unknown_PetType");

            // Assert
            //Is return OK result
            var okResult = Assert.IsType<OkObjectResult>(result);

            var actual = okResult.Value as List<VMD.OwnerViewModel>;

            //Is return zero count list
            Assert.Equal(0, actual.Count);

        }

        private List<VMD.OwnerViewModel> GetOwnerGenderViewModelList(IEnumerable<MD.Pet> pets)
        {
            var genderPets = pets.GroupBy(g => g.Owner.Gender).Select(genderGroup => new VMD.OwnerViewModel()
            {
                Gender = genderGroup.Key == MD.Enum.Gender.Male ? "Male" : "Female",
                Pets = genderGroup.Select(pet => new VMD.PetViewModel() { Name = pet.Name, Type = pet.PetType.Type }).OrderBy(o => o.Name).ToList()

            }).ToList();

            return genderPets;
        }

        private async Task<IEnumerable<MD.Pet>> GetPetsForCat()
        {
            var pets = new List<MD.Pet>(){

                new MD.Pet(){ Name = "Garfield", PetType = new MD.PetType(){ Id = 1, Type = "Cat" }, Owner = new MD.Owner(){ Id = 1, Age = 30, Gender = MD.Enum.Gender.Male, Name ="Bob" } },
                new MD.Pet(){ Name = "Garfield", PetType = new MD.PetType(){ Id = 1, Type = "Cat" } , Owner = new MD.Owner(){ Id = 1, Age = 18, Gender = MD.Enum.Gender.Female, Name ="Jennifer" } },
                new MD.Pet(){ Name = "Tom", PetType = new MD.PetType(){ Id = 1, Type = "Cat" } , Owner = new MD.Owner(){ Id = 1, Age = 40, Gender = MD.Enum.Gender.Male, Name ="Fred" } },
                new MD.Pet(){ Name = "Max", PetType = new MD.PetType(){ Id = 1, Type = "Cat" } , Owner = new MD.Owner(){ Id = 1, Age = 40, Gender = MD.Enum.Gender.Male, Name ="Fred" } }
            };

            var list = await Task.Run(() => pets);
            return list;
        }

        private async Task<IEnumerable<MD.Pet>> GetPetsForUnknownPetType()
        {
            var pets = new List<MD.Pet>(){};

            var list = await Task.Run(() => pets);
            return list;
        }

    }
}
