namespace ColdWater.Models
{
    public class TreinoModel : BaseModel
    {
        #region Variáveis da Classe

        private int _idTreino = 0,
                    _numero = 0;
        private DateTime _dataTreino = DateTime.Today;
        private TimeSpan _horaTreino = TimeSpan.Zero;
        private string _yyyy_Mm_Dd = "",
                       _hh_Mm = "",
                       _hh_Mm_Ss_Gastos = "",
                       _dd_Mm_Yyyy = "",
                       _media_100_metros = "",
                       _totalStr = "",
                       _ambienteStr = "";
        private int _total = 0,
                    _segundosGastos = 0,
                    _idAmbiente = 0,
                    _tempAmbiente = 0,
                    _tempAgua = 0;
        private string _corDeFundo = "#FFFFFFFF";

        private List<ParcialModel> _parciais = new List<ParcialModel>();

        #endregion

        #region Construtor

        public TreinoModel() { }

        #endregion

        #region Propriedades

        public int IdTreino
        {
            get => _idTreino;
            set
            {
                if (_idTreino != value)
                {
                    _idTreino = value;
                    OnPropertyChanged(nameof(IdTreino));
                }
            }
        }

        public DateTime DataTreino
        {
            get => _dataTreino;
            set
            {
                if (_dataTreino != value)
                {
                    _dataTreino = value;
                    OnPropertyChanged(nameof(DataTreino));
                }
            }
        }

        public string Yyyy_Mm_Dd
        {
            get => _yyyy_Mm_Dd;
            set
            {
                if (_yyyy_Mm_Dd != value)
                {
                    _yyyy_Mm_Dd = value;
                    OnPropertyChanged(nameof(Yyyy_Mm_Dd));

                    Dd_Mm_Yyyy = _yyyy_Mm_Dd.Substring(8, 2) +
                                 _yyyy_Mm_Dd.Substring(4, 4) +
                                 _yyyy_Mm_Dd.Substring(0, 4);

                    DataTreino = new DateTime(int.Parse(_yyyy_Mm_Dd.Substring(0, 4)),
                                              int.Parse(_yyyy_Mm_Dd.Substring(5, 2)),
                                              int.Parse(_yyyy_Mm_Dd.Substring(8, 2)));
                }
            }
        }
        public string Hh_Mm
        {
            get => _hh_Mm;
            set
            {
                if (_hh_Mm != value)
                {
                    _hh_Mm = value;
                    OnPropertyChanged(nameof(Hh_Mm));
                }
            }
        }
        public int Total
        {
            get => _total;
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));

                    if (SegundosGastos > 0)
                    {
                        int intMedia = SegundosGastos / (Total / 100),
                            intMM = intMedia / 60,
                            intSS = intMedia - intMM * 60;

                        Media_100_metros = intMM.ToString() + ":" +
                                           (100 + intSS).ToString().Substring(1, 2);
                    }
                }
            }
        }
        public int SegundosGastos
        {
            get => _segundosGastos;
            set
            {
                if (_segundosGastos != value)
                {
                    _segundosGastos = value;
                    OnPropertyChanged(nameof(SegundosGastos));

                    int intH = _segundosGastos / 3600,
                        intM = (_segundosGastos - intH * 3600) / 60,
                        intS = _segundosGastos - intH * 3600 - intM * 60;

                    Hh_Mm_Ss_Gastos = intH.ToString() + ":" +
                                     (100 + intM).ToString().Substring(1, 2) + ":" +
                                     (100 + intS).ToString().Substring(1, 2);

                    if (Total > 0)
                    {
                        TotalStr = Total.ToString("#,##0");

                        int intMedia = SegundosGastos / (Total / 100),
                            intMM = intMedia / 60,
                            intSS = intMedia - intMM * 60;

                        Media_100_metros = intMM.ToString() + ":" +
                                           (100 + intSS).ToString().Substring(1, 2);
                    }
                    else
                    {
                        TotalStr = "";
                        Media_100_metros = "-";
                    }
                }
            }
        }
        public int IdAmbiente
        {
            get => _idAmbiente;
            set
            {
                if (_idAmbiente != value)
                {
                    _idAmbiente = value;
                    OnPropertyChanged(nameof(IdAmbiente));
                }
            }
        }
        public int TempAmbiente
        {
            get => _tempAmbiente;
            set
            {
                if (_tempAmbiente != value)
                {
                    _tempAmbiente = value;
                    OnPropertyChanged(nameof(TempAmbiente));
                }
            }
        }
        public int TempAgua
        {
            get => _tempAgua;
            set
            {
                if (_tempAgua != value)
                {
                    _tempAgua = value;
                    OnPropertyChanged(nameof(TempAgua));
                }
            }
        }

        public int Numero
        {
            get => _numero;
            set
            {
                if (_numero != value)
                {
                    _numero = value;
                    OnPropertyChanged(nameof(Numero));
                }
            }
        }
        public string Dd_Mm_Yyyy
        {
            get => _dd_Mm_Yyyy;
            set
            {
                if (_dd_Mm_Yyyy != value)
                {
                    _dd_Mm_Yyyy = value;
                    OnPropertyChanged(nameof(Dd_Mm_Yyyy));
                }
            }
        }
        public string Hh_Mm_Ss_Gastos
        {
            get => _hh_Mm_Ss_Gastos;
            set
            {
                if (_hh_Mm_Ss_Gastos != value)
                {
                    _hh_Mm_Ss_Gastos = value;
                    OnPropertyChanged(nameof(Hh_Mm_Ss_Gastos));
                }
            }
        }
        public string Media_100_metros
        {
            get => _media_100_metros;
            set
            {
                if (_media_100_metros != value)
                {
                    _media_100_metros = value;
                    OnPropertyChanged(nameof(Media_100_metros));
                }
            }
        }
        public string TotalStr
        {
            get => _totalStr;
            set
            {
                if (_totalStr != value)
                {
                    _totalStr = value;
                    OnPropertyChanged(nameof(TotalStr));
                }
            }
        }
        public string AmbienteStr
        {
            get => _ambienteStr;
            set
            {
                if (_ambienteStr != value)
                {
                    _ambienteStr = value;
                    OnPropertyChanged(nameof(AmbienteStr));
                }
            }
        }
        public string CorDeFundo
        {
            get => _corDeFundo;
            set
            {
                if (_corDeFundo != value)
                {
                    _corDeFundo = value;
                    OnPropertyChanged(nameof(CorDeFundo));
                }
            }
        }

        public TimeSpan HoraTreino
        {
            get => _horaTreino;
            set
            {
                if (_horaTreino != value)
                {
                    _horaTreino = value;
                    OnPropertyChanged(nameof(HoraTreino));
                }
            }
        }

        public List<ParcialModel> Parciais
        {
            get => _parciais;
            set
            {
                if (_parciais != value)
                {
                    _parciais = value;
                    OnPropertyChanged(nameof(Parciais));
                }
            }
        }

        #endregion
    }
}