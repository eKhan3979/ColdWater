using System;

using ColdWater.Models;

namespace ColdWater.ViewModels
{
    public class TreinosViewModel : BaseViewModel
    {
        #region Variáveis da Classe

        private DateTime _periodoDe,
                         _periodoAte;

        private ListaTreinoDTO? _treinoExcluir;

        private List<ListaTreinoDTO> _listaTreinos;

        private ResumoDTO _resumoDoPeriodo;

        #endregion

        #region Construtor

        public TreinosViewModel() { }

        #endregion

        #region Propriedades

        public DateTime PeriodoDe
        {
            get => _periodoDe;
            set => _periodoDe = value;
        }
        public DateTime PeriodoAte
        {
            get => _periodoAte;
            set => _periodoAte = value;
        }

        public List<ListaTreinoDTO> ListaTreinos
        {
            get => _listaTreinos;
            set
            {
                if (_listaTreinos != value)
                    _listaTreinos = value;
            }
        }

        public ResumoDTO ResumoDoPeriodo
        {
            get => _resumoDoPeriodo;
            set
            {
                if (_resumoDoPeriodo != value)
                    _resumoDoPeriodo = value;
            }
        }

        public ListaTreinoDTO? TreinoExcluir
        {
            get => _treinoExcluir;
            set
            {
                if (_treinoExcluir != value)
                    _treinoExcluir = value;
            }
        }

        #endregion
    }
}