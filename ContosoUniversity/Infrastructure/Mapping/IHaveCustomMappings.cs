using AutoMapper;

namespace ContosoUniversity.Infrastructure.Mapping
{
    /// <summary>
    /// Interface to implement complex mapping rules for AutoMapper
    /// </summary>
    /// <remarks>
    /// Taken from 'Fail-Tracker' by Matt Honeycutt:
    /// https://github.com/MattHoneycutt/Fail-Tracker
    /// </remarks>
	public interface IHaveCustomMappings
	{
		void CreateMappings(IConfiguration cfg);
	}
}