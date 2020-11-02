using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MiCarpeta.Document.Common
{
    public class S3Client : IS3Client
    {
        private readonly IConfiguration Configuration;

        public S3Client(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static S3CannedACL[] Permissions { get; set; } = new S3CannedACL[] { S3CannedACL.Private, S3CannedACL.PublicRead, S3CannedACL.PublicReadWrite };

        public string UploadStringFile(Stream cuerpoArchivo, string filename, int permission)
        {
            using (AmazonS3Client client = new AmazonS3Client())
            {
                try
                {
                    StringBuilder url = new StringBuilder(Configuration["MiCarpeta:S3:Entrypoint"]);
                    var request = new PutObjectRequest
                    {
                        BucketName = Configuration["MiCarpeta:S3:BucketName"],
                        InputStream = cuerpoArchivo,
                        CannedACL = Permissions[permission],
                        Key = filename
                    };
                    request.Headers.ContentEncoding = "System.Text.Encoding.Unicode";
                    Task<PutObjectResponse> response = client.PutObjectAsync(request);
                    response.Wait();

                    url = url.Append(filename);

                    return url.ToString();
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
        }
    }
}
