using Android.Graphics;
using Android.Views;

using AndroidX.RecyclerView.Widget;

using ColdWater.Models;

namespace ColdWater.Adapters
{
    public class ListaTreinosNewAdapter: RecyclerView.Adapter
    {
        #region Público

        public event EventHandler<ListaTreinosNewAdapterClickEventArgs> ItemClick;
        public event EventHandler<ListaTreinosNewAdapterClickEventArgs> ItemLongClick;
        public event EventHandler<EventArgs> ItemExcluir;

        public override int ItemCount => _items.Count;

        #endregion

        #region Variáveis da Classe

        private List<ListaTreinoDTO> _items = new List<ListaTreinoDTO>();

        #endregion

        #region ViewHolder

        public ListaTreinosNewAdapter(List<ListaTreinoDTO> items)
        {
            _items = items;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _items[position];

            var viewHolder = holder as ListaTreinosNewAdapterViewHolder;

            viewHolder.Publico_Item = item;
            viewHolder.Evento_Excluir += ViewHolder_Evento_Excluir;
        }

        private void ViewHolder_Evento_Excluir(object? sender, EventArgs e)
        {
            ItemExcluir?.Invoke(sender, EventArgs.Empty);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context)
                                          .Inflate(Resource.Layout.treino_new_item, parent, false);

            var vh = new ListaTreinosNewAdapterViewHolder(itemView, OnClick, OnLongClick);

            return vh;
        }

        #endregion

        #region Eventos

        void OnClick(ListaTreinosNewAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(ListaTreinosNewAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

        #endregion
    }

    public class ListaTreinosNewAdapterViewHolder : RecyclerView.ViewHolder
    {
        #region Público

        public event EventHandler Evento_Excluir;

        public ListaTreinoDTO Publico_Item
        {
            get { return _item; }
            set
            {
                _item = value;

                if (_tvwIdentificador != null)
                    _tvwIdentificador.Text = _item.Identificador;
                if (_tvwData != null)
                    _tvwData.Text = _item.Dd_Mm_Yyyy;
                if (_tvwHora != null)
                    _tvwHora.Text = _item.Hh_Mm;
                if (_tvwDistancia != null)
                    _tvwDistancia.Text = _item.TotalStr;
                if (_tvwTempo != null)
                    _tvwTempo.Text = _item.Hh_Mm_Ss_Gastos;
                if (_tvwMedia != null)
                    _tvwMedia.Text = _item.Media_100_metros;

                if (_llItem != null)
                    if (_item.Numero > 0)
                        if (_item.Numero % 2 == 0)
                            _llItem.SetBackgroundResource(Resource.Drawable.item_par);
                        else
                            _llItem.SetBackgroundResource(Resource.Drawable.item_impar);
                    else
                    {
                        if (_ibnExcluir != null)
                            _ibnExcluir.Visibility = ViewStates.Invisible;

                        if (_item.Numero == 0)
                        {
                            _llItem.SetBackgroundResource(Resource.Drawable.item_titulo);
                            _llItem.SetMinimumHeight(32);

                            if (_tvwIdentificador != null)
                                _tvwIdentificador.SetTextColor(Color.White);
                            if (_tvwData != null)
                                _tvwData.SetTextColor(Color.White);
                            if (_tvwHora != null)
                                _tvwHora.SetTextColor(Color.White);
                            if (_tvwDistancia != null)
                                _tvwDistancia.SetTextColor(Color.White);
                            if (_tvwTempo != null)
                                _tvwTempo.SetTextColor(Color.White);
                            if (_tvwMedia != null)
                                _tvwMedia.SetTextColor(Color.White);
                        }
                        else
                        {
                            _llItem.SetBackgroundResource(Resource.Drawable.item_subtotal);
                            /*
                            if (_tvwData != null)
                                _tvwData.SetTextColor(Color.White);
                            if (_tvwDistancia != null)
                                _tvwDistancia.SetTextColor(Color.White);
                            if (_tvwTempo != null)
                                _tvwTempo.SetTextColor(Color.White);
                            if (_tvwMedia != null)
                                _tvwMedia.SetTextColor(Color.White);
                            */
                        }
                    }
            }
        }

        #endregion

        #region Variáveis da Classe

        private View _itemView;
        private ListaTreinoDTO _item;

        private LinearLayout? _llItem;

        private TextView? _tvwIdentificador,
                          _tvwData,
                          _tvwHora,
                          _tvwDistancia,
                          _tvwTempo,
                          _tvwMedia;

        private ImageButton? _ibnExcluir;

        private bool _checking = false;

        #endregion

        #region ViewHolder

        public ListaTreinosNewAdapterViewHolder(View itemView,
                                                Action<ListaTreinosNewAdapterClickEventArgs> clickListener,
                                                Action<ListaTreinosNewAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            _itemView = itemView;

            _llItem = itemView.FindViewById<LinearLayout>(Resource.Id.treino_new_llItem);
            _tvwIdentificador = itemView.FindViewById<TextView>(Resource.Id.treino_new_tvwIdentificador);
            _tvwData = itemView.FindViewById<TextView>(Resource.Id.treino_new_tvwData);
            _tvwHora = itemView.FindViewById<TextView>(Resource.Id.treino_new_tvwHora);
            _tvwDistancia = itemView.FindViewById<TextView>(Resource.Id.treino_new_tvwDistancia);
            _tvwTempo = itemView.FindViewById<TextView>(Resource.Id.treino_new_tvwTempo);
            _tvwMedia = itemView.FindViewById<TextView>(Resource.Id.treino_new_tvwMedia);
            _ibnExcluir = itemView.FindViewById<ImageButton>(Resource.Id.treino_new_ibnExcluir);
            
            if (_ibnExcluir != null)
                _ibnExcluir.Click += IbnExcluir_Click;
        }

        private void IbnExcluir_Click(object? sender, EventArgs e)
        {
            Evento_Excluir?.Invoke(Publico_Item, EventArgs.Empty);
        }

        #endregion
    }

    public class ListaTreinosNewAdapterClickEventArgs : EventArgs
    {
        public required View view { get; set; }
        public int position { get; set; }
    }
}