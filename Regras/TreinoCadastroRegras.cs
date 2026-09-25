using System;
using System.Collections.Generic;
using System.Text;

using ColdWater.Models;
using ColdWater.Services;
using ColdWater.ViewModels;

namespace ColdWater.Regras
{
    public class TreinoCadastroRegras: BaseRegras
    {
        #region Variáveis da Classe

        private TreinoCadastroViewModel _vm;

        #endregion

        #region Construtor

        public TreinoCadastroRegras(ref TreinoCadastroViewModel vm) 
        {
            _vm = vm;
        }

        #endregion

        #region Público

        public string CalcularMedia()
        {
            string strMedia = "";

            try
            {
                int intDistancia = _vm.TreinoEdit.Total;
            }
            catch { }

            return strMedia;
        }

        public async Task<bool> Gravar()
        {
            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg.Clear();

                TreinoService service = new TreinoService();

                TreinoModel treino = new TreinoModel()
                {
                    IdTreino = _vm.TreinoEdit.IdTreino,
                    Yyyy_Mm_Dd = ParaYyyy_Mm_Dd(_vm.TreinoEdit.Dd_Mm_Yyyy),
                    Hh_Mm = _vm.TreinoEdit.Hh_Mm,
                    Total = _vm.TreinoEdit.Total,
                    SegundosGastos = _vm.TreinoEdit.SegundosGastos                    
                };

                _vm.TreinoEdit.IdTreino = await service.Gravar(treino);
            }
            catch (Exception excErro)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine(excErro.Message);
            }

            return (_vm.ErroIndex == 0);
        }

        public bool Iniciar()
        {
            try
            {
                _vm.Iniciado = false;
                _vm.ErroIndex = 0;
                _vm.ErroMsg = new StringBuilder();

                _vm.TreinoEdit = new TreinoModel()
                {
                    Dd_Mm_Yyyy = DateTime.Today.ToString("dd/MM/yyyy"),
                    Hh_Mm = (100 + DateTime.Now.AddHours(-2).Hour).ToString().Substring(1, 2) + ":00"
                };

                _vm.ListaH = new List<string>() { "0", "1", "2", "3" };
                
                List<string> lstMM = new List<string>();
                List<string> lstSS = new List<string>();

                for (int intSS = 0; intSS < 60; intSS++)
                {
                    lstMM.Add((100 + intSS).ToString().Substring(1, 2));
                    lstSS.Add((100 + intSS).ToString().Substring(1, 2));
                }

                _vm.ListaMM = lstMM;
                _vm.ListaSS = lstSS;

                List<string> lstDistancia = new List<string>();

                for (int intDistancia = 0; intDistancia < 5025; intDistancia += 25)
                    lstDistancia.Add(intDistancia.ToString("#,##0"));

                _vm.ListaDistancia = lstDistancia;

                _vm.Iniciado = true;
            }
            catch (Exception excErro)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine(excErro.Message);
            }

            return _vm.Iniciado;
        }

        public bool PodeGravar()
        {
            _vm.ErroIndex = 0;
            _vm.ErroMsg.Clear();

            if (_vm.TreinoEdit.Total == 0)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine("- Selecione a Distância");
            }
            if (_vm.TreinoEdit.SegundosGastos == 0)
            {
                _vm.ErroIndex = ((_vm.ErroIndex == 0) ? 2 : _vm.ErroIndex);
                _vm.ErroMsg.AppendLine("- Selecione o Tempo Gasto");
            }

            return (_vm.ErroIndex == 0);
        }

        #endregion
    }
}
