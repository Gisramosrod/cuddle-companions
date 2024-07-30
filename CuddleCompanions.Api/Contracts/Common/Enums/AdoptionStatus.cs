﻿using System.Text.Json.Serialization;

namespace CuddleCompanions.Api.Contracts.Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AdoptionStatus
{
    Completed,
    Pending,
    Cancelled
}
