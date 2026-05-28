# AutoCAD Auto Layouts Plugin

A C# (.NET) plugin for AutoCAD that automates the creation of sheet layouts with support for single and multiple viewports (reports).

## Features

- **Single Viewport Layout**: Create layouts with one report
- **Dual Viewport Layout**: Create layouts with two reports (e.g., Plan & Profile)
- **Batch Processing**: Generate multiple sheet layouts automatically
- **Viewport Management**: Automatic width and height configuration
- **Sheet Number Handling**: Intelligent selection and management of sheet numbers
- **Mixed Layouts**: Combine different viewport configurations in one drawing

## Requirements

- AutoCAD 2019 or later
- .NET Framework 4.7.2 or higher
- Visual Studio 2019 or later (for development)

## Project Structure

```
AutoLayoutsPlugin/
├── AutoLayoutsPlugin.csproj
├── App.cs
├── Core/
│   ├── ViewportManager.cs
│   ├── SheetNumberManager.cs
│   ├── LayoutCreator.cs
│   └── LayoutProcessor.cs
├── Models/
│   ├── LayoutConfiguration.cs
│   ├── ViewportInfo.cs
│   └── SheetInfo.cs
├── UI/
│   ├── LayoutDialog.xaml
│   ├── LayoutDialog.xaml.cs
│   └── Commands.cs
├── Utilities/
│   ├── GeometryHelper.cs
│   ├── LayerManager.cs
│   └── Logger.cs
└── Resources/
    └── LayoutResources.resx
```

## Installation

1. Clone the repository
2. Open `AutoLayoutsPlugin.sln` in Visual Studio
3. Build the solution
4. Copy the compiled DLL to your AutoCAD add-ins folder
5. Load the plugin in AutoCAD

## Usage

### Single Viewport Layout

1. Prepare your sheet frame with title block
2. Create sheet numbers in a separate layer (Middle Center justified)
3. Open AutoCAD and load the plugin
4. Run `AUTOLAYOUTS` command
5. Select "One Viewport" option
6. Select sheet numbers
7. Select viewport width and height
8. Execute

### Dual Viewport Layout

1. Prepare two separate viewports (e.g., Plan and Profile)
2. Create sheet numbers for each in separate layers (VP1, VP2, etc.)
3. Run `AUTOLAYOUTS` command
4. Select "Two Viewports" option
5. Select sheet numbers for both reports
6. Configure width and height for each
7. Execute

## Development

### Building

```bash
cd AutoLayoutsPlugin
msbuild AutoLayoutsPlugin.sln
```

### Testing

Unit tests are located in the `Tests/` directory.

```bash
dotnet test
```

## Contributing

Contributions are welcome! Please follow the coding standards and submit pull requests.

## License

MIT License - see LICENSE file for details

## Support

For issues, questions, or feature requests, please open an issue on GitHub.
