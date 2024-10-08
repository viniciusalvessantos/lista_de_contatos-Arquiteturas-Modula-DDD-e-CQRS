﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions {
    public class ErroResponse {
        public ErroResponse(int statusCode, string message) {
            StatusCode = statusCode;
            Message = message;
        }

        public ErroResponse(IEnumerable<ValidationFailure> failures, string errorMessage, int statusCode) {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
            Message = errorMessage;
            StatusCode = statusCode;
        }

        public ErroResponse(int statusCode, string message, List<string> validationMessages) {
            StatusCode = statusCode;
            Message = message;

            Errors = validationMessages
                .Select((s, index) => new { s, index })
                .ToDictionary(x => (x.index + 1).ToString(), x => new string[] { x.s });
        }

        public int StatusCode { get; }
        public string Message { get; }

        public IDictionary<string, string[]> Errors { get; }
    }
}
