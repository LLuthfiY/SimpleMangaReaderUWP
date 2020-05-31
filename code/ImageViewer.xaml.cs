using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;




// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Manga
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImageViewer : Page
    {
        public String[] supportedFormat = { ".jpg", ".png", ".bmp", ".gif", ".tiff", ".ico", ".svg" };
        public ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public String currentpath { get; set; }
        ObservableCollection<ImageList> ObservableImage = new ObservableCollection<ImageList>();
        public ImageViewer()
        {
            this.InitializeComponent();
            //FocusManager.TryMoveFocus(FocusNavigationDirection.Next);



        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            currentpath = (String)e.Parameter;
            currentpath = Path.GetFullPath(Path.Combine(currentpath, @"..\\"));
            
            GetFileAndFolderList(currentpath);

        }

        public void ButtonBack_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(FolderViewer), currentpath);
            

        }

        public async void GetFileAndFolderList(string path)
        {

            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(path);
            IReadOnlyList<StorageFile> files = await folder.GetFilesAsync();
            List<String> pathFiles = new List<string>();
            foreach (StorageFile file in files)
            {
                pathFiles.Add(file.Path);
            }
            
            refreshViewAsync(pathFiles);

        }
        public async void refreshViewAsync(List<String> PathsFile)
        {
            
                ObservableImage.Clear();
                foreach (String path in PathsFile)
                {
                    
                    String name = Path.GetFileName(path);
                    String type = Path.GetExtension(path);
                    if (supportedFormat.Contains(type.ToLower()))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        StorageFile file = await StorageFile.GetFileFromPathAsync(path);
                        using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                        {
                            await bitmapImage.SetSourceAsync(fileStream);
                        }

                        ObservableImage.Add(new ImageList
                        {
                            ImagePath = path,
                            ImageContent = bitmapImage
                        });
                    }
                }

            
            

        }

        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Back)
            {
                this.ButtonBack_Click(sender, e);
            }
            if (e.Key == Windows.System.VirtualKey.B)
            {
                this.getPreviousChapterAsync();
            }
            if (e.Key == Windows.System.VirtualKey.N)
            {
                this.getNextChapterAsync();
            }
        }

        private void scrollView_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            this.ButtonBack_Click(sender, e);
        }

        private async void getNextChapterAsync()
        {
            String path = Path.GetFullPath(Path.Combine(currentpath, @"..\\"));

            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(path);
            var folders = await folder.GetFoldersAsync();


            List<String> pathFolders = new List<string>();
            foreach (StorageFolder fold in folders)
            {
                pathFolders.Add(fold.Path);
            }
            int length = currentpath.Count();
            if (currentpath.Substring(length - 1).Equals("\\"))
            {
                currentpath = currentpath.Substring(0, length - 1);
            }
            int index = pathFolders.IndexOf(currentpath);

            try
            {
                GetFileAndFolderList(pathFolders[index + 1]);

                currentpath = pathFolders[index + 1];
                

            }
            catch
            {

            }

        }

        private async void getPreviousChapterAsync()
        {
            String path = Path.GetFullPath(Path.Combine(currentpath, @"..\\"));

            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(path);
            IReadOnlyList<StorageFolder> folders = await folder.GetFoldersAsync();

            List<String> pathFolders = new List<string>();
            foreach (StorageFolder fold in folders)
            {
                pathFolders.Add(fold.Path);
            }

            int length = currentpath.Count();
            if (currentpath.Substring(length - 1).Equals("\\"))
            {
                currentpath = currentpath.Substring(0, length - 1);
            }
            int index = pathFolders.IndexOf(currentpath);

            try
            {

                GetFileAndFolderList(pathFolders[index - 1]);

                currentpath = pathFolders[index - 1];
                

            }
            catch
            {

            }

        }



        private void scrollView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.getNextChapterAsync();
        }

        private void scrollView_Holding(object sender, HoldingRoutedEventArgs e)
        {
            this.getPreviousChapterAsync();
        }
    }

}
