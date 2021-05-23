using GosyTP.BindingModels;
using GosyTP.Interfaces;
using GosyTP.Models;
using GosyTP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GosyTP.Implementations
{
    class CarService : ICarService
    {

        public List<CarViewModel> GetList()
        {
            List<CarViewModel> result = LoadXML().Select(rec => new CarViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                Model = rec.Model,
                Power = rec.Power
            })
            .ToList();
            return result;
        }

        public CarViewModel GetElement(int id)
        {
            Car element = LoadXML().FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CarViewModel
                {
                    Id = element.Id,
                    Name = element.Name,
                    Model = element.Model,
                    Power = element.Power
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CarBindingModel model)
        {
            List<Car> cars = LoadXML();
            
            cars.Add(new Car
            {
                Id = cars.Count == 0 ? 1 : cars.Last().Id + 1,
                Name = model.Name,
                Model = model.Model,
                Power = model.Power

            });
            SaveXML(cars);
        }

        public void UpdElement(CarBindingModel model)
        {
            List<Car> cars = LoadXML();
           
            Car element = cars.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Name = model.Name;
            element.Model = model.Model;
            element.Power = model.Power;
            SaveXML(cars);

        }

        public void DelElement(int id)
        {
            List<Car> cars = LoadXML();
            Car element = cars.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                cars.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
            SaveXML(cars);

        }

        public List<CarViewModel> GetListPowerMoreThan100()
        {
            List<Car> cars = LoadXML();
            var result = from car in cars
                         where car.Power > 100
                         select new
                         {
                             car.Name,
                             car.Model,
                             car.Power
                         };
                         
            List<CarViewModel> carViewModels = new List<CarViewModel>();

            foreach (var car in result)
            {
                carViewModels.Add(new CarViewModel
                {
                    Name = car.Name,
                    Model = car.Model,
                    Power = car.Power
                });
            }
            return carViewModels;
        }

        public List<CarViewModel> GetListGroup()
        {
            var groupCars = LoadXML().GroupBy(car=>car.Name);
           
            List<CarViewModel> carViewModels = new List<CarViewModel>();

            foreach (var groupCar in groupCars)
            {
                foreach(var car in groupCar)
                {
                    carViewModels.Add(new CarViewModel
                    {
                        Name = car.Name,
                        Model = car.Model,
                        Power = car.Power
                    });
                }
                
            }
            return carViewModels;
        }

        public void SaveXML(List<Car> cars)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Car>));
            using(FileStream fs = new FileStream("car.xml", FileMode.Create))
            {
                formatter.Serialize(fs, cars);
            }
        }

        private List<Car> LoadXML()
        {
            List<Car> cars;
            XmlSerializer formatter = new XmlSerializer(typeof(List<Car>));

            using (FileStream fs = new FileStream("car.xml", FileMode.OpenOrCreate))
            {
                if (fs.Length == 0)
                {
                    cars = new List<Car>();
                    formatter.Serialize(fs, cars);
                }
                cars = (List<Car>)formatter.Deserialize(fs);
            }
            return cars;
        }
    }
}
