using System;
using System.Collections.Generic;
using System.Text;

using UIKit;
using Foundation;
using StarWarsQuotes.Core.Model;
using StarWarsQuotes.Core.Service;
using System.Threading.Tasks;

namespace StarWarsQuote.iOS.Controller
{
    public class QuotesTableSource : UITableViewSource
    {
        private List<CharacterQuote> _quotes;
        private string _cellIdentifier;

        public QuotesTableSource(string cellIdentifier)
        {
            _cellIdentifier = cellIdentifier;
        }

        public async Task SetupData()
        {
            try
            {
                StarWarsService service = new StarWarsService();
                _quotes = await service.GetQuotes();
            }
            catch (Exception ex)
            {
                //set list element so that app doesn't seg fault because of null references
                _quotes = new List<CharacterQuote>();

                //add error modal dialog 
                UIAlertView alert = new UIAlertView()
                {
                    Message = $"Message: {ex.Message.ToString()} {Environment.NewLine} Stack Trace: {ex.StackTrace}",
                    Title = "Error:  Getting Information",
                };
                alert.AddButton("OK");
                alert.Show();
            }

        }

        public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
        {
            return 50.0f;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _quotes.Count;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(_cellIdentifier) as QuoteCell;
            var quote = _quotes[indexPath.Row];

            //set the cell selection style
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;

            //setup binding for cell
            cell.Bind(quote);

            return cell;
        }
    }
}
