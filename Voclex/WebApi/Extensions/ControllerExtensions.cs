using Microsoft.AspNetCore.Mvc;

namespace WebApi.Extensions
{
    public static class ControllerExtensions
    {
        public static ActionResult OkOrNotFound(this ControllerBase controller, bool condition)
        {
            if (condition)
                return controller.Ok();

            return controller.NotFound();
        }

        public static ActionResult OkWithResultOrNotFound(this ControllerBase controller, object? result)
        {
            if (result == null)
                return controller.NotFound();

            return controller.Ok(result);
        }
    }
}
