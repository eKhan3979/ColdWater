using System;

namespace ColdWater.Services
{
    public class BaseService
    {
        public string BaseLink = @"https://lightcyan-echidna-380972.hostingersite.com/knation/";

        public static string Yyyy_Mm_Dd(string dd_mm_yyyy)
        {
            if (!string.IsNullOrWhiteSpace(dd_mm_yyyy))
            {
                if (dd_mm_yyyy.Length == 10)
                    return dd_mm_yyyy.Substring(6, 4) +
                           dd_mm_yyyy.Substring(2, 4) +
                           dd_mm_yyyy.Substring(0, 2);
                else
                {
                    string[] arrData = dd_mm_yyyy.Split(' ');

                    if (arrData.Length > 0)
                        return arrData[0].Substring(6, 4) +
                               arrData[0].Substring(2, 4) +
                               arrData[0].Substring(0, 2);
                    else
                        return "";
                }
            }
            else
                return "";
        }
    }
}