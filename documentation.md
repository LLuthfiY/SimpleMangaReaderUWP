#### motivation and background
i just want to read my downloaded manga in slide down Mode

#### project duration
3 week

#### the structure of my code

- APP is contrainer for the whole thing in this UWP app
- MainPage is a page that have function as contrainer for another page
- folderViewer page => to show folder(manga/chapter) list
- imageViewer page => to show image


when the program start
APP execute the mainpage xml and cs, and mainPage will execute the FolderViewer
if you click a folder in folder list it will show you the subfolder
if you click an image infolder list, it will execute imageViewer

#### class and function interface
MainPage.xaml.cs
| function name | access level | parameter | return |
| :------------:| :----------: | :-------: | :----: |
| mainPage | Public | none | return nothing / void | 
| editbleTitleBar | private | none | void |
| folderPicker_click | private | sender: Object , e: RoutedEventArgs | void |
| grid_keyUp | private | none | void |

FolderViewerl.xaml.cs
| function name | access level | parameter | return |
| :------------:| :----------: | :-------: | :----: |
| FolderViewer | public | none | void |
| onNavigatedTo | private | e:NavigationEventArgs | void |
| gotoImageViewer_click | private | sender:object, e: routedEventArgs | void |
| getFolderandFileList | private | path:string | void |
| refreshView | private | Paths:List<string>, PathFiles:List<String> | void |
| back_click | private | sender:object, e: RoutedEventArgs | void |
| listView_ItemClick | private | sender:object, e:RoutedEventArgs | void |
| Textbox_TextChanged | private | sender:object, e:TextChangedEventArgs | void |
| searchBox_GotFocus | private | sender:object, e:RoutedEventArgs | void |
| Grid_KeyUp | private | sender:object, e:keyRoutedEventArgs | void |
| backFolder | private | path:strng | void | 
| listview_RightTapped | private | sender:object, e: KeyTappedRoutedEventArgs | void |

ImageViewer.xaml.cs
| function name | access level | parameter | return |
| :------------:| :----------: | :-------: | :----: |
| ImageViewer | public | none | void |
| onNavigatedTo | private | e:NavigationEventArgs | void |
| buttonback_click | private | sender:object, e:RoutedEventArgs | void |
| getFolderandFileList | private | path:string | void |
| refreshViewAsync | private | PathFiles:List<String> | void |
| Grid_KeyUp | private | e:KeyRoutedEventArgs | void |
| scrollView_rightTapped | private | e:RightTappedRoutedEventArgs | void |
| getNextChapterAsync | private | none | void |
| getPreviousChapterAsync | private | none | void |
| scrollView_DoubleTapped | private | sender:object, e:DoubleTappedRoutedArgs | void |
| scrollView_Holding | private | sender:object, e:HoldingRoutedArgs | void |

ListItem.cs
| function name | access level | parameter | return |
| :------------:| :----------: | :-------: | :----: |

ImageList.cs
| function name | access level | parameter | return |
| :------------:| :----------: | :-------: | :----: |

#### statistic 
number of lines that i write 

MainPage.xaml.cs = 89 <br>
MainPage.xaml = 82 <br>
FolderViewer.xaml.cs = 207 <br>
FolderViewer.xaml = 60 <br>
ImageViewer.xaml.cs = 260 <br>
ImageViewer.xaml = 135 <br>
ImageList.cs = 7 <br>
ListItem.cs = 8 











 



