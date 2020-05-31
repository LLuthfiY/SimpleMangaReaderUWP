using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;




// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Manga
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    
    public sealed partial class FolderViewer : Page
    {
        public String[] supportedFormat = { ".jpg", ".png", ".bmp", ".gif", ".tiff", ".ico", ".svg" };
        ObservableCollection<ListItem> ObservableItemList = new ObservableCollection<ListItem>();
        ObservableCollection<ListItem> TempObservableItemList = new ObservableCollection<ListItem>();
        public ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        
        String currentPath;

        public FolderViewer()
        {
            this.InitializeComponent();
        }



        private void gotoImageViewer_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ImageViewer));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            String path = (String)e.Parameter;
            currentPath = (String)path.Clone();
            GetFileAndFolderList(path);

            TempObservableItemList = new ObservableCollection<ListItem>(ObservableItemList);
            DirBox.Text = path;
            localSettings.Values["currentPath"] = path;
        }



        public async void GetFileAndFolderList(string path)
        {
            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(path);
            var files = await folder.GetFilesAsync();
            var folders = await folder.GetFoldersAsync();

            List<String> pathFolders = new List<string>();
            List<String> pathFiles = new List<string>();


            foreach (StorageFolder file in folders)
            {
                pathFolders.Add(file.Path);
            }
            foreach (StorageFile file in files)
            {
                pathFiles.Add(file.Path);
            }
            
            refreshView(pathFolders, pathFiles);
        }

        public void refreshView(List<string> Paths, List<String> PathsFile)
        {

            ObservableItemList.Clear();
            foreach (String str in Paths)
            {
                ObservableItemList.Add(
                    new ListItem
                    {
                        Path = str,
                        Name = Path.GetFileName(str),
                        isFolder = true
                    }
                    );
            }
            
            foreach (String str in PathsFile)
            {
                String type = Path.GetExtension(str);
                if (supportedFormat.Contains(type.ToLower()))
                {
                    ObservableItemList.Add(
                    new ListItem
                    {
                        Path = str,
                        Name = Path.GetFileName(str),
                        isFolder = false
                    }
                    );
                }
            }
            TempObservableItemList = new ObservableCollection<ListItem>(ObservableItemList);
        }

        public void back_Click(object sender, RoutedEventArgs e)
        {
            listView.Focus(FocusState.Keyboard);
            string newPath = Path.GetFullPath(Path.Combine(currentPath, @"..\\"));

            GetFileAndFolderList(newPath);

            DirBox.Text = newPath;
            TempObservableItemList = new ObservableCollection<ListItem>(ObservableItemList);
            currentPath = newPath;

        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            
                var clickedItem = (ListItem)e.ClickedItem;
                if (clickedItem.isFolder)
                {

                    DirBox.Text = "wait";
                    GetFileAndFolderList(clickedItem.Path);

                    TempObservableItemList = new ObservableCollection<ListItem>(ObservableItemList);
                    localSettings.Values["currentPath"] = clickedItem.Path;
                    DirBox.Text = clickedItem.Path;
                    currentPath = clickedItem.Path;

                }
                else
                {
                    localSettings.Values["fromImagePath"] = currentPath;
                    this.Frame.Navigate(typeof(ImageViewer), clickedItem.Path);
                }
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableItemList.Clear();


            foreach (ListItem li in TempObservableItemList)
            {

                if (li.Name.ToLower().Contains(searchBox.Text.ToLower()))
                {
                    ObservableItemList.Add(li);
                }

            }
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = "";
            searchBox.GotFocus -= searchBox_GotFocus;
        }





        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Back)
            {
                this.back_Click(sender, e);
            }
        }

        public void backFOlder(String path)
        {

            string newPath = Path.GetFullPath(Path.Combine(path, @"..\\"));
            localSettings.Values["currentPath"] = newPath;
            GetFileAndFolderList(newPath);

            DirBox.Text = newPath;
            TempObservableItemList = new ObservableCollection<ListItem>(ObservableItemList);
            currentPath = newPath;

        }

        private void listView_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            this.back_Click(sender, e);
        }
    }
}
