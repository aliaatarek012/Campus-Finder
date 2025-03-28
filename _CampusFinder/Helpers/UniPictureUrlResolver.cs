using _CampusFinderCore.Entities.UniversityEntities;
using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using CampusFinder.Dto.University_Dtos;
using System.Linq.Expressions;
using System.Reflection;

namespace CampusFinder.Helpers
{
	public class UniPictureUrlResolver<TDestination> : IValueResolver<University, TDestination, string>
	{
		private readonly IConfiguration _configuration;

		public UniPictureUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}


		public string Resolve(University source, TDestination destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureURL))
			{
				return $"{_configuration["ApiBaseUrl"]}/{source.PictureURL}";
			}
			return string.Empty;
		}
	}
}
