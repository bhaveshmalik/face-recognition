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

                int limit = 10;

                ListCollectionsResponse listCollectionsResponse = null;
                String paginationToken = null;

                if (listCollectionsResponse != null)
                    paginationToken = listCollectionsResponse.NextToken;

                ListCollectionsRequest listCollectionsRequest = new ListCollectionsRequest()
                {
                    MaxResults = limit,
                    NextToken = paginationToken
                };

                listCollectionsResponse = rekognitionClient.ListCollectionsAsync(listCollectionsRequest).Result;

                foreach (var resultId in listCollectionsResponse.CollectionIds)
                {
                    if (resultId == collectionId)
                    {
                        return "OK";
                    }
                    else
                    {
                        var createCollectionResponse =
                rekognitionClient.CreateCollectionAsync(createCollectionRequest);
                        Console.WriteLine("CollectionArn : " + createCollectionResponse.Result.CollectionArn);
                        if (createCollectionResponse.Result.StatusCode == 200)
                        {
                            return "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred" + ex);
                return "";
            }


            return "";

        }
    }
}
