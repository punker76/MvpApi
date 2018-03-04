﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MvpApi.Common.Models;
using MvpApi.Uwp.Extensions;
using MvpApi.Uwp.Helpers;
using MvpApi.Uwp.Views;
using Newtonsoft.Json;

namespace MvpApi.Uwp.ViewModels
{
    public class ContributionDetailViewModel : PageViewModelBase
    {
        #region Fields

        private ContributionsModel originalContribution;
        private ContributionsModel selectedContribution;
        private bool isSelectedContributionDirty;
        private bool isContributionTypeEditable = true;
        private string urlHeader = "Url";
        private string annualQuantityHeader = "Annual Quantity";
        private string secondAnnualQuantityHeader = "Second Annual Quantity";
        private string annualReachHeader = "Annual Reach";
        private bool isUrlRequired;
        private bool isAnnualQuantityRequired;
        private bool isSecondAnnualQuantityRequired;
        private bool canSave;
        private bool canUpload = true;
        private bool useFastMode = true;
        private ObservableCollection<ContributionAreaContributionModel> categoryAreas;

        #endregion

        public ContributionDetailViewModel()
        {
            if (DesignMode.DesignModeEnabled || DesignMode.DesignMode2Enabled)
            {
                if (DesignMode.DesignModeEnabled || DesignMode.DesignMode2Enabled)
                {
                    Types = DesignTimeHelpers.GenerateContributionTypes();
                    //CategoryAreas = DesignTimeHelpers.GenerateTechnologyAreas(); //Causing designer layout error
                    Visibilies = DesignTimeHelpers.GenerateVisibilities();

                    SelectedContribution = DesignTimeHelpers.GenerateContributions().FirstOrDefault();
                }
            }
                
        }
        
        #region Properties
        
        public ContributionsModel SelectedContribution
        {
            get => selectedContribution;
            set => Set(ref selectedContribution, value);
        }

        public ObservableCollection<ContributionTypeModel> Types { get; set; } = new ObservableCollection<ContributionTypeModel>();
        
        public ObservableCollection<ContributionAreaContributionModel> CategoryAreas
        {
            get => categoryAreas;
            set => Set(ref categoryAreas, value);
        }
        
        public ObservableCollection<VisibilityViewModel> Visibilies { get; set; } = new ObservableCollection<VisibilityViewModel>();
        
        public bool IsSelectedContributionDirty
        {
            get => isSelectedContributionDirty;
            set => Set(ref isSelectedContributionDirty, value);
        }

        public bool IsContributionTypeEditable
        {
            get => isContributionTypeEditable;
            set => Set(ref isContributionTypeEditable, value);
        }

        public string AnnualQuantityHeader
        {
            get => annualQuantityHeader;
            set => Set(ref annualQuantityHeader, value);
        }

        public string SecondAnnualQuantityHeader
        {
            get => secondAnnualQuantityHeader;
            set => Set(ref secondAnnualQuantityHeader, value);
        }

        public string AnnualReachHeader
        {
            get => annualReachHeader;
            set => Set(ref annualReachHeader, value);
        }

        public string UrlHeader
        {
            get => urlHeader;
            set => Set(ref urlHeader, value);
        }

        public bool IsUrlRequired
        {
            get => isUrlRequired;
            set => Set(ref isUrlRequired, value);
        }

        public bool IsAnnualQuantityRequired
        {
            get => isAnnualQuantityRequired;
            set => Set(ref isAnnualQuantityRequired, value);
        }

        public bool IsSecondAnnualQuantityRequired
        {
            get => isSecondAnnualQuantityRequired;
            set => Set(ref isSecondAnnualQuantityRequired, value);
        }

        public bool CanSave
        {
            get => canSave;
            set => Set(ref canSave, value);
        }

        public bool CanUpload
        {
            get => canUpload;
            set => Set(ref canUpload, value);
        }

        public bool UseFastMode
        {
            get => useFastMode;
            set => Set(ref useFastMode, value);
        }

        #endregion

        #region Event handlers

        public async void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CanSave = await SelectedContribution.Validate();
        }

        public async void UrlBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CanSave = await SelectedContribution.Validate();
        }

        public async void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CanSave = await SelectedContribution.Validate();
        }

        public async void DatePicker_OnDateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            if (e.NewDate < new DateTime(2016, 10, 1) || e.NewDate > new DateTime(2018, 3, 31))
            {
                await new MessageDialog("The contribution date must be after the start of your current award period and before March 31, 2018 in order for it to count towards your evaluation", "Notice: Out of range").ShowAsync();
            }

            CanSave = await SelectedContribution.Validate();
        }

        public async void AnnualQuantityBox_OnValueChanged(object sender, EventArgs e)
        {
            CanSave = await SelectedContribution.Validate();
        }

        public async void SecondAnnualQuantityBox_OnValueChanged(object sender, EventArgs e)
        {
            CanSave = await SelectedContribution.Validate();
        }

        public async void ActivityType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateHeaders(SelectedContribution.ContributionType);
            
            // Also set the type name
            SelectedContribution.ContributionTypeName = SelectedContribution.ContributionType.EnglishName;
            
            CanSave = await SelectedContribution.Validate();
        }
        
        public async void UploadContributionButton_Click(object sender, RoutedEventArgs e)
        {
            var isValid = await SelectedContribution.Validate(true);

            if(!isValid)
                return;
            
            SelectedContribution.UploadStatus = UploadStatus.InProgress;

            var success = await UploadContributionAsync(SelectedContribution);

            // Mark success or failure
            SelectedContribution.UploadStatus = success ? UploadStatus.Success : UploadStatus.Failed;

            if (SelectedContribution.UploadStatus == UploadStatus.Success)
            {

            }
        }

        public async void DeleteContributionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var md = new MessageDialog("Are you sure you want to delete this contribution? Deleting a contribution from the MVP database cannot be undone.", "Delete Contribution?");
                md.Commands.Add(new UICommand("DELETE"));
                md.Commands.Add(new UICommand("cancel"));

                var dialogResult = await md.ShowAsync();

                if (dialogResult.Label != "DELETE")
                    return;

                var result = await App.ApiService.DeleteContributionAsync(SelectedContribution);

                if (result == true)
                {
                    await new MessageDialog("Successfully deleted.").ShowAsync();

                    if (NavigationService.CanGoBack)
                        NavigationService.GoBack();
                }
                else
                {
                    await new MessageDialog("The contribution was not deleted, check your internet connection and try again.").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog($"Something went wrong deleting this item, please try again. Error: {ex.Message}").ShowAsync();
            }
        }
        
        #endregion

        #region Methods
        
        public async Task<bool> UploadContributionAsync(ContributionsModel contribution)
        {
            try
            {
                var submissionResult = await App.ApiService.SubmitContributionAsync(contribution);

                // copying back the ID which was created on the server once the item was added to the database
                contribution.ContributionId = submissionResult.ContributionId;
                
                return true;
            }
            catch (Exception ex)
            {
                await new MessageDialog($"Something went wrong saving the item, please try again. Error: {ex.Message}").ShowAsync();
                return false;
            }
        }
        
        private void UpdateHeaders(ContributionTypeModel contributionType)
        {
            switch (contributionType.EnglishName)
            {
                case "Article":
                    AnnualQuantityHeader = "Number of Articles";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Number of Views";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Blog Site Posts":
                    AnnualQuantityHeader = "Number of Posts";
                    SecondAnnualQuantityHeader = "Number of Subscribers";
                    AnnualReachHeader = "Annual Unique Visitors";
                    IsUrlRequired = true;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Book (Author)":
                    AnnualQuantityHeader = "Number of Books";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Copies Sold";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Book (Co-Author)":
                    AnnualQuantityHeader = "Number of Books";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Copies Sold";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Code Project/Tools":
                    AnnualQuantityHeader = "Number of Projects";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Number of Downloads";
                    IsUrlRequired = true;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Code Samples":
                    AnnualQuantityHeader = "Number of Samples";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Number of Downloads";
                    IsUrlRequired = true;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Conference (booth presenter)":
                    AnnualQuantityHeader = "Number of Conferences";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Number of Visitors";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Conference (organizer)":
                    AnnualQuantityHeader = "Number of Conferences";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Number of Visitors";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Forum Moderator":
                    AnnualQuantityHeader = "Number of Threads Moderated";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Annual Reach";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Forum Participation (3rd Party Forums)":
                    AnnualQuantityHeader = "Number of Answers";
                    SecondAnnualQuantityHeader = "Number of Posts";
                    AnnualReachHeader = "Views of Answers";
                    IsUrlRequired = true;
                    IsAnnualQuantityRequired = false;
                    IsSecondAnnualQuantityRequired = true;
                    break;
                case "Forum Participation (Microsoft Forums)":
                    AnnualQuantityHeader = "Number of Answers";
                    SecondAnnualQuantityHeader = "Number of Posts";
                    AnnualReachHeader = "Views of Answers";
                    IsUrlRequired = true;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Mentorship":
                    AnnualQuantityHeader = "Number of Mentees";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Annual Reach";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Open Source Project(s)":
                    AnnualQuantityHeader = "Project(s)";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Commits";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Other":
                    AnnualQuantityHeader = "Annual Quantity";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Annual Reach";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Product Group Feedback":
                    AnnualQuantityHeader = "Number of Events provided";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Number of Feedbacks provided";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Site Owner":
                    AnnualQuantityHeader = "Posts";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Visitors";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Speaking (Conference)":
                    AnnualQuantityHeader = "Talks";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Attendees of talks";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Speaking (Local)":
                    AnnualQuantityHeader = "Talks";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Attendees of talks";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Speaking (User group)":
                    AnnualQuantityHeader = "Talks";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Attendees of talks";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Technical Social Media (Twitter, Facebook, LinkedIn...)":
                    AnnualQuantityHeader = "Number of Posts";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Number of Followers";
                    IsUrlRequired = true;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Translation Review, Feedback and Editing":
                    AnnualQuantityHeader = "Annual Quantity";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Annual Reach";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "User Group Owner":
                    AnnualQuantityHeader = "Meetings";
                    SecondAnnualQuantityHeader = "Members";
                    AnnualReachHeader = "";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Video":
                    AnnualQuantityHeader = "Number of Videos";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Number of Views";
                    IsUrlRequired = true;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Webcast":
                    AnnualQuantityHeader = "Number of Videos";
                    SecondAnnualQuantityHeader = "Number of Views";
                    AnnualReachHeader = "";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                case "Website Posts":
                    AnnualQuantityHeader = "Number of Posts";
                    SecondAnnualQuantityHeader = "Number of Subscribers";
                    AnnualReachHeader = "Annual Unique Visitors";
                    IsUrlRequired = true;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
                default: // Fall back on 'other'
                    AnnualQuantityHeader = "Annual Quantity";
                    SecondAnnualQuantityHeader = "";
                    AnnualReachHeader = "Annual Reach";
                    IsUrlRequired = false;
                    IsAnnualQuantityRequired = true;
                    IsSecondAnnualQuantityRequired = false;
                    break;
            }
        }
        
        #endregion

        private async Task LoadSupportingDataAsync()
        {
            IsBusyMessage = "getting types...";

            foreach (var type in await App.ApiService.GetContributionTypesAsync())
            {
                Types.Add(type);
            }

            IsBusyMessage = "getting technologies...";

            var areaRoots = await App.ApiService.GetContributionAreasAsync();

            // Flatten out the result so that we only have a single level of grouped data, this is used for the CollectionViewSource, defined in the XAML.
            CategoryAreas = new ObservableCollection<ContributionAreaContributionModel>(areaRoots.SelectMany(areaRoot => areaRoot.Contributions));

            IsBusyMessage = "getting visibility options...";

            foreach (var visibility in await App.ApiService.GetVisibilitiesAsync())
            {
                Visibilies.Add(visibility);
            }
        }
        

        #region Navigation
        
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (App.ShellPage.DataContext is ShellPageViewModel shellVm && shellVm.IsLoggedIn)
            {
                try
                {
                    IsBusy = true;

                    // ** Get the associated lists from the API **

                    await LoadSupportingDataAsync();
                    
                    // Read the passed contribution parameter //

                    if (parameter is ContributionsModel param)
                    {
                        SelectedContribution = param;
                        
                        // Deep cloning the object to serve as a clean original to compare against when editing and determine if the item is dirty or not.
                        var json = JsonConvert.SerializeObject(param);
                        originalContribution = JsonConvert.DeserializeObject<ContributionsModel>(json);
                    }
                    else
                    {
                        await new MessageDialog("Something went wrong loading your selection, going back to Home page").ShowAsync();

                        if(NavigationService.CanGoBack)
                            NavigationService.GoBack();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"LoadDataAsync Exception {ex}");
                }
                finally
                {
                    IsBusyMessage = "";
                    IsBusy = false;
                }
            }
            else
            {
                await NavigationService.NavigateAsync(typeof(LoginPage));
            }
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> pageState, bool suspending)
        {
            return base.OnNavigatedFromAsync(pageState, suspending);
        }

        #endregion
    }
}
