using PdfViewerThumbnail.Model;
using Syncfusion.SfPdfViewer.XForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PdfViewerThumbnail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThumbnailView : ContentView
    {
        internal SfPdfViewer PdfViewerControl;
        public ThumbnailView()
        {
            InitializeComponent();
        }

        private void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            ThumbnailModel thumbnailModel = e.ItemData as ThumbnailModel;

            //Selected page index of the thumbnail view
            int pageIndex = (BindingContext as PdfViewerThumbnailViewModel).ThumbnailImages.IndexOf(thumbnailModel);

            //Bindable property of PdfViewerControl's PageNumber
            (BindingContext as PdfViewerThumbnailViewModel).PageNumber = pageIndex + 1;
        }
    }
}