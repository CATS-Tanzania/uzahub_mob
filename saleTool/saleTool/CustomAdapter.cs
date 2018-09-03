using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

namespace saleTool
{
    class CustomAdapter : BaseAdapter
    {
        private readonly Context mContext;
        private readonly JavaList<ManifestList> mspacecrafts;
        private LayoutInflater inflater;

        /*
         * CONSTRUCTOR
         */
        public CustomAdapter(Context c, JavaList<ManifestList> spacecrafts)
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
                convertView = inflater.Inflate(Resource.Layout.view_manifestproduct, parent, false);
            }
            //BIND DATA
            CustomAdapterViewHolder holder = new CustomAdapterViewHolder(convertView)
            {
                txtviewproname = { Text = mspacecrafts[position].Productname },
                txtviewstockqty = { Text = mspacecrafts[position].Stockqty.ToString() }
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

    class CustomAdapterViewHolder : Java.Lang.Object
    {
        public TextView txtviewproname;
        public TextView txtviewstockqty;
        public CustomAdapterViewHolder(View itemView)
        {
            txtviewproname = itemView.FindViewById<TextView>(Resource.Id.txtviewproname);
            txtviewstockqty = itemView.FindViewById<TextView>(Resource.Id.txtviewstockqty);
        }
    }
}