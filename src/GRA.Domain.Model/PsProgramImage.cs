﻿using System.ComponentModel.DataAnnotations;

namespace GRA.Domain.Model
{
    public class PsProgramImage : Abstract.BaseDomainEntity
    {
        public int ProgramId { get; set; }
        public Program Program { get; set; }

        [MaxLength(255)]
        public string Filename { get; set; }
    }
}
