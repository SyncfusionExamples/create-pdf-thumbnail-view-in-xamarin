using PdfViewerThumbnail.Model;
using Syncfusion.ListView.XForms;
using Syncfusion.SfPdfViewer.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PdfViewerThumbnail
{

    public partial class PdfViewerView : ContentPage
    {
        public PdfViewerView()
        {
            InitializeComponent();
            (BindingContext as PdfViewerThumbnailViewModel).PdfViewer = this.pdfViewerControl;
            this.pdfViewerControl.IsToolbarVisible = false;
            pdfViewerControl.DocumentLoaded += pdfViewerControl_DocumentLoaded;
        }

        private void pdfViewerControl_DocumentLoaded(object sender, EventArgs args)
        {
            int initialThumbnailsCount = 8;            

            // PdfViewerControl object that helps to navigate to the selected page index
            pageThumbnails.PdfViewerControl = this.pdfViewerControl;

            // Export PDF pages as images and add them to the thumbnail view
            var pageCount = pdfViewerControl.PageCount > initialThumbnailsCount ? initialThumbnailsCount :
                pdfViewerControl.PageCount;

            (BindingContext as PdfViewerThumbnailViewModel).AddThumbnailImages(pageThumbnails.listView, 0, pageCount - 1);
        }
    }
}