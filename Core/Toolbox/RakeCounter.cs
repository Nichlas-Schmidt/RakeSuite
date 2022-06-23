using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Linq;
using Rake_Counter.Core.Toolbox;
using System.Runtime;

namespace Rake_Counter.Core.Toolbox
{
    class RakeCounter
    {
        public UserMapping Calculate(string path)
        {

            var userValue = new UserMapping();
            try {
                string text = File.ReadAllText($@"{path}");
                string[] _hands = text.Split(new[] { "\r\n\r\n\r\n\r\n" }, StringSplitOptions.None);
                string[] hands = _hands.Length == 1 ? text.Split(new[] { "\n\n\n\n" }, StringSplitOptions.None) : _hands;
                var pattern = @"^Total pot (\d+)( Main pot \d+\.( Side pot[-\d]* \d+\.)*)* \| Rake (\d+) *\s$";
                var lineregex = @"^(.{4,15}?) collected (\d+) from[ main side]* pot";
                var summaryPattern = @"^\*\*\* SUMMARY \*\*\*\s";
                foreach (var hand in hands)
                {
                    if(hand.Length < 1) { continue;}
                    var gameInfo = Regex.Matches(hand, pattern, RegexOptions.Multiline);
                    var summary = Regex.Matches(hand, summaryPattern, RegexOptions.Multiline);
                    if (gameInfo.Count == 0 || summary.Count == 0)
                    {
                        handleError("No summary", hand);
                    };
                    double totalPot = double.Parse(gameInfo[0].Groups[1].ToString());
                    double rake = double.Parse(gameInfo[0].Groups[4].ToString());
                    MatchCollection winners = Regex.Matches(hand, lineregex, RegexOptions.Multiline);
                    if (winners.Count == 0) { throw new Exception("No winners"); };
                    foreach (Match winner in winners)
                    {
                        string username = winner.Groups[1].ToString();
                        double amount = double.Parse(winner.Groups[2].ToString());
                        double paidRake = (amount / (totalPot - rake) * rake);
                        userValue.Map(username, Math.Round(paidRake, 2));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return userValue;

        }
        private void handleError(string code, string hand)
        {
            MessageBox.Show($"Error code: {code}");
        }

    }
}
