using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using WebApp.Core;
using WebApp.Data;
using WebApp.Filters;
using WebApp.Utilities;

namespace WebApp.Controllers
{
    public class FileController : Controller
    {
        private readonly IConfiguration config;
        private readonly IFyleData fileData;
        private readonly IPathData pathData;

        public FileController(IConfiguration config, IFyleData fileData, IPathData pathData)
        {
            this.config = config;
            this.fileData = fileData;
            this.pathData = pathData;
        }

        public IActionResult Download(int id)
        {
            Fyle file = fileData.GetFileById(id);
            string basePath = pathData.GetBasePath().Path;
            if (file == null || basePath == null || basePath == "")
                return StatusCode(404);
            string filePath = Path.Combine(basePath, file.Folder, file.Name);
            Stream oStream = null;
            try
            {
                oStream =
                    new FileStream
                    (path: filePath,
                    mode: FileMode.Open,
                    share: FileShare.Read,
                    access: FileAccess.Read);
                return new FileStreamResult(oStream, "application/octet-stream")
                { FileDownloadName = file.Name };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return StatusCode(403);
            }
        }

        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {
            Debug.WriteLine("~~~~~~~~~~~~~~Post Request Received~~~~~~~~~~~~~~~~~");
            string[] _permittedExtensions = { ".jpg", ".iso", ".txt", ".msi", ".zip" };
            Int64 _fileSizeLimit = Int64.MaxValue;
            string _targetFilePath = pathData.GetBasePath().Path;
            _targetFilePath = Path.Combine(_targetFilePath, "uploaded");
            
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                ModelState.AddModelError("File",
                    $"The request couldn't be processed (Error 1).");
                // Log error

                return BadRequest(ModelState);
            }

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                int.MaxValue);
            var reader = new Microsoft.AspNetCore.WebUtilities.MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();
            Debug.WriteLine("~~~~~~~~~~~~~~ 1 ~~~~~~~~~~~~~~~~~");
            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);
                Debug.WriteLine("~~~~~~~~~~~~~~ 2 ~~~~~~~~~~~~~~~~~");
                if (hasContentDispositionHeader)
                {

                    if (!MultipartRequestHelper
                        .HasFileContentDisposition(contentDisposition))
                    {
                        ModelState.AddModelError("File",
                            $"The request couldn't be processed (Error 2).");
                        // Log error

                        return BadRequest(ModelState);
                    }
                    else
                    {
                        Debug.WriteLine("~~~~~~~~~~~~~~ 3 ~~~~~~~~~~~~~~~~~");
                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                                contentDisposition.FileName.Value);
                        var trustedFileNameForFileStorage = trustedFileNameForDisplay;


                        var streamedFileContent = await FileHelpers.ProcessStreamedFile(
                            section, contentDisposition, ModelState,
                            _permittedExtensions, _fileSizeLimit);

                        if (!ModelState.IsValid)
                        {

                            return BadRequest(ModelState);
                        }
                        Debug.WriteLine("~~~~~~~~~~~~~~ 4 ~~~~~~~~~~~~~~~~~");
                        using (var targetStream = System.IO.File.Create(
                            Path.Combine(_targetFilePath, trustedFileNameForFileStorage)))
                        {
                            Debug.WriteLine("~~~~~~~~~~~~~~ 5 ~~~~~~~~~~~~~~~~~");
                            await targetStream.WriteAsync(streamedFileContent);
                            Debug.WriteLine("~~~~~~~~~~~~~~ 6 ~~~~~~~~~~~~~~~~~");
                            Debug.WriteLine(
                                $"Uploaded file {trustedFileNameForDisplay} saved to " +
                                $"'{_targetFilePath}' as {trustedFileNameForFileStorage}");
                            Fyle fyle = new Fyle() { Name = trustedFileNameForDisplay, Folder = "uploaded" };
                            fileData.AddFile(fyle);
                        }
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }
            fileData.Commit();
            return Created(nameof(FileController), null);
        }
    }
}
