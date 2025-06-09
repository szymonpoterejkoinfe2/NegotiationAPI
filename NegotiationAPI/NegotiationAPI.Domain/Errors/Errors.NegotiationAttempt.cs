using ErrorOr;

namespace NegotiationAPI.Domain.Errors
{
    public static partial class Errors
    {
        public static class NegotiationAttempt
        {
            public static Error NoAttemptsFound => Error.Conflict(code: "NegotiationAttempt.NoAttemptsFound", description: "No negotiation attempts were found!");
            public static Error NoAttemptWithGivenId => Error.Conflict(code: "NegotiationAttempt.NoAttemptWithGivenId", description: "Negotiation attempt with given id was not found!");
            public static Error ErrorWhileTryingToDelete => Error.Conflict(code: "NegotiationAttempt.ErrorWhileTryingToDelete", description: "Failed to delete the negotiation attempt!");
        }
    }
}
