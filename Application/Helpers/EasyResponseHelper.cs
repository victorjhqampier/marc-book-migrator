using Application.Adapters.Internals;
using Domain.Enums;
using Domain.Exceptions;

/********************************************************************************************************          
* Copyright © 2024 Victor Jhampier Caxi - All rights reserved.   
* 
* Info                  : Easy Response for General Propose.
*
* By                    : Victor Jhampier Caxi Maquera
* Email/Mobile/Phone    : victorjhampier@gmail.com | 968991*14
*
* Creation date         : 20/04/2024
* 
**********************************************************************************************************/

namespace Application.Helpers;

public static class EasyResponseHelper
{
    public static ResponseInternalAdapter EasyErrorRespond(int nErrorCode, int nStatusCode = 500)
    {
        return new ResponseInternalAdapter()
        {
            //Success = 0,
            StatusCode = nStatusCode,
            //Message = cMessage ?? HttpMessageEnum.INTERNAL_SERVER_ERROR,
            Errors = new List<FieldErrorInternalAdapter>()
            {
                new FieldErrorInternalAdapter
                {
                    Code = nErrorCode.ToString(),
                    Message = MessageException.GetErrorByCode(nErrorCode)
                }
            }
        };
    }
    public static ResponseInternalAdapter EasyListErrorRespond(List<FieldErrorInternalAdapter> errorList, int nStatusCode = 400)
    {
        return new ResponseInternalAdapter() {            
            StatusCode = nStatusCode,
            Errors = errorList
        };
    }
    public static ResponseInternalAdapter EasyEmptyRespond()
    {
        return new ResponseInternalAdapter() {            
            StatusCode = 204            
        };
    }
    public static ResponseInternalAdapter EasySuccessRespond(dynamic dataResponse)
    {
        return new ResponseInternalAdapter()
        {            
            StatusCode = 200,            
            Data = dataResponse
        };
    }

    //public static T EasySuccessRespond<T>(dynamic dataResponse) where T : ResponseInternalAdapter, new()
    //{
    //    var result = new T();        
    //    result.StatusCode = 200;

    //    var newProp = typeof(T).GetProperties()
    //        .Where(prop => prop.Name != "Success" && prop.Name != "Message" && prop.Name != "Errors" && prop.Name != "Data")
    //        .FirstOrDefault();
    //    if (newProp != null) newProp.SetValue(result, dataResponse);

    //    return result;
    //}
}
