using System;
using System.Collections.Generic;
using System.Text;

using ColdWater.Models;

namespace ColdWater.ViewModels
{
    public class TreinoCadastroViewModel: BaseViewModel
    {
        #region Variáveis da Classe

        private TreinoModel _treinoEdit;

        private int _horas,
                    _minutos,
                    _segundos;

        private List<string> _listaDistancia,
                             _listaH,
                             _listaMM,
                             _listaSS;

        #endregion

        #region Construtor

        public TreinoCadastroViewModel() { }

        #endregion

        #region Propriedades

        public TreinoModel TreinoEdit
        {
            get => _treinoEdit;
            set
            {
                if (_treinoEdit != value)
                    _treinoEdit = value;
            }
        }

        public int Horas
        {
            get => _horas;
            set
            {
                if (_horas != value)
                    _horas = value;
            }
        }
        public int Minutos
        {
            get => _minutos;
            set
            {
                if (_minutos != value)
                    _minutos = value;
            }
        }
        public int Segundos
        {
            get => _segundos;
            set
            {
                if (_segundos != value)
                    _segundos = value;
            }
        }

        public List<string> ListaDistancia
        {
            get => _listaDistancia;
            set
            {
                if (_listaDistancia != value)
                    _listaDistancia = value;
            }
        }
        public List<string> ListaH
        {
            get => _listaH;
            set
            {
                if (_listaH != value)
                    _listaH = value;
            }
        }
        public List<string> ListaMM
        {
            get => _listaMM;
            set
            {
                if (_listaMM != value)
                    _listaMM = value;
            }
        }
        public List<string> ListaSS
        {
            get => _listaSS;
            set
            {
                if (_listaSS != value)
                    _listaSS = value;
            }
        }

        #endregion
    }
}