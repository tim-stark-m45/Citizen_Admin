using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System.Collections.ObjectModel;


namespace MyCity_Admin.ViewModels
{
    class GovernmentListViewModel : ViewModelBase
    {
        private ObservableCollection<Government> government;
        public ObservableCollection<Government> Government { get => government; set => Set(ref government, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public GovernmentListViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;

            Government = new ObservableCollection<Government>(db.Governments);
        }

        private RelayCommand<Government> openInfoCommand;
        public RelayCommand<Government> OpenInfoCommand
        {
            get => openInfoCommand ?? (openInfoCommand = new RelayCommand<Government>(
              param =>
              {
                  Messenger.Default.Send(new SendGovernment { Data = param });
                  navigationService.Navigate<GovernmentInfoViewModel>();
              }
              ));
        }

        private RelayCommand<Government> deleteCommand;
        public RelayCommand<Government> DeleteCommand
        {
            get => deleteCommand ?? (deleteCommand = new RelayCommand<Government>(
              param =>
              {
                  if (messageService.ShowYesNo("Are you sure?"))
                  {
                      db.Governments.Remove(param);
                      db.SaveChanges();
                      Government.Remove(param);
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
                  Government = new ObservableCollection<Government>(db.Governments);
              }
              ));
        }
    }
}
