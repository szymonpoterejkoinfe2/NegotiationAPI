using ErrorOr;

namespace NegotiationAPI.Domain.Errors
{
    public static partial class Errors
    {
        public static class Negotiation
        {
            public static Error NoNegotiationWithGivenId => Error.Conflict(code: "Negotiation.NoNegotiationWithGivenId", description: "Negotiation with given id does not exist!");
            public static Error NoNegotiations => Error.Conflict(code: "Negotiation.NoNegotiations", description: "No negotiations found!");
            public static Error FailedToChangeStatus => Error.Conflict(code: "Negotiation.FailedToChangeStatus", description: "Failed to change status of negotiation!");
            public static Error FailedToDelete => Error.Conflict(code: "Negotiation.ErrorWhileTryingToDelete", description: "Failed to delete the negotiation!");
            public static Error NoAttemptsLeft => Error.Conflict(code: "Negotiation.NoAttemptsLeft", description: "No attempts left for this negotiation!");
            public static Error FailedToUpdate => Error.Conflict(code: "Negotiation.FailedToUpdate", description: "Failed to update the negotiation!");
        }
    }
}
