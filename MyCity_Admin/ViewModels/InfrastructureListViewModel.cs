using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System.Collections.ObjectModel;

namespace MyCity_Admin.ViewModels
{
    class InfrastructureListViewModel : ViewModelBase
    {
        private ObservableCollection<Infrastructure> infrastructures;
        public ObservableCollection<Infrastructure> Infrastructures { get => infrastructures; set => Set(ref infrastructures, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public InfrastructureListViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;

            Infrastructures = new ObservableCollection<Infrastructure>(db.Infrastructures);
        }

        private RelayCommand<Infrastructure> openInfoCommand;
        public RelayCommand<Infrastructure> OpenInfoCommand
        {
            get => openInfoCommand ?? (openInfoCommand = new RelayCommand<Infrastructure>(
              param =>
              {
                  Messenger.Default.Send(new SendData { Data = param });
                  navigationService.Navigate<InfrastructureInfoViewModel>();
              }
              ));
        }

        private RelayCommand<Infrastructure> deleteCommand;
        public RelayCommand<Infrastructure> DeleteCommand
        {
            get => deleteCommand ?? (deleteCommand = new RelayCommand<Infrastructure>(
              param =>
              {
                  if (messageService.ShowYesNo("Are you sure?"))
                  {
                      db.Infrastructures.Remove(param);
                      db.SaveChanges();
                      Infrastructures.Remove(param);
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
                  Infrastructures = new ObservableCollection<Infrastructure>(db.Infrastructures);
              }
              ));
        }
    }
}
