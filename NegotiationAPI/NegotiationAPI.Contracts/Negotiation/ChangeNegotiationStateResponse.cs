using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Contracts.Negotiation
{
    public record ChangeNegotiationStateResponse
    (
         Guid Id,
         Guid ProductId,
         int AttemptsCount,
         NegotiationStatus Status,
         DateTime CreatedAt,
         DateTime? LastRejectedAt
    );
}
