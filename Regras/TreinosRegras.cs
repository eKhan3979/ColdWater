using System;
using System.Collections.Generic;
using System.Text;

using ColdWater.Models;
using ColdWater.Services;
using ColdWater.ViewModels;

namespace ColdWater.Regras
{
    public class TreinosRegras: BaseRegras
    {
        #region Variáveis da Classe

        private TreinosViewModel _vm;

        #endregion

        #region Construtor

        public TreinosRegras(ref TreinosViewModel vm) 
        {
            _vm = vm;
        }

        #endregion

        #region Público

        public async Task<bool> Excluir()
        {
            try
            {
                _vm.ErroIndex = 0;
                _vm.ErroMsg.Clear();

                bool boolOk = await (new TreinoService()).Delete(_vm.TreinoExcluir.IdTreino);
            }
            catch (Exception excErro)
            {
                _vm.ErroIndex = 1;
                _vm.ErroMsg.AppendLine(excErro.Message);
            }

            return (_vm.ErroIndex == 0);
        }

        public async Task<bool> Iniciar()
        {
            if (!_vm.Iniciado)
            {
                try
                {
                    _vm.ErroIndex = 0;
                    _vm.ErroMsg = new StringBuilder();

                    DateTime dtmDe = DateTime.Today.AddMonths(-2);

                    _vm.PeriodoDe = new DateTime(dtmDe.Year, dtmDe.Month, 1);
                    _vm.PeriodoAte = DateTime.Today;

                    _vm.Iniciado = await Pesquisar();
                }
                catch (Exception excErro)
                {
                    _vm.ErroIndex = 1;
                    _vm.ErroMsg.AppendLine(excErro.Message);
                }
            }

            return _vm.Iniciado;
        }

        public async Task<bool> Pesquisar()
        {
            bool boolOk = false;

            List<ListaTreinoDTO> lstExibir = new List<ListaTreinoDTO>();

            try
            {
                _vm.ResumoDoPeriodo = new ResumoDTO();

                List<TreinoModel> lstTreinos = await(new TreinoService()).Lista(_vm.PeriodoDe.ToString("yyyy/MM/dd"),
                                                                                _vm.PeriodoAte.ToString("yyyy/MM/dd"));
                
                int intDistanciaMes = 0,
                    intMes = 0,
                    intTreinoMes = 1;

                long lngTempoMes = 0;

                for (int intTreino = 0; intTreino < lstTreinos.Count; intTreino++)
                {
                    if (intMes != lstTreinos[intTreino].DataTreino.Month)
                    {
                        if (intMes > 0)
                        {
                            lstExibir.Add(new ListaTreinoDTO(-1,
                                                             0,
                                                           "Sub-Total",
                                                            "",
                                                            intDistanciaMes.ToString("#,##0"),
                                                            ParaHh_Mm(lngTempoMes),
                                                            ParaHh_Mm((long)(lngTempoMes / (intDistanciaMes / 100.0)))));
                        }

                        lstExibir.Add(new ListaTreinoDTO(0,
                                                         0,
                                                        "Data",
                                                        "Hora",
                                                        "Distância",
                                                        "Tempo",
                                                        "P/100 m."));

                        intMes = lstTreinos[intTreino].DataTreino.Month;
                        intTreinoMes = 1;
                        intDistanciaMes = 0;
                        lngTempoMes = 0;
                    }

                    intDistanciaMes += lstTreinos[intTreino].Total;
                    lngTempoMes += lstTreinos[intTreino].SegundosGastos;

                    lstTreinos[intTreino].Numero = intTreinoMes;

                    lstExibir.Add(new ListaTreinoDTO(
                        lstTreinos[intTreino].Numero,
                        lstTreinos[intTreino].IdTreino,
                        lstTreinos[intTreino].Dd_Mm_Yyyy,
                        lstTreinos[intTreino].Hh_Mm,
                        lstTreinos[intTreino].TotalStr,
                        lstTreinos[intTreino].Hh_Mm_Ss_Gastos,
                        lstTreinos[intTreino].Media_100_metros));

                    intTreinoMes++;
                }

                if ((intDistanciaMes > 0) && (intMes > 0))
                    lstExibir.Add(new ListaTreinoDTO(-1,
                                                      0,
                                                    "Sub-Total",
                                                    "",
                                                    intDistanciaMes.ToString("#,##0"),
                                                    ParaHh_Mm(lngTempoMes),
                                                    ParaHh_Mm((long)(lngTempoMes / (intDistanciaMes / 100.0)))));

                _vm.ResumoDoPeriodo = new ResumoDTO()
                {
                    Treinos = lstTreinos.Count,
                    DistanciaTotal = lstTreinos.Sum(t => t.Total),
                    SegundosTotal = lstTreinos.Sum(t => t.SegundosGastos)
                };

                int intDias = _vm.PeriodoAte.Subtract(_vm.PeriodoDe).Days;

                _vm.ResumoDoPeriodo.Por100Metros = (int)Math.Floor(_vm.ResumoDoPeriodo.SegundosTotal /
                                                                   (_vm.ResumoDoPeriodo.DistanciaTotal / 100.0));
                _vm.ResumoDoPeriodo.Por1000Metros = (int)Math.Floor(_vm.ResumoDoPeriodo.SegundosTotal /
                                                                    (_vm.ResumoDoPeriodo.DistanciaTotal / 1000.0));
                _vm.ResumoDoPeriodo.PorDia = (int)Math.Floor(_vm.ResumoDoPeriodo.DistanciaTotal / (intDias / 1.0));

                boolOk = true;
            }
            catch (Exception excErro)
            {
                throw excErro;
            }
            finally
            {
                _vm.ListaTreinos = lstExibir;
            }

            return boolOk;
        }

        #endregion
    }
}