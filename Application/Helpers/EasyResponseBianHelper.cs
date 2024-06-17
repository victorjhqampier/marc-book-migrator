using Application.Adapters.Bians;
using Application.Adapters.Internals;
using Domain.Exceptions;

namespace Application.Helpers;

public static class EasyResponseBianHelper
{
    public static T EasyErrorRespond<T>(List<BianErrorInternalAdapter> arrError, int statusCode=400, string? message = null) where T : BianResponseAdapter, new()
    {
        return new T()
        {            
            statusCode = statusCode,
            errors = arrError
        };  
    }

    public static BianResponseAdapter EasyInternalErrorRespond(int errorCode)
    {
        return new BianResponseAdapter()
        {            
            statusCode = 500,
            errors = new List<BianErrorInternalAdapter>{
                new BianErrorInternalAdapter
                {
                    Status_code = errorCode.ToString(),
                    Message = MessageException.GetErrorByCode(errorCode)
                }
            }
        };
    }

    public static T EasyEmptyRespond<T>() where T : BianResponseAdapter, new()
    {    
        return new T()
        {            
            statusCode = 204       
        };
    }

    public static T EasySuccessRespond<T>(dynamic dataResponse, string? message = null) where T : BianResponseAdapter, new()
    {        
        var result = new T
        {            
            statusCode = 200            
        };

        var newProp = typeof(T).GetProperties()
            .Where(prop => prop.Name != "statusCode" && prop.Name != "errors")
            .FirstOrDefault();

        if (newProp != null) newProp.SetValue(result, dataResponse);

        return result;
    }
}
