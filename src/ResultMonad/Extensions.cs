using System;

namespace ResultMonad {
    public struct Result<TData> {

        public Result(TData data) {
            Data = data;
            Success = true;
            Error = "";
        }

        public bool Success { private set; get; }
        public TData Data { private set; get; }
        public string Error { private set; get; }

        public static Result<TData> FromError(string err) {
            return new Result<TData> {
                Success = false,
                Error = err
            };
        }
        public static Result<TData> FromData(TData data) {
            return new Result<TData> {
                Success = true,
                Data = data
            };
        }
    }

    public static class Extensions {
        public static Result<TResult> SelectMany<TSource, TCollection, TResult>(
                this Result<TSource> source,
                Func<TSource, Result<TCollection>> collectionSelector,
                Func<TSource, TCollection, TResult> resultSelector) {

            if (source.Success) {
                var collection = collectionSelector(source.Data);
                if (collection.Success) {
                    var result = resultSelector(source.Data, collection.Data);
                    return Result<TResult>.FromData(result);
                } else {
                    return Result<TResult>.FromError(collection.Error);
                }
            } else {
                return Result<TResult>.FromError(source.Error);
            }
        }
    }
}
