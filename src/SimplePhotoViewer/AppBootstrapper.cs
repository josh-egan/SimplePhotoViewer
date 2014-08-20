using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using SimplePhotoViewer.UI;
using SimplePhotoViewer.UI.ViewModels;

namespace SimplePhotoViewer
{
    public class AppBootstrapper : BootstrapperBase
    {
        private readonly CompositionBatch batch = new CompositionBatch();
        private CompositionContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            CaliburnCustomizations.AssignCustomBindingFunction();

            batch.AddExportedValue<IWindowManager>(new WindowManager());

            var catalog = new AggregateCatalog(AssemblySource.Instance.Select(assembly => new AssemblyCatalog(assembly)));
            container = new CompositionContainer(catalog);
            container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            try
            {
                var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
                var exports = container.GetExportedValues<object>(contract);

                var enumerable = exports as IList<object> ?? exports.ToList();
                if (enumerable.Any())
                    return enumerable.First();

                throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
            }
            catch (Exception e)
            {
                var message = string.Format("Failed to get instance of: Type - {0} Key - {1}", serviceType, key);
                throw new Exception(message, e);
            }
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            container.SatisfyImportsOnce(instance);
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