using SecureBoxAPINEW.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SecureBoxAPINEW.Controllers
{
    public class UploadFileController : ApiController
    {
        //
        // Post: /UploadFile/

        public HttpResponseMessage UploadFile()
        {

            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {

                var docfiles = new List<string>();

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + Path.GetFileName(postedFile.FileName));

                    #region Encrypt File

                    string inFile = postedFile.FileName ;
                    string outFile = filePath + ".fcfe";
                    string password = ""; //txtEncPass.Text;
                    
                    
                    Stream fileStream = postedFile.InputStream ;
                    EncryptDecrypt.EncryptFile(fileStream, inFile, outFile, password);
                    #endregion  

                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);

            }

            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;

        }
    }
}
