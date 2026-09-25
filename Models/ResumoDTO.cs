namespace ColdWater.Models
{
    public class ResumoDTO: BaseModel
    {
        #region Variáveis da Classe

        private int _treinos,
                    _distanciaTotal,
                    _por100Metros,
                    _porDia,
                    _por1000Metros;

        private long _segundosTotal;
        
        private string _treinosStr,
                       _distanciaTotalStr,
                       _hh_Mm_Ss_TotalStr,
                       _por100MetrosStr,
                       _porDiaStr,
                       _por1000MetrosStr;

        #endregion

        #region Construtor

        public ResumoDTO() { }

        #endregion

        #region Propriedades

        public int Treinos
        {
            get => _treinos;
            set
            {
                if (_treinos != value)
                {
                    _treinos = value;
                    TreinosStr = _treinos.ToString("#,##0");
                }                
            }   
        }
        public int DistanciaTotal
        {
            get => _distanciaTotal;
            set
            {
                if (_distanciaTotal != value)
                {
                    _distanciaTotal = value;
                    DistanciaTotalStr = _distanciaTotal.ToString("#,##0");
                }
            }
        }
        public int Por100Metros
        {
            get => _por100Metros;
            set
            {
                if (_por100Metros != value)
                {
                    _por100Metros = value;
                    Por100MetrosStr = ParaHMS(_por100Metros);
                }
            }
        }
        public int PorDia
        {
            get => _porDia;
            set
            {
                if (_porDia != value)
                {
                    _porDia = value;
                    PorDiaStr = _porDia.ToString("#,##0") + " m.";
                }
            }
        }
        public int Por1000Metros
        {
            get => _por1000Metros;
            set
            {
                if (_por1000Metros != value)
                {
                    _por1000Metros = value;
                    Por1000MetrosStr = ParaHMS(_por1000Metros);
                }
            }
        }

        public long SegundosTotal
        {
            get => _segundosTotal;
            set
            {
                if (_segundosTotal != value)
                {
                    _segundosTotal = value;
                    Hh_Mm_Ss_TotalStr = ParaHMS(_segundosTotal);
                }
            }
        }

        public string TreinosStr
        {
            get => _treinosStr;
            set => _treinosStr = value;
        }
        public string DistanciaTotalStr
        {
            get => _distanciaTotalStr;
            set => _distanciaTotalStr = value;
        }
        public string Hh_Mm_Ss_TotalStr
        {
            get => _hh_Mm_Ss_TotalStr;
            set => _hh_Mm_Ss_TotalStr = value;
        }
        public string Por100MetrosStr
        {
            get => _por100MetrosStr;
            set => _por100MetrosStr = value;
        }
        public string PorDiaStr
        {
            get => _porDiaStr;
            set => _porDiaStr = value;
        }
        public string Por1000MetrosStr
        {
            get => _por1000MetrosStr;
            set => _por1000MetrosStr = value;
        }

        #endregion
    }
}