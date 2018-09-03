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
    public class Fragment1 : Fragment
    {
        private GridView gv;
        private HomeAdapter adapter;
        private JavaList<ManifestList> spacecrafts;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Fragment1 NewInstance()
        {
            var frag1 = new Fragment1 { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            View _view = inflater.Inflate(Resource.Layout.home, null);
            GridView gv = _view.FindViewById<GridView>(Resource.Id.lv);
            //BIND ADAPTER
            JavaList<ManifestList> manproList = Getmanifestproduct();
            adapter = new HomeAdapter(Context, manproList);
            gv.Adapter = adapter;
            return _view;
        }
        private JavaList<ManifestList> Getmanifestproduct()
        {
            string _url = "";
            _url = "http://196.45.144.11:85/api/dashboard/1";
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