using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels
{
    public class FileMonitor : INotifyPropertyChanged
    {

        private string _messageStatus;
        private FileSystemWatcher _fileWatcher;

        public FileMonitor()
        {
            StartMonitoring();
        }

        public string MessageStatus
        {
            get => _messageStatus;
            set
            {
                _messageStatus = value;
                OnPropertyChanged();
            }
        }

        private void StartMonitoring()
        {
            //Path to folder to monitor
            string folderPath = @"C:\received";
            _fileWatcher = new FileSystemWatcher
            {
                Path = folderPath,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.*"
            };

            _fileWatcher.Created += OnFileCreated;
            _fileWatcher.EnableRaisingEvents = true;

            MessageStatus = $"Monitoring folder: {folderPath}";
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            MessageStatus = $"A new File or Folder created";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
