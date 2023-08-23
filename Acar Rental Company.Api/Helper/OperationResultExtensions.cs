using Microsoft.AspNetCore.Mvc;
using RentalCompany.SharedKernel.Operation_Result;

namespace Acar_Rental_Company.Api.Helper
{
    public static class OperationResultExtensions
    {

        public static IActionResult ToActionResultes<T>(this OperationResult<T> result) where T : class
        {
            switch (result.OperationResultType)
            {
                case OperationResultTypes.Exception:
                    return new JsonResult(new { Message = $"Exception ---- Message: {result.Message}" }) { StatusCode = 400 };
                case OperationResultTypes.Success:
                    return new JsonResult(new { Message = "Success ---", Resultes = result.RangeResults }) { StatusCode = 200 };
                case OperationResultTypes.Failed:
                    return new JsonResult(new { Message = $"Failed ---- Message: {result.Message}" }) { StatusCode = 400 };
                case OperationResultTypes.NotExist:
                    return new JsonResult(new { Message = $"Not Found ---- Message: {result.Message}" }) { StatusCode = 404 };
                case OperationResultTypes.NoElement:
                    return new JsonResult(new { Message = $" No Elements Exist---- Message: {result.Message}" }) { StatusCode = 404 };
                default:
                    return new JsonResult(new { Message = $"Unknown ---- Message: {result.Message}" }) { StatusCode = 500 };
            }
        }
        public static IActionResult ToActionResult<T>(this OperationResult<T> result) where T : class
        {
            switch (result.OperationResultType)
            {
                case OperationResultTypes.Exception:
                    return new JsonResult(new { Message = $"Exception ---- Message: {result.Message}" }) { StatusCode = 400 };
                case OperationResultTypes.Success:
                    return new JsonResult(new { Message = "Success ---", Result = result.Reslut }) { StatusCode = 200 };
                case OperationResultTypes.Failed:
                    return new JsonResult(new { Message = $"Failed ---- Message: {result.Message}" }) { StatusCode = 400 };
                case OperationResultTypes.NotExist:
                    return new JsonResult(new { Message = $"Not Found ---- Message: {result.Message}" }) { StatusCode = 404 };
                case OperationResultTypes.NoElement:
                    return new JsonResult(new { Message = $" No Elements Exist---- Message: {result.Message}" }) { StatusCode = 404 };

                default:
                    return new JsonResult(new { Message = $"Unknown ---- Message: {result.Message}" }) { StatusCode = 500 };
            }
        }

    }
}
