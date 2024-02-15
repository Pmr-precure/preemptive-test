using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.Threading.Tasks;

namespace Test.Droid
{
    [Activity(Label = "Test", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            TamperCheck();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void TamperCheck()
        {
        } 
        private void TamperSink(bool tampered)
        {
            // DebugT.WriteLine(tampered.ToString());
            if (tampered)
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetMessage("Tamper detected");
                builder.SetCancelable(false);
                builder.SetPositiveButton("OK", (sender, args) =>
                {
                    // Close the app after the OK button is clicked
                    Java.Lang.JavaSystem.Exit(0);
                });

                this.RunOnUiThread(() =>
                {
                    builder.Create().Show();
                });
                Task.Run(async () =>
                {
                    await Task.Delay(10000);
                    Java.Lang.JavaSystem.Exit(0);


                });

            }

        }
    }
}