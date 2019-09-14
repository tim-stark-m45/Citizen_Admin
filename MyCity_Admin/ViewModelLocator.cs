using Autofac;
using Autofac.Configuration;
using GalaSoft.MvvmLight;
using Microsoft.Extensions.Configuration;
using MyCity_Admin.Services;
using MyCity_Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCity_Admin
{
    class ViewModelLocator
    {
        private MainWindowViewModel mainWindowViewModel;
        private InfrastructureListViewModel infrastructureListViewModel;
        private InfrastructureInfoViewModel infrastructureInfoViewModel;
        private ProblemsViewModel problemsViewModel;
        private BuildingListViewModel buildingListViewModel;
        private GovernmentListViewModel governmentListViewModel;
        private HelloViewModel helloViewModel;
        private HospitalsListViewModel hospitalsListViewModel;
        private IdeaListViewModel ideaListViewModel;
        private ImportantViewModel importantViewModel;
        private NewsViewModel newsViewModel;
        private PublicTransportListViewModel publicTransportListViewModel;
        private RoadListViewModel roadListViewModel;
        private SecurityListViewModel securityListViewModel;
        private Trade_AdvertisingListViewModel trade_AdvertisingListViewModel;
        private YardListViewModel yardListViewModel;
        private BuildingInfoViewModel buildingInfoViewModel;
        private GovernmentInfoViewModel governmentInfoViewModel;
        private HospitalsInfoViewModel hospitalsInfoViewModel;
        private IdeaInfoViewModel ideaInfoViewModel;
        private PublicTransportInfoViewModel publicTransportInfoViewModel;
        private RoadInfoViewModel roadInfoViewModel;
        private SecurityInfoViewModel securityInfoViewModel;
        private Trade_AdvertisingInfoViewModel trade_AdvertisingInfoView;
        private YardInfoViewModel yardInfoViewModel;


        private INavigationService navigationService;
        public static IContainer Container;

        public ViewModelLocator()
        {
            try
            {
                var config = new ConfigurationBuilder();
                config.AddJsonFile("autofac.json");
                var module = new ConfigurationModule(config.Build());
                var builder = new ContainerBuilder();
                builder.RegisterModule(module);
                Container = builder.Build();

                navigationService = Container.Resolve<INavigationService>();
                mainWindowViewModel = Container.Resolve<MainWindowViewModel>();
                infrastructureListViewModel = Container.Resolve<InfrastructureListViewModel>();
                infrastructureInfoViewModel = Container.Resolve<InfrastructureInfoViewModel>();
                problemsViewModel = Container.Resolve<ProblemsViewModel>();
                buildingListViewModel = Container.Resolve<BuildingListViewModel>();
                governmentListViewModel = Container.Resolve<GovernmentListViewModel>();
                helloViewModel = Container.Resolve<HelloViewModel>();
                hospitalsListViewModel = Container.Resolve<HospitalsListViewModel>();
                ideaListViewModel = Container.Resolve<IdeaListViewModel>();
                importantViewModel = Container.Resolve<ImportantViewModel>();
                newsViewModel = Container.Resolve<NewsViewModel>();
                publicTransportListViewModel = Container.Resolve<PublicTransportListViewModel>();
                roadListViewModel = Container.Resolve<RoadListViewModel>();
                securityListViewModel = Container.Resolve<SecurityListViewModel>();
                trade_AdvertisingListViewModel = Container.Resolve<Trade_AdvertisingListViewModel>();
                yardListViewModel = Container.Resolve<YardListViewModel>();
                buildingInfoViewModel = Container.Resolve<BuildingInfoViewModel>();
                governmentInfoViewModel = Container.Resolve<GovernmentInfoViewModel>();
                hospitalsInfoViewModel = Container.Resolve<HospitalsInfoViewModel>();
                ideaInfoViewModel = Container.Resolve<IdeaInfoViewModel>();
                publicTransportInfoViewModel = Container.Resolve<PublicTransportInfoViewModel>();
                roadInfoViewModel = Container.Resolve<RoadInfoViewModel>();
                securityInfoViewModel = Container.Resolve<SecurityInfoViewModel>();
                trade_AdvertisingInfoView = Container.Resolve<Trade_AdvertisingInfoViewModel>();
                yardInfoViewModel = Container.Resolve<YardInfoViewModel>();

                navigationService.Register<InfrastructureListViewModel>(infrastructureListViewModel);
                navigationService.Register<InfrastructureInfoViewModel>(infrastructureInfoViewModel);
                navigationService.Register<ProblemsViewModel>(problemsViewModel);
                navigationService.Register<BuildingListViewModel>(buildingListViewModel);
                navigationService.Register<GovernmentListViewModel>(governmentListViewModel);
                navigationService.Register<HelloViewModel>(helloViewModel);
                navigationService.Register<HospitalsListViewModel>(hospitalsListViewModel);
                navigationService.Register<IdeaListViewModel>(ideaListViewModel);
                navigationService.Register<ImportantViewModel>(importantViewModel);
                navigationService.Register<NewsViewModel>(newsViewModel);
                navigationService.Register<PublicTransportListViewModel>(publicTransportListViewModel);
                navigationService.Register<RoadListViewModel>(roadListViewModel);
                navigationService.Register<SecurityListViewModel>(securityListViewModel);
                navigationService.Register<Trade_AdvertisingListViewModel>(trade_AdvertisingListViewModel);
                navigationService.Register<YardListViewModel>(yardListViewModel);
                navigationService.Register<BuildingInfoViewModel>(buildingInfoViewModel);
                navigationService.Register<GovernmentInfoViewModel>(governmentInfoViewModel);
                navigationService.Register<HospitalsInfoViewModel>(hospitalsInfoViewModel);
                navigationService.Register<IdeaInfoViewModel>(ideaInfoViewModel);
                navigationService.Register<PublicTransportInfoViewModel>(publicTransportInfoViewModel);
                navigationService.Register<RoadInfoViewModel>(roadInfoViewModel);
                navigationService.Register<SecurityInfoViewModel>(securityInfoViewModel);
                navigationService.Register<Trade_AdvertisingInfoViewModel>(trade_AdvertisingInfoView);
                navigationService.Register<YardInfoViewModel>(yardInfoViewModel);

                navigationService.Navigate<HelloViewModel>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ViewModelBase GetMainWindowViewModel()
        {
            return mainWindowViewModel;
        }
    }
}
