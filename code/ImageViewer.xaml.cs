using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;




// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Manga_Tosaku
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImageViewer : Page
    {
        public String[] supportedFormat = { ".jpg", ".png", ".bmp", ".tiff", ".ico", ".svg" };
        public ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public String currentpath { get; set; }
        ObservableCollection<ImageList> ObservableImage = new ObservableCollection<ImageList>();
        

        // 200, 200, 100, 33, 33, 33

        int red1 = 200;
        int green1 = 200;
        int blue1 = 100;

        int red2 = 33;
        int green2 = 65;
        int blue2 = 33;

        bool colorTone;
        public ImageViewer()
        {
            
            
            this.InitializeComponent();
            //FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
            try
            {
                colorTone = Convert.ToBoolean(localSettings.Values["colorTone"]);
                ColorToneSwitch.IsOn = true;

                red1 = Int32.Parse(localSettings.Values["red1"].ToString());
                green1 = Int32.Parse(localSettings.Values["green1"].ToString());
                blue1 = Int32.Parse(localSettings.Values["blue1"].ToString());

                red2 = Int32.Parse(localSettings.Values["red2"].ToString());
                green2 = Int32.Parse(localSettings.Values["green2"].ToString());
                blue2 = Int32.Parse(localSettings.Values["blue2"].ToString());
            }
            catch
            {
                VisualStateManager.GoToState(ColorToneSwitch, "False", false);

                localSettings.Values["red1"] = red1.ToString();
                localSettings.Values["green1"] = green1.ToString();
                localSettings.Values["blue1"] = blue1.ToString();

                localSettings.Values["red2"] = red2.ToString();
                localSettings.Values["green2"] = green2.ToString();
                localSettings.Values["blue2"] = blue2.ToString();

            }

            RED1.Text = red1.ToString();
            RED2.Text = red2.ToString();
            GREEN1.Text = green1.ToString();
            GREEN2.Text = green2.ToString();
            BLUE1.Text = blue1.ToString();
            BLUE2.Text = blue2.ToString();

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
                    /*
                    ImageManagement im = new ImageManagement();
                    BitmapImage bitmapImage = new BitmapImage();
                    StorageFile file = await StorageFile.GetFileFromPathAsync(path);
                    using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                    {
                        await bitmapImage.SetSourceAsync(fileStream);

                    }
                    bitmapImage = im.ColorTintAsync(bitmapImage, 200, 200, 100, 33, 33, 33).Result;
                    */
                    //========================= edit ====================================   

                    //WriteableBitmap wb = BitmapFactory.New(512, 512);
                    
                    StorageFile file = await StorageFile.GetFileFromPathAsync(path);
                    using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                    {

                        WriteableBitmap wb = await BitmapFactory.New(1, 1).FromStream(fileStream);
                        //wb.FromStream(fileStream);
                        
                        if (ColorToneSwitch.IsOn == true)
                        {
                            wb.ForEach((x, y, Color) => Color.FromArgb((byte)Color.A,
                                                                (byte)(((float)Color.R / 255f) * (red1 - red2) + red2),
                                                                (byte)(((float)Color.G / 255f) * (green1 - green2) + green2),
                                                                (byte)(((float)Color.B / 255f) * (blue1 - blue2) + blue2)
                                                                )
                        );
                        }

                        ObservableImage.Add(new ImageList
                        {
                            ImagePath = path,
                            ImageContent = wb
                        });
                    }
                    
                    //bitmapImage = ColorTintAsync(wb, 200, 200, 100, 33, 33, 33);

                    


                    //=====================================================================
                    
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

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var sp = Setting_panel;
            if (sp.Visibility == Visibility.Visible)
            {
                sp.Visibility = Visibility.Collapsed;
            }
            else
            {
                sp.Visibility = Visibility.Visible;
            }
        }
        /*
        private void ColorToneSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            localSettings.Values["colorTone"] = ColorToneSwitch.IsOn;
            colorTone = ColorToneSwitch.IsOn;
            GetFileAndFolderList(currentpath);
        }
        */
        private void ColorToneApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tempred1 = Int32.Parse(RED1.Text);
                int tempred2 = Int32.Parse(RED2.Text);

                int tempgreen1 = Int32.Parse(GREEN1.Text);
                int tempgreen2 = Int32.Parse(GREEN2.Text);

                int tempblue1 = Int32.Parse(BLUE1.Text);
                int tempblue2 = Int32.Parse(BLUE2.Text);

                if (-1 < tempred1 & tempred1 < 256)
                {
                    red1 = tempred1;
                    localSettings.Values["red1"] = tempred1.ToString();
                }

                if (-1 < tempred2 & tempred2 < 256)
                {
                    red2 = tempred2;
                    localSettings.Values["red2"] = tempred2.ToString();
                }

                if (-1 < tempgreen1 & tempgreen1 < 256)
                {
                    green1 = tempgreen1;
                    localSettings.Values["green1"] = tempgreen1.ToString();
                }

                if (-1 < tempgreen2 & tempgreen2 < 256)
                {
                    green2 = tempgreen2;
                    localSettings.Values["green2"] = tempgreen2.ToString();
                }

                if (-1 < tempblue1 & tempblue1 < 256)
                {
                    blue1 = tempblue1;
                    localSettings.Values["blue1"] = tempblue1.ToString();
                }

                if (-1 < tempblue2 & tempblue2 < 256)
                {
                    blue2 = tempblue2;
                    localSettings.Values["blue2"] = tempblue2.ToString();
                }

                GetFileAndFolderList(currentpath);
            }
            catch
            {


            }
        }

        //============================ Playing With Color and image Type ===========================









        public BitmapImage ColorTintAsync(WriteableBitmap wb,
                                     int red1, int green1, int blue1,
                                     int red2, int green2, int blue2)
        {

            /*

            wb.ForEach((x, y, Color) => Windows.UI.Color.FromArgb(Color.A,
                                                                 (byte)((Color.R / 255) * (red1 - red2) + red2),
                                                                 (byte)((Color.G / 255) * (green1 - green2) + green2),
                                                                 (byte)((Color.B / 255) * (blue1 - blue2) + blue2)
                                                                 ));
            */
            textoyy.Text = "masuk 2";
            Byte[] bytes = wb.PixelBuffer.ToArray();

            return ImageFromBytes(bytes).Result;


        }
        public async static Task<BitmapImage> ImageFromBytes(Byte[] bytes)
        {
            BitmapImage image = new BitmapImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(bytes.AsBuffer());
                stream.Seek(0);
                await image.SetSourceAsync(stream);
            }
            return image;
        }

        
    }

}


