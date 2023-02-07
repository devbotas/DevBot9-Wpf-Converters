# DevBot9.Wpf.Converters

Originally I extracted converters from https://github.com/DevExpress/DevExpress.Mvvm.Free library, because their free MVVM libraries conflict with paid ones, and I needed to use both.

Thank you, DevExpress, for the great MVVM library! If only I didn't have to reinvent the wheel... :)

And while I was at it, I improved those converters with static ```Default``` instances, so I do not have to create resources, attach keys, etc. all the time again and again. Now I can simple consume them like this:

```
{Binding SomeProperty, Converter={x:Static conv:BooleanToVisibilityConverter.Default}
```

Moreover, converters related to booleans or ```Visibility``` also have ```Inverting``` static instances:

```
{Binding SomeProperty, Converter={x:Static conv:BooleanToVisibilityConverter.Inverting}
```

Which is also a very handy shortcut.