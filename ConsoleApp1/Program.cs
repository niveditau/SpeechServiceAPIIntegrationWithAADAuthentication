using Azure.Identity;
using Azure.Core;
using System.Net.Http.Headers;


namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Get the AAD token for cognitive service
            TokenRequestContext t = new TokenRequestContext (new[]  {"https://cognitiveservices.azure.com/.default"});
            var credential = new DefaultAzureCredential();
            var token = credential.GetToken(t);
            string textToken = token.Token;

            // Create an HTTP client
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            // Set the Bearer token in the header
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + textToken);

            /* For testing purposes we have picked projects API to get the projects in
             * for a speech instance.(https://westus.dev.cognitive.microsoft.com/docs/services/speech-to-text-api-v3-0/operations/CreateProject)
             * You can use POST /projects endpoint to create projects so that when we call GET /projects endpoint
             * we can see the project we created in the response. This is just for testing.
             * Below is the code for creating a project in speech service instance and then reading it back.
             */

            // CREATE PROJECT
            var uri = "https://<speech-custom-domain-name>.cognitiveservices.azure.com/speechtotext/v3.0/projects";
            string projectBody = "{\"locale\": \"en-US\", \"displayName\": \"YOUR PROJECT NAME\"}";
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(projectBody);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage httpResponse = await client.PostAsync(uri, content);
                Console.WriteLine(httpResponse.Content);
            }
        
            // GET ALL THE PROJECTS
            // Call the speech service instance directly to get the projects in that region
            var getProjectTask = client.GetStringAsync(uri);
            var msg = await getProjectTask;
            Console.Write("My Speech projects:" + msg);
            Console.Write("Finished!");
          }
    }
}