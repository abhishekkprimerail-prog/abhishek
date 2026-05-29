# Development Guide

## Prerequisites
- Visual Studio 2019 or later
- AutoCAD 2019 or later
- .NET Framework 4.7.2

## Setup

1. **Configure AutoCAD References**
   - Open `AutoLayoutsPlugin.csproj`
   - Update the paths to match your AutoCAD installation:
     ```xml
     C:\Program Files\Autodesk\AutoCAD 2024\AcCoreMgd.dll
     C:\Program Files\Autodesk\AutoCAD 2024\AcDbMgd.dll
     C:\Program Files\Autodesk\AutoCAD 2024\AcMgd.dll
     C:\Program Files\Autodesk\AutoCAD 2024\AcWindows.dll
     ```

2. **Build the Project**
   ```bash
   cd AutoLayoutsPlugin
   msbuild AutoLayoutsPlugin.sln /p:Configuration=Release
   ```

3. **Deploy the Plugin**
   - Copy the compiled DLL to your AutoCAD add-ins folder:
     ```
     %APPDATA%\Autodesk\AutoCAD 2024\R24.1\enuS\Support\AutoLayoutsPlugin.dll
     ```
   
   - Or create a `.bundle` file for easier distribution

## Building a Bundle

Create `AutoLayoutsPlugin.bundle\Contents\AutoLayoutsPlugin.xml`:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ApplicationPackage SchemaVersion="1.0">
  <Components>
    <RuntimeRequirements OS="Win32" Platform="AutoCAD" SeriesMin="R24" SeriesMax="R99"/>
    <ComponentEntry AppName="AutoLayouts" ModuleName="AutoLayoutsPlugin.dll" AppDescription="Automated layout creation for AutoCAD" LoadOnAutoCADStartup="True" />
  </Components>
</ApplicationPackage>
```

## Testing

1. Start AutoCAD
2. Load the plugin (Manage Add-ins dialog)
3. Type `AUTOLAYOUTS` in the command line
4. Follow the dialog prompts

## Architecture

### Core Classes
- **LayoutProcessor**: Main orchestrator for layout creation
- **LayoutCreator**: Creates layouts and viewports
- **ViewportManager**: Handles viewport selection
- **SheetNumberManager**: Manages sheet number extraction

### Models
- **LayoutConfiguration**: Configuration for layout creation
- **ViewportInfo**: Viewport dimensions and properties
- **SheetInfo**: Sheet information

### Utilities
- **Logger**: Logging to file
- **GeometryHelper**: Geometry calculations

## Common Issues

### Issue: AutoCAD DLLs not found
**Solution**: Update the paths in the .csproj file to match your AutoCAD installation directory.

### Issue: Plugin not loading
**Solution**: Check the log file in `%APPDATA%\AutoLayouts\AutoLayouts_YYYYMMDD.log`

### Issue: Sheet numbers not detected
**Solution**: Ensure all sheet numbers are MText objects on the same layer with no other text.

## Next Steps

- Add support for more layout templates
- Implement viewport positioning algorithms
- Add support for external layout definition files
- Create installer package
