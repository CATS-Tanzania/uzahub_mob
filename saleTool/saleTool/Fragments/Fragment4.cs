using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace saleTool.Fragments
{
    public class Fragment4 : Fragment
    {
        private GridView gv;
        private CustomAdapter adapter;
        private JavaList<ManifestList> spacecrafts;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Fragment4 NewInstance()
        {
            var frag1 = new Fragment4 { Arguments = new Bundle() };
            return frag1;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var cmlno_key = Arguments.GetString("cmlno_key", string.Empty);
            View _view = inflater.Inflate(Resource.Layout.view_manifest, null);
            TextView txtviewcmlno = _view.FindViewById<TextView>(Resource.Id.txtviewcmlno);
            TextView txtviewtruckno = _view.FindViewById<TextView>(Resource.Id.txtviewtruckno);
            TextView txtviewdrivername = _view.FindViewById<TextView>(Resource.Id.txtviewdrivername);
            TextView txtviewdrvlncno = _view.FindViewById<TextView>(Resource.Id.txtviewdrvlncno);
            TextView txtviewconductorname = _view.FindViewById<TextView>(Resource.Id.txtviewconductorname);
            TextView txtviewdate = _view.FindViewById<TextView>(Resource.Id.txtviewdate);
            List<ManifestList> manList = Getmanifest(cmlno_key);
            foreach (ManifestList man in manList)
            {
                txtviewcmlno.Text = man.Cmlno;
                txtviewtruckno.Text = man.Truckno;
                txtviewdrivername.Text = man.Drivername;
                txtviewdrvlncno.Text = man.Drvlncno;
                txtviewconductorname.Text = man.Conductorname;
                txtviewdate.Text = man.Regdate;
            }
            GridView gv = _view.FindViewById<GridView>(Resource.Id.lv);
            //BIND ADAPTER
            JavaList<ManifestList> manproList = Getmanifestproduct(cmlno_key);
            adapter = new CustomAdapter(Context, manproList);
            gv.Adapter = adapter;
            return _view;
        }
        private List<ManifestList> Getmanifest(string cmlno_key)
        {
            string _url = "";
            _url = "http://196.45.144.11:85/api/manifest/getmanifest/0/" + cmlno_key + "/0";
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
        private JavaList<ManifestList> Getmanifestproduct(string cmlno_key)
        {
            string _url = "";
            _url = "http://196.45.144.11:85/api/manifest/getmanifest/0/" + cmlno_key + "/1";
            var request = System.Net.HttpWebRequest.Create(_url);
            request.ContentType = "application/json";
            request.Method = "GET";
            using (System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                {
                    var content = reader.ReadToEnd();
                    var deserialized = JsonConvert.DeserializeObject<JavaList<ManifestList>>(content);
                    return deserialized;
                }
            }
        }
    }
}