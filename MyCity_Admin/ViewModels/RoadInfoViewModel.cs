using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
namespace MyCity_Admin.ViewModels
{
    class RoadInfoViewModel : ViewModelBase
    {
        private Road road;
        public Road Road { get => road; set => Set(ref road, value); }

        private BitmapImage image;
        public BitmapImage Image { get => image; set => Set(ref image, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public RoadInfoViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;


            Messenger.Default.Register<SendRoad>(this, msg =>
            {
                Road = msg.Data;

                MemoryStream stream = new MemoryStream(msg.Data.Image);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();

                Image = bitmapImage;
            });

        }

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get => backCommand ?? (backCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<RoadListViewModel>();
              }
              ));
        }
    }
}
