﻿namespace TurnitStore.Domain.Models
{
    public class ResultWrapper<TResult>
    {
        public ResultStatus Status { get; private set; }

        public TResult? Result { get; private set; }

        public string? Message { get; private set; }

        public static ResultWrapper<TResult> Success(TResult data) => new ResultWrapper<TResult>
        {
            Result = data,
            Status = ResultStatus.Success,
        };

        public static ResultWrapper<TResult> Faliure(ResultStatus status, string message) => new ResultWrapper<TResult>
        {
            Message = message,
            Status = status,
        };

    }

    public enum ResultStatus
    {
        Success,
        NotFound,
        ShouldNotComplete,
        Insufficient
    }

}
