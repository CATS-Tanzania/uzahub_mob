using Android.App;
using Android.OS;
using saleTool.Fragments;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using System;

namespace saleTool
{
    [Activity(Label = "@string/app_name", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        BottomNavigationView bottomNavigation;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.login);
            Button btn_logIn = FindViewById<Button>(Resource.Id.btn_logIn);
            btn_logIn.Click += btn_logIn_Click;
        }
        private void btn_logIn_Click(object sender, EventArgs e)
        {
            string status = "", _url = "";
            EditText _username = FindViewById<EditText>(Resource.Id.username);
            EditText _password = FindViewById<EditText>(Resource.Id.password);
            if (_username.Text != "" && _password.Text != "")
            {
                _url = "http://196.45.144.11:85/api/Login/GetLogin/" + _username.Text + "," + _password.Text;
                var request = System.Net.HttpWebRequest.Create(_url);
                request.ContentType = "application/json";
                request.Method = "GET";
                using (System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        var result = System.Text.RegularExpressions.Regex.Replace(content, @"[^a-zA-Z0-9]", "");
                        status = result;
                    }
                }
                if (status == "SUCCESSFUL")
                {
                    SetContentView(Resource.Layout.main);
                    var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                    if (toolbar != null)
                    {
                        SetSupportActionBar(toolbar);
                        SupportActionBar.SetDisplayHomeAsUpEnabled(false);
                        SupportActionBar.SetHomeButtonEnabled(false);
                    }
                    bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
                    bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
                    LoadFragment(Resource.Id.menu_home);
                }
                else
                {
                    Toast.MakeText(this, "Wrong credentials found!", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Please enter credentials!", ToastLength.Long).Show();
            }
        }
        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }
        public void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.menu_home:
                    fragment = Fragment1.NewInstance();
                    break;
                case Resource.Id.menu_audio:
                    fragment = Fragment2.NewInstance();
                    break;
                case Resource.Id.menu_video:
                    fragment = Fragment3.NewInstance();
                    break;
            }
            if (fragment == null)
                return;

            SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, fragment)
               .Commit();
        }
    }
}

