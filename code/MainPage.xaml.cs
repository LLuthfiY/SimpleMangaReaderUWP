using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Manga
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public String[] supportedFormat = { ".jpg", ".png", ".bmp", ".gif", ".tiff", ".ico", ".svg" };
        public String rootDir;

        public MainPage()
        {
            this.InitializeComponent();
            editableTitleBar();

            try
            {
                rootDir = localSettings.Values["rootDir"].ToString();
            }
            catch
            {
                rootDir = "C:\\";
                localSettings.Values["rootDir"] = rootDir;
                localSettings.Values["currentPath"] = rootDir;
            }
            MyFrame.Navigate(typeof(FolderViewer), rootDir);
            
        }

        private void editableTitleBar()
        {
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Color.FromArgb(255, 26, 34, 41);
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }

        private async void FolderPicker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var folderPicker = new Windows.Storage.Pickers.FolderPicker();
                folderPicker.FileTypeFilter.Add("*");
                StorageFolder folder = await folderPicker.PickSingleFolderAsync();

                localSettings.Values["rootDir"] = folder.Path;
                localSettings.Values["currentPath"] = folder.Path;
                MyFrame.Navigate(typeof(FolderViewer), folder.Path);
            }
            catch
            {

            }
            


        }

        private void grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Back)
            {
                
            }
        }

        
    }
}
