using OnlineStore.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface INovinoService
    {
        Task<NovinoGetPaymentUrlResponseDto> CreateRequestAsync(NovinoGetPaymentUrlRequestDto model);
        Task<NovinoVerifyPaymentResponseDto> VerifyAsync(NovinoVerifyPaymentRequestDto model);
    }
}
