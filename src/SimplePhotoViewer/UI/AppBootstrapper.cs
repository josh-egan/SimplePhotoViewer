using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using Ninject;
using SimplePhotoViewer.UI.Ninjection;
using SimplePhotoViewer.UI.ViewModels;

namespace SimplePhotoViewer.UI
{
    public class AppBootstrapper : BootstrapperBase
    {
        private IKernel kernel;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            kernel = new StandardKernel(new UIModule(), new BusinessModule());

            CaliburnCustomizations.AssignCustomBindingFunction();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            try
            {
                return kernel.Get(serviceType);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get instance of type: " + serviceType, e);
            }
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShellViewModel>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            Console.WriteLine(@"Exiting!");
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception occurred! " + e.Exception);
        }
    }
}