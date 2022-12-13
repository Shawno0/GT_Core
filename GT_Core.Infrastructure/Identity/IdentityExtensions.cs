using Microsoft.AspNetCore.Identity;
using GT_Core.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Core.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result<string> ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result<string>.Success()
                : Result<string>.Failure(result.Errors.Select(e => e.Description));
        }

        public static Result<string> ToApplicationResult(this IdentityResult result, string entity)
        {
            return result.Succeeded
                ? Result<string>.Success(entity)
                : Result<string>.Failure(result.Errors.Select(e => e.Description));
        }
    }
}