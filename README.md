# SimpleMangaReaderUWP
read your downloaded manga<br/>
manga reader for UWP, only support png and jpg format

error?
try to enable systemFile permission for rthis ap
```
setting --> privacy --> file system then give this app permission to read your file system
```

<br/>
hotkeys

```
backspace --> parent folder/ quit from image viewer
N/ double click --> next chapter
B --> previous chapter
```

red1, green1, blue1 means light color in the manga's page will be converted to rgb1 color <br>
red2, green2, blue2 means dark color in the manga's page will be converted to rgb2 color

---

### originality
the only part i copied from external source excluding UWP documentation is
```
Path.GetFullPath(Path.Combine(currentpath, @"..\\"))
```

this code will give you the parent dir from current dir

### © 2009 M luthfi 
LLuthfiY@outlook.com
