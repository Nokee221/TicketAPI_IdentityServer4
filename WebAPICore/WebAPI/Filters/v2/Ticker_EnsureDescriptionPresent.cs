using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Filters.v2
{
    public class Ticker_EnsureDescriptionPresent : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var ticket = context.ActionArguments["ticket"] as Ticket;

            if(ticket != null && !ticket.ValidateDescripiton())
            {
                context.ModelState.AddModelError("Description", "Description is required!");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            
        }
    }
}
