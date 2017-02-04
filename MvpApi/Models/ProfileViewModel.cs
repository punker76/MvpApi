// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace MvpApi.Models
{
    public partial class ProfileViewModel
    {
        /// <summary>
        /// Initializes a new instance of the ProfileViewModel class.
        /// </summary>
        public ProfileViewModel() { }

        /// <summary>
        /// Initializes a new instance of the ProfileViewModel class.
        /// </summary>
        public ProfileViewModel(ContentMetadata metadata = default(ContentMetadata), string mvpId = default(string), int? yearsAsMvp = default(int?), string firstAwardYear = default(string), string awardCategoryDisplay = default(string), string technicalExpertise = default(string), bool? inTheSpotlight = default(bool?), string headline = default(string), string biography = default(string), string displayName = default(string), string fullName = default(string), string primaryEmailAddress = default(string), string shippingCountry = default(string), string shippingStateCity = default(string), string languages = default(string), IList<OnlineIdentityViewModel> onlineIdentities = default(IList<OnlineIdentityViewModel>), IList<CertificationViewModel> certifications = default(IList<CertificationViewModel>), IList<ActivityViewModel> activities = default(IList<ActivityViewModel>), IList<AwardRecognitionViewModel> communityAwards = default(IList<AwardRecognitionViewModel>), IList<MvpHighlightViewModel> newsHighlights = default(IList<MvpHighlightViewModel>), IList<MvpHighlightViewModel> upcomingEvent = default(IList<MvpHighlightViewModel>))
        {
            Metadata = metadata;
            MvpId = mvpId;
            YearsAsMvp = yearsAsMvp;
            FirstAwardYear = firstAwardYear;
            AwardCategoryDisplay = awardCategoryDisplay;
            TechnicalExpertise = technicalExpertise;
            InTheSpotlight = inTheSpotlight;
            Headline = headline;
            Biography = biography;
            DisplayName = displayName;
            FullName = fullName;
            PrimaryEmailAddress = primaryEmailAddress;
            ShippingCountry = shippingCountry;
            ShippingStateCity = shippingStateCity;
            Languages = languages;
            OnlineIdentities = onlineIdentities;
            Certifications = certifications;
            Activities = activities;
            CommunityAwards = communityAwards;
            NewsHighlights = newsHighlights;
            UpcomingEvent = upcomingEvent;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Metadata")]
        public ContentMetadata Metadata { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "MvpId")]
        public string MvpId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "YearsAsMvp")]
        public int? YearsAsMvp { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "FirstAwardYear")]
        public string FirstAwardYear { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AwardCategoryDisplay")]
        public string AwardCategoryDisplay { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "TechnicalExpertise")]
        public string TechnicalExpertise { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "InTheSpotlight")]
        public bool? InTheSpotlight { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Headline")]
        public string Headline { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Biography")]
        public string Biography { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "DisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "FullName")]
        public string FullName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PrimaryEmailAddress")]
        public string PrimaryEmailAddress { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ShippingCountry")]
        public string ShippingCountry { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ShippingStateCity")]
        public string ShippingStateCity { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Languages")]
        public string Languages { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "OnlineIdentities")]
        public IList<OnlineIdentityViewModel> OnlineIdentities { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Certifications")]
        public IList<CertificationViewModel> Certifications { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Activities")]
        public IList<ActivityViewModel> Activities { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CommunityAwards")]
        public IList<AwardRecognitionViewModel> CommunityAwards { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "NewsHighlights")]
        public IList<MvpHighlightViewModel> NewsHighlights { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "UpcomingEvent")]
        public IList<MvpHighlightViewModel> UpcomingEvent { get; set; }

    }
}
