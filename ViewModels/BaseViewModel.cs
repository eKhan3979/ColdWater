using AndroidX.Lifecycle;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColdWater.ViewModels
{
    public class BaseViewModel
    {
        #region Variáveis da Classe

        private bool _confirmacao = false;
        private int _erroIndex;
        private StringBuilder _erroMsg;
        private bool _iniciado,
                     _transacao;
        private string _pontoDecimal = ".",
                     _virgulaDecimal = ",";

        #endregion

        #region Construtor

        public BaseViewModel()
        {
            _virgulaDecimal = (1.1).ToString().Substring(1, 1);
            _pontoDecimal = ((_virgulaDecimal == ".") ? "," : ".");
        }

        #endregion

        #region Propriedades

        public bool Confirmacao
        {
            get { return _confirmacao; }
            set { _confirmacao = value; }
        }
        public int ErroIndex
        {
            get { return _erroIndex; }
            set { _erroIndex = value; }
        }
        public StringBuilder ErroMsg
        {
            get { return _erroMsg; }
            set { _erroMsg = value; }
        }
        public bool Iniciado
        {
            get { return _iniciado; }
            set { _iniciado = value; }
        }
        public string PontoDecimal
        {
            get { return _pontoDecimal; }
            set { _pontoDecimal = value; }
        }
        public bool Transacao
        {
            get { return _transacao; }
            set { _transacao = value; }
        }
        public string VirgulaDecimal
        {
            get { return _virgulaDecimal; }
            set { _virgulaDecimal = value; }
        }

        #endregion
    }
}
