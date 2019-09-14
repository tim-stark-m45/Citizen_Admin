using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System.Collections.ObjectModel;


namespace MyCity_Admin.ViewModels
{
    class RoadListViewModel : ViewModelBase
    {
        private ObservableCollection<Road> road;
        public ObservableCollection<Road> Road { get => road; set => Set(ref road, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public RoadListViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;

            Road = new ObservableCollection<Road>(db.Roads);
        }

        private RelayCommand<Road> openInfoCommand;
        public RelayCommand<Road> OpenInfoCommand
        {
            get => openInfoCommand ?? (openInfoCommand = new RelayCommand<Road>(
              param =>
              {
                  Messenger.Default.Send(new SendRoad { Data = param });
                  navigationService.Navigate<RoadInfoViewModel>();
              }
              ));
        }

        private RelayCommand<Road> deleteCommand;
        public RelayCommand<Road> DeleteCommand
        {
            get => deleteCommand ?? (deleteCommand = new RelayCommand<Road>(
              param =>
              {
                  if (messageService.ShowYesNo("Are you sure?"))
                  {
                      db.Roads.Remove(param);
                      db.SaveChanges();
                      Road.Remove(param);
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
                  Road = new ObservableCollection<Road>(db.Roads);
              }
              ));
        }
    }
}
