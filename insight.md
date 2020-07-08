UWP ( universal windows platform ) is platform that based on WIndowss runtime API ( WinRT API), its different from wpf ( windows presentation foundation ) that based on .NET framework and .NETCORE, so i cannot use method from .NET, aslo UWP has more strictly permission when compared with WPF, i cannot use some basic method from basic c# like getFiles(), Directory.Delete() etc. or it will make my app crashed, that makes me need to learn UWP documentation. 

#### so why i choose to make an UWP program instead of WPF?
- first, it's new for me.
- second, uwp has better interface than wpf
![UWP](https://github.com/LLuthfiY/SimpleMangaReaderUWP/blob/master/screenshot/main.png)
![WPF](https://github.com/LLuthfiY/SimpleMangaReaderUWP/blob/master/screenshot/WPF/Untitled.png)
- third, scrollViewer in WPF doesn't have zoom function, so i must to create fuction to resize the image 

## TODO
-[x] create a filter function, so i can change the image colorTone, it's easy in python, but not in UWP. there so many image format like bitmap, bitmapImage, WriteableBitmap, softwareBitmap etc. 
-[] create setting interface for colorTone function
#### update
colorTone Function
![ColorTone](https://github.com/LLuthfiY/SimpleMangaReaderUWP/blob/master/screenshot/Todo/Progress/progress_colorToneFunction.png)

 
 
