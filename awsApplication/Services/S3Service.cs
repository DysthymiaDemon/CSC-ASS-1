using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using awsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace awsApplication.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;

        public S3Service(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<S3Response> CreateBucketAsync(string bucketName)
        {
            try
            {
                if(await AmazonS3Util.DoesS3BucketExistAsync(_client, bucketName) == false)
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };

                    var response = await _client.PutBucketAsync(putBucketRequest);

                    return new S3Response
                    {
                        Message = response.ResponseMetadata.RequestId,
                        Status = response.HttpStatusCode
                    };
                }
            }
            catch (AmazonS3Exception e)
            {
                return new S3Response
                {
                    Status = e.StatusCode,
                    Message = e.Message
                };
            }
            catch(Exception ex)
            {
                return new S3Response
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }

            return new S3Response
            {
                Status = HttpStatusCode.InternalServerError,
                Message = "Something else went wrong"
            };
        }
    }
}
