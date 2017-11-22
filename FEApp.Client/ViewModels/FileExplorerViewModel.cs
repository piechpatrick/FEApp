using FEApp.Client.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FEApp.Client.ViewModels
{
    public class FileExplorerViewModel : BindableBase
    {
        static string path = "http://localhost:32584/api/";

        Core.Models.FolderExplorerModel _folderModel =
            new Core.Models.FolderExplorerModel(path + "Folders/");

        Core.Models.FileExplorerModel _fileModel =
            new Core.Models.FileExplorerModel(path + "Files/");


        private ICommand _openDownloadFolderCommand;
        public ICommand OpenDownloadFolderCommand
        {
            get
            {
                if (_openDownloadFolderCommand == null)
                    _openDownloadFolderCommand = new Commands.RelayCommand(
                        p => this.CanOpenDownloadFolder,
                        p => this.OpenDownloadFolder(p));
                return _openDownloadFolderCommand;
            }
        }

        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                    _refreshCommand = new Commands.RelayCommand(
                        p => this.CanRefresh,
                        p => this.Refresh(p));
                return _refreshCommand;
            }
        }

        private ICommand _addDirCommand;
        public ICommand AddDirCommand
        {
            get
            {
                if (_addDirCommand == null)
                    _addDirCommand = new Commands.RelayCommand(
                        p => this.CanAddDir,
                        p => this.AddDir(p));
                return _addDirCommand;
            }
        }

        private ICommand _deleteDirCommand;
        public ICommand DeleteDirCommand
        {
            get
            {
                if (_deleteDirCommand == null)
                    _deleteDirCommand = new Commands.RelayCommand(
                        p => this.CanDeleteDir,
                        p => this.DeleteDir(p));
                return _deleteDirCommand;
            }
        }

        private ICommand _deleteFileCommand;
        public ICommand DeleteFileCommand
        {
            get
            {
                if (_deleteFileCommand == null)
                    _deleteFileCommand = new Commands.RelayCommand(
                        p => this.CanDeleteFile,
                        p => this.DeleteFile(p));
                return _deleteFileCommand;
            }
        }

        private ICommand _downloadFileCommand;
        public ICommand DownloadFileCommand
        {
            get
            {
                if (_downloadFileCommand == null)
                    _downloadFileCommand = new Commands.RelayCommand(
                        p => this.CanDownloadFile,
                        p => this.DownloadFile(p));
                return _downloadFileCommand;
            }
        }

        private ICommand _getFileCommand;
        public ICommand GetFileCommand
        {
            get
            {
                if (_getFileCommand == null)
                    _getFileCommand = new Commands.RelayCommand(
                        p => this.CanGetFile,
                        p => this.GetFile(p));
                return _getFileCommand;
            }
        }

        private string _downloadPath = @"C:\Users\root\Desktop\Download\";
        public string DownloadPath
        {
            get
            {
                _fileModel.DownloadPath = _downloadPath;
                return _downloadPath;
            }
            set
            {
                _fileModel.DownloadPath = value;
                SetProperty(ref _downloadPath, value);
            }
        }

        private BitmapImage _previewImage;
        public BitmapImage PreviewImage
        {
            get { return _previewImage; }
            set
            {
                SetProperty(ref _previewImage, value);
            }
        }

        private Common.Folder _folder;
        public Common.Folder Folder
        {
            get { return _folder; }
            set
            {
                SetProperty(ref _folder, value);
            }
        }

        private Common.Folder _selectedFolder;
        public Common.Folder SelectedFolder
        {
            get { return _selectedFolder; }
            set
            {
                SetProperty(ref _selectedFolder, value);
            }
        }

        public FileExplorerViewModel()
        {
            Folder = _folderModel.GetFilesAndDirs();
        }

        #region Commands
        public void Refresh(object param)
        {
            Folder = _folderModel.GetFilesAndDirs();
        }
        public bool CanRefresh
        {
            get { return _folderModel != null; }
        }

        public async void AddDir(object param)
        {
            var res = await _folderModel.AddDir(param.ToString());
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
                Refresh(null);
        }
        public bool CanAddDir
        {
            get { return _folderModel != null; }
        }

        public async void DeleteDir(object param)
        {
            var folder = (Common.Folder)param;
            if(folder != null)
            {
                var res = await _folderModel.DeleteDir(folder);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    Refresh(null);
            }
        }
        public bool CanDeleteDir
        {
            get { return _folderModel != null; }
        }

        public async void DeleteFile(object param)
        {
            var file = (Common.FileInfo)param;
            if (file != null)
            {
                var res = await _fileModel.DeleteFile(file);
                if(res != null)
                    Refresh(null);
            }
        }
        public bool CanDeleteFile
        {
            get { return _folderModel != null; }
        }

        public async void DownloadFile(object param)
        {
            var file = (Common.FileInfo)param;
            if (file != null)
            {
                var res = await _fileModel.GetFile(file);
                if (res != null && res is Common.DownloadedFileInfo)
                {
                    File.WriteAllBytes(res.Path, res.Buffor);
                    Refresh(null);
                }
            }
        }
        public bool CanDownloadFile
        {
            get { return _fileModel != null; }
        }

        private ObservableCollection<Models.FileContent> _files;
        public ObservableCollection<Models.FileContent> Files
        {
            get
            {
                if (_files == null)
                    _files = new ObservableCollection<Models.FileContent>();
                return _files;
            }
            set
            {
                SetProperty(ref _files, value);
            }
        }

        public async void GetFile(object param)
        {
            Files.Clear();
            if (param != null)
            {           
                var files = (param as IEnumerable<object>).Cast<Common.FileInfo>().ToList();
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        var res = await _fileModel.GetFile(file);
                        if (res != null && res is Common.DownloadedFileInfo)
                        {
                            if (res.Name.Contains(".png") || res.Name.Contains(".jpg"))
                            {
                                var imgFile = new Models.ImageFileContent(res);
                                Files.Add(imgFile);
                            }
                            else if (res.Name.Contains(".txt"))
                            {
                                var txtFile = new Models.TextFileContent(res);
                                Files.Add(txtFile);
                            }
                            else
                            {
                                Files.Add(new Models.UnsupportedFIleContent(res));
                            }
                        }
                    }
                }
            }
        }
        public bool CanGetFile
        {
            get { return _fileModel != null; }
        }

        public void OpenDownloadFolder(object param)
        {
            Process.Start(_downloadPath);
        }
        public bool CanOpenDownloadFolder
        {
            get { return true; }
        }
            


        #endregion
    }
}
