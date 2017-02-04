// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

using System;
using Newtonsoft.Json;

namespace MvpApi.Models
{
    /// <summary>
    /// The activity technology model.
    /// </summary>
    public partial class ActivityTechnologyViewModel
    {
        /// <summary>
        /// Initializes a new instance of the ActivityTechnologyViewModel
        /// class.
        /// </summary>
        public ActivityTechnologyViewModel() { }

        /// <summary>
        /// Initializes a new instance of the ActivityTechnologyViewModel
        /// class.
        /// </summary>
        public ActivityTechnologyViewModel(Guid id, string name = default(string), string awardName = default(string), string awardCategory = default(string), int? statuscode = default(int?), bool? active = default(bool?))
        {
            Id = id;
            Name = name;
            AwardName = awardName;
            AwardCategory = awardCategory;
            Statuscode = statuscode;
            Active = active;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AwardName")]
        public string AwardName { get; set; }

        /// <summary>
        /// Property to hold the Award Category value
        /// </summary>
        [JsonProperty(PropertyName = "AwardCategory")]
        public string AwardCategory { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Statuscode")]
        public int? Statuscode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Active")]
        public bool? Active { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            //Nothing to validate
        }
    }
}
