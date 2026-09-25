using System;
using System.Collections.Generic;
using System.Text;

namespace ColdWater.Regras
{
    public class BaseRegras
    {
        public BaseRegras() { }

        public string ParaDd_Mm_Yyyy(DateTime dtmData)
        {
            return dtmData.ToString("dd/MM/yyyy");
        }

        public string ParaHh_Mm(long lngSegundo)
        {
            int intHh = (int)lngSegundo / 3600,
                intMm = ((int)lngSegundo - intHh * 3600) / 60,
                intSs = ((int)lngSegundo - intHh * 3600 - intMm * 60);

            if (intHh > 0)
                return intHh.ToString() + ":" +
                      (100 + intMm).ToString().Substring(1, 2) + ":" +
                      (100 + intSs).ToString().Substring(1, 2);
            else
                return intMm.ToString() + ":" +
                       (100 + intSs).ToString().Substring(1, 2);
        }

        public string ParaYyyy_Mm_Dd(string strDd_Mm_Yyyy)
        {
            if ((strDd_Mm_Yyyy != null) &&
                (strDd_Mm_Yyyy.Length == 10))
                return strDd_Mm_Yyyy.Substring(6, 4) + "/" +
                       strDd_Mm_Yyyy.Substring(3, 2) + "/" +
                       strDd_Mm_Yyyy.Substring(0, 2);
            else
                return "";
        }
    }
}
