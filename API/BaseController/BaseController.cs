using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using API.Helper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.GeneralController
{
    public class BaseController : Controller
    {
        private readonly IConfiguration _configuration;

        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetIp()
        {
            // using var httpClient = new HttpClient();
            // var remoteIpAddress = await httpClient.GetStringAsync("http://icanhazip.com");
            // return remoteIpAddress;
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            return remoteIpAddress;

        }

        private string DecodeHeader(string headerContent)
        {
            string userID = HttpUtility.UrlDecode(headerContent.ToString());
            byte[] byteUser = Convert.FromBase64String(userID);
            string decodedString = Encoding.UTF8.GetString(byteUser);
            char[] charArray = decodedString.ToCharArray();
            Array.Reverse(charArray);
            var reversedText = new String(charArray);

            return reversedText;
        }

        [NonAction]
        public void SetBaseModelProperties(BaseModel model)
        {
            // Ambil nilai header
            var headerContent = Request.Headers["User"].ToString();

            model.ID ??= Guid.NewGuid().ToString("N").ToLower();
            model.CreDate = DateTime.Now;
            model.CreBy = DecodeHeader(headerContent);
            model.CreIPAddress = GetIp();
            model.ModDate = DateTime.Now;
            model.ModBy = DecodeHeader(headerContent);
            model.ModIPAddress = GetIp();
        }

        protected ActionResult ResponseSuccess<T>(IEnumerable<T> data)
        {
            var dataList = data.Select(d =>
                    d.GetType()
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(prop => prop.Name == "ID" || prop.DeclaringType == d.GetType())
                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(d))
                )
                .ToList();

            var okResponse = Api.CreateResponse(data.Count(), HttpStatusCode.OK, dataList, "");
            return Ok(okResponse);
        }

        protected ActionResult ResponseSuccess(object? data, int res = 1)
        {
            data = data?.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.Name == "ID" || prop.DeclaringType == data.GetType())
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(data));
            var okResponse = Api.CreateResponse(res, HttpStatusCode.OK, data, "");
            return Ok(okResponse);
        }

        protected ActionResult ResponseSuccess(int data)
        {

            var okResponse = Api.CreateResponse(data, HttpStatusCode.OK, "", "");
            return Ok(okResponse);
        }

        protected ActionResult ResponseError(Exception ex)
        {
            var okResponse = Api.CreateResponse<object>(
                0,
                HttpStatusCode.InternalServerError,
                null,
                ex.Message
            );
            return Ok(okResponse);
        }

        protected async Task<ActionResult> ResUpload(IFormFile files)
        {
            try
            {
                var fileName = Path.GetFileNameWithoutExtension(files.FileName);
                var extension = Path.GetExtension(files.FileName);
                var dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                var hashedName = $"{fileName}_{dateTime}{extension}";

                var filePath = Path.Combine(
                    _configuration.GetValue<string>("FileStorage"),
                    hashedName
                );

                using (var stream = System.IO.File.Create(filePath))
                {
                    await files.CopyToAsync(stream);
                }

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        protected ActionResult ResDownload(string fileName)
        {
            var filePath = Path.Combine(_configuration.GetValue<string>("FileStorage"), fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            var mimeType = GetMimeType(Path.GetExtension(fileName));

            return File(memory, mimeType, fileName);
        }

        private string GetMimeType(string extension)
        {
            var types = new Dictionary<string, string>
            {
                { ".txt", "text/plain" },
                { ".pdf", "application/pdf" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" },
            };

            return types.GetValueOrDefault(extension.ToLower(), "application/octet-stream");
        }

        // protected IActionResult ResPreview(string fileName)
        // {
        //     var filePath = Path.Combine(_configuration.GetValue<string>("FileStorage"), fileName);

        //     if (!System.IO.File.Exists(filePath))
        //     {
        //         return NotFound();
        //     }
        //     var memory = new MemoryStream();
        //     using (var stream = new FileStream(filePath, FileMode.Open))
        //     {
        //         stream.CopyTo(memory);
        //     }
        //     memory.Position = 0;

        //     var mimeType = GetMimeType(Path.GetExtension(fileName));

        //     var cd = new System.Net.Mime.ContentDisposition { FileName = fileName, Inline = true, };
        //     Response.Headers.Add("Content-Disposition", cd.ToString());
        //     return File(memory, mimeType);
        // }
    }
}
