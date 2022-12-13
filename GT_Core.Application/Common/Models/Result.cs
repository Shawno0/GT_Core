using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Core.Application.Common.Models
{
    public class Result<T>
    {
        public Result(bool succeeded)
        {
            Succeeded = succeeded;
            Errors = Enumerable.Empty<string>();
        }

        public Result(bool succeeded, T? entity)
        {
            Succeeded = succeeded;
            Entity = entity;
            Errors = Enumerable.Empty<string>();
        }

        public Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }

        public Result(bool succeeded, T? entity, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Entity = entity;
            Errors = errors;
        }

        public bool Succeeded { get; set; }
        public T? Entity { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public static Result<T> Success()
        {
            return new Result<T>(true);
        }

        public static Result<T> Success(T? entity)
        {
            return new Result<T>(true, entity);
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>(false, errors);
        }

        public static Result<T> Failure(T? entity, IEnumerable<string> errors)
        {
            return new Result<T>(false, entity, errors);
        }
    }
}