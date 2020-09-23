using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum ServiceType
    {
        [Description("Chat/Voice")]
        [Display(Name = "Video Call")]
        ChatVoice,

        [Description("Video Call")]
        [Display(Name = "Video Call")]
        VideoCal,

        [Description("Voice Call")]
        [Display(Name = "Voice Call")]
        VoiceCall,

        [Description("Service")]
        [Display(Name = "Service")]
        Service,

        [Description("Course")]
        [Display(Name = "Course")]
        Course,
    }
}
