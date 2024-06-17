using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Domain.Commons;

public static class FunctionCommon
{
    public static void CopyPropertiesFrom<TTarget, TSource>(this TTarget target, TSource source)
    {
        if (target == null || source == null)
            return;

        var sourceProperties = source.GetType().GetProperties();
        var targetProperties = target.GetType().GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            foreach (var targetProperty in targetProperties)
            {
                //if (sourceProperty.Name == targetProperty.Name && sourceProperty.PropertyType == targetProperty.PropertyType)
                if (sourceProperty.Name == targetProperty.Name)
                {
                    targetProperty.SetValue(target, sourceProperty.GetValue(source));
                    break;
                }
            }
        }
    }

    public static string ConvertirListaObjetosToXml<T>(List<T> objeto, string nombreRaiz, string nombreNodo)
    {
        IList<PropertyInfo> propiedadesObjeto;
        XElement xmlElementoRaiz = new XElement(nombreRaiz);
        XElement xmlElementoNodo;

        if (objeto.Count < 1)
            xmlElementoRaiz.ToString();

        propiedadesObjeto = typeof(T).GetProperties().ToList();

        foreach (T objItem in objeto)
        {
            //propiedadesObjeto = typeof(T).GetProperties().ToList();
            xmlElementoNodo = new XElement(nombreNodo);
            xmlElementoRaiz.Add(xmlElementoNodo);
            foreach (var propiedad in propiedadesObjeto)
                xmlElementoNodo.Add(new XElement(propiedad.Name, propiedad.GetValue(objItem)));
        }
        return Regex.Replace(xmlElementoRaiz.ToString().Replace("\r\n", ""), @"\s+", " ");
    }

    public static List<T> ConvertirXmlToListaObjetos<T>(string xmlString, string nombreRaiz, string nombreNodo) where T : class
    {
        var result = new List<T>();
        try
        {
            if (xmlString == string.Empty) return result;
            var GetXmlOrigin = new XmlSerializer(typeof(T));
            var objName = typeof(T).Name;
            var xmlTmp = XDocument.Parse(xmlString);
            var xmlNode = xmlTmp.Descendants(nombreRaiz).Elements(nombreNodo);
            foreach (var item in xmlNode)
            {
                item.Name = objName;
                using (StringReader sr = new StringReader(item.ToString()))
                {
                    result.Add((T)GetXmlOrigin.Deserialize(sr));
                }
            }
            return result;
        }
        catch
        {
            return result;
        }
    }

    public static decimal TruncarItfCorebank(decimal nValor, int nDecimales, int nMoneda)
    {
        decimal nItf = Convert.ToDecimal(0.00);

        if (nMoneda == 2)
        {
            nItf = TruncarNumeroCorebank((nValor), nDecimales);
        }
        else if (nMoneda == 1)
        {
            decimal nMonItf = TruncarNumeroCorebank((nValor), nDecimales);

            decimal nRes = (nMonItf % Convert.ToDecimal(0.05));

            if (Math.Round(nRes, 2) == Convert.ToDecimal(0.05))
            {
                nItf = nMonItf;
            }
            else
            {
                nItf = nMonItf - Math.Round(nRes, 2);
            }

        }
        return Convert.ToDecimal(nItf);
    }

    public static decimal TruncarNumeroCorebank(decimal value, int length)
    {
        string[] param = value.ToString().Split('.');
        if (value.ToString().IndexOf(".") > 0)
        {
            if (param[1].Length >= length)

                return Convert.ToDecimal(param[0] + "." + param[1].Substring(0, length));

            else

                return Convert.ToDecimal(param[0] + "." + param[1].Substring(0, param[1].Length));
        }
        else
        {
            return Convert.ToDecimal(value);
        }
    }

    public static string DarFormatoNombre(string cCaracter)
    {
        if (cCaracter == null || cCaracter.Trim().Length == 0)
            return "";
        cCaracter = string.Concat(
            Regex.Replace(cCaracter, @"(?i)[\p{L}-[ña-z]]+", m => m.Value.Normalize(NormalizationForm.FormD))
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            ).ToUpper();
        cCaracter = Regex.Replace(cCaracter, @"[^A-Za-zñÑ\s]", "");
        return Regex.Replace(cCaracter.Replace("\r\n", " "), @"\s+", " ").Trim();
    }

    public static string DarFormatoDireccion(string cCaracter)
    {
        if (cCaracter == null || cCaracter.Trim().Length == 0)
            return "";
        cCaracter = string.Concat(
            Regex.Replace(cCaracter, @"(?i)[\p{L}-[ña-z]]+", m => m.Value.Normalize(NormalizationForm.FormD))
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            ).ToUpper();
        cCaracter = Regex.Replace(cCaracter, @"[^-:0-9A-Za-zñÑ\s]", "");
        return Regex.Replace(cCaracter.Replace("\r\n", " "), @"\s+", " ").Trim();
    }

    public static int CalcularCuotasAtrasadas(DateTime dFechaPago, DateTime dFechaActual)
    {
        dFechaActual = new DateTime(2021, 08, 24);
        int mesesRespuesta = (dFechaActual.Day - dFechaPago.Day) + ((dFechaActual.Year - dFechaPago.Year) * 12);
        int nCuotasAtrasadas = mesesRespuesta < 0 ? 0 : mesesRespuesta + (dFechaActual.Day >= dFechaActual.Day ? 0 : 1);
        return nCuotasAtrasadas < 0 ? 0 : nCuotasAtrasadas;
    }

    public static string CrearReciboLongitudFijo(string cString, int nLongitud)
    {
        if (cString.Length >= nLongitud)
            return cString.Substring(0, nLongitud);

        return cString.PadLeft(nLongitud, '0');
    }
}
