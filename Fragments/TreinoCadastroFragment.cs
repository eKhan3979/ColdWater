using Android.Views;

using System;

using ColdWater.Adapters;
using ColdWater.Models;
using ColdWater.ViewModels;
using ColdWater.Regras;

namespace ColdWater.Fragments
{
    public class TreinoCadastroFragment : DialogFragment
    {
        #region Público

        public event EventHandler Evento_Fechar,
                                  Evento_Gravado;

        #endregion

        #region Variáveis da Classe

        private TreinoCadastroViewModel _ViewModel;
        private TreinoCadastroRegras _Regras;

        private Spinner? _spnDistancia,
                         _spnH,
                         _spnMM,
                         _spnSS;

        private ImageButton? _ibnFechar,
                             _ibnGravar;

        private DatePickerDialog _dpdData;
        private TimePickerDialog _tpdHora;

        private EditText? _edtData,
                          _edtHora,
                          _edtTempo,
                          _edtPor100m;

        private AlertDialog.Builder? _alertBuilder,
                                     _alertBuilderGravar;
        private AlertDialog? _alert,
                             _alertGravar;

        #endregion

        #region Construtor

        public override View? OnCreateView(LayoutInflater? inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View? view = inflater.Inflate(Resource.Layout.treino_cadastro_fragment, container, false);

            _ViewModel = new TreinoCadastroViewModel();
            _Regras = new TreinoCadastroRegras(ref _ViewModel);

            _edtData = view.FindViewById<EditText>(Resource.Id.treino_cadastro_edtData);
            _edtHora = view.FindViewById<EditText>(Resource.Id.treino_cadastro_edtHora);
            _edtPor100m = view.FindViewById<EditText>(Resource.Id.treino_cadastro_edtPor100m);

            _edtData.Click += EdtData_Click;
            _edtHora.Click += EdtHora_Click;
            
            _edtPor100m.Text = "";

            _ibnFechar = view.FindViewById<ImageButton>(Resource.Id.treino_cadastro_ibnFechar);
            _ibnGravar = view.FindViewById<ImageButton>(Resource.Id.treino_cadastro_ibnGravar);

            _ibnFechar.Click += IbnFechar_Click;
            _ibnGravar.Click += IbnGravar_Click;

            _spnDistancia = view.FindViewById<Spinner>(Resource.Id.treino_cadastro_spnDistancia);
            _spnH = view.FindViewById<Spinner>(Resource.Id.treino_cadastro_spnH);
            _spnMM = view.FindViewById<Spinner>(Resource.Id.treino_cadastro_spnMM);
            _spnSS = view.FindViewById<Spinner>(Resource.Id.treino_cadastro_spnSS);

            _spnDistancia.ItemSelected += SpnDistancia_ItemSelected;
            _spnH.ItemSelected += SpnH_ItemSelected;
            _spnMM.ItemSelected += SpnMM_ItemSelected;
            _spnSS.ItemSelected += SpnSS_ItemSelected;

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();

            if (!_ViewModel.Iniciado)
            {
                try
                {
                    if (_Regras.Iniciar())
                    {
                        _edtData.Text = _ViewModel.TreinoEdit.Dd_Mm_Yyyy;
                        _edtHora.Text = _ViewModel.TreinoEdit.Hh_Mm;

                        ArrayAdapter adapterH = new ArrayAdapter(this.Context, Resource.Layout.spinner_item, _ViewModel.ListaH);

                        if (_spnH != null)
                            _spnH.Adapter = adapterH;

                        ArrayAdapter adapterMM = new ArrayAdapter(this.Context, Resource.Layout.spinner_item, _ViewModel.ListaMM);

                        if (_spnMM != null)
                            _spnMM.Adapter = adapterMM;

                        ArrayAdapter adapterSS = new ArrayAdapter(this.Context, Resource.Layout.spinner_item, _ViewModel.ListaSS);

                        if (_spnSS != null)
                            _spnSS.Adapter = adapterSS;

                        ArrayAdapter adapterDistancia = new ArrayAdapter(this.Context, Resource.Layout.spinner_item, _ViewModel.ListaDistancia);

                        if (_spnDistancia != null)
                            _spnDistancia.Adapter = adapterDistancia;
                    }
                }
                catch (Exception excErro)
                {
                    exibirMensagem("Erro Resume", "- " + excErro.Message);
                }
            }
        }

        #endregion

        #region Private

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

        private string getMedia()
        {
            string strM_Ss = "";

            try
            {
                if ((_ViewModel.TreinoEdit.Total > 0) &&
                    (_ViewModel.TreinoEdit.SegundosGastos > 0))
                {
                    int intMediaEmSegundos = (int)Math.Floor((1.0 * _ViewModel.TreinoEdit.SegundosGastos) /
                                                             ((1.0 * _ViewModel.TreinoEdit.Total) / 100.0));

                    int intM = intMediaEmSegundos / 60;

                    strM_Ss = intM.ToString() + ":" +
                             (100 + intMediaEmSegundos - intM * 60).ToString().Substring(1, 2);
                }
            }
            catch { }

            return strM_Ss;
        }

        private int getTempoEmSegundos()
        {
            int intSegundos = 0;

            try
            {
                if ((_ViewModel.Horas > 0) ||
                    (_ViewModel.Minutos > 0))
                    intSegundos = (int)Math.Floor(3600.0 * _ViewModel.Horas + 
                                                    60.0 * _ViewModel.Minutos + 
                                                           _ViewModel.Segundos);
            }
            catch { intSegundos = 0; }

            return intSegundos;
        }

        private async Task<bool> gravar()
        {
            try
            {
                _ViewModel.ErroIndex = 0;
                _ViewModel.ErroMsg.Clear();

                if (await _Regras.Gravar())
                {
                    if (_alertGravar != null)
                    {
                        _alertGravar.Dismiss();
                        _alertGravar.Dispose();
                        _alertGravar = null;
                    }

                    Evento_Gravado?.Invoke(_ViewModel.TreinoEdit.IdTreino, EventArgs.Empty);
                }
                else
                    throw new Exception("- Gravação NÃO realizada !");
            }
            catch (Exception excErro)
            {
                _ViewModel.ErroIndex = 1;
                _ViewModel.ErroMsg.AppendLine(excErro.Message);

                exibirMensagem("Erro Gravar 2", excErro.Message);
            }

            return (_ViewModel.ErroIndex == 0);
        }

        #endregion

        #region Eventos

        private void DpdData_DateSet(object? sender, DatePickerDialog.DateSetEventArgs e)
        {
            _ViewModel.TreinoEdit.Yyyy_Mm_Dd = e.Date.ToString("yyyy/MM/dd");

            _edtData.Text = _ViewModel.TreinoEdit.Dd_Mm_Yyyy;
        }

        private void EdtData_Click(object? sender, EventArgs e)
        {
            if (_dpdData == null)
            {
                _dpdData = new DatePickerDialog(this.Context);
                _dpdData.DateSet += DpdData_DateSet;
            }

            _dpdData.Show();
        }

        private void EdtHora_Click(object? sender, EventArgs e)
        {
            if (_tpdHora == null)
                _tpdHora = new TimePickerDialog(this.Context, TpdHora_TimeSet, 0, 0, true);

            _tpdHora.Show();
        }

        private void IbnFechar_Click(object? sender, EventArgs e)
        {
            Evento_Fechar?.Invoke(this, EventArgs.Empty);
        }

        private void IbnGravar_Click(object? sender, EventArgs e)
        {
            if (_Regras.PodeGravar())
            {
                try
                {
                    if (_alertBuilderGravar == null)
                        _alertBuilderGravar = new AlertDialog.Builder(this.Context);

                    _alertBuilderGravar.SetTitle("Cadastro de Treinos");
                    _alertBuilderGravar.SetMessage("- Confirma a gravação ?");

                    _alertBuilderGravar.SetPositiveButton("Sim", async (s, e) =>
                    {
                        await gravar();
                    });

                    _alertBuilderGravar.SetNegativeButton("Não", (s, e) =>
                    {
                        if (_alertGravar != null)
                        {
                            _alertGravar.Dismiss();
                            _alertGravar.Dispose();
                            _alertGravar = null;
                        }
                    });

                    _alertGravar = _alertBuilderGravar.Create();
                    _alertGravar.Show();
                }
                catch (Exception excErro)
                {
                    exibirMensagem("Erro Gravar", excErro.Message);
                }
            }
            else
                exibirMensagem("Operação não permitida", _ViewModel.ErroMsg.ToString());
        }

        private void SpnDistancia_ItemSelected(object? sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                _ViewModel.TreinoEdit.Total = int.Parse(_ViewModel.ListaDistancia[e.Position].Replace(".", "").Replace(",", ""));
                _ViewModel.TreinoEdit.SegundosGastos = getTempoEmSegundos();
                _ViewModel.TreinoEdit.Media_100_metros = getMedia();

                _edtPor100m.Text = _ViewModel.TreinoEdit.Media_100_metros;
            }
            catch (Exception excErro)
            {
                exibirMensagem("Erro Distância", excErro.Message);
            }
        }

        private void SpnH_ItemSelected(object? sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                _ViewModel.Horas = int.Parse(_ViewModel.ListaH[e.Position].Replace(".", "").Replace(",", ""));
                _ViewModel.TreinoEdit.SegundosGastos = getTempoEmSegundos();
                _ViewModel.TreinoEdit.Media_100_metros = getMedia();

                _edtPor100m.Text = _ViewModel.TreinoEdit.Media_100_metros;
            }
            catch { _ViewModel.Horas = 0; }
        }

        private void SpnMM_ItemSelected(object? sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                _ViewModel.Minutos = int.Parse(_ViewModel.ListaMM[e.Position].Replace(".", "").Replace(",", ""));
                _ViewModel.TreinoEdit.SegundosGastos = getTempoEmSegundos();
                _ViewModel.TreinoEdit.Media_100_metros = getMedia();

                _edtPor100m.Text = _ViewModel.TreinoEdit.Media_100_metros;
            }
            catch { _ViewModel.Minutos = 0; }
        }

        private void SpnSS_ItemSelected(object? sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                _ViewModel.Segundos = int.Parse(_ViewModel.ListaSS[e.Position].Replace(".", "").Replace(",", ""));
                _ViewModel.TreinoEdit.SegundosGastos = getTempoEmSegundos();
                _ViewModel.TreinoEdit.Media_100_metros = getMedia();

                _edtPor100m.Text = _ViewModel.TreinoEdit.Media_100_metros;
            }
            catch { _ViewModel.Segundos = 0; }
        }

        private void TpdHora_TimeSet(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            if (_edtHora != null)
            {
                _ViewModel.TreinoEdit.Hh_Mm = (e.HourOfDay + 100).ToString().Substring(1, 2) + ":" +
                                              (e.Minute + 100).ToString().Substring(1, 2);
                _edtHora.Text = _ViewModel.TreinoEdit.Hh_Mm;
            }
        }

        #endregion
    }
}