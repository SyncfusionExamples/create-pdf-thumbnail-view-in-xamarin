using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PdfViewerThumbnail
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new PdfViewerView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
