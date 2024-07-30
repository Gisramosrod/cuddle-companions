using CuddleCompanions.Api.Abstractions;
using CuddleCompanions.Api.Contracts.Adopters;
using CuddleCompanions.Application.Adopters.Commands.CreateAdopter;
using CuddleCompanions.Application.Adopters.Queries.GetAdopterById;
using CuddleCompanions.Application.Adopters.Queries.ListAdopters;
using CuddleCompanions.Application.AdoptionRecords.Command.CancelAdoption;
using CuddleCompanions.Application.AdoptionRecords.Command.CompleteAdoption;
using CuddleCompanions.Application.AdoptionRecords.Command.InitiateAdoption;
using CuddleCompanions.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CuddleCompanions.Api.Controllers;

[Route("api/adopters")]
[ApiController]
public class AdopterController : ApiController
{
    public AdopterController(ISender sender) : base(sender) { }

    [HttpGet("{adopterId:guid}")]
    public async Task<IActionResult> GetAdopterById(Guid adopterId, CancellationToken cancellationToken)
    {
        var query = new GetAdopterByIdQuery(adopterId);
        var result = await Sender.Send(query, cancellationToken);

        return result.Match(
            adopter => Ok(ToDto(adopter)),
            HandleFailure);
    }

    [HttpGet]
    public async Task<IActionResult> ListAdopters(CancellationToken cancellationToken)
    {
        var query = new ListAdoptersQuery();
        var result = await Sender.Send(query, cancellationToken);

        return result.Match(
            adopters => Ok(adopters.ConvertAll(ToDto)),
            HandleFailure);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAdopter([FromBody] RegisterAdopterRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAdopterCommand(
           request.FirstName,
           request.LastName,
           request.Email,
           request.PhoneCountryCode,
           request.PhoneNumber,
           request.AddressStreet,
           request.AddressCity,
           request.AddressState,
           request.AddressPostalCode,
           request.AddressCountry);

        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            adopter => CreatedAtAction(
                nameof(GetAdopterById),
                new { AdopterId = adopter.Id },
                Ok(ToDto(adopter))),
            HandleFailure);
    }

    [HttpPost("{adopterId:guid}/initiate-adoption")]
    public async Task<IActionResult> InitiateAdoption(Guid adopterId, Guid petId, CancellationToken cancellationToken)
    {
        var command = new InitiateAdoptionCommand(adopterId, petId);
        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            _ => NoContent(),
            HandleFailure);
    }

    [HttpPost("{adopterId:guid}/complete-adoption")]
    public async Task<IActionResult> CompleteAdoption(Guid adopterId, Guid adoptionRecordId, CancellationToken cancellationToken)
    {
        var command = new CompleteAdoptionCommand(adopterId, adoptionRecordId);
        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            _ => NoContent(),
            HandleFailure);

    }

    [HttpPost("{adopterId:guid}/cancel-adoption")]
    public async Task<IActionResult> CancelAdoption(Guid adopterId, Guid adoptionRecordId, CancellationToken cancellationToken)
    {
        var command = new CancelAdoptionCommand(adopterId, adoptionRecordId);
        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            _ => NoContent(),
            HandleFailure);
    }

    private static AdopterResponse ToDto(Adopter adopter) =>
        new(adopter.Id,
            adopter.FirstName,
            adopter.LastName,
            adopter.Email.Value,
            adopter.Phone.CountryCode,
            adopter.Phone.Number,
            adopter.Address.Street,
            adopter.Address.City,
            adopter.Address.State,
            adopter.Address.PostalCode,
            adopter.Address.Country);
}
