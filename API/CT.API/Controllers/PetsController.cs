using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ABS = CT.Service.Abstract;
using MD = CT.Models;
using VMD = CT.API.ViewModels;

namespace CT.API.Controllers
{
    [Produces("application/json", new string[] { })]
    [Route("api/[controller]")]
    public class PetsController : Controller
    {

        private readonly ABS.IPetService petService;
        private readonly ILogger<PetsController> _logger;

        public PetsController(ABS.IPetService petService, ILogger<PetsController> logger)
        {
            this.petService = petService;
            this._logger = logger;
        }

        // GET api/pets/5
        [HttpGet("{petType}")]
        [Route("GetPetsByPetType")]
        public async Task<IActionResult> GetPetsByPetType(string petType)
        {

            try
            {
                if (string.IsNullOrEmpty(petType))
                {
                    ModelState.AddModelError("BadRequest", "Pet Type should not be null or empty");

                    return BadRequest(ModelState);
                }
                
                var genderPets = (await petService.GetPetsByPetTypeAsync(petType)).GroupBy(g => g.Owner.Gender).Select(genderGroup => new VMD.OwnerViewModel()
                {
                    Gender = genderGroup.Key == MD.Enum.Gender.Male ? "Male" : "Female",
                    Pets = genderGroup.Select(pet => new VMD.PetViewModel() { Name = pet.Name, Type = pet.PetType.Type }).OrderBy(o => o.Name).ToList()

                }).ToList();

                return Ok(genderPets);
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(ex.HResult), ex, "GET : api/Pets/GetOwnerGenderPetsByPetType/" + petType + "/");

                ModelState.AddModelError("", "Unable to read pets. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");

                return BadRequest(ModelState);
            }
        }

    }
}
