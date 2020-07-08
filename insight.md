UWP ( universal windows platform ) is platform that based on WIndowss runtime API ( WinRT API), its different from wpf ( windows presentation foundation ) that based on .NET framework and .NETCORE, so i cannot use method from .NET, aslo UWP has more strictly permission when compared with WPF, i cannot use some basic method from basic c# like getFiles(), Directory.Delete() etc. or it will make my app crashed, that makes me need to learn UWP documentation. 

#### so why i choose to make an UWP program instead of WPF?
- first, it's new for me.
- second, uwp has better interface than wpf
![UWP](https://github.com/LLuthfiY/SimpleMangaReaderUWP/blob/master/screenshot/main.png)
![WPF](https://github.com/LLuthfiY/SimpleMangaReaderUWP/blob/master/screenshot/WPF/Untitled.png)
- third, scrollViewer in WPF doesn't have zoom function, so i must to create fuction to resize the image 

## TODO
- [x] create a filter function, so i can change the image colorTone, it's easy in python, but not in UWP. there so many image format like bitmap, bitmapImage, WriteableBitmap, softwareBitmap etc. 
- [ ] create setting interface for colorTone function
#### update
colorTone Function
![ColorTone](https://github.com/LLuthfiY/SimpleMangaReaderUWP/blob/master/screenshot/Todo/Progress/progress_colorToneFunction.png)

 i need to edit my image in stream function 
 ```
WriteableBitmap writeableBitmap = BitmapFactory.New(1, 1);
StorageFile file = await StorageFile.GetFileFromPathAsync(path);
using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
{
    WriteableBitmap wb = await BitmapFactory.New(1, 1).FromStream(fileStream);

    Debug.WriteLine((wb.GetPixel(1, 1).B / 255) * (blue1 - blue2) + blue2);
    wb.ForEach((x, y, Color) => Color.FromArgb(Color.A,
                                            (byte)(((float)Color.R / 255f) * (red1 - red2) + red2),
                                            (byte)(((float)Color.G / 255f) * (green1 - green2) + green2),
                                            (byte)(((float)Color.B / 255f) * (blue1 - blue2) + blue2)
                                            )
    );


    //WriteableBitmap newWB = ColorTintAsync(writeableBitmap, 200, 200, 100, 33, 33, 33);
    ObservableImage.Add(new ImageListTest
    {
        ImagePath = path,
        ImageContent = wb

    });
}

 ```
 
 or it will send me an error "System.AccessViolationException: 'Attempted to read or write protected memory. This is often an indication that other memory is corrupt", 
 
 
