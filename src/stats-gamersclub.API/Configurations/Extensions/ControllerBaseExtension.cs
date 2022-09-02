using Microsoft.AspNetCore.Mvc;
using stats_gamersclub.Domain.Comum.Entidades;
using stats_gamersclub.Domain.Comum.Results;
using System.Text;
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
                ResultStatus.UnprocessableEntity => ProcessUnprocessableEntity(controller, result),
                ResultStatus.InternalServerError => controller.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, result.Errors),
                _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported."),
            };
        }

        private static IActionResult ProcessUnprocessableEntity(ControllerBase controller, IResult result) {
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (string error in result.Errors) {
                stringBuilder.Append(error);
            }

            return controller.UnprocessableEntity(new GenericResponse {
                Codigo = StatusCodes.Status422UnprocessableEntity,
                Mensagem = stringBuilder.ToString()
            });
        }
    }
}
