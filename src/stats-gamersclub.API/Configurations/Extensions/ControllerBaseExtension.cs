using Microsoft.AspNetCore.Mvc;
using stats_gamersclub.Domain.Comum.Results;
using IResult = stats_gamersclub.Domain.Comum.Results.IResult;

namespace stats_gamersclub.API.Configurations.Extensions {
    public static class ControllerBaseExtension {

        public static IActionResult ProcessResult(this ControllerBase controller, IResult result) {
            return result.Status switch {
                ResultStatus.Ok => result.GetValue() == null ? controller.Ok() : controller.Ok(result.GetValue()),
                ResultStatus.Created => controller.Created("", result.GetValue()),
                ResultStatus.NotFound => controller.NotFound(),
                ResultStatus.Unauthorized => controller.Unauthorized(),
                ResultStatus.Forbidden => controller.Forbid(),
                ResultStatus.BadRequest => controller.BadRequest(),
                ResultStatus.UnprocessableEntity => controller.UnprocessableEntity(),
                ResultStatus.InternalServerError => controller.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, result.Errors),
                _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported."),
            };
        }
    }
}
