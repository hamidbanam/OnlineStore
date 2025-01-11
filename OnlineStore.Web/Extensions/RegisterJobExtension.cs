using OnlineStore.Web.Jobs;
using Quartz;
using Quartz.AspNetCore;
namespace OnlineStore.Web.Extensions

{
    public static class RegisterJobExtension
    {
        public static IServiceCollection RegisterJobs(this IServiceCollection services)
        {
            services.AddQuartz(quartz =>
            {
                JobKey closeOrderJobKey = new("closeOrderJobKey");
                quartz.AddJob<CloseOrdersJob>(closeOrderJobKey);
                quartz.AddTrigger(s =>
                {
                    s.StartNow()
                    .ForJob(closeOrderJobKey)
                    .WithIdentity("closeOrderJob")
                    .WithSimpleSchedule(sc => sc.RepeatForever().WithInterval(TimeSpan.FromMinutes(30)));
                });

            });

            services.AddQuartzServer(options => options.WaitForJobsToComplete = true);

            return services;
        }
    }
}
