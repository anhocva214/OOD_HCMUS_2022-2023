using System.Diagnostics;
using System.Net;
using System.Text;
using financial_management_service.Core.Object;
using financial_management_service.Extensions;
using Newtonsoft.Json;


namespace financial_management_service.Core.Exceptions
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate request;
        private readonly static string defaultMessage = "Hệ thống tạm thời gián đoạn vui lòng thử lại sau ít phút.";

        public GlobalExceptionHandler(RequestDelegate pipeline)
        {
            this.request = pipeline;
        }

        public Task Invoke(HttpContext context) => this.InvokeAsync(context); // Stops VS from nagging about async method without ...Async suffix.

        async Task InvokeAsync(HttpContext context)
        {
            var l = InitLog();
            MemoryStream? responseBody = null;
            Stream? originalBodyStream = context.Response.Body;
            try
            {
                l = await FormatRequest(context.Request, l);

                originalBodyStream = context.Response.Body;

                responseBody = new MemoryStream();

                context.Response.Body = responseBody;
                await this.request(context);
            }
            catch (Exception exception)
            {
                l.Level = "ERROR";
                l.Message.Response.StackTrace = exception.StackTrace;
                l.Message.Response.ErrorMessage = $"{exception.Message} {exception.GetBaseException()?.Message}";

                await Ext(exception, context);
            }
            finally
            {
                await Log(context, l, responseBody ?? new MemoryStream(), originalBodyStream);
            }
        }

        #region Private Methods
        private static async Task Ext(Exception exception, HttpContext context)
        {
            if (exception is BizException)
            {
                var ex = exception as BizException;
                await Respond(context, ex != null ? ex.ErrorCode : "400.00", ex != null ? ex.ErrorMessage : defaultMessage, HttpStatusCode.BadRequest);
                return;
            }
            if (exception is UnauthorizedException)
            {
                await Respond(context, "401.00", "", HttpStatusCode.Unauthorized);
                return;
            }
            if (exception is CorsException exc)
            {
                await Respond(context, "403.00", exc.ErrorMessage, HttpStatusCode.Forbidden);
                return;
            }
            if (exception is PermissionException)
                await Respond(context, "403.00", "Bạn không có quyền truy cập tính năng này.", HttpStatusCode.Forbidden);

            else
                await Respond(context, "500.00", defaultMessage, HttpStatusCode.InternalServerError);
        }

        private static async Task Log(HttpContext context, LoggingObj l, MemoryStream responseBody, Stream originalBodyStream)
        {
            l = await FormatResponse(context.Response, l);
            l.EndTime = DateTime.Now;

            await responseBody.CopyToAsync(originalBodyStream);

            Log(l);
        }

        private static void Log(LoggingObj l)
        {
            var headers = l.Message.Request.Headers;
            if (headers != null)
                l.Message.Request.Headers = RemoveSensitiveInfo(headers);

            var info = JsonConvert.SerializeObject(l);
            if (!info.IsNullOrEmpty())
                Console.WriteLine(info);
        }

        private static async Task Respond(HttpContext context, string code, string mes, HttpStatusCode httpStatusCode)
        {
            context.Response.StatusCode = (int)httpStatusCode;
            await context.Response.WriteAsJsonAsync(new ApiExceptionResDto(code, mes));
        }

        private static LoggingObj InitLog()
        {
            return new LoggingObj()
            {
                SpanId = Activity.Current != null ? Activity.Current.SpanId.ToString() : string.Empty,
                TraceId = Activity.Current != null ? Activity.Current.TraceId.ToString() : string.Empty,
                Timestamp = DateTime.Now,
                ThreadName = AppDomain.CurrentDomain.FriendlyName,
            };
        }

        private static async Task<LoggingObj> FormatRequest(HttpRequest request, LoggingObj l)
        {
            var body = request.Body;
            var headers = request.Headers;

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length));

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body = body;
            request.Body.Position = 0;
            l.Message.Request.Headers = headers;
            l.Message.Request.Payload = bodyAsText;

            return l;
        }

        private static async Task<LoggingObj> FormatResponse(HttpResponse response, LoggingObj l)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            l.Message.Response.Payload = text;
            l.Message.Response.HttpStatusCode = response.StatusCode.ToString();

            return l;
        }

        private static IHeaderDictionary RemoveSensitiveInfo(IHeaderDictionary input)
        {
            input.Authorization = string.Empty;
            return input;
        }
        #endregion
    }

}

