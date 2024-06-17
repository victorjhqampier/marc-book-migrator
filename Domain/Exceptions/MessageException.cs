
namespace Domain.Exceptions;

public static class MessageException
{
    private static readonly Dictionary<int, string> codigoErrores = new Dictionary<int, string>()
    {
        {200,"La petición se completó exitosamente."},
        {204,"La petición se completó exitosamente, pero su respuesta no tiene ningún contenido"},
        {307,"Lo sentimos, en este momento no podemos atenderte"},
        {400,"Lo sentimos, la petición no pudo completarse debido a que la solicitud no fue válida"},
        {500,"Lo sentimos, la petición no pudo completarse debido a un conflicto interno en nuestros servidores."},
        {501,"¡Disculpa las molestias! En este momento no podemos atenderte."},
        {10001, "## cannot be null."},
        {10002, "## cannot be empty."},
        {10003, "## is out of the allowed bounds."},
        {10004, "Allowed length (##)."},
        {10005, "Enter a type of ##."},
        {10006, "Invalid type of ##."},
        {10007, "Enter a name for ##."},
        {10008, "Invalid name for ##."},
        {10009, "Enter a ##."},
        {10010, "Invalid ##."},
        {10011, "Entering a ## is not appropriate."},
        {10012, "Please use alphanumeric characters (a-z and 0-9)."},
        {10013, "Please use allowed characters (a-z)."},
        {10014, "Please use numeric characters (0-9)."},
        {10095, "The obtained information is incomplete. We recommend verifying the information through the central system."},
        {10096, "Does not authorize data processing."},
        {10097, "Incorrect parameters."},
        {10098, "General internal error."}
    };

    public static string GetErrorByCode(int nCode, string cName = "elemento")
    {
        string result = string.Empty;
        if (codigoErrores.TryGetValue(nCode, out result))
            return result.Replace("##", cName);
        return "Error desconocido en ##".Replace("##", cName);
    }
}