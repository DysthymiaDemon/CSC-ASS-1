using awsApplication.Models;
using System.Threading.Tasks;

namespace awsApplication.Services
{
    public interface IS3Service
    {
        Task<S3Response> CreateBucketAsync(string bucketName);
    }
}
