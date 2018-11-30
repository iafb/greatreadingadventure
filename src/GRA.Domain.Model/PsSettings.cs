﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GRA.Domain.Model
{
    public class PsSettings : Abstract.BaseDomainEntity
    {
        public int SiteId { get; set; }

        public string ContactEmail { get; set; }

        [Range(1, int.MaxValue)]
        public int? SelectionsPerBranch { get; set; }

        [DisplayName("Registration Open")]
        public DateTime? RegistrationOpen { get; set; }
        [DisplayName("Registration Closed")]
        public DateTime? RegistrationClosed { get; set; }
        [DisplayName("Scheduling Preview")]
        public DateTime? SchedulingPreview { get; set; }
        [DisplayName("Scheduling Open")]
        public DateTime? SchedulingOpen { get; set; }
        [DisplayName("Scheduling Closed")]
        public DateTime? SchedulingClosed { get; set; }
        [DisplayName("Schedule Posted")]
        public DateTime? SchedulePosted { get; set; }

        [DisplayName("Schedule Start Date")]
        public DateTime? ScheduleStartDate { get; set; }
        [DisplayName("Schedule End Date")]
        public DateTime? ScheduleEndDate { get; set; }
    }
}
