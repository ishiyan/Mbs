# Convert TeX formulat to XAML

## TeX to SVG

Use [LaTeX to SVG converter](https://viereck.ch/latex-to-svg/).

Select `Use the default scale factor (1ex = 8px)` and `Display mode` in settings.

Download the generated SVG.

## SVG to XAML

Install the latest `Inkscape`.

Open the SVG file, the displyed size may vary.

- Select `Select All in All Layers` from the `Edit` menu, `Ctrl + Alt + A`, you will see the selection around your equation
- Select `Resize Page to Selection` from the `Edit` menu, `Shift + Ctrl + R`
- Select `Object to Path` from the `Path` menu, `Shift + Ctrl + C`
- Select `Save As ...` from the `File` menu, `Shift + Ctrl + S`, select `XAML` as an output format
- Check `Silverlight compatibility` when prompted

## Cleanup converted XAML

In the converted XAML file:

- Delete `Canvas.Resources` block
- Delete all comments
- Delete all occurences of ` Name="g.*"` (regex)
- Replace all occurences of ` xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" Name=".*" F` with ` F` (regex)
- Replace all occurences of ` xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" Name="svg.*" W` with ` W` (regex)
- Replace all occurrences of `Fill="currentColor" StrokeThickness="0" Stroke="currentColor"` with `Style="{StaticResource EqPath}"`
- Delete all occurrences of ` xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"` .
- Delete the ` xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"` attribute from the top `Canvas`.

For "two-line" equations wrap the top `Canvas` inside the following `Viewbox` snippet:

```xaml
        <Viewbox x:Key="MyKey" Style="{StaticResource EqViewbox}" x:Shared="False">
        <!-- Canvas here -->
        </Viewbox>
```

For "single-line" equation use a snippet with a custom height:

```xaml
        <Viewbox x:Key="MyKey" Style="{StaticResource EqViewbox}" x:Shared="False" Height="10">
        <!-- Canvas here -->
        </Viewbox>
```

Here the `MyKey` should be a unique key, you will use it in the code.
