using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.ModelBinding;

namespace Domain.DTO.Response
{
    //public class ApiBadRequestResponse : ApiResponse
    //{

    //    public List<string> Errors { get; }

    //    public ApiBadRequestResponse(ModelStateDictionary modelState)
    //        : base(400)
    //    {
    //        if (modelState.IsValid)
    //        {
    //            throw new ArgumentException("ModelState must be invalid", nameof(modelState));
    //        }

    //        var errors = new List<string>();
    //        foreach (var item in modelState.Values)
    //        {
    //            foreach (var err in item.Errors)
    //            {
    //                errors.Add(err.ErrorMessage);
    //            }
    //        }

    //        Errors = errors;

    //        //Errors = modelState.SelectMany(x => x.Value.Errors)
    //        //    .Select(x => x.ErrorMessage).ToArray();
    //    }
    //}



    public class ApiBadRequestResponse : ApiResponse
    {

        public List<string> Errors { get; }

        public ApiBadRequestResponse(List<string> errors, string message = "")
            : base(400, message)
        {
            Errors = errors;
        }
    }
}
