namespace ColdWater.Models
{
    public class ListaTreinoDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _numero,
                    _idTreino;
        private string _identificador,
                       _dd_Mm_Yyyy,
                       _hh_Mm,
                       _totalStr,
                       _hh_Mm_Ss_Gastos,
                       _media_100_metros;

        #endregion

        #region Construtor

        public ListaTreinoDTO(int numero,
                              int idTreino,
                              string dd_Mm_Yyyy,
                              string hh_Mm,
                              string totalStr,
                              string hh_Mm_Ss_Gastos,
                              string media_100_metros)
        {  
            Numero = numero;
            IdTreino = idTreino;
            Dd_Mm_Yyyy = dd_Mm_Yyyy;
            Hh_Mm = hh_Mm;
            TotalStr = totalStr;
            Hh_Mm_Ss_Gastos = hh_Mm_Ss_Gastos;
            Media_100_metros = media_100_metros;
        }

        #endregion

        #region Propriedades

        public int Numero
        {
            get => _numero;
            set
            {
                _numero = value;

                Identificador = ((_numero > 0) ? _numero.ToString() :
                                 (_numero == 0) ? "Nº" : "");
            }
        }
        public int IdTreino
        {
            get => _idTreino;
            set
            {
                if (_idTreino != value)
                    _idTreino = value;
            }
        }
        public string Identificador
        {
            get => _identificador;
            set => _identificador = value;
        }
        public string Dd_Mm_Yyyy
        {
            get => _dd_Mm_Yyyy;
            set => _dd_Mm_Yyyy = value;
        }
        public string Hh_Mm
        {
            get => _hh_Mm;
            set => _hh_Mm = value;
        }
        public string TotalStr
        {
            get => _totalStr;
            set => _totalStr = value;
        }
        public string Hh_Mm_Ss_Gastos
        {
            get => _hh_Mm_Ss_Gastos;
            set => _hh_Mm_Ss_Gastos = value;
        }
        public string Media_100_metros
        {
            get => _media_100_metros;
            set => _media_100_metros = value;
        }

        #endregion
    }
}