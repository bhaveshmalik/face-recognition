using System;
using System.Collections.Generic;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace FacialRecognitionSystem.Services
{
    public class AddFaces
    {
        public static void FaceAddition(string collectionId,string bucket, string faceToBeAdded)
        {


            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.APSouth1);

            Image image = new Image()
            {
                S3Object = new S3Object()
                {
                    Bucket = bucket,
                    Name = faceToBeAdded
                }
            };

            IndexFacesRequest indexFacesRequest = new IndexFacesRequest()
            {
                Image = image,
                CollectionId = collectionId,
                ExternalImageId = faceToBeAdded,
                DetectionAttributes = new List<String>() {"ALL"}
            };

            var indexFacesResponse = rekognitionClient.IndexFacesAsync(indexFacesRequest);

            Console.WriteLine(faceToBeAdded + " added");
            foreach (FaceRecord faceRecord in indexFacesResponse.Result.FaceRecords)
            {
                Console.WriteLine("Face detected: Faceid is " +
                                  faceRecord.Face.FaceId);
               
            }
        }
    }

}