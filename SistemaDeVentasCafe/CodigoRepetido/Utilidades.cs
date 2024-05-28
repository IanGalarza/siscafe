using Microsoft.AspNetCore.Mvc;
using SistemaDeVentasCafe.Models;
using System.Net;

namespace SistemaDeVentasCafe.CodigoRepetido
{
    public class Utilidades
    {
        public static ActionResult<APIResponse> AyudaControlador(APIResponse apiresponse) //funcion para los controllers
        {
            switch (apiresponse.statusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(apiresponse);
                case HttpStatusCode.Created:
                    return new OkObjectResult(apiresponse);
                case HttpStatusCode.Conflict:
                    return new ConflictObjectResult(apiresponse);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(apiresponse);
                case HttpStatusCode.InternalServerError:
                    return new NotFoundObjectResult(apiresponse);
                default:
                    return new NotFoundObjectResult(apiresponse);
            }
        }

        public static APIResponse ErrorHandling(Exception ex, APIResponse apiresponse, ILogger logger) //funcion para manejar el catch
        {
            logger.LogError("Ocurrio un error inesperado. Error: " + ex.Message);
            apiresponse.fueExitoso = false;
            apiresponse.statusCode = HttpStatusCode.InternalServerError;
            apiresponse.Errores = new List<string> { ex.ToString() }; //lista para mantener el error
            return apiresponse;

        }

        public static APIResponse NotFoundResponse(APIResponse apiresponse) //respuesta para los notfound
        {
            apiresponse.fueExitoso = false;
            apiresponse.statusCode = HttpStatusCode.NotFound;
            return apiresponse;
        }

        public static APIResponse OKResponse<T>(T obj,APIResponse apiresponse) //respuesta para los notfound
        {
            apiresponse.fueExitoso = true;
            apiresponse.statusCode = HttpStatusCode.OK;
            apiresponse.Resultado = obj;
            return apiresponse;
        }

        public static APIResponse CreatedResponse(APIResponse apiresponse) //respuesta para los notfound
        {
            apiresponse.fueExitoso = true;
            apiresponse.statusCode = HttpStatusCode.Created;
            return apiresponse;
        }

        public static APIResponse ListOKResponse<T>(List<T> objs, APIResponse apiresponse) //funcion para responder correctamente los listar
        {
            apiresponse.fueExitoso = true;
            apiresponse.statusCode = HttpStatusCode.OK;
            apiresponse.Resultado = objs;
            return apiresponse;
        }

        public static APIResponse ConflictedResponse(APIResponse apiresponse) //respuesta para los conflicted
        {
            apiresponse.fueExitoso = false;
            apiresponse.statusCode = HttpStatusCode.Conflict;
            return apiresponse;
        }
    }
}
