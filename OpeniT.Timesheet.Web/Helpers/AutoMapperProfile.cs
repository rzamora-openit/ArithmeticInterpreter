namespace OpeniT.Timesheet.Web.Helpers
{
	using AutoMapper;

	using Models;

	using ViewModels;

	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			this.CreateMap<ProcessViewModel, Process>().ReverseMap();
			this.CreateMap<SubProcessViewModel, SubProcess>().ReverseMap();
			this.CreateMap<TaskGroupViewModel, TaskGroup>().ReverseMap();
			this.CreateMap<RecordViewModel, Record>().ReverseMap();
			this.CreateMap<CarryoverVLViewModel, User>().ReverseMap();
			this.CreateMap<DashboardViewModel, User>().ReverseMap();
			this.CreateMap<PeopleViewModel, User>().ReverseMap();
			this.CreateMap<StatisticsViewModel, User>().ReverseMap();
			this.CreateMap<StatisticsDetailViewModel, User>().ReverseMap();			
			this.CreateMap<TimesheetViewModel, User>().ReverseMap();			
			this.CreateMap<UserViewModel, User>().ReverseMap();
			this.CreateMap<OfficesViewModel, UserLocation>().ReverseMap();
			this.CreateMap<UserLocationViewModel, UserLocation>().ReverseMap();
			this.CreateMap<RecordSummary, RecordSummaryViewModel>().ReverseMap();			
			this.CreateMap<Request, RequestViewModel>().ReverseMap();			
			this.CreateMap<UserContract, UserContractViewModel>().ReverseMap();			
			this.CreateMap<SiteValuesViewModel, SiteValues>().ReverseMap();
			this.CreateMap<RoleViewModel, Role>().ReverseMap();
			this.CreateMap<UserRoleViewModel, User>().ReverseMap();
			this.CreateMap<UserExcessHoursViewModel, UserExcessHours>().ReverseMap();

		}
	}
}