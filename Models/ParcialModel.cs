namespace ColdWater.Models
{
    public class ParcialModel : BaseModel
    {
        #region Variáveis da Classe

        private int _idParcial = 0,
                    _idTreino = 0,
                    _idEstilo = 0,
                    _distancia = 0,
                    _segundosGastos = 0,
                    _repeticoes = 0;

        private int _numero = 0,
                    _segundos100M = 0;

        private string _distanciaStr = "",
                       _estilo = "",
                       _hh_Mm_Ss = "",
                       _segundos100MStr = "";

        #endregion

        #region Construtor

        public ParcialModel() { }

        #endregion

        #region Propriedades

        public int IdParcial
        {
            get => _idParcial;
            set
            {
                if (_idParcial != value)
                {
                    _idParcial = value;
                    OnPropertyChanged(nameof(IdParcial));
                }
            }
        }
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
        public int IdEstilo
        {
            get => _idEstilo;
            set
            {
                if (_idEstilo != value)
                {
                    _idEstilo = value;
                    OnPropertyChanged(nameof(IdEstilo));
                }
            }
        }
        public int Distancia
        {
            get => _distancia;
            set
            {
                if (_distancia != value)
                {
                    _distancia = value;
                    OnPropertyChanged(nameof(Distancia));

                    if (Distancia > 0)
                        Segundos100M = (int)(_segundosGastos / (Distancia / 100.0));
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

                    if (_segundosGastos > 0)
                    {
                        int intH = (int)Math.Floor(_segundosGastos / 3600.0),
                            intM = (int)Math.Floor((_segundosGastos - intH * 3600.0) / 60.0),
                            intS = _segundosGastos - intH * 3600 - intM * 60;

                        Hh_Mm_Ss = intH.ToString() + ":" +
                                  (intM + 100).ToString().Substring(1, 2) + ":" +
                                  (intS + 100).ToString().Substring(1, 2);

                        if (Distancia > 0)
                            Segundos100M = (int)(_segundosGastos / (Distancia / 100.0));
                    }
                    else
                        Hh_Mm_Ss = "";
                }
            }
        }
        public int Repeticoes
        {
            get => _repeticoes;
            set
            {
                if (_repeticoes != value)
                {
                    _repeticoes = value;
                    OnPropertyChanged(nameof(Repeticoes));
                }
            }
        }

        public string DistanciaStr
        {
            get => _distanciaStr;
            set
            {
                if (_distanciaStr != value)
                {
                    _distanciaStr = value;
                    OnPropertyChanged(nameof(DistanciaStr));

                    Distancia = int.Parse(_distanciaStr);
                }
            }
        }
        public string Estilo
        {
            get => _estilo;
            set
            {
                if (_estilo != value)
                {
                    _estilo = value;
                    OnPropertyChanged(nameof(Estilo));
                }
            }
        }
        public string Hh_Mm_Ss
        {
            get => (!string.IsNullOrWhiteSpace(_hh_Mm_Ss) ? _hh_Mm_Ss : "-");
            set
            {
                if (_hh_Mm_Ss != value)
                {
                    _hh_Mm_Ss = value;
                    OnPropertyChanged(nameof(Hh_Mm_Ss));
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
        public int Segundos100M
        {
            get => _segundos100M;
            set
            {
                if (_segundos100M != value)
                {
                    _segundos100M = value;
                    OnPropertyChanged(nameof(Segundos100M));

                    if (_segundos100M > 0)
                    {
                        int intM = (int)Math.Floor(_segundos100M / 60.0),
                            intS = _segundos100M - intM * 60;

                        Segundos100MStr = intM.ToString() + ":" +
                                         (intS + 100).ToString().Substring(1, 2);
                    }
                    else
                        Segundos100MStr = "-";
                }
            }
        }
        public string Segundos100MStr
        {
            get => (!string.IsNullOrWhiteSpace(_segundos100MStr) ? _segundos100MStr : "-");
            set
            {
                if (_segundos100MStr != value)
                {
                    _segundos100MStr = value;
                    OnPropertyChanged(nameof(Segundos100MStr));
                }
            }
        }

        #endregion
    }
}