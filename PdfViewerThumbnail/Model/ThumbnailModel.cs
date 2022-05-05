using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PdfViewerThumbnail.Model
{
    public class ThumbnailModel: INotifyPropertyChanged
    {
        private byte[] _imageSource;

        public byte[] ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                this.RaisedOnPropertyChanged("ImageSource");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ThumbnailModel(byte[] imageSource)
        {
            ImageSource = imageSource;
        }

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }
    }
}
