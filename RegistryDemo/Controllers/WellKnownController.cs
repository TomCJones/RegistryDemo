// WellKnownController copyright 2021 tomjones
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;

namespace RegistryDemo.Controllers
{
    /// <summary>
    /// This is the code to enable let's encrypto to find the challange
    /// </summary>
    [ApiController]
    [Route(".well-known/acme-challenge/{balance?}")]
    public class WellKnownController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get(string? balance)
        {
            //default response
            string foo = balance;
            string path = "/app/wwwroot/TextDocument.txt";
            if (!string.IsNullOrWhiteSpace(balance))
            {
                path = "/app/wwwroot/" + balance;
            }

            string strOut;
            try
            {

                string content = await System.IO.File.ReadAllTextAsync(path, Encoding.UTF8);
                /*
                if (content.StartsWith("<DOCTYPE"))
                {
                    strOut = String.Format(content, part);
                }
                else
                */
                {
                    strOut = "<!DOCTYPE html><body><pre>" + content + "</pre></body>";
                }
                return content;
            }
            catch (Exception ex)
            {
                //        return "The page you are seeking could not be found, or could not be output. " + ex.Message;
                return "orKtkEuMVTa8bQoHpeN8n5MYWAusyDFBD1Rz4Y_HQ7s.R5Pdn5cwDU36dCaEUseg8k2-ykmZJIcv2F7ivAV_xgs";
            }
        }
        [HttpPost]
        public async Task<ActionResult<string>> Post()
        {
//            _logger.LogInformation("Start processing CSP request: " + Request.Method + " - " + Request.Protocol + " - " + Request.Path);
            try
            {
                string respBody = await new StreamReader(Request.Body).ReadToEndAsync();
                string contentType = Request.ContentType;
                string jsonFull = "{'input':'" + respBody + "'}";
                string path = "/app/wwwroot/TextDocument.txt";
                System.IO.File.WriteAllText(path, respBody);

                return CreatedAtAction("Post", jsonFull);
            }
            catch (Exception ex)
            {
                return CreatedAtAction("POST", "Error reading input " + ex.Message);
            }
        }
    }
}
