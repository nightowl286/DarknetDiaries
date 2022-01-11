using DarknetDiaries.Standard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DarknetDiaries.Core
{
   public class EpisodeFeed : IEpisodeFeed
   {
      #region Private
      private HttpClient _Client = new HttpClient();
      private const string URL = "https://feeds.megaphone.fm/darknetdiaries";
      private XmlDocument? _Doc;
      #endregion

      #region Methods
      public async Task RefreshData()
      {
         Stream rssStream = await _Client.GetStreamAsync(URL);
         _Doc = new XmlDocument();
         _Doc.Load(rssStream);
      }
      public IEnumerable<IEpisode> GetEpisodes()
      {
         XmlNode channel = GetChannel();
         XmlNodeList items = channel.SelectNodes("./item")!;

         for (int i = 0; i < items.Count; i++)
         {
            XmlNode item = items[i];

            Debug.Assert(item != null);
            string? numberStr = GetNode(item, "episode")?.InnerText;

            if (string.IsNullOrEmpty(numberStr))
               continue;

            int number = int.Parse(numberStr);
            string title = GetNodes(item, "title")![^1]!.InnerText;

            int seconds = int.Parse(GetNode(item, "duration")!.InnerText);
            TimeSpan duration = TimeSpan.FromSeconds(seconds);

            string? image = GetNode(item, "image")?.Attributes["href"]?.Value;
            string audio = GetNode(item, "enclosure")!.Attributes["url"]!.Value;

            Episode episode = new Episode(number, title, new Uri(audio))
            {
               Duration = duration,
               Image = string.IsNullOrEmpty(image) ? null : new Uri(image),
            };

            yield return episode;

         }
      }
      public ISeriesInfo GetInfo()
      {
         XmlNode channel = GetChannel();
         string desc = channel.SelectSingleNode("./description")!.InnerText;
         XmlNode image = channel.SelectSingleNode("./image")!;

         string url = image.SelectSingleNode("./url")!.InnerText;
         string title = image.SelectSingleNode("./title")!.InnerText;
         string link = image.SelectSingleNode("./link")!.InnerText;

         SeriesInfo info = new SeriesInfo(title,
            new Uri(link),
            new Uri(url),
            desc);

         return info;
      }
      #endregion

      #region Helpers
      private XmlNode? GetNode(XmlNode node, string name) => node.SelectSingleNode($"./*[local-name()='{name}']");
      private XmlNodeList? GetNodes(XmlNode node, string name) => node.SelectNodes($"./*[local-name()='{name}']");
      private XmlNode GetChannel() => _Doc!.DocumentElement!.SelectSingleNode("./channel")!;
      #endregion
   }
}
