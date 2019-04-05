using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;

namespace FacialRecognitionSystem.Services
{
    public static class SearchFacesMatchingImage
    {


        public const string bucket = "bhavesh-aws-bucket";
        public const string photo = "recognition/BM.jpg";


        public static string RecogniseFaceMatch(string collectionId)
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
                return face.Face.FaceId;
            }
            return "";
        }
    }
}