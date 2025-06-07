using ErrorOr;

namespace NegotiationAPI.Domain.Errors
{
    public static class Errors
    {
        public static class Authentication
        {
            public static Error DuplicateEmail => Error.Conflict(code: "Authentication.DuplicateEmail", description: "Account with this E-mail already exists");
            public static Error InvalidCredentials => Error.Conflict(code: "User.InvalidCredentials", description: "Invalid email or password");
        }
    }
}
