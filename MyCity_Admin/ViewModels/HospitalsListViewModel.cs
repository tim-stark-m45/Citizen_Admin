using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System.Collections.ObjectModel;


namespace MyCity_Admin.ViewModels
{
    class HospitalsListViewModel : ViewModelBase
    {
        private ObservableCollection<Hospitals> hospitals;
        public ObservableCollection<Hospitals> Hospitals { get => hospitals; set => Set(ref hospitals, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public HospitalsListViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;

            Hospitals = new ObservableCollection<Hospitals>(db.Hospitals);
        }

        private RelayCommand<Hospitals> openInfoCommand;
        public RelayCommand<Hospitals> OpenInfoCommand
        {
            get => openInfoCommand ?? (openInfoCommand = new RelayCommand<Hospitals>(
              param =>
              {
                  Messenger.Default.Send(new SendHospitals { Data = param });
                  navigationService.Navigate<HospitalsInfoViewModel>();
              }
              ));
        }

        private RelayCommand<Hospitals> deleteCommand;
        public RelayCommand<Hospitals> DeleteCommand
        {
            get => deleteCommand ?? (deleteCommand = new RelayCommand<Hospitals>(
              param =>
              {
                  if (messageService.ShowYesNo("Are you sure?"))
                  {
                      db.Hospitals.Remove(param);
                      db.SaveChanges();
                      Hospitals.Remove(param);
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
                  Hospitals = new ObservableCollection<Hospitals>(db.Hospitals);
              }
              ));
        }
    }
}
