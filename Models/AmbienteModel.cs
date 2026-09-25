namespace ColdWater.Models
{
    public class AmbienteModel : BaseModel
    {
        #region Variáveis da Classe

        private int _idAmbiente = 0;
        private string _avaliacao = "";

        #endregion

        #region Construtor

        public AmbienteModel() { }

        #endregion

        #region Propriedades

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
        public string Avaliacao
        {
            get => _avaliacao;
            set
            {
                if (_avaliacao != value)
                {
                    _avaliacao = value;
                    OnPropertyChanged(nameof(Avaliacao));
                }
            }
        }

        #endregion
    }
}