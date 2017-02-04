// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

using Newtonsoft.Json;

namespace MvpApi.Models
{
    /// <summary>
    /// The visibility.
    /// </summary>
    public partial class VisibilityViewModel
    {
        /// <summary>
        /// Initializes a new instance of the VisibilityViewModel class.
        /// </summary>
        public VisibilityViewModel() { }

        /// <summary>
        /// Initializes a new instance of the VisibilityViewModel class.
        /// </summary>
        public VisibilityViewModel(int? id = default(int?), string description = default(string), string localizeKey = default(string))
        {
            Id = id;
            Description = description;
            LocalizeKey = localizeKey;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "LocalizeKey")]
        public string LocalizeKey { get; set; }

    }
}
