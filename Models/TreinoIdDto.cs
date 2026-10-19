namespace ColdWater.Models
{
    public class TreinoIdDto: BaseModel
    {
        #region Variáveis da Classe

        private int _idTreino;

        #endregion

        #region Construtor

        public TreinoIdDto() { }

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

        #endregion
    }
}