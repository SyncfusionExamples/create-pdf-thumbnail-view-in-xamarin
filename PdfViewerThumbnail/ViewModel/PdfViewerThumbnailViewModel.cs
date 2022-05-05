using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using PdfViewerThumbnail.Model;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Syncfusion.ListView.XForms;
using Syncfusion.SfPdfViewer.XForms;

namespace PdfViewerThumbnail
{
    class PdfViewerThumbnailViewModel : INotifyPropertyChanged
    {
        private const int loadMoreItemsCount = 8;

        private Stream m_pdfDocumentStream;
        /// <summary>
        /// The PDF document stream that is loaded into the instance of the PDF viewer. 
        /// </summary>
        public Stream PdfDocumentStream
        {
            get
            {
                return m_pdfDocumentStream;
            }
            set
            {
                m_pdfDocumentStream = value;
                NotifyPropertyChanged("PdfDocumentStream");
            }
        }

        private ObservableCollection<ThumbnailModel> thumbnailImages = new ObservableCollection<ThumbnailModel>();
        /// <summary>
        /// Thumbnail image collection
        /// </summary>
        public ObservableCollection<ThumbnailModel> ThumbnailImages
        {
            get
            {
                return thumbnailImages;
            }
            set
            {
                thumbnailImages = value;
                NotifyPropertyChanged("ImageCollection");
            }
        }

        public Command<object> LoadMoreItemsCommand { get; set; }
        internal SfPdfViewer PdfViewer { get; set; }

        /// <summary>
        /// An event to detect the change in the value of a property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int pageNumber = 1;

        /// <summary>
        /// Page number to which the PDF Viewer control to be navigated
        /// </summary>
        public int PageNumber
        {
            get
            {
                return pageNumber;
            }
            set
            {
                pageNumber = value;
                NotifyPropertyChanged("PageNumber");
            }
        }

        /// <summary>
        /// Constructor of the view model class
        /// </summary>
        public PdfViewerThumbnailViewModel()
        {
            LoadMoreItemsCommand = new Command<object>(LoadMoreItems);
            //Accessing the PDF document that is added as embedded resource as stream.
            m_pdfDocumentStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("PdfViewerThumbnail.Assets.GIS Succinctly.pdf");
        }

        /// <summary>
        /// Load more items to the thumbnail view on-demand
        /// </summary>
        /// <param name="obj">List view</param>
        internal void LoadMoreItems(object obj)
        {
            var startPageIndex = this.ThumbnailImages.Count;
            if (startPageIndex == this.PdfViewer.PageCount)
            {
                return;
            }
            int endPageIndex = startPageIndex + loadMoreItemsCount;
            endPageIndex = endPageIndex > this.PdfViewer.PageCount - 1 ? this.PdfViewer.PageCount - 1 : endPageIndex - 1;

            AddThumbnailImages(obj as SfListView, startPageIndex, endPageIndex);
        }

        /// <summary>
        /// Export PDF pages as images and add them to the thumbnail view
        /// </summary>
        /// <param name="listView">list view object</param>
        /// <param name="startPageIndex">start page index of the document</param>
        /// <param name="endPageIndex">end page index of the document</param>
        internal async void AddThumbnailImages(SfListView listView, int startPageIndex, int endPageIndex)
        {
            if (!listView.IsBusy)
            {
                listView.IsBusy = true;
                Stream[] imageStreamList = null;
                imageStreamList = await PdfViewer?.ExportAsImageAsync(startPageIndex, endPageIndex, 0.3f);
                foreach (Stream imageStream in imageStreamList)
                {
                    imageStream.Position = 0;
                    byte[] bytes = ReadBytes(imageStream);
                    this.ThumbnailImages.Add(new ThumbnailModel(bytes));
                }
                listView.IsBusy = false;
            }
        }

        /// <summary>
        /// Convert image stream to byte array
        /// </summary>
        /// <param name="imageStream">image stream</param>
        /// <returns>image byte array</returns>
        internal byte[] ReadBytes(Stream imageStream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = imageStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}