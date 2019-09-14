using System;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;
using GalaSoft.MvvmLight;
using MyCity_Admin.Services;
using GalaSoft.MvvmLight.CommandWpf;
using MyCity_Admin.Models;

namespace MyCity_Admin.ViewModels
{
    class NewsViewModel : ViewModelBase
    {
        private News news = new News();
        public News News { get => news; set => Set(ref news, value); }

        private BitmapImage image;
        public BitmapImage Image { get => image; set => Set(ref image, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public NewsViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;
        }

        private RelayCommand browseCommand;
        public RelayCommand BrowseCommand
        {
            get => browseCommand ?? (browseCommand = new RelayCommand(
              () =>
              {
                  OpenFileDialog ofdPicture = new OpenFileDialog();
                  ofdPicture.Filter =
                      "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
                  ofdPicture.FilterIndex = 1;

                  if (ofdPicture.ShowDialog() == true)
                      Image =
                          new BitmapImage(new Uri(ofdPicture.FileName));
              }
              ));
        }

        private RelayCommand sendCommand;
        public RelayCommand SendCommand
        {
            get => sendCommand ?? (sendCommand = new RelayCommand(
              () =>
              {
                  byte[] data;
                  JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                  encoder.Frames.Add(BitmapFrame.Create(Image));
                  using (MemoryStream ms = new MemoryStream())
                  {
                      encoder.Save(ms);
                      data = ms.ToArray();
                  }
                  News.Image = data;
                  db.News.Add(News);
                  db.SaveChanges();

                  navigationService.Navigate<ProblemsViewModel>();
              }
              ));
        }

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get => backCommand ?? (backCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<HelloViewModel>();
              }
              ));
        }
    }
}
