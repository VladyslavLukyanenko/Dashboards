using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ProjectIndustries.Dashboards.App.Audit.Data;
using ProjectIndustries.Dashboards.App.Identity.Services;
using ProjectIndustries.Dashboards.Core.Audit;
using ProjectIndustries.Dashboards.Infra.Services;

namespace ProjectIndustries.Dashboards.Infra.Audit.DataConverters
{
  public class ChangeSetDataConverter : DataConverterBase<ChangeSet, ChangeSetData>
  {
    private readonly IUserRefProvider _userRefProvider;
    private readonly IMapper _mapper;

    public ChangeSetDataConverter(IUserRefProvider userRefProvider, IMapper mapper)
    {
      _userRefProvider = userRefProvider;
      _mapper = mapper;
    }

    public override async Task<IList<ChangeSetData>> ConvertAsync(IEnumerable<ChangeSet> src,
      CancellationToken ct = default)
    {
      var list = _mapper.Map<List<ChangeSetData>>(src);

      await _userRefProvider.InitializeAsync(list.Select(_ => _.UpdatedBy), ct);

      return list;
    }
  }
}