using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace TipCalculator
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            InitializeComponent();

		}

        private void OnAmountChanged(object sender, TextChangedEventArgs e)
        {
           CalculateTip(e.NewTextValue, (int)tipSlider.Value);
        }

        private void OnPercentClicked(object sender, EventArgs e)
        {
            var buttonSender = (Button)sender;
            tipSlider.Value = Convert.ToDouble(buttonSender.Text.Replace("%", string.Empty));
            CalculateTip(Amount.Text, (int)tipSlider.Value);
        }

        private void OnRoundOffClicked(object sender, EventArgs e)
        {
            if (IsAmountValid(Amount.Text))
            {
                var buttonSender = (Button)sender;
                Double _tip = 0;
                if (buttonSender.Text == "Round Down")

                {
                    _tip = Math.Floor(Convert.ToDouble(TipAmount.Text));
                }
                else
                {
                    _tip = Math.Ceiling(Convert.ToDouble(TipAmount.Text));
                }

                TipAmount.Text = Convert.ToString(_tip);
                CalculateTotal(_tip, Convert.ToDouble(Amount.Text));
            }          

        }

        private void OnTipSliderChange(object sender, ValueChangedEventArgs args)
        {
            CalculateTip(Amount.Text, Convert.ToInt64(args.NewValue));
        }

        private void CalculateTip(String amount, Double tip)
        {
            
            if (IsAmountValid(amount))
            {
                var _amount = Convert.ToDouble(amount);
                var _tip = _amount * (tip / 100);
                TipAmount.Text = Convert.ToString(_tip);
                TotalAmount.Text = Convert.ToString(_tip + Convert.ToDouble(amount));
                CalculateTotal(_tip, _amount);            
            } else
            {
                TipAmount.Text = "0.00";
                TotalAmount.Text = "0.00";
            }
        }

        private void CalculateTotal(Double tip, Double amount)
        {
            TotalAmount.Text = Convert.ToString(tip + amount);
        }

        private Boolean IsAmountValid(string amount)
        {
            return (!string.IsNullOrWhiteSpace(amount) && !Regex.IsMatch(amount, @"^[.+]*$|^[-]"));
        }

    }
}
