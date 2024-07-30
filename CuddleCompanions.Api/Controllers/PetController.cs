using CuddleCompanions.Api.Abstractions;
using CuddleCompanions.Api.Contracts.Adopters;
using CuddleCompanions.Api.Contracts.Common.Converters;
using CuddleCompanions.Api.Contracts.Pets;
using CuddleCompanions.Application.Pets.Commands.CreatePet;
using CuddleCompanions.Application.Pets.Queries.GetPetById;
using CuddleCompanions.Application.Pets.Queries.ListAvailablePets;
using CuddleCompanions.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CuddleCompanions.Api.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetController : ApiController
    {
        public PetController(ISender sender) : base(sender) { }

        [HttpGet("{petId:guid}")]
        public async Task<IActionResult> GetPetById(Guid petId, CancellationToken cancellationToken)
        {
            var query = new GetPetByIdQuery(petId);
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                pet => Ok(ToDto(pet)),
                HandleFailure);
        }

        [HttpGet("available")]
        public async Task<IActionResult> ListAvailablePets(CancellationToken cancellationToken)
        {
            var query = new ListAvailablePetsQuery();
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                availablePets => Ok(availablePets.ConvertAll(ToDto)),
                HandleFailure);
        }
       
        [HttpPost]
        public async Task<IActionResult> RegisterPet([FromBody] RegisterPetRequest request, CancellationToken cancellationToken)
        {
            var command = new CreatePetCommand(
                request.Name,
                PetEnumConverter.ToDomainSpecie(request.Specie),
                request.Breed,
                request.Years,
                request.Months,
                PetEnumConverter.ToDomainGender(request.Gender),
                request.DateArrived,
                request.Description);

            var result = await Sender.Send(command, cancellationToken);

            return result.Match(
                pet => CreatedAtAction(
                    nameof(GetPetById),
                    new { PetId = pet.Id },
                    Ok(ToDto(pet))),
                HandleFailure);
        }

        private static PetResponse ToDto(Pet pet) =>
          new(
              pet.Id,
              pet.Name,
              PetEnumConverter.ToDtoSpecie(pet.Specie),
              pet.Breed,
              pet.Age.Years,
              pet.Age.Months,
              PetEnumConverter.ToDtoGender(pet.Gender),
              PetEnumConverter.ToDtoPetStatus(pet.PetStatus),
              pet.DateArrived,
              pet.Description);
    }
}
