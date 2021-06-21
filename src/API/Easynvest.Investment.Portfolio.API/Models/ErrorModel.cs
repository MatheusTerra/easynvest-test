using System;
using System.Collections.Generic;

namespace Easynvest.Investment.Portfolio.API.Models
{
    public class ErrorModel
    {
        const string NO_INFO = "http://URL-dicionario-erros?id={0}";

        public int Code { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }

        private string _info;
        public string Info
        {
            get { return _info ?? string.Format(NO_INFO, Code); }
            set { _info = value; }
        }

        public IList<ErrorItemModel> Errors { get; set; }

        public ErrorModel(Exception ex)
        {
            Code = ex.HResult;
            Message = Description = ex.Message;
            Errors = new List<ErrorItemModel>();
        }

        public ErrorModel(int code, string message, string description, List<ErrorItemModel> errors = null)
        {
            Code = code;
            Message = message;
            Description = description;
            Errors = errors ?? new List<ErrorItemModel>();
        }

        public ErrorModel(int code, string message, string description, string moreInfoUrl, List<ErrorItemModel> errors = null)
        {
            Code = code;
            Message = message;
            Description = description;
            Info = moreInfoUrl;
            Errors = errors ?? new List<ErrorItemModel>();
        }

        public void AddItem(string name, string message)
        {
            Errors.Add(new ErrorItemModel(name, message));
        }
    }

    public class ErrorItemModel
    {
        public ErrorItemModel(string name, string message)
        {
            Name = name;
            Message = message;
        }

        public string Name { get; set; }
        public string Message { get; set; }
    }
}