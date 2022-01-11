using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DarknetDiaries.Standard
{
   public interface IEpisodeFeed
   {
      #region Methods
      Task RefreshData();
      ISeriesInfo GetInfo();
      IEnumerable<IEpisode> GetEpisodes();
      #endregion
   }
}
