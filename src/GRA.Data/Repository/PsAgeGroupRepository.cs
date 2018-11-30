﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using GRA.Domain.Model;
using GRA.Domain.Model.Filters;
using GRA.Domain.Repository;
using GRA.Domain.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GRA.Data.Repository
{
    public class PsAgeGroupRepository 
        : AuditingRepository<Model.PsAgeGroup, Domain.Model.PsAgeGroup>, IPsAgeGroupRepository
    {
        public PsAgeGroupRepository(ServiceFacade.Repository repositoryFacade,
            ILogger<PsAgeGroupRepository> logger) : base(repositoryFacade, logger)
        {
        }

        public async Task<ICollection<PsAgeGroup>> GetAllAsync()
        {
            return await DbSet
                .AsNoTracking()
                .ProjectTo<PsAgeGroup>()
                .ToListAsync();
        }

        public async Task<DataWithCount<ICollection<PsAgeGroup>>> PageAsync(
            BaseFilter filter)
        {
            var ageGroups = DbSet.AsNoTracking();

            var count = await ageGroups.CountAsync();

            var ageGroupList = await ageGroups
                .ApplyPagination(filter)
                .ProjectTo<PsAgeGroup>()
                .ToListAsync();

            return new DataWithCount<ICollection<PsAgeGroup>>
            {
                Data = ageGroupList,
                Count = count
            };
        }

        public async Task<bool> BranchHasBackToBackAsync(int ageGroupId, int branchId)
        {
            return await _context.PsBackToBack
                .AsNoTracking()
                .Where(_ => _.PsAgeGroupId == ageGroupId && _.BranchId == branchId)
                .AnyAsync();
        }
    }
}
