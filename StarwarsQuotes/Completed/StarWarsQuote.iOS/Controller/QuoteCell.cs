using System;
using System.Collections.Generic;
using System.Text;

using UIKit;
using Foundation;

using Cirrious.FluentLayouts.Touch;
using StarWarsQuotes.Core.Model;

namespace StarWarsQuote.iOS.Controller
{
    public class QuoteCell : UITableViewCell
    {
        #region private vars

        private UILabel _character;
        private UILabel _movie;
        private UILabel _quote;

        private bool _isControlsCreated;

        private const int _topBottomMargin = 15;
        private const int _leftRightMargin = 15;

        #endregion

        #region constructor and destructor

        public QuoteCell(IntPtr handle):base(handle)
        {
            _isControlsCreated = false;

            SetupUi();
            UpdateConstraints(); 
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (_character != null)
                {
                    _character.Dispose();
                    _character = null;
                }
                if (_movie != null)
                {
                    _movie.Dispose();
                    _movie = null;
                }

                if (_quote != null)
                {
                    _quote.Dispose();
                    _quote = null;
                }
            }
        }

        #endregion

        #region public methods

        public override void UpdateConstraints()
        {
            if (NeedsUpdateConstraints() && _isControlsCreated)
            {
                SetupConstraints();
            }
            base.UpdateConstraints();
        }

        public void Bind(CharacterQuote quote)
        {
            _character.Text = quote.Character;
            _movie.Text = quote.Film;
            _quote.Text = $"\"{quote.Quote}\"";

            //tell the UI to update
            this.SetNeedsLayout();      
        }

        #endregion

        #region private helper methods

        private void SetupConstraints()
        {
            ContentView.AddConstraints(
                //character control
                _quote.AtTopOf(ContentView, _topBottomMargin),
                _quote.AtLeftOf(ContentView, _leftRightMargin),
                _quote.AtRightOf(ContentView, _leftRightMargin),
                //movie control
                _character.Below(_quote, 10),
                _character.WithSameLeft(_quote),
                _character.WithSameRight(_quote),
                //quote control
                _movie.Below(_character, 2),
                _movie.WithSameLeft(_character),
                _movie.WithSameRight(_character),
                _movie.AtBottomOf(ContentView, _topBottomMargin)
           );
        }

        private void SetupUi()
        {
            _character = new UILabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                TextAlignment = UITextAlignment.Right,
                Font = UIFont.BoldSystemFontOfSize(14)
            };
            ContentView.Add(_character);

            _movie = new UILabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                TextAlignment = UITextAlignment.Right,
                Font = UIFont.SystemFontOfSize(12)
            };
            ContentView.Add(_movie);

            _quote = new UILabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                Font = UIFont.ItalicSystemFontOfSize(20),
            };
            ContentView.Add(_quote);

            //used to make sure we have UI setup before attempting to touch constraints
            _isControlsCreated = true;
        }

        #endregion
    }
}
