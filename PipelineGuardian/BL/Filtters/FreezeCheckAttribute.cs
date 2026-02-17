using BL.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace BL.Filtters
{
    public class FreezeCheckAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var repo = context.HttpContext.RequestServices.GetService<BankRepository>();

            if (context.RouteData.Values.TryGetValue("accountId", out var objId) &&
                int.TryParse(objId?.ToString(), out int id))
            {
                var account = repo.GetAccount(id);
                if (account == null || account.IsFrozen)
                {
                    context.Result = new ContentResult { Content = "Freezed.", StatusCode = 403 };
                }
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        { }
    }
}