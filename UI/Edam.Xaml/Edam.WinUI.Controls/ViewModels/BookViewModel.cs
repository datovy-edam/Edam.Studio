﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// -----------------------------------------------------------------------------
using Edam.Data.AssetConsole;
using Edam.Data.AssetSchema;
using Edam.Data.AssetProject;
using Edam.InOut;
using Edam.WinUI.Controls.ViewModels;
using Edam.Helpers;
using Edam.WinUI.Controls.DataModels;
using System.Collections.ObjectModel;
using Edam.Json.JsonDataTree;
using Edam.WinUI.Controls.Common;
using Edam.WinUI.Controls.Booklets;
using Edam.Data.Books;
using Microsoft.UI.Xaml.Controls;

namespace Edam.WinUI.Controls.ViewModels
{

   public class BookViewModel : ObservableObject
   {

      public BookModel Model { get; set; }
      public NotificationEvent ManageEvent { get; set; }
      public string JsonInstanceSample { get; set; } = String.Empty;

      private DataMapContext m_Context;
      public DataMapContext Context
      {
         get { return m_Context ?? Model.Context; }
      }

      public void FindBooklet(string bookletId)
      {
         var blet = Model.FindBooklet(bookletId);
      }

      /// <summary>
      /// Set context.
      /// </summary>
      /// <param name="context"></param>
      public void SetContext(DataMapContext context)
      {
         m_Context = context;
         if (Model == null)
         {
            Model = new BookModel(context);
         }
      }

      /// <summary>
      /// Add cell of given type.
      /// </summary>
      /// <param name="type">Text or Code cell type</param>
      public void AddCell(BookletCellType type)
      {
         if (Model == null || Model.Book != Context.UseCase.Book)
         {
            Model = new BookModel(Context);
            Context.BookModel = this;
         }

         Model.AddControl(this, type, Context.SelectedItem.ItemId);
      }

      /// <summary>
      /// If the cell has already been defined call this method instead...
      /// </summary>
      /// <param name="cell">Booklet Cell</param>
      public void AddCell(BookletCellInfo cell)
      {
         Model.AddControl(this, cell.CellType, cell.ReferenceId, cell);
      }

      /// <summary>
      /// Add a Text cell.
      /// </summary>
      public void AddTextCell()
      {
         AddCell(BookletCellType.Text);
      }

      /// <summary>
      /// Add a Code cell.
      /// </summary>
      public void AddCodeCell()
      {
         AddCell(BookletCellType.Code);
      }

      /// <summary>
      /// Delete given Cell...
      /// </summary>
      /// <param name="item">cell item to delete</param>
      public void DeleteCell(object item = null)
      {

      }

      /// <summary>
      /// Move the Cell Down.
      /// </summary>
      /// <param name="cell">cell to be moved down</param>
      public void MoveCellDown(BookletCellInfo cell)
      {
         Context.MoveCellDown(cell);
      }

      /// <summary>
      /// Manage notification and processing...
      /// </summary>
      /// <param name="type">notification type</param>
      /// <param name="messageText">message details (if any)</param>
      /// <param name="data">cell to be managed</param>
      public void NotifyEvent(
         NotificationType type, string messageText, object data = null)
      {
         if (type == NotificationType.ExecuteItem)
         {
            BookletCellInfo cell = (BookletCellInfo)data;
            ProcessCell(cell);
         }

         if (ManageEvent != null)
         {
            NotificationArgs args = new NotificationArgs();
            args.Type = type;
            args.EventData = data ?? Model;
            args.MessageText = messageText;
            ManageEvent(this, args);
         }
      }

      /// <summary>
      /// Process Item using given Cell information...
      /// </summary>
      /// <param name="cell">Cell to process</param>
      public void ProcessCell(BookletCellInfo cell)
      {
         Context.Execute(cell);
      }

   }

}