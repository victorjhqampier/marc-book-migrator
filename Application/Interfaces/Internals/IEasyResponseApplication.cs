using Application.Adapters.Internals;

namespace Application.Interfaces.Internals;

interface IEasyResponseApplication
{
    public ResponseInternalAdapter EasyErrorRespond(string cErrorCode, string cErrorMessage, string? cMessage = null);
    public ResponseInternalAdapter EasyListErrorRespond(dynamic errorList, string? cMessage = null);
    public ResponseInternalAdapter EasyEmptyRespond(string? cMessage = null);
    public ResponseInternalAdapter EasySuccessRespond(dynamic dataResponse, string? cMessage = null);
}
