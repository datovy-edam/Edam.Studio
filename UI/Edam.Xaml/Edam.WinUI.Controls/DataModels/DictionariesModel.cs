using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using Edam.Data.Dictionary.Api;
using Edam.Data.Dictionary;
using Edam.Data.Books;
using Edam.Data.Lexicon.Semantics;
using Edam.Data.Lexicon;
using Edam.WinUI.Controls.Common;
using DocumentFormat.OpenXml.InkML;
using Edam.Data.AssetSchema;
using System.Collections.ObjectModel;
using Jsonata.Net.Native.Json;

namespace Edam.WinUI.Controls.DataModels
{

   public class DictionariesModel : ObservableObject
   {

      public const string DICTIONARIES_UPDATE = "DictionariesUpdate";

      public DataMapContext DataMapContext { get; set; }

      public ObservableCollection<DictionaryEntryInfo> Entries
         { get; set; } = new ObservableCollection<DictionaryEntryInfo>();

      public NotificationEvent ManageNotification { get; set; }

      /// <summary>
      /// Set the Context based on given data-map context that may be of a
      /// different project or map-item.
      /// </summary>
      /// <param name="context">Mapping context to use</param>
      public void SetContext(DataMapContext context)
      {
         if (context == null)
            return;

         DataMapContext = context as DataMapContext;
      }

      /// <summary>
      /// Define elements looking up those in the dictionaries.
      /// </summary>
      /// <param name="booklet"></param>
      public void Define(BookletInfo booklet)
      {
         List<DictionaryEntryInfo> items = new List<DictionaryEntryInfo>();
         foreach (var sitem in DataMapContext.SourceItems)
         {
            foreach (var titem in DataMapContext.TargetItems)
            {
               string sourceText = sitem.GetAnnotation().Description;
               string targetText = titem.GetAnnotation().Description;

               DictionaryHelper.GetSentenceTerms(sourceText, items);
               DictionaryHelper.GetSentenceTerms(targetText, items);

               foreach (var item in items)
               {
                  Entries.Add(item);
               }
            }
         }

         // notify about found similarities...
         if (ManageNotification != null)
         {
            NotificationArgs args = new NotificationArgs();
            args.Type = NotificationType.ExecuteItem;
            args.EventData = Entries;
            args.MessageText = DICTIONARIES_UPDATE;
            ManageNotification(this, args);
         }
      }

   }

}
