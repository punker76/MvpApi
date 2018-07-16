using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MvpApi.Common.ServiceModels;
using Newtonsoft.Json;

namespace MvpApi.Common.Services
{
    public class MvpApiService : IDisposable
    {
        private readonly HttpClient client;

        /// <summary>
        /// Service that interacts with the MVP API
        /// </summary>
        /// <param name="apiKey">the Ocp-Apim-Subscription-Key you got from your MVP API portal</param>
        /// <param name="authorizationHeader">Authorization header. Example: "Bearer AccessTokenGoesHere"</param>
        public MvpApiService(string apiKey, string authorizationHeader)
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
                handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);
            client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
        }

        /// <summary>
        /// Returns the profile data of the currently signed in MVP
        /// </summary>
        /// <returns>The MVP's profile information</returns>
        public async Task<ProfileViewModel> GetProfileAsync()
        {
            try
            {
                using (var response = await client.GetAsync("https://mvpapi.azure-api.net/mvp/api/profile"))
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ProfileViewModel>(json);
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"GetProfileAsync HttpRequestException: {e}");
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GetProfileAsync Exception: {e}");
                return null;
            }
        }

        /// <summary>
        /// Get the profile picture of the currently signed in MVP
        /// </summary>
        /// <returns>JPG image byte array</returns>
        public async Task<byte[]> GetProfileImageAsync()
        {
            // the result is Detected mime type: image/jpeg; charset=binary
            using (var response = await client.GetAsync("https://mvpapi.azure-api.net/mvp/api/profile/photo"))
            {
                var base64String = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(base64String))
                {
                    return null;
                }

                base64String = base64String.TrimStart('"').TrimEnd('"');

                var imgBytes = Convert.FromBase64String(base64String);

                Debug.WriteLine($"Image Decoded: {imgBytes?.Length} bytes");

                return imgBytes;
            }
        }

        /// <summary>
        /// Downloads and saves the image file to LocalFolder
        /// </summary>
        /// <param name="folder">StorageFolder location</param>
        /// <param name="fileNameWithoutExtension">filename for image, default is</param>
        /// <returns>File path</returns>
        public async Task<string> DownloadAndSaveProfileImage(string fileNameWithoutExtension = "ProfilePicture")
        {
            // the result is Detected mime type: image/jpeg; charset=binary
            using (var response = await client.GetAsync("https://mvpapi.azure-api.net/mvp/api/profile/photo"))
            {
                var base64String = await response.Content.ReadAsStringAsync();

                try
                {
                    if (string.IsNullOrEmpty(base64String))
                    {
                        return null;
                    }

                    base64String = base64String.TrimStart('"').TrimEnd('"');

                    // determine file type
                    var data = base64String.Substring(0, 5);

                    var fileExtension = string.Empty;

                    switch (data.ToUpper())
                    {
                        case "IVBOR":
                            fileExtension = "png";
                            break;
                        case "/9J/4":
                            fileExtension = "jpg";
                            break;
                    }

                    var imgBytes = Convert.FromBase64String(base64String);

                    Debug.WriteLine($"Image Decoded: {imgBytes?.Length} bytes");

                    var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                    // Use Combine so that the correct file path slashes are used
                    var filePath = Path.Combine(localFolder, $"{fileNameWithoutExtension}.{fileExtension}");

                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    File.WriteAllBytes(filePath, imgBytes);

                    return filePath;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the MVPs activities, depending on the offset (page) and the limit (number of items per-page)
        /// </summary>
        /// <param name="offset">page to return</param>
        /// <param name="limit">number of items for the page</param>
        /// <returns></returns>
        public async Task<ContributionViewModel> GetContributionsAsync(int? offset, int limit)
        {
            if (offset == null)
                offset = 0;

            try
            {
                using (var response = await client.GetAsync($"https://mvpapi.azure-api.net/mvp/api/contributions/{offset}/{limit}"))
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ContributionViewModel>(json);
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"GetContributionsAsync HttpRequestException: {e}");
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GetContributionsAsync Exception: {e}");
                return null;
            }
        }

        /// <summary>
        /// Submits a new contribution to the currently sign-in MVP profile
        /// </summary>
        /// <param name="contribution">The contribution to be submitted</param>
        /// <returns>Contribution submitted. This object should now have a valid ID and be added to the app's Contributions collection</returns>
        public async Task<ContributionsModel> SubmitContributionAsync(ContributionsModel contribution)
        {
            if (contribution == null)
                throw new NullReferenceException("The contribution parameter was null.");

            try
            {
                var serializedContribution = JsonConvert.SerializeObject(contribution);
                byte[] byteData = Encoding.UTF8.GetBytes(serializedContribution);
                
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    
                    using (var response = await client.PostAsync("https://mvpapi.azure-api.net/mvp/api/contributions?", content))
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        Debug.WriteLine($"Submission Save JSON: {json}");

                        var result = JsonConvert.DeserializeObject<ContributionsModel>(json);

                        Debug.WriteLine($"Submission Save Result: ID {result.ContributionId}");

                        return result;
                    }
                }
            }
            catch (HttpRequestException e)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Updates an existing contribution, identified by the contribution ID
        /// </summary>
        /// <param name="contribution">Contribution to be updated</param>
        /// <returns>Bool to denote update success or failure</returns>
        public async Task<bool?> UpdateContributionAsync(ContributionsModel contribution)
        {
            if(contribution == null)
                throw new NullReferenceException("The contribution parameter was null.");

            try
            {
                // Request body
                var serializedContribution = JsonConvert.SerializeObject(contribution);
                byte[] byteData = Encoding.UTF8.GetBytes(serializedContribution);

                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    using (var response = await client.PutAsync("https://mvpapi.azure-api.net/mvp/api/contributions", content))
                    {
                        return response.IsSuccessStatusCode;
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"UpdateContributionAsync HttpRequestException: {e}");
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GetProfileAsync Exception: {e}");
                return null;
            }
        }

        /// <summary>
        /// Deleted s contribution
        /// </summary>
        /// <param name="contribution">Item to delete</param>
        /// <returns>Success or failure</returns>
        public async Task<bool?> DeleteContributionAsync(ContributionsModel contribution)
        {
            if (contribution == null)
                throw new NullReferenceException("The contribution parameter was null.");

            try
            {
                using (var response = await client.DeleteAsync($"https://mvpapi.azure-api.net/mvp/api/contributions?id={contribution.ContributionId}"))
                {
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"UpdateContributionAsync HttpRequestException: {e}");
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GetProfileAsync Exception: {e}");
                return null;
            }
        }

        /// <summary>
        /// This gets a list if the different contributions types
        /// </summary>
        /// <returns>List of contributions types</returns>
        public async Task<IReadOnlyList<ContributionTypeModel>> GetContributionTypesAsync()
        {
            try
            {
                using (var response = await client.GetAsync("https://mvpapi.azure-api.net/mvp/api/contributions/contributiontypes"))
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IReadOnlyList<ContributionTypeModel>>(json);
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"GetContributionTypesAsync HttpRequestException: {e}");
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GetContributionTypesAsync Exception: {e}");
                return null;
            }
        }

        /// <summary>
        /// Gets a list of the Contibution Technologies (aka Contribution Areas)
        /// </summary>
        /// <returns>A list of available contribution areas</returns>
        public async Task<IReadOnlyList<ContributionAreasRootItem>> GetContributionAreasAsync()
        {
            try
            {
                using (var response = await client.GetAsync("https://mvpapi.azure-api.net/mvp/api/contributions/contributionareas"))
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IReadOnlyList<ContributionAreasRootItem>>(json);
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"GetContributionTechnologiesAsync HttpRequestException: {e}");
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GetContributionTechnologiesAsync Exception: {e}");
                return null;
            }
        }

        /// <summary>
        /// Gets a list of contribution visibility options (aka Sharing Preferences). The traditional results are "Microsoft Only", "MVP Community" and "Everyone"
        /// </summary>
        /// <returns>A list of available visibilities</returns>
        public async Task<IReadOnlyList<VisibilityViewModel>> GetVisibilitiesAsync()
        {
            try
            {
                using (var response = await client.GetAsync("https://mvpapi.azure-api.net/mvp/api/contributions/sharingpreferences"))
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IReadOnlyList<VisibilityViewModel>>(json);
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"GetVisibilitiesAsync HttpRequestException: {e}");
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GetVisibilitiesAsync Exception: {e}");
                return null;
            }
        }
        
        public void Dispose()
        {
            client?.Dispose();
        }
    }
}