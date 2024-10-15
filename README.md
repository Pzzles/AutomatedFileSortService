Automated File Sort Service
This project is a background service that automatically monitors a folder (e.g., your Downloads folder) and moves files into categorized folders on your desktop based on their file type. The service uses a FileSystemWatcher to detect file system changes (such as new files created or existing files changed) and then sorts the files into specific folders for easy organization.

Features
* Automatically monitors the Downloads folder for newly created or modified files.
* Moves files to categorized folders based on file type:
  * TXTs for .txt files
  * PDFs for .pdf files
  * Images for .jpg and .png files
  * Docs for .docx files
  * Others for all other file types not explicitly categorized
* Works as a Windows service that runs in the background.
Prerequisites
* .NET 6 SDK or later
* Windows operating system (the current implementation uses Windows paths)
Installation
1. Clone the repository:
git clone https://github.com/Pzzles/automated-file-sort-service.git
cd automated-file-sort-service
2. Build the project:
dotnet build
3. Run the project:
dotnet run


How It Works
The service monitors the Downloads folder using FileSystemWatcher. When a file is created or changed, the service:

1. Determines the file's extension.
2. Moves the file into a corresponding folder on the desktop based on its type:
.txt files are moved to the TXTs folder.
.pdf files are moved to the PDFs folder.
.jpg and .png files are moved to the Images folder.
.docx files are moved to the Docs folder.
Any other file types are moved to the Others folder.
If the destination folder does not exist, it will be created.

Configuration
* The source folder being monitored is currently hardcoded as the Downloads folder (C:\Users\user\Downloads\).
* The destination folders are created on the Desktop under appropriate categories (TXTs, PDFs, Images, etc.).
* You can modify these paths in the CheckFolder method or pass them as configuration options.

Customization
To extend the functionality to handle additional file types:

1. Open Worker.cs.
2. Add new cases in the SortFile method's switch expression
string destinationDir = extension switch
{
    ".mp4" => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Videos"),
    ".avi" => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Videos"),
    // Add more file types here
    _ => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Others")
};
Error Handling
If an error occurs (such as a file being in use or a permission issue), the error will be caught, and a message will be logged to the console. You can extend this to log errors to a more persistent store (e.g., a file or monitoring system).

Future Enhancements
* Add more file type categories (e.g., videos, music, archives).
* Monitor multiple directories or allow user configuration of monitored folders.
* Implement a UI to allow users to configure file sorting preferences.
  
Contributing
If you'd like to contribute, please fork the repository and use a feature branch. Pull requests are welcome.



