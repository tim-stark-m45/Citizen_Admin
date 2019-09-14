﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyCity_Admin.Messages;
using MyCity_Admin.Models;
using MyCity_Admin.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyCity_Admin.ViewModels
{
    class HospitalsInfoViewModel : ViewModelBase
    {
        private Hospitals hospitals;
        public Hospitals Hospitals { get => hospitals; set => Set(ref hospitals, value); }

        private BitmapImage image;
        public BitmapImage Image { get => image; set => Set(ref image, value); }

        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
        private readonly AppDbContext db;

        public HospitalsInfoViewModel(
            INavigationService navigationService,
            IMessageService messageService,
            AppDbContext db)
        {
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.db = db;


            Messenger.Default.Register<SendHospitals>(this, msg =>
            {
                Hospitals = msg.Data;

                MemoryStream stream = new MemoryStream(msg.Data.Image);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();

                Image = bitmapImage;
            });

        }

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get => backCommand ?? (backCommand = new RelayCommand(
              () =>
              {
                  navigationService.Navigate<HospitalsListViewModel>();
              }
              ));
        }
    }
}
