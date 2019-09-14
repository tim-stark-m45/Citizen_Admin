using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System.IO;
using System.Windows.Media.Imaging;

namespace MyCity_Admin.ViewModels
{
    class BuildingInfoViewModel : ViewModelBase
    {
        private Building building;
        public Building Building { get => building; set => Set(ref building, value); }

        private BitmapImage image;
        public BitmapImage Image { get => image; set => Set(ref image, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public BuildingInfoViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;


            Messenger.Default.Register<SendBuilding>(this, msg =>
            {
                Building = msg.Data;

                MemoryStream stream = new MemoryStream(msg.Data.Image1);

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
                  navigationService.Navigate<BuildingListViewModel>();
              }
              ));
        }
    }
}
