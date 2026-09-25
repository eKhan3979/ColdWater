using System;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

using ColdWater.Models;

namespace ColdWater.Services
{
    public class EstiloService : BaseService
    {
        #region Construtor

        public EstiloService() { }

        #endregion

        #region Público

        public async Task<int> Gravar(TreinoModel treino)
        {
            int intIdTreino = treino.IdTreino;

            try
            {
                string strLink = $@"{BaseLink}TreinoGravar/{treino.IdTreino}/{treino.Yyyy_Mm_Dd}/{treino.Hh_Mm}/{treino.Total}/{treino.SegundosGastos}/{1}/{20}/{20}";

                using (HttpClient client = new HttpClient())
                {
                    string strId = await client.GetStringAsync(strLink);

                    intIdTreino = JsonConvert.DeserializeObject<int>(strId);

                    client.Dispose();
                }
            }
            catch (Exception excErro)
            {
                throw;
            }

            return intIdTreino;
        }

        public async Task<ObservableCollection<EstiloModel>> Todos()
        {
            ObservableCollection<EstiloModel> lstEstilos = new ObservableCollection<EstiloModel>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Uri uriLink = new Uri($"{BaseLink}estiloLista");

                    string strData = await client.GetStringAsync(uriLink);

                    lstEstilos = JsonConvert.DeserializeObject<ObservableCollection<EstiloModel>>(strData);
                }
            }
            catch (Exception excErro)
            {
                throw excErro;
            }

            return lstEstilos;
        }

        #endregion
    }
}