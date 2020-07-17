using System;
using Presentation.Wpf.Utility;

namespace Presentation.Wpf.User
{
    /// <summary>
    ///     User Dto.
    /// </summary>
    public class UserDto : BaseObservable
    {
        /// <summary>
        ///     Email.
        /// </summary>
        private string _email;

        /// <summary>
        ///     FirstName.
        /// </summary>
        private string _firstName;

        /// <summary>
        ///     Id.
        /// </summary>
        private Guid _id;

        /// <summary>
        ///     LastName.
        /// </summary>
        private string _lastName;

        /// <summary>
        ///     Id.
        /// </summary>
        public Guid Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        ///     FirstName.
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName == value) return;
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        /// <summary>
        ///     LastName.
        /// </summary>
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName == value) return;
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        /// <summary>
        ///     Email.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                if (_email == value) return;
                _email = value;
                OnPropertyChanged("Email");
            }
        }
    }
}