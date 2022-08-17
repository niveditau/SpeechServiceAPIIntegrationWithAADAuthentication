# SpeechServiceAPIIntegrationWithAADAuthentication
This project demonstrates how to call Speech Service REST APIs with Azure Active Directory authentication

Prerequisites
1. A Speech Service resource in Azure with custom domain.
2. A Service Principal for Cognitive Services access permissions

For testing purposes we have picked projects REST API to get the projects in for a speech instance.(https://westus.dev.cognitive.microsoft.com/docs/services/speech-to-text-api-v3-0/operations/CreateProject)
First we create a project for a Speech resource and then we call GET for that Speech Resource so that we can see the project we created in the response. 
