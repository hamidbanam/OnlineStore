using mpNuget;
using OnlineStore.Application.Sender.Interface;
using OnlineStore.Application.Static;

namespace OnlineStore.Application.Sender.Implentation
{
    public class SmsSender : ISmsSender
    {

        private readonly RestClient MeliSmsApi;
        public SmsSender()
        {
            MeliSmsApi = new RestClient(MeliSms.UserName, MeliSms.Password);
        }

        public async Task<RestResponse> SendSms(string mobile, string message, bool isFlash = true)
        {
           return MeliSmsApi.Send(mobile, MeliSms.From, message, isFlash);
        }
    }
}
