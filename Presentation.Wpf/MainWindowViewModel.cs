using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Presentation.Wpf.Kitchen;
using Presentation.Wpf.Utility;

namespace Presentation.Wpf
{
    /// <summary>
    ///     Main Windows View Model.
    /// </summary>
    public class MainWindowViewModel : BaseObservable
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public MainWindowViewModel()
        {
            // Add available pages
            PageViewModels.Add(new KitchenViewModel());
            
            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        #region Methods

        /// <summary>
        ///     Change View Model.
        /// </summary>
        /// <param name="viewModel"></param>
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion

        #region Fields

        /// <summary>
        ///     Change Page.
        /// </summary>
        private ICommand _changePageCommand;

        /// <summary>
        ///     Current Page.
        /// </summary>
        private IPageViewModel _currentPageViewModel;

        /// <summary>
        ///     Page View Model.
        /// </summary>
        private List<IPageViewModel> _pageViewModels;

        #endregion

        #region Properties / Commands

        /// <summary>
        ///     Change Page Command.
        /// </summary>
        public ICommand ChangePageCommand
        {
            get
            {
                return _changePageCommand ??= new RelayCommand(
                    p => ChangeViewModel((IPageViewModel) p),
                    p => p is IPageViewModel);
            }
        }

        /// <summary>
        ///     PageViewModels.
        /// </summary>
        public List<IPageViewModel> PageViewModels
        {
            get { return _pageViewModels ??= new List<IPageViewModel>(); }
        }

        /// <summary>
        ///     Current Page View Model.
        /// </summary>
        public IPageViewModel CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion
    }
}