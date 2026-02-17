using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;


namespace BL.Filtters
{
    public class TransactionLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                Debug.WriteLine($" [Audit Log]: Processed Transaction for Account {5} at {DateTime.Now}");
            }
        }
    }
}