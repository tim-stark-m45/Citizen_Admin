using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System.Collections.ObjectModel;


namespace MyCity_Admin.ViewModels
{
    class BuildingListViewModel : ViewModelBase
    {
        private ObservableCollection<Building> building;
        public ObservableCollection<Building> Building { get => building; set => Set(ref building, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public BuildingListViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;

            Building = new ObservableCollection<Building>(db.Buildings);
        }

        private RelayCommand<Building> openInfoCommand;
        public RelayCommand<Building> OpenInfoCommand
        {
            get => openInfoCommand ?? (openInfoCommand = new RelayCommand<Building>(
              param =>
              {
                  Messenger.Default.Send(new SendBuilding { Data = param });
                  navigationService.Navigate<BuildingInfoViewModel>();
              }
              ));
        }

        private RelayCommand<Building> deleteCommand;
        public RelayCommand<Building> DeleteCommand
        {
            get => deleteCommand ?? (deleteCommand = new RelayCommand<Building>(
              param =>
              {
                  if (messageService.ShowYesNo("Are you sure?"))
                  {
                      db.Buildings.Remove(param);
                      db.SaveChanges();
                      Building.Remove(param);
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
                  Building = new ObservableCollection<Building>(db.Buildings);
              }
              ));
        }
    }
}
