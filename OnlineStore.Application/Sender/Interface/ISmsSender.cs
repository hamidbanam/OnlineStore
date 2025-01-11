using mpNuget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Sender.Interface
{
    public interface ISmsSender
    {
        public Task<RestResponse> SendSms(string mobile, string message, bool isFlash = true);
    }
}
