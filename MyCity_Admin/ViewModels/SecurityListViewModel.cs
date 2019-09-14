using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System.Collections.ObjectModel;


namespace MyCity_Admin.ViewModels
{
    class SecurityListViewModel : ViewModelBase
    {
        private ObservableCollection<Security> security;
        public ObservableCollection<Security> Security { get => security; set => Set(ref security, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public SecurityListViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;

            Security = new ObservableCollection<Security>(db.Securities);
        }

        private RelayCommand<Security> openInfoCommand;
        public RelayCommand<Security> OpenInfoCommand
        {
            get => openInfoCommand ?? (openInfoCommand = new RelayCommand<Security>(
              param =>
              {
                  Messenger.Default.Send(new SendSecurity { Data = param });
                  navigationService.Navigate<SecurityInfoViewModel>();
              }
              ));
        }

        private RelayCommand<Security> deleteCommand;
        public RelayCommand<Security> DeleteCommand
        {
            get => deleteCommand ?? (deleteCommand = new RelayCommand<Security>(
              param =>
              {
                  if (messageService.ShowYesNo("Are you sure?"))
                  {
                      db.Securities.Remove(param);
                      db.SaveChanges();
                      Security.Remove(param);
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
                  Security = new ObservableCollection<Security>(db.Securities);
              }
              ));
        }
    }
}
