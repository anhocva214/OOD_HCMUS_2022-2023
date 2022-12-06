using System;
using Newtonsoft.Json;

namespace financial_management_service.Core.Object
{
    internal class LoggingObj
    {
        [JsonProperty("@timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("@version")]
        public string Version { get; set; }

        [JsonProperty("thread_name")]
        public string ThreadName { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("application_name")]
        public string ApplicationName { get; set; }

        [JsonProperty("traceId")]
        public string TraceId { get; set; }

        [JsonProperty("spanId")]
        public string SpanId { get; set; }

        [JsonProperty("message")]
        public LogMessage Message { get; set; }

        [JsonProperty("executionTimeInSeconde")]
        public string ExecutionTimeInSeconde
        {
            get
            {
                return (EndTime - StartTime).TotalSeconds.ToString();
            }
        }

        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }

        [JsonProperty("endTime")]
        public DateTime EndTime { get; set; }

        public LoggingObj()
        {
            this.Timestamp = DateTime.Now;
            this.ApplicationName = "dbo-user-service";
            this.Version = "1";
            this.Level = "INFO";
            this.StartTime = DateTime.Now;
            Message = new LogMessage();
            ThreadName = String.Empty;
            TraceId = String.Empty;
            SpanId = String.Empty;
        }
    }

    public class LogMessage
    {
        public LogRequest Request { get; set; }
        public LogResponse Response { get; set; }

        public LogMessage()
        {
            Request = new LogRequest();
            Response = new LogResponse();
        }
    }

    public class LogRequest
    {
        public IHeaderDictionary? Headers { get; set; }
        public object? Payload { get; set; }
    }

    public class LogResponse
    {
        public object? Payload { get; set; }
        public object? StackTrace { get; set; }
        public object? ErrorMessage { get; set; }
        public string? HttpStatusCode { get; set; }
    }

}

