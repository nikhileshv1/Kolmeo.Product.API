using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Kolmeo.WebApi
{
    /// <summary>
    /// DevelopmentOnly Attribute Filter for hiding in progress Endpoints
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DevelopmentOnlyAttribute : Attribute, IResourceFilter
    {
        /// <summary>
        /// OnResourceExecuting for DevelopmentOnly Attribute
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var env = context.HttpContext.RequestServices.GetService<IWebHostEnvironment>();
            if (!env.IsDevelopment())
            {
                context.Result = new NotFoundResult();
            }
        }
        /// <summary>
        /// OnResourceExecuting for DevelopmentOnly Attribute
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Nothing in here, as this is run after our Controller (Endpoint) method finishes
        }
    }
}
