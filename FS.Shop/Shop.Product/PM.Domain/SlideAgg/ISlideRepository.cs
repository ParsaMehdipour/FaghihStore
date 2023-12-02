using _0_Framework.Domain;

using PM.Application.Contracts.Slide;

using System.Collections.Generic;

namespace PM.Domain.SlideAgg
{
    public interface ISlideRepository : IRepository<long, Slide>
    {
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
