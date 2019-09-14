using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System.Collections.ObjectModel;


namespace MyCity_Admin.ViewModels
{
    class IdeaListViewModel : ViewModelBase
    {
        private ObservableCollection<Idea> idea;
        public ObservableCollection<Idea> Idea { get => idea; set => Set(ref idea, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public IdeaListViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;

            Idea = new ObservableCollection<Idea>(db.Ideas);
        }

        private RelayCommand<Idea> openInfoCommand;
        public RelayCommand<Idea> OpenInfoCommand
        {
            get => openInfoCommand ?? (openInfoCommand = new RelayCommand<Idea>(
              param =>
              {
                  Messenger.Default.Send(new SendIdea { Data = param });
                  navigationService.Navigate<IdeaInfoViewModel>();
              }
              ));
        }

        private RelayCommand<Idea> deleteCommand;
        public RelayCommand<Idea> DeleteCommand
        {
            get => deleteCommand ?? (deleteCommand = new RelayCommand<Idea>(
              param =>
              {
                  if (messageService.ShowYesNo("Are you sure?"))
                  {
                      db.Ideas.Remove(param);
                      db.SaveChanges();
                      Idea.Remove(param);
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
                  navigationService.Navigate<HelloViewModel>();
              }
              ));
        }

        private RelayCommand refreshCommand;
        public RelayCommand RefreshCommand
        {
            get => refreshCommand ?? (refreshCommand = new RelayCommand(
              () =>
              {
                  Idea = new ObservableCollection<Idea>(db.Ideas);
              }
              ));
        }
    }
}
