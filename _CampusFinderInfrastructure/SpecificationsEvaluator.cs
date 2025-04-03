﻿using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderInfrastructure
{
    public static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery; //_dbContext.Set<Product>()

            //Make Filteration then Sorting because in Sqlserver (Where) executed the first then (Orderby)  then Top

            if (spec.Criteria is not null) // P => P.Id == 1
                query = query.Where(spec.Criteria); //Dynamic

            if (spec.OrderBy is not null) //P =>P.Name
                query = query.OrderBy(spec.OrderBy);

            //query = _dbContext.Set<Product>().OrderBy(P =>P.Name)

            else if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);




            //query = _dbContext.Set<Product>().OrderBy(P =>P.Name)
            //Includes
            //1. P => P.Brand
            //2. P => P.Category

            //to add (Include) with (Where) must make Aggregate(accumulate/دمج)

            //This line Build query with  Dynamic Way
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            //First iterate:
            // _dbContext.Set<Product>().OrderBy(P =>P.Name).Include(P => P.Brand)
            //Second iterate:            
            // _dbContext.Set<Product>().OrderBy(P =>P.Name).Include(P => P.Brand).Include( P => P.Category)




            return query;
        }


    }
}
