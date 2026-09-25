using System;
using System.ComponentModel;

namespace ColdWater.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string strPropriedade)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(strPropriedade));
        }

        public string ParaDecimal(decimal? dcmValor, int intDecimais = 2)
        {
            if ((dcmValor != null) && (dcmValor.Value > 0))
            {
                string strMask = "0.";

                for (int intDec = 1; intDec < intDecimais; intDec++)
                    strMask += "#";

                strMask += "0";

                return dcmValor.Value.ToString(strMask);
            }
            else
                return "";
        }

        public string ParaHMS(long lngSegundo)
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


        public string ParaMoeda(decimal dcmValor, bool cifrao = false, bool setedecimais = false)
        {
            if (dcmValor != 0)
                if (!setedecimais)
                    return ((cifrao) ? "R$ " : "") + dcmValor.ToString("#,##0.#0");
                else
                    return ((cifrao) ? "R$ " : "") + dcmValor.ToString("#,##0.#0#####");
            else
                return "-";
        }

        public string ParaMoeda(decimal? dcmValor, bool cifrao = false)
        {
            if ((dcmValor != null) && (dcmValor != 0))
                return ((cifrao) ? "R$ " : "") + dcmValor.Value.ToString("#,##0.#0");
            else
                return "-";
        }

        public string ParaPercentagem(decimal dcmValor)
        {
            if (dcmValor != 0)
                return dcmValor.ToString("#,##0.#0") + "%";
            else
                return "-";
        }
    }
}