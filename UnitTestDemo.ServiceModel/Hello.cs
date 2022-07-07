using ServiceStack;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnitTestDemo.ServiceModel;

[Route("/hello")]
[Route("/hello/{Name}")]
public class Hello : IReturn<HelloResponse>
{
    public string Name { get; set; }
}

public class HelloResponse
{
    public string Result { get; set; }
}

[Route("/secured")]
public class Secured : IReturn<SecuredResponse>
{
    public string Name { get; set; }
}

public class SecuredResponse
{
    public string Result { get; set; }

    public ResponseStatus ResponseStatus { get; set; }
}

public class RequiresCustomAuth
{
    public string Name { get; set; }
}

public class RequiresCustomAuthResponse
{
    public string Result { get; set; }

    public ResponseStatus ResponseStatus { get; set; }
}

[Route("/securedfileupload")]
public class SecuredFileUpload
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
}

[DataContract]
[Route("/secure")]
public class Secure
{
    [DataMember]
    public string UserName { get; set; }
}

[DataContract]
public class SecureResponse : IHasResponseStatus
{
    [DataMember]
    public string Result { get; set; }

    [DataMember]
    public ResponseStatus ResponseStatus { get; set; }
}







public class RequiresRole
{
    public string Name { get; set; }
}

public class RequiresRoleResponse
{
    public string Result { get; set; }

    public ResponseStatus ResponseStatus { get; set; }
}

public class RequiresAnyRole
{
    public List<string> Roles { get; set; }

    public RequiresAnyRole()
    {
        Roles = new List<string>();
    }
}

public class RequiresAnyRoleResponse
{
    public List<string> Result { get; set; }

    public ResponseStatus RepsonseStatus { get; set; }

    public RequiresAnyRoleResponse()
    {
        Result = new List<string>();
    }
}

public class RequiresPermission
{
    public string Name { get; set; }
}

public class RequiresPermissionResponse
{
    public string Result { get; set; }

    public ResponseStatus ResponseStatus { get; set; }
}

public class RequiresAuthAction : IReturn<RequiresAuthAction> { }
public class RequiresRoleAction : IReturn<RequiresRoleAction> { }
public class RequiresAuthRoleAction : IReturn<RequiresAuthRoleAction> { }

[DataContract]
[Route("/fileuploads/{RelativePath*}")]
[Route("/fileuploads", HttpMethods.Post)]
public class FileUpload : IReturn<FileUploadResponse>
{
    [DataMember]
    public string RelativePath { get; set; }

    [DataMember]
    public string CustomerName { get; set; }

    [DataMember]
    public int? CustomerId { get; set; }

    [DataMember]
    public DateTime? CreatedDate { get; set; }
}

[DataContract]
public class FileUploadResponse : IHasResponseStatus
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string FileName { get; set; }

    [DataMember]
    public long ContentLength { get; set; }

    [DataMember]
    public string ContentType { get; set; }

    [DataMember]
    public string Contents { get; set; }

    [DataMember]
    public ResponseStatus ResponseStatus { get; set; }

    [DataMember]
    public string CustomerName { get; set; }

    [DataMember]
    public int? CustomerId { get; set; }

    [DataMember]
    public DateTime? CreatedDate { get; set; }
}

[Route("/multi-fileuploads", HttpMethods.Post)]
public class MultipleFileUpload : IReturn<MultipleFileUploadResponse>
{
    public string RelativePath { get; set; }
    public string CustomerName { get; set; }
    public int? CustomerId { get; set; }
    public DateTime? CreatedDate { get; set; }
}

public class MultipleFileUploadResponse : IHasResponseStatus
{
    public List<FileUploadResponse> Results { get; set; }
    public ResponseStatus ResponseStatus { get; set; }
}