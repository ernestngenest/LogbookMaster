namespace Kalbe.App.InternsipLogbookMasterData.API.Objects
{
    using System;
    public interface IServiceResponse
    {
        void Fail(Exception ex);
        bool IsSuccess();
    }
    public class ServiceResponse<T> : IServiceResponse
    {
        private T _data;
        private bool _success = true;
        private string _message = "";

        public T Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public bool Success
        {
            get
            {
                return _success;
            }
            set
            {
                _success = value;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        public void Fail(Exception ex)
        {
            _data = default;
            _success = false;
            _message = ex.Message;
        }

        public bool IsSuccess()
        {
            return this._success;
        }
    }
}
