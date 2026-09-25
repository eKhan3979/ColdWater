namespace ColdWater.Models
{
    public class EstiloModel : BaseModel
    {
        #region Variáveis da Classe

        private int _idEstilo = 0;
        private string _nome = "";
        private bool _ativo = true;

        #endregion

        #region Construtor

        public EstiloModel() { }

        #endregion

        #region Propriedades

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
        public string Nome
        {
            get => _nome;
            set
            {
                if (_nome != value)
                {
                    _nome = value;
                    OnPropertyChanged(nameof(Nome));
                }
            }
        }
        public bool Ativo
        {
            get => _ativo;
            set
            {
                if (_ativo != value)
                {
                    _ativo = value;
                    OnPropertyChanged(nameof(Ativo));
                }
            }
        }

        #endregion
    }
}