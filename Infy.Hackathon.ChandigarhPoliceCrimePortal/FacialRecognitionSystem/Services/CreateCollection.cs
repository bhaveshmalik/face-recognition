using System;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;


namespace FacialRecognitionSystem.Services
{
    public static class CreateCollection
    {
        public static string Collection(string collectionId)
        {
            try
            {
                var rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.APSouth1);


                Console.WriteLine("Creating collection: " + collectionId);

                var createCollectionRequest = new CreateCollectionRequest()
                {
                    CollectionId = collectionId
                };

                var createCollectionResponse =
                    rekognitionClient.CreateCollectionAsync(createCollectionRequest);
                Console.WriteLine("CollectionArn : " + createCollectionResponse.Result.CollectionArn);
                if (createCollectionResponse.Result.StatusCode == 200)
                {
                    return "OK";
                }
            }
            catch (Exception ex)
            {

                return "";
            }
          

            return "";
            
        }
    }
}
