using System.ComponentModel.DataAnnotations;

namespace Kolmeo.WebApi
{
    /// <summary>
    /// Extensions needed for reusability
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets CorrelationId for traceability purpose
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static string? GetCorrelationId(this HttpRequest request) => request.Headers["x-correllationid"].FirstOrDefault();
        /// <summary>
        /// Used to Validate User Input Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal static bool IsModelValid(object model)
        {
            var validationContext = new ValidationContext(model, null, null);
            return Validator.TryValidateObject(model, validationContext, null, true);
        }
    }
}
