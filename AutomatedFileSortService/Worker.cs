using System.Formats.Tar;
using System.Security.Permissions;

namespace AutomatedFileSortService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CheckFolder();
        }

        protected void CheckFolder()
        {
            string downloadsPath = @"C:\\Users\\user\\Downloads\\";
            var systemWatcher = new FileSystemWatcher()
            {
                Path = downloadsPath,
                Filter = "*.*",
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.Attributes,
            };

            systemWatcher.Created += new FileSystemEventHandler(OnChanged);
            systemWatcher.Changed += new FileSystemEventHandler(OnChanged);

            systemWatcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            string downloadsPath = @"C:\\Users\\user\\Downloads\\";

            WatcherChangeTypes wct = e.ChangeType;
            SortFile(wct, downloadsPath, e.FullPath);
        }

        private bool SortFile(WatcherChangeTypes wct, string downloadsFolderPath, string filePath)
        {
            HashSet<string> options = new HashSet<string>
            {
                "Deleted",
                "Changed",
                "Created"
            };

            if (!options.Contains(wct.ToString()))
            {
                return false;
            }

            try
            {
                if (wct.ToString() == "Created")
                {
                    string extension = Path.GetExtension(filePath).ToLower();

                    string destinationDir = extension switch
                    {
                        ".txt" => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TXTs"),
                        ".pdf" => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PDFs"),
                        ".jpg" => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Images"),
                        ".png" => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Images"),
                        ".docx" => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Docs"),
                        _ => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Others")
                    };

                    if (!Directory.Exists(destinationDir))
                    {
                        Directory.CreateDirectory(destinationDir);
                    }

                    string destinationPath = Path.Combine(destinationDir, Path.GetFileName(filePath));
                    File.Move(filePath, destinationPath);
                }
                else if (wct.ToString() == "Changed")
                {
                    // Handle changed files if needed
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                options.Clear();
            }

            return true;
        }
    }
}
git config --global user.email "tshehlap@gmail.com"
  git config --global user.name "Pzzles"