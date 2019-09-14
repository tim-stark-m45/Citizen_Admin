using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System.Collections.ObjectModel;


namespace MyCity_Admin.ViewModels
{
    class YardListViewModel : ViewModelBase
    {
        private ObservableCollection<Yard> yard;
        public ObservableCollection<Yard> Yard { get => yard; set => Set(ref yard, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public YardListViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;

            Yard = new ObservableCollection<Yard>(db.Yards);
        }

        private RelayCommand<Yard> openInfoCommand;
        public RelayCommand<Yard> OpenInfoCommand
        {
            get => openInfoCommand ?? (openInfoCommand = new RelayCommand<Yard>(
              param =>
              {
                  Messenger.Default.Send(new SendYard { Data = param });
                  navigationService.Navigate<YardInfoViewModel>();
              }
              ));
        }

        private RelayCommand<Yard> deleteCommand;
        public RelayCommand<Yard> DeleteCommand
        {
            get => deleteCommand ?? (deleteCommand = new RelayCommand<Yard>(
              param =>
              {
                  if (messageService.ShowYesNo("Are you sure?"))
                  {
                      db.Yards.Remove(param);
                      db.SaveChanges();
                      Yard.Remove(param);
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
                  Yard = new ObservableCollection<Yard>(db.Yards);
              }
              ));
        }
    }
}
