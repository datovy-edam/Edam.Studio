﻿using System;
using Edam.Helpers;

namespace Edam.UI.DataModel.Models
{
   public class BaseDataObject : BaseViewModel
   {
      public BaseDataObject()
      {
         Id = Guid.NewGuid().ToString();
      }

      /// <summary>
      /// Id for item
      /// </summary>
      public string Id { get; set; }

      /// <summary>
      /// Azure created at time stamp
      /// </summary>
      public DateTimeOffset CreatedAt { get; set; }

      /// <summary>
      /// Azure UpdateAt timestamp for online/offline sync
      /// </summary>
      public DateTimeOffset UpdatedAt { get; set; }

      /// <summary>
      /// Azure version for online/offline sync
      /// </summary>
      public string AzureVersion { get; set; }
   }
}
