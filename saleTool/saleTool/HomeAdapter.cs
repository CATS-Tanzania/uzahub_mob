using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

namespace saleTool
{
    class HomeAdapter : BaseAdapter
    {
        private readonly Context mContext;
        private readonly JavaList<ManifestList> mspacecrafts;
        private LayoutInflater inflater;

        /*
         * CONSTRUCTOR
         */
        public HomeAdapter(Context c, JavaList<ManifestList> spacecrafts)
        {
            this.mContext = c;
            this.mspacecrafts = spacecrafts;
        }

        /*
         * RETURN SPACECRAFT
         */
        public override Object GetItem(int position)
        {
            return mspacecrafts.Get(position);
        }

        /*
         * SPACECRAFT ID
         */
        public override long GetItemId(int position)
        {
            return position;
        }

        /*
         * RETURN INFLATED VIEW
         */
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //INITIALIZE INFLATER
            if (inflater == null)
            {
                inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
            }
            //INFLATE OUR MODEL LAYOUT
            if (convertView == null)
            {
                convertView = inflater.Inflate(Resource.Layout.homeitem, parent, false);
            }
            //BIND DATA
            HomeAdapterViewHolder holder = new HomeAdapterViewHolder(convertView)
            {
                txtviewdate = { Text = mspacecrafts[position].Regdate },
                txtviewproname = { Text = mspacecrafts[position].Productname },
                txtviewstockqty = { Text = mspacecrafts[position].Saleqty.ToString() }
            };

            return convertView;
        }
        /*
         * TOTAL NUM OF SPACECRAFTS
         */
        public override int Count
        {
            get { return mspacecrafts.Size(); }
        }
    }

    class HomeAdapterViewHolder : Java.Lang.Object
    {
        public TextView txtviewdate;
        public TextView txtviewproname;
        public TextView txtviewstockqty;
        public HomeAdapterViewHolder(View itemView)
        {
            txtviewdate = itemView.FindViewById<TextView>(Resource.Id.txtviewdate);
            txtviewproname = itemView.FindViewById<TextView>(Resource.Id.txtviewproname);
            txtviewstockqty = itemView.FindViewById<TextView>(Resource.Id.txtviewstockqty);
        }
    }
}