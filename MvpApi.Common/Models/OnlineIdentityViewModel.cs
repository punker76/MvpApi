// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

using System;
using Newtonsoft.Json;

namespace MvpApi.Common.Models
{
    public partial class OnlineIdentityViewModel
    {
        /// <summary>
        /// Initializes a new instance of the OnlineIdentityViewModel class.
        /// </summary>
        public OnlineIdentityViewModel() { }

        /// <summary>
        /// Initializes a new instance of the OnlineIdentityViewModel class.
        /// </summary>
        public OnlineIdentityViewModel(SocialNetworkViewModel socialNetwork, string url, int? privateSiteId = default(int?), VisibilityViewModel onlineIdentityVisibility = default(VisibilityViewModel), bool? contributionCollected = default(bool?), string displayName = default(string), string userId = default(string), string microsoftAccount = default(string), bool? privacyConsentStatus = default(bool?), bool? privacyConsentCheckStatus = default(bool?), DateTime? privacyConsentCheckDate = default(DateTime?), DateTime? privacyConsentUnCheckDate = default(DateTime?), bool? submitted = default(bool?))
        {
            PrivateSiteId = privateSiteId;
            SocialNetwork = socialNetwork;
            Url = url;
            OnlineIdentityVisibility = onlineIdentityVisibility;
            ContributionCollected = contributionCollected;
            DisplayName = displayName;
            UserId = userId;
            MicrosoftAccount = microsoftAccount;
            PrivacyConsentStatus = privacyConsentStatus;
            PrivacyConsentCheckStatus = privacyConsentCheckStatus;
            PrivacyConsentCheckDate = privacyConsentCheckDate;
            PrivacyConsentUnCheckDate = privacyConsentUnCheckDate;
            Submitted = submitted;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PrivateSiteId")]
        public int? PrivateSiteId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "SocialNetwork")]
        public SocialNetworkViewModel SocialNetwork { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Url")]
        public string Url { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "OnlineIdentityVisibility")]
        public VisibilityViewModel OnlineIdentityVisibility { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ContributionCollected")]
        public bool? ContributionCollected { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "DisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "UserId")]
        public string UserId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "MicrosoftAccount")]
        public string MicrosoftAccount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PrivacyConsentStatus")]
        public bool? PrivacyConsentStatus { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PrivacyConsentCheckStatus")]
        public bool? PrivacyConsentCheckStatus { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PrivacyConsentCheckDate")]
        public DateTime? PrivacyConsentCheckDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PrivacyConsentUnCheckDate")]
        public DateTime? PrivacyConsentUnCheckDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Submitted")]
        public bool? Submitted { get; set; }
    }
}