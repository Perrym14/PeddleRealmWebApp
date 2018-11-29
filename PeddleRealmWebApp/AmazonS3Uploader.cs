using Amazon.S3;
using Amazon.S3.Model;
using System;

namespace PeddleRealmWebApp
{
    public class AmazonS3Uploader
    {
        private string _bucketName = "peddlerealmt";
        private string _keyName;
        private string _filePath;

        public AmazonS3Uploader(string fileName, string filePath)
        {
            _keyName = fileName;
            _filePath = filePath;
        }

        public void UploadFile()
        {
            var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    FilePath = _filePath,
                    Key = _keyName,

                };

                client.PutObject(putRequest);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                     ||
                     amazonS3Exception.ErrorCode.Equals("InvalidSecurity")
                    ))
                {
                    throw new Exception("Check the provided AWS Credentials");
                }
                else
                {
                    throw new Exception("Error occured: " + amazonS3Exception.Message);
                }
            }
        }
    }
}