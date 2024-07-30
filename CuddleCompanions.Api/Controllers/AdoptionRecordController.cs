using CuddleCompanions.Api.Abstractions;
using CuddleCompanions.Api.Contracts.AdoptionRecord;
using CuddleCompanions.Api.Contracts.Common.Converters;
using CuddleCompanions.Application.AdoptionRecords.Queries.ListAdoptionRecords;
using CuddleCompanions.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CuddleCompanions.Api.Controllers
{
    [Route("api/adopters/{adopterId:guid}/adoptionRecords")]
    [ApiController]
    public class AdoptionRecordController : ApiController
    {
        public AdoptionRecordController(ISender sender) : base(sender) { }

        [HttpGet]
        public async Task<IActionResult> ListAdoptionRecords(Guid adopterId, CancellationToken cancellationToken)
        {
            var query = new ListAdoptionRecordsQuery(adopterId);
            var result = await Sender.Send(query, cancellationToken);

            return result.Match(
                adoptionRecords => Ok(adoptionRecords.ConvertAll(ToDto)),
                HandleFailure);
        }

        private static AdoptionRecordResponse ToDto(AdoptionRecord adoptionRecord) =>
            new(adoptionRecord.Id,
                adoptionRecord.AdopterId,
                adoptionRecord.PetId,
                adoptionRecord.AdoptionStartDate,
                AdoptionRecordEnumConverter.ToDtoAdoptionStatus(adoptionRecord.Status));
    }
}
