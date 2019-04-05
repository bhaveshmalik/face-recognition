using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.IO;
using System.Text;

namespace FacialRecognitionSystem.Services
{
    public static class SearchFacesMatchingImage
    {
        public static string RecogniseFaceMatchFromS3(string collectionId, string bucket, string photo)
        {
            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.APSouth1);

            // Get an image object from S3 bucket.
            
                Image image = new Image()
                {
                    S3Object = new S3Object()
                    {
                        Bucket = bucket,
                        Name = photo
                    }

                };

            SearchFacesByImageRequest searchFacesByImageRequest = new SearchFacesByImageRequest()
            {
                CollectionId = collectionId,
                Image = image,
                FaceMatchThreshold = 70F,
                MaxFaces = 2
            };

            var searchFacesByImageResponse = rekognitionClient.SearchFacesByImageAsync(searchFacesByImageRequest);

            Console.WriteLine("Faces matching largest face in image from " + photo);
            foreach (FaceMatch face in searchFacesByImageResponse.Result.FaceMatches)
            {
                Console.WriteLine("FaceId: " + face.Face.FaceId + ", Similarity: " + face.Similarity);
                return face.Face.Confidence.ToString();
            }
            return "";
        }

        public static string RecogniseFaceMatchFromFileSystem(string collectionId, byte[] photo)
        {
            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.APSouth1);

            // Get an image object from File system.

            Image image = new Image()
            {
                Bytes = new MemoryStream(photo)
            };
            

            //using (FileStream fs = new FileStream(photo, FileMode.Open, FileAccess.Read))
            //{
            //    byte[] data = null;
            //    data = new byte[fs.Length];
            //    fs.Read(data, 0, (int)fs.Length);
            //    image.Bytes = new MemoryStream(data);
            //}

           

            SearchFacesByImageRequest searchFacesByImageRequest = new SearchFacesByImageRequest()
            {
                CollectionId = collectionId,
                Image = image,
                FaceMatchThreshold = 70F,
                MaxFaces = 2
            };

            var searchFacesByImageResponse = rekognitionClient.SearchFacesByImageAsync(searchFacesByImageRequest);

            Console.WriteLine("Faces matching largest face in image from " + photo);
            foreach (FaceMatch face in searchFacesByImageResponse.Result.FaceMatches)
            {
                Console.WriteLine("FaceId: " + face.Face.FaceId + ", Similarity: " + face.Similarity);
                return face.Face.FaceId;
            }
            return "";
        }
    }
}