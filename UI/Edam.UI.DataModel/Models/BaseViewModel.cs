﻿using Edam.Helpers;
using Edam.Services;

// -----------------------------------------------------------------------------
//using Xamarin.Forms;
using Edam.Application;

namespace Edam.UI.DataModel.Models
{
   public class BaseViewModel : ObservableObject
   {
      /// <summary>
      /// Get the azure service instance
      /// </summary>
      public IDataStore<Item> DataStore => 
         DependencyService.Get<IDataStore<Item>>();

      bool isBusy = false;
      public bool IsBusy
      {
         get { return isBusy; }
         set { SetProperty(ref isBusy, value); }
      }
      /// <summary>
      /// Private backing field to hold the title
      /// </summary>
      string title = string.Empty;
      /// <summary>
      /// Public property to set and get the title of the item
      /// </summary>
      public string Title
      {
         get { return title; }
         set { SetProperty(ref title, value); }
      }
   }
}

