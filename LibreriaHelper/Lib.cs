﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace LibreriaHelper
{
    public static class Extensiones
    {
        public static Double aFloat(this object Objeto)
        {
            try { return Convert.ToDouble(Objeto); }
            catch { return 0; }
        }
        public static Double aFloat(this object Objeto, Double Default)
        {
            try { return Convert.ToDouble(Objeto); }
            catch { return Default; }
        }

        public static int aInt32(this object Objeto)
        {
            try { return Convert.ToInt32(Objeto); }
            catch { return 0; }
        }
        public static int aInt32(this object Objeto, Int32 Default)
        {
            try { return Convert.ToInt32(Objeto); }
            catch { return Default; }
        }

        public static int aInt16(this object Objeto)
        {
            try { return Convert.ToInt16(Objeto); }
            catch { return 0; }
        }
        public static int aInt16(this object Objeto, Int16 Default)
        {
            try { return Convert.ToInt16(Objeto); }
            catch { return Default; }
        }

        public static bool aBool(this object Objeto)
        {
            try { return Convert.ToBoolean(Objeto); }
            catch { return false; }
        }
        public static bool aBool(this object Objeto, bool Default)
        {
            try { return Convert.ToBoolean(Objeto); }
            catch { return Default; }
        }

        public static string aString(this object Objeto)
        {
            try { return Convert.ToString(Objeto); }
            catch { return string.Empty; }
        }
        public static string aString(this object Objeto, string Default)
        {
            try { return Convert.ToString(Objeto); }
            catch { return Default; }
        }

        public static bool esFloat(this object Objeto)
        {
            try { Convert.ToDouble(Objeto); return true; }
            catch { return false; }
        }

        public static bool esInt(this object Objeto)
        {
            try { Convert.ToInt32(Objeto); return true; }
            catch { return false; }
        }

        public static bool esStringEmpty(this object Objeto)
        {
            return string.IsNullOrEmpty(Objeto.aString());
        }
    }

    public abstract class Lib
    {

        public static readonly string enProd = System.Configuration.ConfigurationManager.AppSettings["ProdMode"];
        // Verifica si la fecha es valida deacuerdo al formato especificado
        public static Boolean isValidDate(string dateString, string format) {
            DateTime dt;
            return DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
        }
        // Verifica si la fecha es valida deacuerdo al formato especificado y regresa una fecha
        public static Boolean isValidDate(string dateString, string format, ref DateTime dt) {
            bool response = DateTime.TryParseExact(dateString, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dt);
            return response;
        }

        public static DateTime getDateTime(string dateString, string format) {
            try {
                DateTime dt = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
                return dt;
            } catch {
                return DateTime.Now;
            }
        }

        public static bool isValidHour(string timeString, string format) {
            DateTime dt;

            return DateTime.TryParseExact(timeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
        }

        public static bool isFloat(string pFloatValue) {
            try {
                Convert.ToDouble(pFloatValue);
                return true;
            }
            catch {
                return false;
            }

        }

        public static bool isDate(string pValue) {
            try {
                Convert.ToDateTime(pValue);
                return true;
            }
            catch {
                return false;
            }

        }

        public static bool isInt(string pIntValue) {
            try {
                Convert.ToInt32(pIntValue);
                return true;
            }
            catch {
                return false;
            }

        }

        public static bool isText(string pStringValue) {
            try {
                Convert.ToString(pStringValue);
                //TODO: add validation for normal text... without <> or no permited signals.

                return true;
            }
            catch {
                return false;
            }
        }

        public static bool getBool(Object Objeto) {
            try {
                if (getText(Objeto).ToUpper() == "SI") {
                    return true;
                }

                if (getText(Objeto).ToUpper() == "NO") {
                    return false;
                }

                return Convert.ToBoolean(Objeto);
            }
            catch {
                return false;
            }
        }

        public static String getText(Object pStringValue) {
            string value = pStringValue.aString();
            value = String.IsNullOrEmpty(pStringValue.aString()) ? "" : value;
            return value;
        }

        public static String getText(Object pStringValue, string defaultNullValue) {
            try {
                string value = pStringValue.aString();
                value = String.IsNullOrEmpty(pStringValue.aString()) ? defaultNullValue : value;
                return value;
            }
            catch {
                return "";
            }
        }

        public static Object getTextNull(Object pStringValue) {
            try {
                if (string.IsNullOrEmpty(pStringValue.aString())) {
                    return DBNull.Value;
                } else {
                    return Convert.ToString(pStringValue);
                }
            }
            catch {
                return DBNull.Value;
            }
        }

        public static double getGeoDistancia(double lat1, double long1, double lat2, double long2) {
            double degtorad = 0.01745329;
            double radtodeg = 57.29577951;

            if (((Convert.ToString(lat2)) + ".") == ".") {
                lat2 = 1;
                long2 = -100;
            }


            double dlong = (long1 - long2);
            double dvalue = (Math.Sin(lat1 * degtorad) * Math.Sin(lat2 * degtorad)) + (Math.Cos(lat1 * degtorad) * Math.Cos(lat2 * degtorad) * Math.Cos(dlong * degtorad));
            double dd = Math.Acos(dvalue) * radtodeg;

            double miles = (dd * 69.16);
            miles = (miles * 100) / 100;
            double km = (dd * 111.302);
            km = (km * 100) / 100;

            // Displays value with the default number of decimal digits (2).
            double distancia = Convert.ToDouble(km.ToString("N2"));
            return distancia;
        }

        /// <summary>
        /// Convierte a Entero, en caso de error regresa 0.
        /// </summary>
        /// <param name="pIntValue">Cadena a convertir</param>
        /// <returns>Entero</returns>
        public static int getInt(string pIntValue) {
            try {
                int value = Convert.ToInt32(pIntValue);
                return value;
            }
            catch (Exception ex) {
                return 0;
            }

        }

        public static int getInt(object pIntValue) {
            try {
                int value = Convert.ToInt32(pIntValue);
                return value;
            }
            catch {
                return 0;
            }

        }

        public static int getInt(string pIntValue, int defaultValue) {
            int resultNum;
            bool result = int.TryParse(pIntValue, out resultNum);
            if (result) {
                return resultNum;
            } else {
                return defaultValue;
            }
        }

        public static object getIntNull(string pIntValue) {
            try {
                int value = Convert.ToInt32(pIntValue);
                return value;
            }
            catch {
                return DBNull.Value;
            }
        }

        public static decimal getDecimal(string pIntValue) {
            try {
                decimal value = Convert.ToDecimal(pIntValue);
                return value;
            }
            catch {
                return 0;
            }
        }

        public static decimal getDecimal(object pIntValue) {
            try {
                decimal value = Convert.ToDecimal(pIntValue);
                return value;
            } catch {
                return 0;
            }
        }

        public static bool getBoolean(string pIntValue) {
            try {
                bool value = Convert.ToBoolean(pIntValue);
                return value;
            }
            catch {
                return false;
            }
        }

        public static bool getBoolean(object pIntValue) {
            try {
                bool value = Convert.ToBoolean(pIntValue);
                return value;
            } catch {
                return false;
            }
        }

        public static Double getFloat(object pIntValue) {
            try {
                Double value = Convert.ToDouble(pIntValue);
                return value;
            }
            catch {
                return 0;
            }

        }

        public static Double getDouble(Object pIntValue, Double defaultValue = 0.0)
        {
            try
            {
                Double value = Convert.ToDouble(pIntValue);
                return value;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static Double getFloat(string pIntValue, double Default) {
            try {
                Double value = Convert.ToDouble(pIntValue);
                return value;
            }
            catch {
                return Default;
            }

        }
        public static Double getSingle(string pIntValue) {
            try {
                Double value = Convert.ToSingle(pIntValue);
                return value;
            }
            catch {
                return 0;
            }

        }

        public static Double getSmallInt(string pIntValue) {
            try {
                Double value = Convert.ToInt16(pIntValue);
                return value;
            }
            catch {
                return 0;
            }

        }

        public static long getLong(string pLongValue)
        {
            try
            {
                long value = Convert.ToInt64(pLongValue);
                return value;
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime getDate(string pIntValue) {
            try {
                DateTime value = Convert.ToDateTime(pIntValue);
                return value;
            }
            catch {
                return new DateTime();
            }

        }
        public static DateTime GetDateNow(string date)
        {
            try
            {
                return Convert.ToDateTime(date).Date;
            }
            catch
            {
                return DateTime.Now.Date;
            }
        }
        public static Object getDateNull(string pIntValue) {
            try {
                DateTime value = Convert.ToDateTime(pIntValue);
                if (value.Year > 1753) {
                    return value;
                } else {
                    return DBNull.Value;
                }
            }
            catch {
                return DBNull.Value;
            }

        }

        public static Object getDateNullDDMMYYYY(string pIntValue)
        {
            try {
                if (!String.IsNullOrEmpty(pIntValue)) {
                    return getDate(DateTime.Parse(pIntValue).ToString("dd/MM/yyyy"));
                } else {
                    return DBNull.Value;
                }
            }
            catch {
                return DBNull.Value;
            }

        }

        public static DateTime GetDateDefault(string date)
        {
            try
            {
                return Convert.ToDateTime(date).Date;
            }
            catch
            {
                return DateTime.Now.Date;
            }
        }

        public static DateTime getDate2(string pIntValue)
        {
            try
            {
                DateTime value = Convert.ToDateTime(pIntValue);
                return value;
            }
            catch
            {
                return new DateTime();
            }

        }

        public static String fixString(string pValue, int pSize) {
            try {
                if (!String.IsNullOrEmpty(pValue)) {
                    if (pValue.Length >= pSize) {
                        pValue.Substring(0, pSize);
                    }
                } else {
                    pValue = "";
                }

            }
            catch {
                pValue = "";

            }

            return pValue;
        }

        public static object todbsmalldate(string pValue)
        {
            Object retorno;
            try
            {
                if (!String.IsNullOrEmpty(pValue))
                {
                    retorno = getDate(DateTime.Parse(pValue).ToString("dd/MM/yyyy"));
                }
                else
                {
                    retorno = DBNull.Value;
                }

            }
            catch
            {
                retorno = DBNull.Value;

            }

            return retorno;
        }

        public static string datetostring(Object pValue)
        {
            String retorno;
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(pValue)))
                {
                    retorno = DateTime.Parse(Convert.ToString(Convert.ToString(pValue))).ToString("dd/MM/yyyy");

                }
                else
                {
                    retorno = "";
                }

            }
            catch
            {
                retorno = "";

            }

            return retorno;
        }

        public static string caracteresespeciales(string entrada)
        {
            string invalidos = "{}[]!\"#$%&/=?¡¿|*+¨´:;,<>^Ç-_";
            for (int i = 0; i < invalidos.Length; i++)
            {
                entrada = entrada.Replace(invalidos.Substring(i, 1), "");
            }
            return entrada;
        }

        /// <summary>
        /// Limpia un RFC de elementos ajenos. En caso de algun error devuelve el RFC generico
        /// </summary>
        /// <param name="RFC">RFC</param>
        /// <returns></returns>
        public static string LimpiarRFC(string RFC)
        {
            string RFCGenerico = "XAXX010101000";
            string sRFC = RFC.ToString();
            if (!string.IsNullOrEmpty(RFC))
            {
                sRFC = sRFC.Replace(" ", String.Empty);
                sRFC = sRFC.Replace("-", String.Empty);
                sRFC = sRFC.Replace("_", String.Empty);
                sRFC = sRFC.Replace("/", String.Empty);
                sRFC = sRFC.Replace(".", String.Empty);
                sRFC = sRFC.Replace(";", String.Empty);
                sRFC = sRFC.Replace(":", String.Empty);
                sRFC = sRFC.Replace(",", String.Empty);
                sRFC = sRFC.Replace("*", String.Empty);
                sRFC = sRFC.Replace("+", String.Empty);
                sRFC = sRFC.Replace("<", String.Empty);
                sRFC = sRFC.Replace(">", String.Empty);
                sRFC = sRFC.Trim();
                if ((sRFC.Length == 0))
                {
                    sRFC = RFCGenerico;
                }
                else if ((sRFC.Length < 12))
                {
                    sRFC = RFCGenerico;
                }
                else if ((sRFC.Length > 13))
                {
                    sRFC = RFCGenerico;
                }
            }
            else
            {
                sRFC = RFCGenerico;
            }
            return sRFC;
        }


        public static object validaEntrada()
        {
            return "";

        }

        /// <summary>
        /// Validacion para evitar SQL injection
        /// </summary>
        /// <param name="strDesc">Cadena a validar.</param>
        /// <param name="tipo">Tipod de Validacion a realizar, "str" u otro.</param>
        /// <returns>Objeto</returns>
        public static object validaEntrada(string strDesc, string tipo)
        {
            object functionReturnValue = null;
            dynamic strTemp = null;
            dynamic intTemp = null;

            if (tipo == "str")
            {
                strTemp = strDesc;
                if (!string.IsNullOrEmpty(strDesc))
                {
                    strTemp = strTemp.Replace("'", "''");
                    strTemp = strTemp.Replace("select ", "");
                    strTemp = strTemp.Replace("--", " ");
                    strTemp = strTemp.Replace("drop ", "");
                    strTemp = strTemp.Replace("delete ", "");
                    strTemp = strTemp.Replace("insert ", "");
                    strTemp = strTemp.Replace("exec", "");
                    strTemp = strTemp.Replace("execute", "");
                    strTemp = strTemp.Replace(" or ", "");
                    strTemp = strTemp.Replace(" and ", "");
                }
                else
                {
                    strTemp = string.Empty;
                }

                functionReturnValue = strTemp;
            }
            else
            {
                //numeros
                intTemp = strDesc;
                intTemp = getInt(intTemp, 0);

                functionReturnValue = intTemp;
                //regresa entero listo
            }

            return functionReturnValue;

        }

        public static DateTime[] ObtenerQuincenas(string FechaInicio, int Frecuencia)
        {

            List<DateTime> Resultado = new List<DateTime>();

            DateTime tmp;
            tmp = (System.DateTime.Parse(FechaInicio, new System.Globalization.CultureInfo("es-MX")));
            bool ultimo = tmp.AddDays(1).Day.Equals(1);
            int Q;

            if (tmp.Day < 15 || ultimo) { Q = 1; tmp = tmp.AddMonths(-1); }
            else { Q = 2; }

            if (ultimo) { tmp = tmp.AddMonths(1); }

            for (int x = 0; x < Frecuencia; x++)
            {
                if (Q.Equals(1))
                {
                    tmp = System.DateTime.Parse("15/" + (tmp.AddMonths(1)).Month.ToString("00") + "/" + (tmp.AddMonths(1)).Year.ToString("0000"), new System.Globalization.CultureInfo("es-MX"));
                    Q = 2;
                }
                else
                {
                    tmp = (System.DateTime.Parse("01/" + (tmp.AddMonths(1)).Month.ToString("00") + "/" + (tmp.AddMonths(1)).Year.ToString("0000"), new System.Globalization.CultureInfo("es-MX"))).AddDays(-1);
                    Q = 1;
                }

                Resultado.Add(tmp);
            }

            return Resultado.ToArray();
        }

        public static DateTime[] ObtenerSemanas(string FechaInicio, DayOfWeek DiaDeseado, int Frecuencia)
        {
            DateTime tmp = getDate(FechaInicio);
            int c = (int)tmp.DayOfWeek;
            int d = (int)DiaDeseado;
            int n = (7 - c + d);
            int r = (n > 7) ? n % 7 : n;
            List<DateTime> Resultado = new List<DateTime>();
            tmp = tmp.AddDays(r);
            for (int x = 0; x < Frecuencia; x++)
            {
                Resultado.Add(tmp);
                tmp = tmp.AddDays(7);
            }

            return Resultado.ToArray();
        }


        public static Object SetDBNull(Object Param)
        {
            if (Param == null)
            {
                return DBNull.Value;
            }
            else
            {
                string tipo = Param.GetType().FullName;
                if (tipo.Equals("System.String"))
                {
                    if (string.IsNullOrEmpty((string)Param))
                    {
                        return DBNull.Value;
                    }
                }
                return Param;
            }
        }



        public static String getStr(Object var)
        {
            string valor = "";
            try
            {
                valor = Convert.ToString(var);
            }
            catch
            {
                valor = "-1";
            }
            return valor;
        }



    }

    public abstract class Token
    {
        /// <summary>
        ///  Returns Token as String from DateTime.Now.ToString("ddMMyyHHmmss");
        /// </summary>
        
        public static string  TokenDDMMYYhhmmss()
        {
             string valor = "";
             try
             {
                 valor = DateTime.Now.ToString("ddMMyyHHmmss");
             }
             catch
             {
                 valor = DateTime.Now.ToString("ddMMyyHHmmss");
             }

            return valor;        
        }


        public static string TokenVersiones()
        {
            string valor = "";
            try
            {
                valor = DateTime.Now.ToString("yyMMddHHmmss");
            }
            catch
            {
                valor = DateTime.Now.ToString("yyMMddHHmmss");
            }

            return valor;
        }


    }
}
