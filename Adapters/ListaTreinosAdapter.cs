using Android.Views;
using AndroidX.RecyclerView.Widget;

using ColdWater.Models;

namespace ColdWater.Adapters
{
    public class ListaTreinosAdapter : RecyclerView.Adapter
    {
        #region Público

        public event EventHandler<ListaTreinosAdapterClickEventArgs> ItemClick;
        public event EventHandler<ListaTreinosAdapterClickEventArgs> ItemLongClick;

        public override int ItemCount => _items.Count;

        #endregion

        #region Variáveis da Classe

        private List<TreinoModel> _items = new List<TreinoModel>();

        #endregion

        #region ViewHolder

        public ListaTreinosAdapter(List<TreinoModel> items)
        {
            _items = items;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _items[position];

            var viewHolder = holder as ListaTreinosAdapterViewHolder;

            viewHolder.Publico_Item = item;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context)
                                          .Inflate(Resource.Layout.treino_item, parent, false);

            var vh = new ListaTreinosAdapterViewHolder(itemView, OnClick, OnLongClick);

            return vh;
        }

        #endregion

        #region Eventos

        void OnClick(ListaTreinosAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(ListaTreinosAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

        #endregion
    }

    public class ListaTreinosAdapterViewHolder : RecyclerView.ViewHolder
    {
        #region Público

        public TreinoModel Publico_Item
        {
            get { return _item; }
            set
            {
                _item = value;

                if (_tvwNumero != null)
                    _tvwNumero.Text = _item.Numero.ToString();
                if (_tvwData != null)
                    _tvwData.Text = _item.Dd_Mm_Yyyy;
                if (_tvwHora != null)
                    _tvwHora.Text = _item.Hh_Mm;
                if (_tvwDistancia != null)
                    _tvwDistancia.Text = _item.TotalStr;
                if (_tvwMedia != null)
                    _tvwMedia.Text = _item.Media_100_metros;

                if (_rlItem != null)
                    if (_item.Numero % 2 == 0)
                        _rlItem.SetBackgroundResource(Resource.Drawable.item_par);
                    else
                        _rlItem.SetBackgroundResource(Resource.Drawable.item_impar);
            }
        }

        #endregion

        #region Variáveis da Classe

        private View _itemView;
        private TreinoModel _item;

        private RelativeLayout? _rlItem;

        private TextView? _tvwNumero,
                          _tvwData,
                          _tvwHora,
                          _tvwDistancia,
                          _tvwMedia;

        private bool _checking = false;

        #endregion

        #region ViewHolder

        public ListaTreinosAdapterViewHolder(View itemView,
                                             Action<ListaTreinosAdapterClickEventArgs> clickListener,
                                             Action<ListaTreinosAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            _itemView = itemView;

            _rlItem = itemView.FindViewById<RelativeLayout>(Resource.Id.treino_item_rlItem);
            _tvwNumero = itemView.FindViewById<TextView>(Resource.Id.treino_item_tvwNumero);
            _tvwData = itemView.FindViewById<TextView>(Resource.Id.treino_item_tvwData);
            _tvwHora = itemView.FindViewById<TextView>(Resource.Id.treino_item_tvwHora);
            _tvwDistancia = itemView.FindViewById<TextView>(Resource.Id.treino_item_tvwDistancia);
            _tvwMedia = itemView.FindViewById<TextView>(Resource.Id.treino_item_tvwMedia);
        }

        #endregion
    }

    public class ListaTreinosAdapterClickEventArgs : EventArgs
    {
        public required View view { get; set; }
        public int position { get; set; }
    }
}