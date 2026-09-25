using System;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

using ColdWater.Models;

namespace ColdWater.Services
{
    public class TreinoService : BaseService
    {
        #region Construtor

        public TreinoService() { }

        #endregion

        #region Público

        public async Task<bool> Delete(int intIdTreino)
        {
            bool boolOk = false;

            try
            {
                string strLink = $"{BaseLink}TreinoDelete/{intIdTreino}";

                using (HttpClient client = new HttpClient())
                {
                    var retorno = await client.GetStringAsync(strLink);

                    boolOk = JsonConvert.DeserializeObject<bool>(retorno);

                    client.Dispose();
                }
            }
            catch (Exception excErro)
            {
                throw new Exception("Erro (Service Treino Delete): - " + excErro.Message);
            }

            return boolOk;
        }

        public async Task<int> Gravar(TreinoModel treino)
        {
            int intIdTreino = treino.IdTreino;

            try
            {
                string strLink = $@"{BaseLink}TreinoGravar/{treino.IdTreino}/{treino.Yyyy_Mm_Dd.Replace("/", "-")}/{treino.Hh_Mm}/{treino.Total}/{treino.SegundosGastos}/1/20/20";

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

        public async Task<List<TreinoModel>> Lista(string strYyyy_Mm_Dd_De, string strYyyy_Mm_Dd_Ate)
        {
            List<TreinoModel> lstTreinos = new List<TreinoModel>();

            try
            {
                string strLink = $@"{BaseLink}treinoLista/{strYyyy_Mm_Dd_De.Replace("/", "-")}/{strYyyy_Mm_Dd_Ate.Replace("/", "-")}";

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 3, 0);

                    string strRetorno = await client.GetStringAsync(strLink);

                    lstTreinos = JsonConvert.DeserializeObject<List<TreinoModel>>(strRetorno);

                    client.Dispose();
                }
            }
            catch (Exception excErro)
            {
                throw;
            }

            return lstTreinos;
        }

        public string GetLinkLista(string strYyyy_Mm_Dd_De, string strYyyy_Mm_Dd_Ate)
        {
            string strLink = $@"{BaseLink}treinoLista/{strYyyy_Mm_Dd_De.Replace("/", "-")}/{strYyyy_Mm_Dd_Ate.Replace("/", "-")}";

            return strLink;
        }

        #endregion
    }
}