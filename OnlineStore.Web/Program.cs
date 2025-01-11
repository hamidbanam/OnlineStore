using NLog;
using NLog.Web;
using OnlineStore.Web.Extensions;


var logger =LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args)
    .RegisterServices()
    .RegisterPipeLines();
}
catch (Exception? exception)
{
    logger.Error(exception,"Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}




