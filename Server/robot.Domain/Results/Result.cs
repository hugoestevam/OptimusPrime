using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace robot.Domain.Results
{
    public struct Result<T>
    {
        // Success constructor
        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = null;
        }

        // Failure constructor
        private Result(Exception error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccess { get; }
        public T? Value { get; }
        public Exception? Error { get; }

        // This Method takes two Func<T>, one for the success case and one for the error case
        public Result<TReturn> Match<TReturn>(
            Func<T, TReturn> onSuccess,
            Func<Exception, Exception> onFailure)
        {
            if (IsSuccess)
            {
                // If this result has a value, run the success method,
                // which returns a different value, and then we create a
                // Result<TReturn> from it (implicitly)
                var result = onSuccess(Value);
                return result;
            }
            else
            {
                {
                    // If this result is an error, run the error method
                    // to allow the user to manipulate/inspect the error.
                    // We then create a new Result<TReturn> result object
                    // from the error it returns
                    var err = onFailure(Error);
                    return Result<TReturn>.Fail(err);
                }
            }

        }

        // Helper methods for constructing the `Result<T>`
        public static Result<T> Success(T value) => new(value);
        public static Result<T> Fail(Exception error) => new(error);
        public static Result<T> TryRun(Func<Result<T>> func)
        {
            try
            {
                return func();
            }
            catch (Exception e)
            {
                return Fail(e);
            }
        }
        public static async Task<Result<T>> TryRunAsync(Func<Task<Result<T>>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception e)
            {
                return Fail(e);
            }
        }

        // Allow converting a T directly into Result<T>
        public static implicit operator Result<T>(T value) => Success(value);

    }

    /// <summary>
    /// Struct utilizado quando não existe um objeto de retorno.
    /// </summary>
    public struct Unit
    {
        public static Unit Successful { get { return new Unit(); } }
    }

    public static partial class Helpers
    {
        private static readonly Unit unit = new Unit();
        public static Unit Unit() => unit;

        public static Func<T, Unit> ToFunc<T>(Action<T> action) => o =>
        {
            action(o);
            return Unit();
        };

        public static Func<Unit> ToFunc(Action action) => () =>
        {
            action();
            return Unit();
        };
    }
}

