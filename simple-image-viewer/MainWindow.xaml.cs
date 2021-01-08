using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace simple_image_viewer
{
    public partial class MainWindow : Window
    {
        folderHandler _folderHandler = new folderHandler();
        public MainWindow()
        {
            InitializeComponent();
            String[] args = App.mArgs;
            FileInfo[] imageFiles = _folderHandler.GetImageFilesList();
            string firstImageFile = _folderHandler.GetFileName(0);
            if (args.Length == 0)
            {
                _folderHandler.UpdateImageSource(showingImage, firstImageFile);
            } else if (args.Length > 1) {
                List<FileInfo> BatchImageList = new List<FileInfo>();
                foreach (var arg in args)
                {
                    BatchImageList.Add(new FileInfo(arg));
                }
                _folderHandler.ImageFiles = BatchImageList.ToArray();
                _folderHandler.UpdateImageSource(showingImage, args[0]);
            } else
            {
                _folderHandler.UpdateImageSource(showingImage, args[0]);
                _folderHandler.nextFile = Array.FindIndex(imageFiles, imageFile => imageFile.FullName == args[0]);
            }
            fileNumber.Content = $"{_folderHandler.nextFile + 1} of {_folderHandler.ImageFiles.Length}";
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_folderHandler.ImageFilesExist())
            {
                if (e.Key == Key.Right)
                {
                    var NextFileName = _folderHandler.NextFile();
                    _folderHandler.UpdateImageSource(showingImage, NextFileName);
                }
                else if (e.Key == Key.Left)
                {
                    var PrevFileName = _folderHandler.PrevFile();
                    _folderHandler.UpdateImageSource(showingImage, PrevFileName);
                }
                else if (e.Key == Key.Escape)
                {
                    mainWin.Close();
                }
                fileNumber.Content = $"{_folderHandler.nextFile + 1} of {_folderHandler.ImageFiles.Length}";
            }
        }

        private void mainWin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ChangedButton == MouseButton.Left)
            //{
            //    mainWin.Close();
            //}
        }
    }
}
