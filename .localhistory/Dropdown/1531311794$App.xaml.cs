namespace Dropdown
{
    using System.Windows;
    using Ninject;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IKernel Kernel { get; } = CreateKernel();

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<MainViewModel>().ToSelf().InSingletonScope();
            return kernel;
        }
    }
}
