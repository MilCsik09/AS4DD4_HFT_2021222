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
    public class CPUEditorViewModel : ObservableRecipient
    {

        public RestCollection<CPU> CPUs { get; set; }

		private CPU selectedCPU;

		public CPU SelectedCPU
        {
			get { return selectedCPU; }
			set 
			{
                if (value != null)
                {
                    selectedCPU = new CPU()     //deep copy
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
                    (DeleteCPUCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
		}

        public ICommand CreateCPUCommand { get; set; }
        public ICommand DeleteCPUCommand { get; set; }
        public ICommand UpdateCPUCommand { get; set; }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public CPUEditorViewModel()
        {
            if (!IsInDesignMode)
            {

                CPUs = new RestCollection<CPU>("http://localhost:21845/", "CPU", "hub");       //átírni

                //create
                CreateCPUCommand = new RelayCommand(() =>
                {

                    CPUs.Add(new CPU()
                    {
                        Model = SelectedCPU.Model
                    });

                });

                //update
                UpdateCPUCommand = new RelayCommand(() =>
                {
                    CPUs.Update(SelectedCPU);
                });

                //delete
                DeleteCPUCommand = new RelayCommand(() =>
                {
                    CPUs.Delete(selectedCPU.Id);
                },
                () =>   //canexecute
                {
                    return selectedCPU != null;
                });


                selectedCPU = new CPU();
            }
        }
        
    }
}
