using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace simple_image_viewer
{
    class folderHandler : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int _nextFile = 0;
        public FileInfo[] Files { get; set; }
        public FileInfo[] ImageFiles { get; set; }


        public int nextFile
        {
            get { return _nextFile; }
            set
            {
                _nextFile = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string CurrentDir;

        public FileInfo[] GetImageFilesList()
        {
            CurrentDir = Directory.GetCurrentDirectory();
            DirectoryInfo _DirectoryInfo = new DirectoryInfo(CurrentDir);
            Files = _DirectoryInfo.GetFiles();
            List<FileInfo> image_files = new List<FileInfo>();
            foreach (FileInfo f in Files)
            {
                if (Regex.IsMatch(f.FullName, @".jpg|.png|.gif$"))
                    image_files.Add(f);
            }
            ImageFiles = image_files.ToArray();
            return ImageFiles;
        }
        public void UpdateImageSource(Image image, string fileName)
        {
            var newPath = new Uri(fileName);
            image.Source = new BitmapImage(newPath);
        }
        public string GetFileName(int n)
        {
            return ImageFiles[n].FullName;
        }

        public bool ImageFilesExist()
        {
            return ImageFiles.Length > 0;
        }
        public string NextFile()
        {
            nextFile++;
            if (nextFile == ImageFiles.Length)
            {
                nextFile = 0;
            }
            return GetFileName(nextFile);
        }
        public string PrevFile()
        {
            nextFile--;
            if (nextFile <= -1)
            {
                nextFile = ImageFiles.Length - 1;
            }
            return GetFileName(nextFile);
        }
    }
}
