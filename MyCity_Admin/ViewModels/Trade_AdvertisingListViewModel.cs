using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System.Collections.ObjectModel;


namespace MyCity_Admin.ViewModels
{
    class Trade_AdvertisingListViewModel : ViewModelBase
    {
        private ObservableCollection<Trade_Advertising> trade_Advertising;
        public ObservableCollection<Trade_Advertising> Trade_Advertising { get => trade_Advertising; set => Set(ref trade_Advertising, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public Trade_AdvertisingListViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;

            Trade_Advertising = new ObservableCollection<Trade_Advertising>(db.Trade_Advertisings);
        }

        private RelayCommand<Trade_Advertising> openInfoCommand;
        public RelayCommand<Trade_Advertising> OpenInfoCommand
        {
            get => openInfoCommand ?? (openInfoCommand = new RelayCommand<Trade_Advertising>(
              param =>
              {
                  Messenger.Default.Send(new SendTrade_Advertising { Data = param });
                  navigationService.Navigate<Trade_AdvertisingInfoViewModel>();
              }
              ));
        }

        private RelayCommand<Trade_Advertising> deleteCommand;
        public RelayCommand<Trade_Advertising> DeleteCommand
        {
            get => deleteCommand ?? (deleteCommand = new RelayCommand<Trade_Advertising>(
              param =>
              {
                  if (messageService.ShowYesNo("Are you sure?"))
                  {
                      db.Trade_Advertisings.Remove(param);
                      db.SaveChanges();
                      Trade_Advertising.Remove(param);
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
                  Trade_Advertising = new ObservableCollection<Trade_Advertising>(db.Trade_Advertisings);
              }
              ));
        }
    }
}
