using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ImageExporting
{
  public  class FontMappingHelper
    {
        public static string Thumbnail = "\uE700";
        public static string Export = "\uE701";    

          public static string SyncfusionFont =
         Device.RuntimePlatform == Device.Android ?

         "SyncfusionFont.ttf" :

         Device.RuntimePlatform == Device.iOS ?

         "SyncfusionFont" :

         "/Assets/SyncfusionFont.ttf#SyncfusionFont";
    }
}
