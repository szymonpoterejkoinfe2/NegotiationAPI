using ErrorOr;

namespace NegotiationAPI.Domain.Errors
{
    public static partial class Errors
    {
        public static class Product
        {
            public static Error NoProducts => Error.Conflict(code: "Product.NoProducts", description: "Product list is empty!");
            public static Error NoProductWithGivenId => Error.Conflict(code: "Product.NoProductWithGivenId", description: "Product with given id does not exist!");
            public static Error NotDeleted => Error.Conflict(code: "Product.NotDeleted", description: "Error while trying to delete product!");
        }
    }
}
