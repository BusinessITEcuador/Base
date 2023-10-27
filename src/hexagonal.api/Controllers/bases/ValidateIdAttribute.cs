using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace hexagonal.infrastructure.api.Controllers.bases
{
    public class ValidateIdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!Guid.TryParse(context.RouteData.Values["id"].ToString(), out _))
            {
                context.Result = new OkObjectResult(new
                {
                    code = 200,
                    message = "",
                    data = new
                    {
                        exist = false
                    },
                    codeText = "SUCCESS"
                });
            }

            base.OnActionExecuting(context);
        }
    }
}
