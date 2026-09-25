using Android.OS;
using Android.Views;

using AndroidX.AppCompat.App;

using ColdWater.Fragments;

namespace ColdWater
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Váriaveis da Classe

        private TreinosFragment _frgTreinos;

        #endregion

        #region Create

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            showTreinos();
        }

        public override bool OnCreateOptionsMenu(IMenu? menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);

            return true;
        }

        #endregion

        #region Private

        private void showTreinos()
        {
            if (_frgTreinos == null)
            {
                _frgTreinos = new TreinosFragment();

                FragmentTransaction ftTreinos = FragmentManager.BeginTransaction();

                ftTreinos.Replace(Resource.Id.activity_main_frlFragment, _frgTreinos);
                ftTreinos.Commit();
            }
        }

        #endregion
    }
}