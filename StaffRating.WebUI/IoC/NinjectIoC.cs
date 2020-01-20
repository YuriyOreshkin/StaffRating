using Ninject;
using StaffRating.Domain.Repository.Interfaces;
using StaffRating.Domain.Repository.Realizations.EF;

namespace StaffRating.WebUI.IoC
{
    public static class NinjectIoC
    {
        public static IKernel Initialize()
        {
            IKernel kernel = new StandardKernel();
            AddBindings(kernel);
            return kernel;
        }

        private static IKernel AddBindings(IKernel ninjectKernel)
        {
            ninjectKernel.Bind<IDBRepository>().To<EFRepository>();
           
            return ninjectKernel;
        }
    }
}