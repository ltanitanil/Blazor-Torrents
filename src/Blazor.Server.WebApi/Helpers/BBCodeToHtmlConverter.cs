using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blazor.Server.WebApi.Helpers
{
    public class BBCodeToHTMLConverter
    {
        #region Helper Classes
        interface IHtmlFormatter
        {
            string Format(string data);
        }

        private class RegexFormatter : IHtmlFormatter
        {
            private readonly string _replace;
            private readonly Regex _regex;

            public RegexFormatter(string pattern, string replace)
                : this(pattern, replace, true)
            {

            }

            public RegexFormatter(string pattern, string replace, bool ignoreCase)
            {
                var options = RegexOptions.Compiled;

                if (ignoreCase)
                {
                    options |= RegexOptions.IgnoreCase;
                }

                _replace = replace;
                _regex = new Regex(pattern, options);
            }

            public string Format(string data)
            {
                return _regex.Replace(data, _replace);
            }
        }

        private class SearchReplaceFormatter : IHtmlFormatter
        {
            private readonly string _pattern;
            private readonly string _replace;

            public SearchReplaceFormatter(string pattern, string replace)
            {
                _pattern = pattern;
                _replace = replace;
            }

            public string Format(string data)
            {
                return data.Replace(_pattern, _replace);
            }
        }
        #endregion

        #region BBCode
        static List<IHtmlFormatter> _formatters;

        static BBCodeToHTMLConverter()
        {
            var sListFormat = "<ol class=\"bbcode-list\" style=\"list-style:{0};\">$1</ol>";

            _formatters = new List<IHtmlFormatter>
            {
                new RegexFormatter(@"<(.|\n)*?>", string.Empty),
                new SearchReplaceFormatter("\r", ""),
                new SearchReplaceFormatter("\n\n", "</p><p>"),
                new SearchReplaceFormatter("\n", "<br />"),
                new RegexFormatter(@"\[b(?:\s*)\]((.|\n)*?)\[/b(?:\s*)\]", "<b>$1</b>"),
                new RegexFormatter(@"\[i(?:\s*)\]((.|\n)*?)\[/i(?:\s*)\]", "<i>$1</i>"),
                new RegexFormatter(@"\[s(?:\s*)\]((.|\n)*?)\[/s(?:\s*)\]", "<strike>$1</strike>"),
                new RegexFormatter(@"\[left(?:\s*)\]((.|\n)*?)\[/left(?:\s*)]", "<div style=\"text-align:left\">$1</div>"),
                new RegexFormatter(@"\[center(?:\s*)\]((.|\n)*?)\[/center(?:\s*)]", "<div style=\"text-align:center\">$1</div>"),
                new RegexFormatter(@"\[right(?:\s*)\]((.|\n)*?)\[/right(?:\s*)]", "<div style=\"text-align:right\">$1</div>"),
                new RegexFormatter(@"\[quote=((.|\n)*?)(?:\s*)\]", "<blockquote><b>$1 said:</b></p><p>"),
                new RegexFormatter(@"\[quote(?:\s*)\]", "<blockquote>"),
                new RegexFormatter(@"\[/quote(?:\s*)\]", "</blockquote>"),
                new RegexFormatter(@"\[url(?:\s*)\]www\.(.*?)\[/url(?:\s*)\]", "<a class=\"bbcode-link\" href=\"http://www.$1\" target=\"_blank\" title=\"$1\">$1</a>"),
                new RegexFormatter(@"\[url(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "<a class=\"bbcode-link\" href=\"$1\" target=\"_blank\" title=\"$1\">$1</a>"),
                new RegexFormatter(@"\[url=""((.|\n)*?)(?:\s*)""\]((.|\n)*?)\[/url(?:\s*)\]", "<a class=\"bbcode-link\" href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>"),
                new RegexFormatter(@"\[url=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "<a class=\"bbcode-link\" href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>"),
                new RegexFormatter(@"\[link(?:\s*)\]((.|\n)*?)\[/link(?:\s*)\]", "<a class=\"bbcode-link\" href=\"$1\" target=\"_blank\" title=\"$1\">$1</a>"),
                new RegexFormatter(@"\[link=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/link(?:\s*)\]", "<a class=\"bbcode-link\" href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>"),
                new RegexFormatter(@"\[img(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img src=\"$1\" border=\"0\" alt=\"\" />"),
                new RegexFormatter(@"\[img align=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img src=\"$3\" border=\"0\" align=\"$1\" alt=\"\" />"),
                new RegexFormatter(@"\[img=((.|\n)*?)x((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img width=\"$1\" height=\"$3\" src=\"$5\" border=\"0\" alt=\"\" />"),
                new RegexFormatter(@"\[color=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/color(?:\s*)\]", "<span style=\"color=$1;\">$3</span>"),
                new RegexFormatter(@"\[hr(?:\s*)\]", "<hr />"),
                new RegexFormatter(@"\[email(?:\s*)\]((.|\n)*?)\[/email(?:\s*)\]", "<a href=\"mailto:$1\">$1</a>"),
                new RegexFormatter(@"\[size=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/size(?:\s*)\]", "<span style=\"font-size:$1\">$3</span>"),
                new RegexFormatter(@"\[font=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/font(?:\s*)\]", "<span style=\"font-family:$1;\">$3</span>"),
                new RegexFormatter(@"\[align=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/align(?:\s*)\]", "<span style=\"text-align:$1;\">$3</span>"),
                new RegexFormatter(@"\[float=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/float(?:\s*)\]", "<span style=\"float:$1;\">$3</div>"),
                new RegexFormatter(@"\[\*(?:\s*)]\s*([^\[]*)", "<li>$1</li>"),
                new RegexFormatter(@"\[list(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", "<ul class=\"bbcode-list\">$1</ul>"),
                new RegexFormatter(@"\[list=1(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format(sListFormat, "decimal"), false),
                new RegexFormatter(@"\[list=i(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format(sListFormat, "lower-roman"), false),
                new RegexFormatter(@"\[list=I(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format(sListFormat, "upper-roman"), false),
                new RegexFormatter(@"\[list=a(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format(sListFormat, "lower-alpha"), false),
                new RegexFormatter(@"\[list=A(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format(sListFormat, "upper-alpha"), false)
            };
        }
        #endregion

        #region Format
        public static string Format(string data)
        {
            foreach (IHtmlFormatter formatter in _formatters)
            {
                data = formatter.Format(data);
            }

            return data;
        }
        #endregion
    }
}
