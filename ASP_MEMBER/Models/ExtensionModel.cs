using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ASP_MEMBER.Models
{
    public static class ExtensionModel
    {
        public static string GetErrorModelState(this ModelStateDictionary modelstate)
        {
            var modelValue = modelstate.Values
                                   .Select(value => value.Errors)
                                   .Where(value => value.Count() > 0)
                                   .FirstOrDefault();
            return modelValue[0].ErrorMessage;
        }

        public static Exception GetErrorException(this Exception e)
        {
            if (e.InnerException != null)
                return e.InnerException.GetErrorException();
            return e;
        }
    }
}