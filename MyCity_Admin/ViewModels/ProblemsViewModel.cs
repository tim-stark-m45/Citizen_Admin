using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MyCity_Admin.Models;
using MyCity_Admin.Services;

namespace MyCity_Admin.ViewModels
{
    class ProblemsViewModel : ViewModelBase
    {

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public ProblemsViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;
        }

        private RelayCommand infrastructureCommand;
        public RelayCommand InfrastructureCommand
        {
            get => infrastructureCommand ?? (infrastructureCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<InfrastructureListViewModel>();
              }
              ));
        }

        private RelayCommand buildingCommand;
        public RelayCommand BuildingCommand
        {
            get => buildingCommand ?? (buildingCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<BuildingListViewModel>();
              }
              ));
        }

        private RelayCommand governmentCommand;
        public RelayCommand GovernmentCommand
        {
            get => governmentCommand ?? (governmentCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<GovernmentListViewModel>();
              }
              ));
        }

        private RelayCommand hospitalCommand;
        public RelayCommand HospitalCommand
        {
            get => hospitalCommand ?? (hospitalCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<HospitalsListViewModel>();
              }
              ));
        }

        private RelayCommand transportCommand;
        public RelayCommand TransportCommand
        {
            get => transportCommand ?? (transportCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<PublicTransportListViewModel>();
              }
              ));
        }

        private RelayCommand roadCommand;
        public RelayCommand RoadCommand
        {
            get => roadCommand ?? (roadCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<RoadListViewModel>();
              }
              ));
        }

        private RelayCommand securityCommand;
        public RelayCommand SecurityCommand
        {
            get => securityCommand ?? (securityCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<SecurityListViewModel>();
              }
              ));
        }

        private RelayCommand tradeCommand;
        public RelayCommand TradeCommand
        {
            get => tradeCommand ?? (tradeCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<Trade_AdvertisingListViewModel>();
              }
              ));
        }

        private RelayCommand yardCommand;
        public RelayCommand YardCommand
        {
            get => yardCommand ?? (yardCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<YardListViewModel>();
              }
              ));
        }
    }
}
