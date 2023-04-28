using Microsoft.CodeAnalysis.CSharp.Syntax;
using Repository;
using Services.Interfaces;
using Services;

namespace GymMasterPro.Extension
{
    public static class IocContainer
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ITrainerService, TrainerService>();
        }
    }
}
