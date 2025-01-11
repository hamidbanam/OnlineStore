using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetModelError(this ModelStateDictionary modelState)
        {
            string errorMessage = string.Empty;
            int row = 1;

            foreach (var item in modelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    errorMessage += $"{row}. {error.ErrorMessage}\n";

                    row++;
                }
            }

            return errorMessage;
        }
    }
}
