// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

using Newtonsoft.Json;

namespace MvpApi.Models
{
    public partial class SocialNetworkStatusCode
    {
        /// <summary>
        /// Initializes a new instance of the SocialNetworkStatusCode class.
        /// </summary>
        public SocialNetworkStatusCode() { }

        /// <summary>
        /// Initializes a new instance of the SocialNetworkStatusCode class.
        /// </summary>
        public SocialNetworkStatusCode(int? id = default(int?), string description = default(string))
        {
            Id = id;
            Description = description;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public int? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

    }
}
