using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO.Resource
{
    public class EditErrorMessageDTO
    {

        [Required(ErrorMessage = "{0} is  Required")]
        //public string BlockUserMessage => GetString(nameof(BlockUserMessage));
        public string BlockUserMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string cardNameAlreadyExist { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string ConfirmPhoneMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string EditableProfileNotAllowed { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string ErrorMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string ForniddenMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string HasReserveRequest { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InCorrectPassword { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InternalServerMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvaliAmountForTransaction { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidAnswer { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidaServiceCategory { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidAttamp { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidDiscointCode { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidInput { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidPackageType { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidPhoneNumber { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InvalidQuestion { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string InValidServiceType { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string LockedOutMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string Noquestoin { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string NotEnoughtBalance { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string NotFound { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string NumberOfFreeMessagesCompleted { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string PhoneNumberAlreadyExist { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string problemUploadingTheFileMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string ProfileRejectedMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string ProviderIsUnAvailableMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string RequestNotConfirmedMessgaes { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string ServiceIsDisabled { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string ServiceNotFound { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string somethingWentWrongForChaing { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string SuccessMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string TextIsRequired { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string TheFileIsInValid { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string TheFileIsRequired { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string TheFileIsTooLarge { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string TicketIsClosedMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string UnauthorizedMessage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string UnMathPhoneNumberPassword { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string VoiceIsrequired { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string VoiceIsTooLarge { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouAreProviderOfThisService { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouCantRequestToACompay { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouCantRequestToYourSelf { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouCurrentlyHaveAnActivePackage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouDOntAnyWallet { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouDontHaveAnyPackage { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string YouHaveReservedService { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string YourPackageExpired { get; set; }
        [Required(ErrorMessage = "{0} is  Required")]
        public string YourPackageExpiredOrNoPlan { get; set; }


    }


    public class EditErrorMessageDTO2 : EditErrorMessageDTO
    {
        /// <summary>
        /// این نوع زبان را معلوم میکند
        /// </summary>
        [Required(ErrorMessage = "{0} is  Required")]
        public LanguageHeader LanguageHeader { get; set; }

    }


}
