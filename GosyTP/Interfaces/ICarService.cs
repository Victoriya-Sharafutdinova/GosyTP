using GosyTP.ViewModels;
using GosyTP.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GosyTP.Models;

namespace GosyTP.Interfaces
{
    public interface ICarService
    {
    
        List<CarViewModel> GetList();

        CarViewModel GetElement(int id);

        void AddElement(CarBindingModel model);

        void UpdElement(CarBindingModel model);

        void DelElement(int id);

        List<CarViewModel> GetListPowerMoreThan100();

        List<CarViewModel> GetListGroup();

    }
}
