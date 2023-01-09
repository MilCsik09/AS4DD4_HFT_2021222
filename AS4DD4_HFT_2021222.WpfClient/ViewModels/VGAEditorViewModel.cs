using AS4DD4_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AS4DD4_HFT_2021222.WpfClient.ViewModels
{
    public class VGAEditorViewModel : ObservableRecipient
    {

        public RestCollection<VGA> VGAs { get; set; }

		private VGA selectedVGA;

		public VGA SelectedVGA
        {
			get { return selectedVGA; }
			set 
			{
                if (value != null)
                {
                    selectedVGA = new VGA()     //deep copy
                    {
                        Id = value.Id,
                        Model = value.Model,
                        Price = value.Price,
                        isUsed = value.isUsed,
                        isOperational = value.isOperational,
                        BrandId = value.BrandId,
                        Brand = value.Brand,
                        Computers = value.Computers
                    };
                    OnPropertyChanged();
                    (DeleteVGACommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
		}

        public ICommand CreateVGACommand { get; set; }
        public ICommand DeleteVGACommand { get; set; }
        public ICommand UpdateVGACommand { get; set; }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public VGAEditorViewModel()
        {
            if (!IsInDesignMode)
            {

                VGAs = new RestCollection<VGA>("http://localhost:21845/", "VGA", "hub");       //átírni

                //create
                CreateVGACommand = new RelayCommand(() =>
                {

                    VGAs.Add(new VGA()
                    {
                        Model = SelectedVGA.Model
                    });

                });

                //update
                UpdateVGACommand = new RelayCommand(() =>
                {
                    VGAs.Update(SelectedVGA);
                });

                //delete
                DeleteVGACommand = new RelayCommand(() =>
                {
                    VGAs.Delete(selectedVGA.Id);
                },
                () =>   //canexecute
                {
                    return selectedVGA != null;
                });


                selectedVGA = new VGA();
            }
        }
        
    }
}
