using Domain.Enums;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Resource
{

    /// <summary>
    /// این مدل برای÷رکردن فایل  ShareResource
    /// که فقط برای دیتا انوتیشن ها وارور مسیج هااست
    /// </summary>
    public class DataAnotationAndErrorMessageDTO
    {

        private readonly IStringLocalizer<ShareResource> _localizer;

        public DataAnotationAndErrorMessageDTO(IStringLocalizer<ShareResource> localizer)
        {
            _localizer = localizer;
        }

        private string GetString(string name) => _localizer[name].Value.ToString();

        /// <summary>
        /// این نوع زبان را معلوم میکند
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        public LanguageHeader LanguageHeader { get; set; }






        [Required(ErrorMessage = "{0} is  Required")]
        public string BlockUserMessage => GetString(nameof(BlockUserMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string cardNameAlreadyExist => GetString(nameof(cardNameAlreadyExist));
        [Required(ErrorMessage = "{0} is  Required")]
        public string ConfirmPhoneMessage => GetString(nameof(ConfirmPhoneMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string EditableProfileNotAllowed => GetString(nameof(EditableProfileNotAllowed));
        [Required(ErrorMessage = "{0} is  Required")]
        public string ErrorMessage => GetString(nameof(ErrorMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string ForniddenMessage => GetString(nameof(ForniddenMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string HasReserveRequest => GetString(nameof(HasReserveRequest));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InCorrectPassword => GetString(nameof(InCorrectPassword));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InternalServerMessage => GetString(nameof(InternalServerMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvaliAmountForTransaction => GetString(nameof(InvaliAmountForTransaction));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidAnswer => GetString(nameof(InvalidAnswer));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidaServiceCategory => GetString(nameof(InvalidaServiceCategory));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidAttamp => GetString(nameof(InvalidAttamp));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidDiscointCode => GetString(nameof(InvalidDiscointCode));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidInput => GetString(nameof(InvalidInput));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidPackageType => GetString(nameof(InvalidPackageType));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidPhoneNumber => GetString(nameof(InvalidPhoneNumber));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidQuestion => GetString(nameof(InvalidQuestion));
        [Required(ErrorMessage = "{0} is  Required")]
        public string InValidServiceType => GetString(nameof(InValidServiceType));
        [Required(ErrorMessage = "{0} is  Required")]
        public string LockedOutMessage => GetString(nameof(LockedOutMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string Noquestoin => GetString(nameof(Noquestoin));
        [Required(ErrorMessage = "{0} is  Required")]
        public string NotEnoughtBalance => GetString(nameof(NotEnoughtBalance));
        [Required(ErrorMessage = "{0} is  Required")]
        public string NotFound => GetString(nameof(NotFound));
        [Required(ErrorMessage = "{0} is  Required")]
        public string NumberOfFreeMessagesCompleted => GetString(nameof(NumberOfFreeMessagesCompleted));
        [Required(ErrorMessage = "{0} is  Required")]
        public string PhoneNumberAlreadyExist => GetString(nameof(PhoneNumberAlreadyExist));
        [Required(ErrorMessage = "{0} is  Required")]
        public string problemUploadingTheFileMessage => GetString(nameof(problemUploadingTheFileMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string ProfileRejectedMessage => GetString(nameof(ProfileRejectedMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string ProviderIsUnAvailableMessage => GetString(nameof(ProviderIsUnAvailableMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string RequestNotConfirmedMessgaes => GetString(nameof(RequestNotConfirmedMessgaes));
        [Required(ErrorMessage = "{0} is  Required")]
        public string ServiceIsDisabled => GetString(nameof(ServiceIsDisabled));
        [Required(ErrorMessage = "{0} is  Required")]
        public string ServiceNotFound => GetString(nameof(ServiceNotFound));
        [Required(ErrorMessage = "{0} is  Required")]
        public string somethingWentWrongForChaing => GetString(nameof(somethingWentWrongForChaing));
        [Required(ErrorMessage = "{0} is  Required")]
        public string SuccessMessage => GetString(nameof(SuccessMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string TextIsRequired => GetString(nameof(TextIsRequired));
        [Required(ErrorMessage = "{0} is  Required")]
        public string TheFileIsInValid => GetString(nameof(TheFileIsInValid));
        [Required(ErrorMessage = "{0} is  Required")]
        public string TheFileIsRequired => GetString(nameof(TheFileIsRequired));
        [Required(ErrorMessage = "{0} is  Required")]
        public string TheFileIsTooLarge => GetString(nameof(TheFileIsTooLarge));
        [Required(ErrorMessage = "{0} is  Required")]
        public string TicketIsClosedMessage => GetString(nameof(TicketIsClosedMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string UnauthorizedMessage => GetString(nameof(UnauthorizedMessage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string UnMathPhoneNumberPassword => GetString(nameof(UnMathPhoneNumberPassword));
        [Required(ErrorMessage = "{0} is  Required")]
        public string VoiceIsrequired => GetString(nameof(VoiceIsrequired));
        [Required(ErrorMessage = "{0} is  Required")]
        public string VoiceIsTooLarge => GetString(nameof(VoiceIsTooLarge));
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouAreProviderOfThisService => GetString(nameof(YouAreProviderOfThisService));
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouCantRequestToACompay => GetString(nameof(YouCantRequestToACompay));
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouCantRequestToYourSelf => GetString(nameof(YouCantRequestToYourSelf));
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouCurrentlyHaveAnActivePackage => GetString(nameof(YouCurrentlyHaveAnActivePackage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouDOntAnyWallet => GetString(nameof(YouDOntAnyWallet));
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouDontHaveAnyPackage => GetString(nameof(YouDontHaveAnyPackage));
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouHaveReservedService => GetString(nameof(YouHaveReservedService));
        [Required(ErrorMessage = "{0} is  Required")]
        public string YourPackageExpired => GetString(nameof(YourPackageExpired));
        [Required(ErrorMessage = "{0} is  Required")]
        public string YourPackageExpiredOrNoPlan => GetString(nameof(YourPackageExpiredOrNoPlan));

    }

}
