// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

using System;
using Newtonsoft.Json;

namespace MvpApi.Models
{
    public partial class ContributionTypeModel
    {
        /// <summary>
        /// Initializes a new instance of the ContributionTypeModel class.
        /// </summary>
        public ContributionTypeModel() { }

        /// <summary>
        /// Initializes a new instance of the ContributionTypeModel class.
        /// </summary>
        public ContributionTypeModel(Guid? id = default(Guid?), string name = default(string), string englishName = default(string))
        {
            Id = id;
            Name = name;
            EnglishName = englishName;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets the localized name.
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the english name
        /// </summary>
        [JsonProperty(PropertyName = "EnglishName")]
        public string EnglishName { get; set; }

    }
}
