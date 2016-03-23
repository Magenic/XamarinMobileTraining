using System;
using System.Collections.Generic;
using System.Text;

using UIKit;
using Foundation;

using StarWarsQuotes.Core.Service;
using StarWarsQuotes.Core.Model;

namespace StarWarsQuote.iOS.Controller
{
    public class QuotesViewController : UITableViewController
    {
       
        private static readonly string _cellIdentifier = "quoteCell";
        private QuotesTableSource _source;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if(_source != null)
                {
                    _source.Dispose();
                    _source = null;
                }
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupTableView();
        }

        private async void SetupTableView()
        {
            TableView.RegisterClassForCellReuse(typeof(QuoteCell), _cellIdentifier);

            //set the row height to dynamically calculate
            TableView.RowHeight = UITableView.AutomaticDimension;

            //set the UITableView so that it's below the status bar
            this.EdgesForExtendedLayout = UIRectEdge.None;
            this.ExtendedLayoutIncludesOpaqueBars = false;
            this.AutomaticallyAdjustsScrollViewInsets = false;

            TableView.AllowsSelection = false;
           
            //setup data source
            _source = new QuotesTableSource(_cellIdentifier);
            await _source.SetupData();
            TableView.Source = _source;
            TableView.ReloadData();
        }
    }

}
