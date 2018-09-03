using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Telecom;
using Android.Views;
using Android.Widget;
using Java.Sql;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace saleTool.Fragments
{
    public class Fragment2 : Fragment
    {
        List<int> CustomerId, ProductId, AssignjobId;
        List<String> Cusname, Productname, Assignjobname;
        Spinner manifest_spinner, product_spinner, customer_spinner;
        EditText edittext_quantity, edittext_amount;
        RadioGroup radioGroup_payment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public static Fragment2 NewInstance()
        {
            var frag2 = new Fragment2 { Arguments = new Bundle() };
            return frag2;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            CustomerId = new List<int>();
            Cusname = new List<String>();
            ProductId = new List<int>();
            Productname = new List<String>();
            AssignjobId = new List<int>();
            Assignjobname = new List<String>();
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            View _view = inflater.Inflate(Resource.Layout.sales, null);

            List<ManifestList> manList = Getmanifest();
            foreach (ManifestList man in manList)
            {
                AssignjobId.Add((int)man.AssignjobId);
                Assignjobname.Add((string)man.Assignjobname);
            }
            //branch spinner
            manifest_spinner = _view.FindViewById<Spinner>(Resource.Id.manifest_spinner);
            var manifest_adapter = new ArrayAdapter<string>(_view.Context, Android.Resource.Layout.SimpleSpinnerItem, Assignjobname);
            manifest_adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            manifest_spinner.Adapter = manifest_adapter;

            List<ProductList> proList = Getproduct();
            foreach (ProductList pro in proList)
            {
                ProductId.Add((int)pro.ProductId);
                Productname.Add((string)pro.Productname);
            }
            //product spinner
            product_spinner = _view.FindViewById<Spinner>(Resource.Id.product_spinner);
            var product_adapter = new ArrayAdapter<string>(_view.Context, Android.Resource.Layout.SimpleSpinnerItem, Productname);
            product_adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            product_spinner.Adapter = product_adapter;

            ////measurement spinner
            //Spinner measurementQty_spinner = _view.FindViewById<Spinner>(Resource.Id.measurementQty_spinner);
            ////measurementQty_spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(measurementQty_spinner_ItemSelected);
            //var measurementQty_adapter = ArrayAdapter.CreateFromResource(_view.Context, Resource.Array.measurementQty_array, Android.Resource.Layout.SimpleSpinnerItem);
            //measurementQty_adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //measurementQty_spinner.Adapter = measurementQty_adapter;

            List<CustomerList> cusList = Getcustomer();
            foreach (CustomerList cus in cusList)
            {
                CustomerId.Add((int)cus.CustomerId);
                Cusname.Add((string)cus.Cusname);
            }
            ////customer spinner
            customer_spinner = _view.FindViewById<Spinner>(Resource.Id.customer_spinner);
            var customer_adapter = new ArrayAdapter<string>(_view.Context, Android.Resource.Layout.SimpleSpinnerItem, Cusname);
            customer_adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            customer_spinner.Adapter = customer_adapter;

            edittext_quantity = _view.FindViewById<EditText>(Resource.Id.edittextquantity);
            edittext_amount = _view.FindViewById<EditText>(Resource.Id.edittextamount);
            radioGroup_payment = _view.FindViewById<RadioGroup>(Resource.Id.radioGrouppayment);
            Button btnsave = _view.FindViewById<Button>(Resource.Id.btnsave);
            btnsave.Click += btnsave_Click;
            return _view;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            postData();
        }

        private List<CustomerList> Getcustomer()
        {
            string _url = "";
            _url = "http://196.45.144.11:85/api/Spinner/0";
            var request = System.Net.HttpWebRequest.Create(_url);
            request.ContentType = "application/json";
            request.Method = "GET";
            using (System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                {
                    var content = reader.ReadToEnd();
                    var deserialized = JsonConvert.DeserializeObject<List<CustomerList>>(content);
                    return deserialized;
                }
            }
        }
        private List<ProductList> Getproduct()
        {
            string _url = "";
            _url = "http://196.45.144.11:85/api/Spinner/1";
            var request = System.Net.HttpWebRequest.Create(_url);
            request.ContentType = "application/json";
            request.Method = "GET";
            using (System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
                {
                    var content = reader.ReadToEnd();
                    var deserialized = JsonConvert.DeserializeObject<List<ProductList>>(content);
                    return deserialized;
                }
            }
        }
        private List<ManifestList> Getmanifest()
        {
            string _url = "";
            _url = "http://196.45.144.11:85/api/Spinner/2";
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

        private void postData()
        {
            string _customerName, _manifestName, _productName, _amt, _paymode, _qty;
            _manifestName = manifest_spinner.SelectedItem.ToString();
            _customerName = customer_spinner.SelectedItem.ToString();
            _productName = product_spinner.SelectedItem.ToString();
            _qty = edittext_quantity.Text == "" ? "0" : edittext_quantity.Text;
            _amt = edittext_amount.Text == "" ? "0" : edittext_amount.Text;
            _paymode = radioGroup_payment.Selected.ToString();
            var obj = new ManifestList
            {
                Name = _customerName,
                Cmlno = _manifestName,
                Productname = _productName,
                Saleqty = Convert.ToInt32(_qty),
                UserId = 1
            };
            
            if (_manifestName.Equals("-Select-"))
            {
                Toast.MakeText(Context, "Select Manifest", ToastLength.Long).Show();
            }
            else if (_customerName.Equals("-Select-"))
            {
                Toast.MakeText(Context, "Select Customer", ToastLength.Long).Show();
            }
            else if (_productName.Equals("-Select-"))
            {
                Toast.MakeText(Context, "Select Product", ToastLength.Long).Show();
            }
            else if (_qty.Equals("0"))
            {
                Toast.MakeText(Context, "Enter Quantity", ToastLength.Long).Show();
            }
            else if (_amt.Equals("0"))
            {
                Toast.MakeText(Context, "Enter Amount", ToastLength.Long).Show();
            }
            else
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://196.45.144.11:85/api/Manifest/AddManifest");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
                Toast.MakeText(Context, "Record Saved Successfully", ToastLength.Long).Show();
                manifest_spinner.SetSelection(0);
                customer_spinner.SetSelection(0);
                product_spinner.SetSelection(0);
                edittext_quantity.Text = "";
                edittext_amount.Text = "";
            }
        }
        private void refresh()
        {

        }
    }
}