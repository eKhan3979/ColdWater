using Android.Content;
using Android.OS;
using Android.Views;
using AndroidX.RecyclerView.Widget;

using ColdWater.Adapters;
using ColdWater.Models;
using ColdWater.Regras;
using ColdWater.ViewModels;

namespace ColdWater.Fragments
{
    public class TreinosFragment : DialogFragment
    {
        #region Variáveis da Classe

        private TreinosRegras? _Regras;
        private TreinosViewModel? _ViewModel;

        private RecyclerView? _rvwLista;

        private ListaTreinosNewAdapter? _adapter;

        private LinearLayout? _llAguarde,
                              _llTreinosMain;

        private EditText? _edt_ate,
                          _edt_de;

        private TextView? _tvwNTreinos,
                          _tvwKm,
                          _tvwTempo,
                          _tvwMedia,
                          _tvwPorDia,
                          _tvwMil;

        private DatePickerDialog? _dpdAte,
                                  _dpdDe;

        private ImageButton _ibnCadastro,
                            _ibnPesquisar;

        private TreinoCadastroFragment? _cadastroFragment;

        private TreinoExcluirFragment? _excluirFragment;

        private AlertDialog.Builder? _alertBuilder,
                                     _alertBuilderGravar;

        private AlertDialog? _alert,
                             _alertGravar;
        #endregion

        #region Create

        public override View? OnCreateView(LayoutInflater? inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.treinos_fragment, container, false);

            _llAguarde = view.FindViewById<LinearLayout>(Resource.Id.treinos_llAguarde);
            _llTreinosMain = view.FindViewById<LinearLayout>(Resource.Id.treinos_llTreinosMain);

            _edt_de = view.FindViewById<EditText>(Resource.Id.treinos_edt_de);
            _edt_ate = view.FindViewById<EditText>(Resource.Id.treinos_edt_ate);

            if (_edt_de != null)
                _edt_de.Click += Edt_de_Click;
            if (_edt_ate != null)
                _edt_ate.Click += Edt_ate_Click;

            _tvwNTreinos = view.FindViewById<TextView>(Resource.Id.treinos_tvwNTreinos);
            _tvwKm = view.FindViewById<TextView>(Resource.Id.treinos_tvwKm);
            _tvwTempo = view.FindViewById<TextView>(Resource.Id.treinos_tvwTempo);
            _tvwMedia = view.FindViewById<TextView>(Resource.Id.treinos_tvwMedia);
            _tvwPorDia = view.FindViewById<TextView>(Resource.Id.treinos_tvwPorDia);
            _tvwMil = view.FindViewById<TextView>(Resource.Id.treinos_tvwMil);

            _ibnPesquisar = view.FindViewById<ImageButton>(Resource.Id.treinos_ibnPesquisar);
            _ibnPesquisar.Click += IbnPesquisar_Click;

            _ibnCadastro = view.FindViewById<ImageButton>(Resource.Id.treinos_ibnCadastro);
            _ibnCadastro.Click += IbnCadastro_Click;

            if (view != null)
            {
                _rvwLista = view.FindViewById<RecyclerView>(Resource.Id.treinos_rvwLista);

                if (_rvwLista != null) 
                    _rvwLista.SetLayoutManager(new LinearLayoutManager(Context));
            }

            return view;
        }

        public override void OnDismiss(IDialogInterface? dialog)
        {
            base.OnDismiss(dialog);

            if (_dpdAte != null)
            {
                _dpdAte.Dismiss();
                _dpdAte.Dispose();
                _dpdAte = null;
            }

            if (_dpdDe != null)
            {
                _dpdDe.Dismiss();
                _dpdDe.Dispose();
                _dpdDe = null;
            }
        }

        public async override void OnResume()
        {
            base.OnResume();

            if (_ViewModel == null)
                _ViewModel = new TreinosViewModel();

            if (!_ViewModel.Iniciado)
            {
                try
                {
                    new Handler().PostDelayed(new Action(() =>
                    {
                        if (_llAguarde != null) _llAguarde.Visibility = ViewStates.Visible;
                        if (_llTreinosMain != null) _llTreinosMain.Visibility = ViewStates.Invisible;
                    }), 0);

                    _Regras = new TreinosRegras(ref _ViewModel);

                    if (await _Regras.Iniciar())
                        exibirListaTreinos();
                    else
                        throw new Exception("Inicialização SEM Sucesso: - " + (char)10 + _ViewModel.ErroMsg.ToString());
                }
                catch (Exception excErro)
                {
                    exibirMensagem("Erro Resume", excErro.Message);
                }
                finally
                {
                    try
                    {
                        new Handler().PostDelayed(new Action(() =>
                        {
                            if (_llAguarde != null) _llAguarde.Visibility = ViewStates.Invisible;
                            if (_llTreinosMain != null) _llTreinosMain.Visibility = ViewStates.Visible;
                        }), 0);
                    }
                    catch (Exception excErro1)
                    {
                        exibirMensagem("Erro Resume 2", excErro1.Message);
                    }
                }
            }
        }

        #endregion

        #region Private

        private void exibirListaTreinos()
        {
            try
            {
                if (_edt_de != null) _edt_de.Text = _ViewModel.PeriodoDe.ToString("dd/MM/yyyy");
                if (_edt_ate != null) _edt_ate.Text = _ViewModel.PeriodoAte.ToString("dd/MM/yyyy");

                if (_tvwNTreinos != null) _tvwNTreinos.Text = _ViewModel.ResumoDoPeriodo.TreinosStr;
                if (_tvwKm != null) _tvwKm.Text = _ViewModel.ResumoDoPeriodo.DistanciaTotalStr;
                if (_tvwTempo != null) _tvwTempo.Text = _ViewModel.ResumoDoPeriodo.Hh_Mm_Ss_TotalStr;
                if (_tvwMedia != null) _tvwMedia.Text = _ViewModel.ResumoDoPeriodo.Por100MetrosStr;
                if (_tvwPorDia != null) _tvwPorDia.Text = _ViewModel.ResumoDoPeriodo.PorDiaStr;
                if (_tvwMil != null) _tvwMil.Text = _ViewModel.ResumoDoPeriodo.Por1000MetrosStr;

                if (_rvwLista != null)
                {
                    _adapter = new ListaTreinosNewAdapter(_ViewModel.ListaTreinos);
                    _adapter.ItemExcluir += Adapter_ItemExcluir;

                    _rvwLista.SetAdapter(_adapter);
                }
            }
            catch (Exception excErro)
            {
                exibirMensagem("Erro Lista", excErro.Message);
            }
        }

        private void exibirMensagem(string strTitulo,
                                    string strMensagem)
        {
            if (_alertBuilder == null)
                _alertBuilder = new AlertDialog.Builder(this.Context);

            _alertBuilder.SetTitle(strTitulo);
            _alertBuilder.SetMessage(strMensagem);
            _alertBuilder.SetNeutralButton("OK", (s, e) =>
            {
                _alert.Dismiss();
                _alert.Dispose();
            });

            _alert = _alertBuilder.Create();
            _alert.Show();
        }

        #endregion

        #region Eventos

        private void Adapter_ItemExcluir(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                _ViewModel.TreinoExcluir = (ListaTreinoDTO)sender;

                if (_excluirFragment == null)
                {
                    _excluirFragment = new TreinoExcluirFragment();

                    _excluirFragment.Evento_Excluir += ExcluirFragment_Evento_Excluir;
                    _excluirFragment.Evento_Fechar += ExcluirFragment_Evento_Fechar;
                    _excluirFragment.Evento_Ready += ExcluirFragment_Evento_Ready;

                    _excluirFragment.Show(FragmentManager, "FrgTreinoExcluir");
                }
            }
        }

        private void CadastroFragment_Evento_Fechar(object? sender, EventArgs e)
        {
            if (_cadastroFragment != null)
            {
                _cadastroFragment.Dismiss();
                _cadastroFragment.Dispose();
                _cadastroFragment = null;
            }
        }

        private async void CadastroFragment_Evento_Gravado(object? sender, EventArgs e)
        {
            try
            {
                if (_cadastroFragment != null)
                {
                    _cadastroFragment.Dismiss();
                    _cadastroFragment.Dispose();

                    _cadastroFragment = null;

                    IbnPesquisar_Click(null, EventArgs.Empty);
                }
            }
            catch (Exception excErro)
            {
                exibirMensagem("Erro Gravação", "- " + excErro.Message);
            }
        }

        private void DpdDe_DateSet(object? sender, DatePickerDialog.DateSetEventArgs e)
        {
            _ViewModel.PeriodoDe = _dpdDe.DatePicker.DateTime;
            _edt_de.Text = _ViewModel.PeriodoDe.ToString("dd/MM/yyyy");
        }

        private void DpdAte_DateSet(object? sender, DatePickerDialog.DateSetEventArgs e)
        {
            _ViewModel.PeriodoAte = _dpdAte.DatePicker.DateTime;
            _edt_ate.Text = _ViewModel.PeriodoAte.ToString("dd/MM/yyyy");
        }

        private void Edt_ate_Click(object? sender, EventArgs e)
        {
            if (_dpdAte == null)
            {
                _dpdAte = new DatePickerDialog(this.Context);

                long maxDateMillis = (long)(DateTime.Today.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;

                _dpdAte.DatePicker.MaxDate = maxDateMillis;
                _dpdAte.DateSet += DpdAte_DateSet;

                _dpdAte.DatePicker.DateTime = _ViewModel.PeriodoAte;
            }

            _dpdAte.Show();
        }

        private void Edt_de_Click(object? sender, EventArgs e)
        {
            if (_dpdDe == null)
            {
                _dpdDe = new DatePickerDialog(this.Context);

                long minDateMillis = (long)((new DateTime(2025, 1, 1)).ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;

                _dpdDe.DatePicker.MinDate = minDateMillis;
                _dpdDe.DateSet += DpdDe_DateSet;
                _dpdDe.DatePicker.DateTime = _ViewModel.PeriodoDe;
                
                _dpdDe.Show();
            }
        }

        private async void ExcluirFragment_Evento_Excluir(object? sender, EventArgs e)
        {
            if (_excluirFragment != null)
            {
                _excluirFragment.Dismiss();
                _excluirFragment.Dispose();
                _excluirFragment = null;

                if ((_ViewModel != null) &&
                    (_ViewModel.TreinoExcluir != null))
                {
                    try
                    {
                        if (await _Regras.Excluir())
                        {
                            _ViewModel.TreinoExcluir = null;

                            ExcluirFragment_Evento_Fechar(null, EventArgs.Empty);

                            await _Regras.Pesquisar();

                            exibirListaTreinos();
                        }
                        else
                        {
                            throw new Exception("- O Treino não foi excluído !");
                        }
                    }
                    catch (Exception excErro)
                    {
                        exibirMensagem("Erro na Exclusão", excErro.Message);
                    }
                }
            }
        }

        private void ExcluirFragment_Evento_Fechar(object? sender, EventArgs e)
        {
            if (_excluirFragment != null)
            {
                _excluirFragment.Dismiss();
                _excluirFragment.Dispose();
                _excluirFragment = null;
            }
        }

        private void ExcluirFragment_Evento_Ready(object? sender, EventArgs e)
        {
            if ((_ViewModel != null) &&
                (_ViewModel.TreinoExcluir != null))
                _excluirFragment.Publico_Treino(_ViewModel.TreinoExcluir);
        }

        private void IbnCadastro_Click(object? sender, EventArgs e)
        {
            if (_cadastroFragment == null)
            {
                _cadastroFragment = new TreinoCadastroFragment();
                _cadastroFragment.Evento_Fechar += CadastroFragment_Evento_Fechar;
                _cadastroFragment.Evento_Gravado += CadastroFragment_Evento_Gravado;

                _cadastroFragment.Show(FragmentManager, "FrgCadastro");
            }
        }

        private async void IbnPesquisar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_rvwLista != null)
                {
                    await _Regras.Pesquisar();

                    exibirListaTreinos();
                }
            }
            catch (Exception excErro)
            {
                Toast.MakeText(this.Context, excErro.Message, ToastLength.Long);
            }
        }

        #endregion

    }
}