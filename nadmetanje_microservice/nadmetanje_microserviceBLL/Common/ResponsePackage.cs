using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nadmetanje_microserviceBLL.Common
{
    public class ResponsePackage<T>
    {
        public ResponseStatus Status { get; set; } = ResponseStatus.OK;

        public string Message { get; set; }

        public T Data { get; set; }

        public ResponsePackage()
        {
            Status = ResponseStatus.OK;
            Message = string.Empty;
        }

        public ResponsePackage(T data, ResponseStatus status = ResponseStatus.OK, string message = "")
        {
            Data = data;
            Status = status;
            Message = message;
        }

        public ResponsePackage(ResponseStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }

    public class ResponsePackageNoData
    {
        public ResponseStatus Status { get; set; } = ResponseStatus.OK;

        public string Message { get; set; }

        public ResponsePackageNoData()
        {
            Status = ResponseStatus.OK;
            Message = string.Empty;
        }

        public ResponsePackageNoData(ResponseStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }

    public enum ResponseStatus
    {
        OK = 200,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        InternalServerError = 500
    }
}
