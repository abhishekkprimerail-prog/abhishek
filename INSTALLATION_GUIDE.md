# AutoCAD Auto Layouts Plugin - Step by Step Installation Guide

## Table of Contents
1. [Prerequisites & Downloads](#prerequisites--downloads)
2. [System Requirements](#system-requirements)
3. [Step-by-Step Installation](#step-by-step-installation)
4. [Building from Source](#building-from-source)
5. [Deploying the Plugin](#deploying-the-plugin)
6. [Testing & Verification](#testing--verification)
7. [Troubleshooting](#troubleshooting)

---

## Prerequisites & Downloads

### Required Downloads

#### 1. **Git (Version Control)**
- **Download**: https://git-scm.com/download/win
- **Version**: Latest (currently 2.40+)
- **Purpose**: Clone the repository

#### 2. **Visual Studio Community 2022** (Free)
- **Download**: https://visualstudio.microsoft.com/downloads/
- **Choose**: "Community" edition (Free)
- **Purpose**: Compile the C# code
- **Size**: ~5-10 GB
- **Installation Time**: 30-45 minutes

#### 3. **.NET Framework 4.7.2**
- **Usually Included**: With Windows 10/11
- **If Missing**: https://dotnet.microsoft.com/download/dotnet-framework/net472
- **Purpose**: Required runtime for the plugin

#### 4. **AutoCAD 2019 or Later**
- **Download**: From Autodesk website (requires license/subscription)
- **Minimum Version**: AutoCAD 2019
- **Recommended**: AutoCAD 2023 or 2024
- **Purpose**: Host application for the plugin

---

## System Requirements

| Component | Requirement |
|-----------|-------------|
| **OS** | Windows 10 or Windows 11 (64-bit) |
| **RAM** | Minimum 8 GB (16 GB recommended) |
| **Disk Space** | 15-20 GB free (for VS, AutoCAD, project) |
| **Processor** | Intel i5 or equivalent (or better) |
| **.NET Framework** | 4.7.2 or higher |
| **Visual Studio** | 2019, 2022 (Community Edition fine) |
| **AutoCAD** | 2019-2024 or later |

---

## Step-by-Step Installation

### STEP 1: Install Git

**Windows Users:**

1. Visit https://git-scm.com/download/win
2. Download the installer (usually `Git-2.xx.x-64-bit.exe`)
3. Run the installer
4. Click "Next" through all prompts (default settings are fine)
5. Finish the installation

**Verify Installation:**
```bash
# Open Command Prompt or PowerShell and type:
git --version
# Output should show: git version 2.xx.x.windows.1
```

---

### STEP 2: Install Visual Studio 2022 Community

**Download & Install:**

1. Visit https://visualstudio.microsoft.com/downloads/
2. Click "Free Download" under Visual Studio Community 2022
3. Save the `VisualStudioSetup.exe` file (~2 MB)
4. Run the installer - it will download additional components (~5-10 GB)
5. When prompted, select these workloads:
   - ✅ **Desktop development with C++**
   - ✅ **.NET desktop development**
   - ✅ **Windows development** (optional)

6. Click "Install" and wait (30-45 minutes)

**Verify Installation:**
- Launch Visual Studio Community 2022 from Start Menu
- It should open successfully

---

### STEP 3: Download/Clone the Project

**Using Command Prompt/PowerShell:**

1. Open Command Prompt (Win + R, type `cmd`)
2. Navigate to where you want to store the project:
   ```bash
   # Example: Go to Documents
   cd Documents
   ```

3. Clone the repository:
   ```bash
   git clone https://github.com/abhishekkprimerail-prog/abhishek.git
   ```

4. Wait for download to complete (about 1 minute)

5. Navigate into the project:
   ```bash
   cd abhishek
   ```

**Folder Structure Created:**
```
C:\Users\YourName\Documents\abhishek\
├── AutoLayoutsPlugin/
│   ├── App.cs
│   ├── Models/
│   ├── Core/
│   ├── UI/
│   ├── Utilities/
│   └── AutoLayoutsPlugin.csproj
├── README.md
├── DEVELOPMENT.md
└── BUILD_COMPLETE.md
```

---

### STEP 4: Update AutoCAD DLL References

**Important: This step customizes the project for YOUR AutoCAD installation**

1. Open the project folder: `AutoLayoutsPlugin`
2. Find and open: `AutoLayoutsPlugin.csproj`
   - Right-click → "Open with" → Choose "Notepad" or Visual Studio Code

3. Find this section (around line 16-25):
   ```xml
   <ItemGroup>
       <Reference Include="AcCoreMgd">
           <HintPath>C:\Program Files\Autodesk\AutoCAD 2024\AcCoreMgd.dll</HintPath>
           <Private>false</Private>
       </Reference>
   ```

4. **Replace the paths** to match YOUR AutoCAD installation:
   - **Find**: `C:\Program Files\Autodesk\AutoCAD 2024\`
   - **Replace with**: Your actual AutoCAD install path
   
   **Common Paths:**
   - AutoCAD 2024: `C:\Program Files\Autodesk\AutoCAD 2024\`
   - AutoCAD 2023: `C:\Program Files\Autodesk\AutoCAD 2023\`
   - AutoCAD 2022: `C:\Program Files\Autodesk\AutoCAD 2022\`
   - AutoCAD 2021: `C:\Program Files\Autodesk\AutoCAD 2021\`
   - AutoCAD 2019: `C:\Program Files\Autodesk\AutoCAD 2019\`

5. **How to find YOUR AutoCAD path:**
   - Open AutoCAD
   - Type in command line: `APPDATA`
   - A folder opens → look for `Autodesk\AutoCAD 2024` (or your version)
   - The install folder is usually in `C:\Program Files\Autodesk\AutoCAD 202X\`

6. Save the file (Ctrl + S)

---

## Building from Source

### STEP 5: Open Project in Visual Studio

1. Open Visual Studio Community 2022
2. Click "File" → "Open" → "Project/Solution"
3. Navigate to: `Documents\abhishek\AutoLayoutsPlugin.sln`
4. Click "Open"

**Visual Studio will:**
- Load the project
- Show warnings (if any) - these are okay for now
- Display the Solution Explorer on the right

### STEP 6: Configure Build Settings

1. In Visual Studio, look for the dropdown at the top (currently shows "Debug")
2. Click it and select: **"Release"**
3. Also ensure platform is set to: **"Any CPU"** or **"x64"**

### STEP 7: Build the Project

1. Click "Build" menu → "Build Solution"
2. Wait for build to complete (usually 1-2 minutes)
3. Check the "Output" window at the bottom:
   - ✅ **Success**: "Build succeeded"
   - ❌ **Failed**: Shows error messages (see Troubleshooting)

**After successful build, you'll have:**
```
AutoLayoutsPlugin\bin\Release\AutoLayoutsPlugin.dll
```

This is your plugin file!

---

## Deploying the Plugin

### STEP 8: Find AutoCAD Add-Ins Folder

**Windows Path to AutoCAD Plugins:**

1. Open File Explorer
2. Copy this path into the address bar:
   ```
   %APPDATA%\Autodesk\AutoCAD 2024\R24.1\enuS\Support
   ```
   
   **Replace "2024" with YOUR AutoCAD version:**
   - AutoCAD 2024: `AutoCAD 2024\R24.1\enuS\Support`
   - AutoCAD 2023: `AutoCAD 2023\R23.1\enuS\Support`
   - AutoCAD 2022: `AutoCAD 2022\R22.1\enuS\Support`
   - AutoCAD 2021: `AutoCAD 2021\R21.1\enuS\Support`
   - AutoCAD 2019: `AutoCAD 2019\R23.1\enuS\Support`

3. If the folder doesn't exist, create it manually:
   - Navigate to `C:\Users\YourName\AppData\Roaming\Autodesk\AutoCAD 2024`
   - Create folder: `R24.1` → `enuS` → `Support`

### STEP 9: Copy DLL to AutoCAD Folder

1. Go to your project folder:
   ```
   Documents\abhishek\AutoLayoutsPlugin\bin\Release\
   ```

2. Find: `AutoLayoutsPlugin.dll`

3. Copy this file (Ctrl + C)

4. Navigate to your AutoCAD Support folder (from STEP 8)

5. Paste the DLL file (Ctrl + V)

**Result:**
```
C:\Users\YourName\AppData\Roaming\Autodesk\AutoCAD 2024\R24.1\enuS\Support\AutoLayoutsPlugin.dll
```

---

## Testing & Verification

### STEP 10: Load Plugin in AutoCAD

1. **Close AutoCAD** if it's running
2. **Restart AutoCAD** (this loads all plugins on startup)
3. Once AutoCAD opens completely, type in the command line:
   ```
   AUTOLAYOUTS
   ```
4. Press ENTER

**Expected Result:**
- A dialog window appears titled: **"AutoCAD Auto Layouts"**
- The dialog shows options for layout type selection
- ✅ **Success**: Plugin loaded correctly!

### STEP 11: Test Plugin Features

**Test Single Viewport Layout:**

1. In the dialog, select "One Viewport"
2. Click "Select Sheet Numbers"
3. Click "Cancel" (just testing)
4. Click "Cancel" on main dialog

**Test Dual Viewport Layout:**

1. Run `AUTOLAYOUTS` again
2. Select "Two Viewports"
3. Verify the dialog changes to show dual options
4. Click "Cancel"

✅ **If these steps work, your plugin is installed correctly!**

---

## Troubleshooting

### Problem 1: "AutoLayoutsPlugin.dll not found after build"

**Solution:**
- Make sure you built in **Release** mode, not Debug
- Build → Clean Solution → Build Solution again
- Check: `AutoLayoutsPlugin\bin\Release\` folder

### Problem 2: "AutoCAD paths in .csproj are incorrect"

**Solution:**
1. Open AutoCAD
2. Type: `ACADLOCATIONPATH`
3. This shows your AutoCAD folder
4. Update the .csproj file with correct paths
5. Rebuild in Visual Studio

### Problem 3: "Build failed with C# errors"

**Solutions:**
1. Check that you installed .NET Framework 4.7.2
2. Make sure AutoCAD DLL references exist at the paths
3. Close Visual Studio → Delete `bin` and `obj` folders → Reopen
4. Check "Error List" window for specific errors

### Problem 4: "AUTOLAYOUTS command not recognized"

**Solutions:**
1. Check that DLL was copied to correct AutoCAD folder
2. **Restart AutoCAD completely** (close and reopen)
3. Type: `NETLOAD`
4. Browse to the DLL file manually
5. If it loads, plugin is working

### Problem 5: "The type initializer threw an exception"

**Solution:**
1. Verify AutoCAD 2024 is installed (or your version)
2. Check that .NET Framework 4.7.2 is installed
3. Update the AutoCAD DLL paths in .csproj

### Problem 6: Visual Studio can't find AutoCAD DLLs

**Solution:**
1. Verify AutoCAD is installed at the path you specified
2. Open File Explorer and navigate to:
   ```
   C:\Program Files\Autodesk\AutoCAD 2024\
   ```
3. Look for files: `AcCoreMgd.dll`, `AcDbMgd.dll`, `AcMgd.dll`, `AcWindows.dll`
4. If they exist, update the paths in .csproj to exact location
5. Rebuild

### Problem 7: "Permission denied" when copying DLL

**Solution:**
1. Make sure AutoCAD is **completely closed**
2. Open Command Prompt as **Administrator**
3. Manually copy the DLL:
   ```bash
   copy "C:\Users\YourName\Documents\abhishek\AutoLayoutsPlugin\bin\Release\AutoLayoutsPlugin.dll" "C:\Users\YourName\AppData\Roaming\Autodesk\AutoCAD 2024\R24.1\enuS\Support\"
   ```

---

## Quick Reference Checklist

- [ ] Git installed and working
- [ ] Visual Studio 2022 installed
- [ ] .NET Framework 4.7.2 installed
- [ ] AutoCAD 2019+ installed
- [ ] Project cloned from GitHub
- [ ] AutoCAD DLL paths updated in .csproj
- [ ] Project built successfully (Release mode)
- [ ] DLL copied to AutoCAD Support folder
- [ ] AutoCAD restarted
- [ ] AUTOLAYOUTS command works

---

## Getting Help

If you get stuck:

1. Check the error log:
   ```
   %APPDATA%\AutoLayouts\AutoLayouts_YYYYMMDD.log
   ```

2. Review build errors in Visual Studio Output window

3. Visit GitHub repository:
   ```
   https://github.com/abhishekkprimerail-prog/abhishek
   ```

4. Create an issue with:
   - Your AutoCAD version
   - Error message
   - Steps to reproduce

---

## Summary

**Total Time Required:**
- Downloads: 30-45 minutes
- Installation: 15-20 minutes
- Building: 5 minutes
- Deployment: 5 minutes
- Testing: 5 minutes
- **Total: ~1-1.5 hours**

**Next Steps After Installation:**
1. Start using the plugin with your AutoCAD drawings
2. Read DEVELOPMENT.md for customization options
3. Add to startup suite in AutoCAD for automatic loading

Congratulations! You now have a working AutoCAD Auto Layouts Plugin! 🎉
