using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace saleTool.Fragments
{
    public class Fragment3 : Fragment
    {
        List<int> AssignjobId;
        List<String> Cmlno, Branchname, Name, Regdate;
        private RecyclerView recyclerview;
        RecyclerView.LayoutManager recyclerview_layoutmanger;
        private RecyclerView.Adapter recyclerview_adapter;
        private ManifestListAdapter<ManifestList> ManifestListitems;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Fragment3 NewInstance()
        {
            var frag3 = new Fragment3 { Arguments = new Bundle() };
            return frag3;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            string _url = "";
            int userId = 1;
            AssignjobId = new List<int>();
            Cmlno = new List<String>();
            Name = new List<String>();
            Branchname = new List<String>();
            Regdate = new List<String>();
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            View _view = inflater.Inflate(Resource.Layout.manifest, null);
            recyclerview = _view.FindViewById<RecyclerView>(Resource.Id.recyclerview);

            List<ManifestList> manList = Getallmanifest();
            foreach (ManifestList man in manList)
            {
                AssignjobId.Add((int)man.AssignjobId);
                Cmlno.Add((string)man.Cmlno);
                Name.Add((string)man.Name);
                Branchname.Add((string)man.Branchname);
                Regdate.Add((string)man.Regdate);
            }
            ManifestListitems = new ManifestListAdapter<ManifestList>();
            foreach (var s in manList)
            {
                ManifestListitems.Add(s);
            }
            recyclerview_layoutmanger = new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false);
            recyclerview.SetLayoutManager(recyclerview_layoutmanger);
            recyclerview_adapter = new RecyclerAdapter(ManifestListitems, recyclerview, Context);
            recyclerview.SetAdapter(recyclerview_adapter);
            return _view;
        }
        public class RecyclerAdapter : RecyclerView.Adapter
        {
            private ManifestListAdapter<ManifestList> Mitems;
            private RecyclerView mrecyclerView;
            private LayoutInflater minflater;
            private Context mContext;

            public RecyclerAdapter(ManifestListAdapter<ManifestList> Mitems, RecyclerView recyclerView, Context context)
            {
                this.Mitems = Mitems;
                NotifyDataSetChanged();
                mrecyclerView = recyclerView;
                mContext = context;
            }
            public class MyView : RecyclerView.ViewHolder
            {
                public View mainview { get; set; }
                public TextView mtxtcmlno { get; set; }
                public TextView mtxtbranchname { get; set; }
                public TextView mtxtname { get; set; }
                public TextView mtxtregdate { get; set; }
                public Button mbtnview { get; set; }
                public EventHandler ClickHandler { get; set; }

                public MyView(View view) : base(view)
                {
                    mainview = view;
                }
            }
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View listitem = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.manifestitem, parent, false);
                TextView txtcmlno = listitem.FindViewById<TextView>(Resource.Id.txtcmlno);
                TextView txtbranchname = listitem.FindViewById<TextView>(Resource.Id.txtbranchname);
                TextView txtname = listitem.FindViewById<TextView>(Resource.Id.txtname);
                TextView txtregdate = listitem.FindViewById<TextView>(Resource.Id.txtregdate);
                Button btnview = listitem.FindViewById<Button>(Resource.Id.btnview);
                MyView view = new MyView(listitem) { mtxtcmlno = txtcmlno, mtxtbranchname = txtbranchname, mtxtname = txtname, mtxtregdate = txtregdate, mbtnview = btnview };
                return view;
            }
            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                MyView myholder = holder as MyView;
                myholder.mtxtcmlno.Text = Mitems[position].Cmlno;
                myholder.mtxtbranchname.Text = Mitems[position].Branchname;
                myholder.mtxtname.Text = Mitems[position].Name;
                myholder.mtxtregdate.Text = Mitems[position].Regdate;

                if (!myholder.mbtnview.HasOnClickListeners)
                {
                    myholder.mbtnview.Click += delegate {
                        Android.Support.V4.App.Fragment fragment = null;
                        fragment = Fragment4.NewInstance();
                        Bundle utilBundle = new Bundle();
                        utilBundle.PutString("cmlno_key", myholder.mtxtcmlno.Text);
                        ((AppCompatActivity)mContext).SupportFragmentManager.BeginTransaction()
                       .Replace(Resource.Id.content_frame, fragment)
                       .Commit();
                        fragment.Arguments = utilBundle;
                    };
                }
            }
            public override int ItemCount
            {
                get
                {
                    return Mitems.Count;
                }
            }
        }

        private List<ManifestList> Getallmanifest()
        {
            string _url = "";
            _url = "http://196.45.144.11:85/api/manifest/getmanifest/0/0/0";
            var request = System.Net.HttpWebRequest.Create(_url);
            request.ContentType = "application/json";
            request.Method = "GET";
            using (System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                {
                    var content = reader.ReadToEnd();
                    var deserialized = JsonConvert.DeserializeObject<List<ManifestList>>(content);
                    return deserialized;
                }
            }
        }
    }
}