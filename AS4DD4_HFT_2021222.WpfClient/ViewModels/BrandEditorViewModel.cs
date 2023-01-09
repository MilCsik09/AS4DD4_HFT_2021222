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
    public class BrandEditorViewModel : ObservableRecipient
    {

        public RestCollection<Brand> Brands { get; set; }

		private Brand selectedBrand;

		public Brand SelectedBrand
        {
			get { return selectedBrand; }
			set 
			{
                if (value != null)
                {
                    selectedBrand = new Brand()     //deep copy
                    {
                        Name = value.Name,
                        Id = value.Id,
                        CpuProducts = value.CpuProducts,
                        VgaProducts = value.VgaProducts
                    };
                    OnPropertyChanged();
                    (DeleteBrandCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
		}

        public ICommand CreateBrandCommand { get; set; }
        public ICommand DeleteBrandCommand { get; set; }
        public ICommand UpdateBrandCommand { get; set; }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public BrandEditorViewModel()
        {
            if (!IsInDesignMode)
            {

                Brands = new RestCollection<Brand>("http://localhost:21845/", "Brand", "hub");       //átírni

                //create
                CreateBrandCommand = new RelayCommand(() =>
                {

                    Brands.Add(new Brand()
                    {
                        Name = SelectedBrand.Name
                    });

                });

                //update
                UpdateBrandCommand = new RelayCommand(() =>
                {
                    Brands.Update(SelectedBrand);
                });

                //delete
                DeleteBrandCommand = new RelayCommand(() =>
                {
                    Brands.Delete(selectedBrand.Id);
                },
                () =>   //canexecute
                {
                    return selectedBrand != null;
                });


                selectedBrand = new Brand();
            }
        }
        
    }
}
