using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System.Collections.ObjectModel;


namespace MyCity_Admin.ViewModels
{
    class PublicTransportListViewModel : ViewModelBase
    {
        private ObservableCollection<PublicTransport> publicTransport;
        public ObservableCollection<PublicTransport> PublicTransport { get => publicTransport; set => Set(ref publicTransport, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public PublicTransportListViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;

            PublicTransport = new ObservableCollection<PublicTransport>(db.PublicTransports);
        }

        private RelayCommand<PublicTransport> openInfoCommand;
        public RelayCommand<PublicTransport> OpenInfoCommand
        {
            get => openInfoCommand ?? (openInfoCommand = new RelayCommand<PublicTransport>(
              param =>
              {
                  Messenger.Default.Send(new SendPublicTransport { Data = param });
                  navigationService.Navigate<PublicTransportInfoViewModel>();
              }
              ));
        }

        private RelayCommand<PublicTransport> deleteCommand;
        public RelayCommand<PublicTransport> DeleteCommand
        {
            get => deleteCommand ?? (deleteCommand = new RelayCommand<PublicTransport>(
              param =>
              {
                  if (messageService.ShowYesNo("Are you sure?"))
                  {
                      db.PublicTransports.Remove(param);
                      db.SaveChanges();
                      PublicTransport.Remove(param);
                  }
              }
              ));
        }

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get => backCommand ?? (backCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<ProblemsViewModel>();
              }
              ));
        }

        private RelayCommand refreshCommand;
        public RelayCommand RefreshCommand
        {
            get => refreshCommand ?? (refreshCommand = new RelayCommand(
              () =>
              {
                  PublicTransport = new ObservableCollection<PublicTransport>(db.PublicTransports);
              }
              ));
        }
    }
}
