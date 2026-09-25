using System;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

using ColdWater.Models;

namespace ColdWater.Services
{
    public class ParcialService: BaseService
    {
        #region Construtor

        public ParcialService() { }

        #endregion

        #region Público

        public async Task<int> Gravar(ParcialModel parcial)
        {
            int intIdParcial = parcial.IdParcial;

            try
            {
                string strLink = $@"{BaseLink}ParcialGravar/{parcial.IdTreino}/{parcial.IdEstilo}/{parcial.Distancia}/{parcial.SegundosGastos}/{parcial.Repeticoes}";

                using (HttpClient cliente = new HttpClient())
                {
                    string strRetorno = await cliente.GetStringAsync(strLink);

                    intIdParcial = JsonConvert.DeserializeObject<int>(strRetorno);

                    cliente.Dispose();
                }
            }
            catch (Exception excErro)
            {
                throw new Exception("Erro Gravar Parcial: - " + excErro.Message);
            }

            return intIdParcial;
        }

        public async Task<List<int>> GravarLista(int intIdTreino, ObservableCollection<ParcialModel> lstParciais)
        {
            List<int> lstIdParcial = new List<int>();

            try
            {
                foreach (ParcialModel parcial in lstParciais)
                {
                    parcial.IdTreino = intIdTreino;

                    lstIdParcial.Add(await Gravar(parcial));
                }
            }
            catch (Exception excErro)
            {
                throw;
            }

            return lstIdParcial;
        }

        public async Task<ObservableCollection<ParcialModel>> GetParciaisTreino(int intIdTreino)
        {
            ObservableCollection<ParcialModel> lstParciais = new ObservableCollection<ParcialModel>();

            try
            {
                string strLink = $"{BaseLink}parciaisDoTreino/{intIdTreino}";

                using (HttpClient client = new HttpClient())
                {
                    string strJson = await client.GetStringAsync(strLink);

                    lstParciais = JsonConvert.DeserializeObject<ObservableCollection<ParcialModel>>(strJson);

                    client.Dispose();
                }
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return lstParciais;
        }

        #endregion
    }
}