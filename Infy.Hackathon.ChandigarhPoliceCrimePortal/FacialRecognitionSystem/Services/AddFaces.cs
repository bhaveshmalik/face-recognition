using System;
using System.Collections.Generic;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace FacialRecognitionSystem.Services
{
    public class AddFaces
    {
        public static string FaceAddition(string collectionId)
        {

            var bucket = "bhavesh-aws-bucket";
            var photo = "download (1).jpg";

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.APSouth1);

            Image image = new Image()
            {
                S3Object = new S3Object()
                {
                    Bucket = bucket,
                    Name = photo
                }
            };

            IndexFacesRequest indexFacesRequest = new IndexFacesRequest()
            {
                Image = image,
                CollectionId = collectionId,
                ExternalImageId = photo,
                DetectionAttributes = new List<String>() {"ALL"}
            };

            var indexFacesResponse = rekognitionClient.IndexFacesAsync(indexFacesRequest);

            Console.WriteLine(photo + " added");
            foreach (FaceRecord faceRecord in indexFacesResponse.Result.FaceRecords)
            {
                Console.WriteLine("Face detected: Faceid is " +
                                  faceRecord.Face.FaceId);
                return faceRecord.Face.FaceId;
            }
            return null;
        }
    }

}