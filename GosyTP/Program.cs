using GosyTP.Implementations;
using GosyTP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace GosyTP
{

    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<Form1>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ICarService, CarService>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
