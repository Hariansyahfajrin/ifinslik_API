using API.GeneralController;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Filters;
public class SetBaseModelPropertiesAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.Controller as BaseController;
        if (controller != null)
        {
            foreach (var argument in context.ActionArguments)
            {
                if (argument.Value is BaseModel model)
                {
                    controller.SetBaseModelProperties(model);
                }
            }
        }

        base.OnActionExecuting(context);
    }
}
