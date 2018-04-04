using System;
using System.Collections.Generic;
using System.Text;
using GoogleClosureProcessor.Models;
using System.IO;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using GoogleClosure.Infrastructure;

namespace GoogleClosureProcessor
{
    public class GoogleClosureProcessor
    {
        private List<KeyValuePair<string, string>> _postParameters = new List<KeyValuePair<string, string>>();
        
        private string _compilationLevel;
        private bool _prettyPrint;
        private bool _printInputDelimiter;

        public GoogleClosureProcessor(CompilationType compilationType = CompilationType.ADVANCED, bool prettyPrint = false, bool printInputDelimiter = false)
        {
            string compilationLevel = String.Empty;
            switch (compilationType)
            {
                case CompilationType.ADVANCED:
                    _compilationLevel = "ADVANCED_OPTIMIZATIONS";
                    break;
                case CompilationType.SIMPLE:
                    _compilationLevel = "SIMPLE_OPTIMIZATIONS";
                    break;
                case CompilationType.WHITESPACE_ONLY:
                    _compilationLevel = "WHITESPACE_ONLY";
                    break;
            }

            _prettyPrint = prettyPrint;
            _printInputDelimiter = printInputDelimiter;

            fillPostParameters();
        }

        public GoogleClosureResponse Compress(string source)
        {
            string post = generatePost(source);
            GoogleClosureResponse googleClosureResponse = CallApi(post, source);

            googleClosureResponse.OutputFilePath = Constants.GOOGLECLOSUREAPI + googleClosureResponse.OutputFilePath;

            return googleClosureResponse;
        }

        private void fillPostParameters()
        {
            _postParameters.Add(new KeyValuePair<string, string>("output_format", "json"));

            _postParameters.Add(new KeyValuePair<string, string>("output_info", "compiled_code"));
            _postParameters.Add(new KeyValuePair<string, string>("output_info", "warnings"));
            _postParameters.Add(new KeyValuePair<string, string>("output_info", "errors"));
            _postParameters.Add(new KeyValuePair<string, string>("output_info", "statistics"));
            _postParameters.Add(new KeyValuePair<string, string>("compilation_level", _compilationLevel));

            //_postParameters.Add(new KeyValuePair<string, string>("warning_level", "quiet"));
            _postParameters.Add(new KeyValuePair<string, string>("formatting", "pretty_print"));
            _postParameters.Add(new KeyValuePair<string, string>("formatting", "print_input_delimiter"));
            _postParameters.Add(new KeyValuePair<string, string>("output_file_name", "default.js"));
            _postParameters.Add(new KeyValuePair<string, string>("js_code", "{0}"));
        }

        private string generatePost(string javascriptCode)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (KeyValuePair<string, string> entry in _postParameters)
            {
                if (entry.Key == "formatting" && entry.Value == "pretty_print")
                {
                    if (_prettyPrint)
                    {
                        stringBuilder.Append($"{entry.Key}={entry.Value}&");
                    }
                }
                else if (entry.Key == "formatting" && entry.Value == "print_input_delimiter")
                {
                    if (_printInputDelimiter)
                    {
                        stringBuilder.Append($"{entry.Key}={entry.Value}&");
                    }
                }
                else
                {
                    stringBuilder.Append($"{entry.Key}={entry.Value}&");
                }

            }
            stringBuilder.Length--;

            return stringBuilder.ToString();
        }

        private static GoogleClosureResponse CallApi(string postData, string javascriptCode)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("content-type", "application/x-www-form-urlencoded");
                string data = string.Format(postData, HttpUtility.UrlEncode(javascriptCode));
                string result = client.UploadString($"{Constants.GOOGLECLOSUREAPI}/compile", data);
                return JsonConvert.DeserializeObject<GoogleClosureResponse>(result);
            }
        }
    }
}
