using ServiceStack;
using System;
using System.IO;
using UnitTestDemo.ServiceModel;

namespace UnitTestDemo.ServiceInterface;

public class FileUploadService : Service
{
    public object Get(FileUpload request)
    {
        if (request.RelativePath.IsNullOrEmpty())
            throw new ArgumentNullException("RelativePath");

        var filePath = ("~/" + request.RelativePath).MapProjectPlatformPath();
        if (!File.Exists(filePath))
            throw new FileNotFoundException(request.RelativePath);

        var result = new HttpResult(new FileInfo(filePath));
        return result;
    }

    public object Post(FileUpload request)
    {
        if (this.Request.Files.Length == 0)
            throw new FileNotFoundException("UploadError", "No such file exists");

        if (request.RelativePath == "ThrowError")
            throw new NotSupportedException("ThrowError");

        var file = this.Request.Files[0];
        return new FileUploadResponse
        {
            Name = file.Name,
            FileName = file.FileName,
            ContentLength = file.ContentLength,
            ContentType = file.ContentType,
            Contents = file.InputStream.ReadToEnd(),
            CustomerId = request.CustomerId,
            CustomerName = request.CustomerName,
            CreatedDate = request.CreatedDate
        };
    }

    public object Put(FileUpload request)
    {
        return new FileUploadResponse
        {
            CustomerId = request.CustomerId,
            CustomerName = request.CustomerName,
            CreatedDate = request.CreatedDate
        };
    }

    public object Post(MultipleFileUpload request)
    {
        return new MultipleFileUploadResponse
        {
            Results = this.Request.Files.Map(file => new FileUploadResponse
            {
                Name = file.Name,
                FileName = file.FileName,
                ContentLength = file.ContentLength,
                ContentType = file.ContentType,
                Contents = file.InputStream.ReadToEnd(),
                CustomerId = request.CustomerId,
                CustomerName = request.CustomerName,
                CreatedDate = request.CreatedDate
            })
        };
    }

}