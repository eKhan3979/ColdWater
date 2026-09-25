using Android.Views;

using System;

using ColdWater.Models;

namespace ColdWater.Fragments
{
    public class TreinoExcluirFragment: DialogFragment
    {
        #region Público

        public event EventHandler Evento_Excluir,
                                  Evento_Fechar,
                                  Evento_Ready;

        public void Publico_Treino(ListaTreinoDTO treino)
        {
            if (_tvwData != null) _tvwData.Text = treino.Dd_Mm_Yyyy;
            if (_tvwHora != null) _tvwHora.Text = treino.Hh_Mm;
            if (_tvwDistancia != null) _tvwDistancia.Text = treino.TotalStr;
            if (_tvwTempo != null) _tvwTempo.Text = treino.Hh_Mm_Ss_Gastos;
            if (_tvwPor100m != null) _tvwPor100m.Text = treino.Media_100_metros;
        }

        #endregion

        #region Variáveis da Classe

        private bool _iniciado = false;

        private ImageButton? _ibnExcluir, 
                             _ibnFechar;

        private TextView? _tvwData,
                          _tvwHora,
                          _tvwDistancia,
                          _tvwTempo,
                          _tvwPor100m;

        private AlertDialog.Builder? _alertBuilderMensagem,
                                     _alertBuilderConfirmacao;

        private AlertDialog? _alertMensagem,
                             _alertConfirmacao;

        #endregion

        #region Create

        public override View? OnCreateView(LayoutInflater? inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View? view = inflater.Inflate(Resource.Layout.treino_excluir_fragment, container, false);

            if (view != null)
            {
                _ibnFechar = view.FindViewById<ImageButton>(Resource.Id.treino_excluir_ibnFechar);
                _ibnExcluir = view.FindViewById<ImageButton>(Resource.Id.treino_excluir_ibnExcluir);

                _tvwData = view.FindViewById<TextView>(Resource.Id.treino_excluir_tvwData);
                _tvwHora = view.FindViewById<TextView>(Resource.Id.treino_excluir_tvwHora);
                _tvwDistancia = view.FindViewById<TextView>(Resource.Id.treino_excluir_tvwDistancia);
                _tvwTempo = view.FindViewById<TextView>(Resource.Id.treino_excluir_tvwTempo);
                _tvwPor100m = view.FindViewById<TextView>(Resource.Id.treino_excluir_tvwPor100m);
            }

            if (_ibnFechar != null)
                _ibnFechar.Click += IbnFechar_Click;

            if (_ibnExcluir != null)
                _ibnExcluir.Click += IbnExcluir_Click;

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();

            if (!_iniciado)
            {
                _iniciado = true;

                Evento_Ready?.Invoke(null, EventArgs.Empty);
            }
        }

        #endregion

        #region Private

        private void mensagem(string strTitulo,
                              string strMensagem)
        {
            if (_alertBuilderMensagem == null)
                _alertBuilderMensagem = new AlertDialog.Builder(this.Context);

            _alertBuilderMensagem.SetTitle(strTitulo);
            _alertBuilderMensagem.SetMessage(strMensagem);

            _alertBuilderMensagem.SetPositiveButton("Ok", (s, e) =>
            {
                _alertMensagem.Dismiss();
                _alertMensagem.Dispose();
                _alertMensagem = null;
            });

            _alertMensagem = _alertBuilderMensagem.Create();
            _alertMensagem.Show();
        }

        #endregion

        #region Eventos

        private void IbnExcluir_Click(object? sender, EventArgs e)
        {
            if (_alertBuilderConfirmacao == null)
            {
                _alertBuilderConfirmacao = new AlertDialog.Builder(this.Context);
                _alertBuilderConfirmacao.SetTitle("Cadastro de Treinos");
                _alertBuilderConfirmacao.SetMessage("- Confirma a Exclusão do Treino ?");

                _alertBuilderConfirmacao.SetPositiveButton("Sim", (s, e) =>
                {
                    Evento_Excluir?.Invoke(null, EventArgs.Empty);

                    _alertConfirmacao.Dismiss();
                    _alertConfirmacao.Dispose();
                    _alertConfirmacao = null;

                    _alertBuilderConfirmacao.Dispose();
                    _alertBuilderConfirmacao = null;
                });

                _alertBuilderConfirmacao.SetNegativeButton("Não", (s, e) =>
                {
                    _alertConfirmacao.Dismiss();
                    _alertConfirmacao.Dispose();
                    _alertConfirmacao = null;

                    _alertBuilderConfirmacao.Dispose();
                    _alertBuilderConfirmacao = null;
                });
                
                _alertConfirmacao = _alertBuilderConfirmacao.Create();
                
                if (_alertConfirmacao != null)
                    _alertConfirmacao.Show();
            }
        }

        private void IbnFechar_Click(object? sender, EventArgs e)
        {
            Evento_Fechar?.Invoke(null, EventArgs.Empty);
        }

        #endregion
    }
}