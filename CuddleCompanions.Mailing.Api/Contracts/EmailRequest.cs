namespace CuddleCompanions.Mailing.Api.Contracts;

public sealed record EmailRequest(string To, string Subject, string Body);